using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

    public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
      var jobject = JObject.ReadFrom(reader);
      var values = jobject.ToDictionary(t => t.Path.ToLower());
      var target = new T();

      foreach (var p in typeof(T).GetProperties(BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.Instance))
      {
        JToken val;
        if (values.TryGetValue(p.Name.ToLower(), out val))
        {
          try
          {
            var jprop = (val as JProperty);
            if (jprop == null) continue;

            if (jprop.Value.Type == JTokenType.Object)
            {
              var result = jprop.Value.ToObject<FiwareNormalzedProperty>();
              if (result != null && result.Value != null)
              {
                if (result.Value is JObject)
                  p.SetValue(target, ((JObject)result.Value).ToObject(p.PropertyType));
                else
                  p.SetValue(target, result.Value);
              }
              continue;
            }

            // fallback -- all other properties will be deserialized in a standard way

            var myvalue = jprop.Value.ToObject(p.PropertyType);
            p.SetValue(target, myvalue);
          }
          catch (Exception ex)
          {
            throw new Exception($"Deserialization Error: property: {p.Name} -- {ex.Message}", ex);
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