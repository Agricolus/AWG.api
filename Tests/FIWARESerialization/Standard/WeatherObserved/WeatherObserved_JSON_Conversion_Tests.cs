using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BAMCIS.GeoJSON;
using JsonDiffer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace Standard.WeatherObserved
{
  [TestClass]
  public class WeatherObserved_JSON_Conversion_Tests
  {
    //this test seems more suited for the serializer in general 
    // [TestMethod]
    // public void Deserialization_ThrowOnInvalidCbEntity()
    // {
    //   Assert.ThrowsException<AWG.FIWARE.Serializers.Exceptions.DeserializationException>(() =>
    //   {
    //     AWG.FIWARE.DataModels.WeatherObserved weatherObserved = JsonConvert.DeserializeObject<AWG.FIWARE.DataModels.WeatherObserved>("{}", new FiwareNormalizedJsonConverter<AWG.FIWARE.DataModels.WeatherObserved>());
    //   });
    // }

    //read from the entity present in "example.json", deserialize it to the WeatherObserved object,
    //serialize it back to string and compare the result with the original text.
    [TestMethod]
    public void Deserialization_Serialization_EndToEnd()
    {
      string exampleWeatherObservedSerialized = System.IO.File.ReadAllText(@"WeatherObserved.example.json");
      AWG.FIWARE.DataModels.WeatherObserved weatherObservedDeserialized = JsonConvert.DeserializeObject<AWG.FIWARE.DataModels.WeatherObserved>(exampleWeatherObservedSerialized);

      var weatherObservedSerialized = JsonConvert.SerializeObject(weatherObservedDeserialized);

      var diff = JsonDifferentiator.Differentiate(JObject.Parse(exampleWeatherObservedSerialized), JObject.Parse(weatherObservedSerialized));

      string diffstring = JsonConvert.SerializeObject(diff, Formatting.Indented);
      // Assert.AreEqual(exampletrimmed, serializedtrimmed);
      Assert.IsNull(diff, "differences:\n{0}", diffstring);
    }

    //check if a WeatherObserved object serialization is conform to the schema definition
    [TestMethod]
    public void Serialization_IsConformToSchema()
    {
      string jsonSchema = System.IO.File.ReadAllText(@"WeatherObserved.schema.json");
      JSchemaUrlResolver resolver = new JSchemaUrlResolver();
      JSchema schema = JSchema.Parse(jsonSchema, resolver);

      var weatherObserved = new AWG.FIWARE.DataModels.WeatherObserved()
      {
        Id = "WeatherObserved:test",
        Address = new AWG.FIWARE.DataModels.Address()
        {
          AddressCountry = "it",
          AddressLocality = "test address who knows"
        },
        AtmosphericPressure = 43.34,
        DataProvider = "data provider field",
        DateCreated = new DateTime(),
        DateModified = new DateTime(),
        DateObserved = new DateTime(),
        DewPoint = null,
        Illuminance = null,
        Location = new Point(new Position(0, 0)),
        Name = "i have no name, should have stationName",
        Precipitation = null,
        PressureTendency = null,
        RefDevice = "Device:test",
        RefPointOfInterest = null,
        RelativeHumidity = 44,
        SnowHeight = null,
        Temperature = 25.25,
        Visibility = null,
        WeatherType = null,
        WindDirection = null,
        WindSpeed = null
      };
      var weatherObservedSerialized = JsonConvert.SerializeObject(weatherObserved);

      JObject weatherObservedJObject = JObject.Parse(weatherObservedSerialized);
      IList<string> messages = new List<string>();
      var isValid = weatherObservedJObject.IsValid(schema, out messages);

      Assert.IsTrue(isValid, "Schema validation failed:\n\t\t\t{0}", string.Join("\n\t\t\t", messages));
    }
  }
}
