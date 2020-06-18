using System.Collections.Generic;
using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using AWG.FIWARE.Serializers.Exceptions;
using AWG.FIWARE.Serializers.Attributes;

namespace AWG.FIWARE.Serializers
{

  public class FiwareNormalizedJsonConverter<T> : JsonConverter<T>
         where T : class, new()
  {
    public static HashSet<Type> StandardTypes { get; private set; } = new HashSet<Type>()
    {
      typeof(String), typeof(DateTime), typeof(int), typeof(char), typeof(short), typeof(byte), typeof(float),
      typeof(double), typeof(long), typeof(ushort), typeof(uint), typeof(ulong), typeof(bool)
    };

    public static HashSet<Type> NumberTypes { get; private set; } = new HashSet<Type>()
    {
      typeof(int), typeof(short), typeof(float),
      typeof(double), typeof(long), typeof(ushort), typeof(uint), typeof(ulong)
    };

    public static HashSet<Type> StringTypes { get; private set; } = new HashSet<Type>()
    {
      typeof(String), typeof(char)
    };




    public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer)
    {

      // if (!objectType.GetInterfaces().Contains(typeof(IContextBrokerEntity)))
      // {
      //   throw new DeserializationException($"The type {objectType.Name} is not a IContextBrokerEntity entity");
      // }
      try
      {
        var jobject = JObject.ReadFrom(reader);

        var values = jobject.ToDictionary(t => t.Path.ToLower());
        var target = new T();

        var contractTerms = (JsonObjectContract)serializer.ContractResolver.ResolveContract(objectType);
        foreach (var contractProperty in contractTerms.Properties)
        {
          var targetPropertyName = string.IsNullOrEmpty(contractProperty.UnderlyingName) ? contractProperty.PropertyName : contractProperty.UnderlyingName;
          var jsonPropertyName = contractProperty.PropertyName.ToLower();
          var targetProperty = typeof(T).GetProperty(targetPropertyName);
          var targetPropertyType = targetProperty.PropertyType;

          Type nullableProperty = Nullable.GetUnderlyingType(contractProperty.PropertyType);
          if (nullableProperty != null)
            targetPropertyType = nullableProperty;

          JToken jsonPropertyValue;
          if (values.TryGetValue(jsonPropertyName, out jsonPropertyValue))
          {
            try
            {
              var jprop = (jsonPropertyValue as JProperty);
              if (jprop == null) continue;

              if (jprop.Value.Type == JTokenType.Object)
              {
                var result = jprop.Value.ToObject<FiwareNormalzedProperty>();
                if (result != null && result.Value != null)
                {
                  if (targetPropertyType.IsEnum)
                    targetProperty.SetValue(target, Enum.Parse(targetPropertyType, (string)result.Value));
                  else if (result.Value is JObject)
                    targetProperty.SetValue(target, ((JObject)result.Value).ToObject(targetPropertyType));
                  else
                    targetProperty.SetValue(target, Convert.ChangeType(result.Value, targetPropertyType));
                }
                continue;
              }

              // fallback -- all other properties will be deserialized in a standard way

              var myvalue = jprop.Value.ToObject(targetProperty.PropertyType);
              targetProperty.SetValue(target, Convert.ChangeType(myvalue, targetPropertyType));
            }
            catch (Exception ex)
            {
              throw new DeserializationException($"Deserialization Error: property: {targetProperty.Name} -- {ex.Message}", ex);
            }
          }
        }

        return target;
      }
      catch (Exception e)
      {
        throw new DeserializationException("Deserialization Error: json is not valid", e);
      }
    }

    public override bool CanWrite { get { return true; } }
    public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
    {
      if (value == null)
      {
        serializer.Serialize(writer, null);
        return;
      }
      writer.WriteStartObject();
      var contractTerms = (JsonObjectContract)serializer.ContractResolver.ResolveContract(typeof(T));

      foreach (var contractProperty in contractTerms.Properties)
      {
        var propertyType = contractProperty.PropertyType;
        var targetProperty = typeof(T).GetProperty(contractProperty.UnderlyingName);
        Type nullableProperty = Nullable.GetUnderlyingType(propertyType);
        if (nullableProperty != null)
          propertyType = nullableProperty;
        var propertyValue = contractProperty.ValueProvider.GetValue(value);
        if (propertyValue == null && contractTerms.ItemNullValueHandling == NullValueHandling.Ignore)
          continue;
        if (typeof(IContextBrokerEntity).GetProperty(contractProperty.UnderlyingName) != null) //gestione degli attributi da non normalizzare (definiti dentro l'interfaccia)
        {
          writer.WritePropertyName(contractProperty.PropertyName);
          serializer.Serialize(writer, propertyValue);
          continue;
        }

        writer.WritePropertyName(contractProperty.PropertyName);
        writer.WriteStartObject();
        writer.WritePropertyName("value");
        if (contractProperty.Converter != null)
        {
          contractProperty.Converter.WriteJson(writer, propertyValue, serializer);
        }
        else
        {
          if (NumberTypes.Contains(propertyType))
            propertyValue = String.Format("{0:0.###########}", propertyValue);
          serializer.Serialize(writer, propertyValue);
        }

        // if (NumberTypes.Contains(propertyType))
        //   writer.WriteValue("Number");
        //if (StringTypes.Contains(propertyType))
        //  writer.WriteValue("Text");

        //geojson serialization
        if (propertyType.GetInterface(typeof(IGeoJSON).Name) != null || Attribute.GetCustomAttribute(targetProperty, typeof(GeoJSON)) != null)
        {
          writer.WritePropertyName("type");
          writer.WriteValue("geo:json");
        }
        if (targetProperty.PropertyType.IsAssignableFrom(typeof(DateTime)))
        {
          writer.WritePropertyName("type");
          writer.WriteValue("DateTime");
        }
        writer.WriteEndObject();
      }

      writer.WriteEndObject();
    }
  }
}
