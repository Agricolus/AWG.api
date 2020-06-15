using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

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
            throw new Exception($"Deserialization Error: property: {targetProperty.Name} -- {ex.Message}", ex);
          }
        }
      }

      return target;
    }

    public override bool CanWrite { get { return false; } }
    public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
    { }
  }
}