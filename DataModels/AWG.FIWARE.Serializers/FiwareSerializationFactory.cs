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
    public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
      var jobject = JObject.ReadFrom(reader);
      var values = jobject.ToDictionary(t => t.Path);
      var target = new T();
      foreach (var p in typeof(T).GetProperties(BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.Instance))
      {
        JToken val;
        if (values.TryGetValue(p.Name, out val))
        {
          if (val.Type == JTokenType.Object && !p.PropertyType.IsClass)
          {
            var result = val.ToObject<FiwareNormalzedProperty>();
            if (result != null && result.Value != null)
              p.SetValue(target, result.Value);
          }

          // fallback -- all other properties will be deserialized in a standard way
          p.SetValue(target, val.ToObject(p.PropertyType));
        }
      }

      return target;
    }


    public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
    {
      throw new NotImplementedException();
    }
  }
}