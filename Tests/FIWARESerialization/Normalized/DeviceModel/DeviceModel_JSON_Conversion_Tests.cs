using System;
using System.Collections.Generic;
using System.IO;
using AWG.FIWARE.Serializers;
using JsonDiffPatchDotNet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace Normalized.DeviceModel
{
  [TestClass]
  public class DeviceModel_JSON_Conversion_Tests
  {
    [TestMethod]
    public void Deserialization_Serialization_EndToEnd()
    {
      string exampleDeviceModelSerialized = System.IO.File.ReadAllText(@"DeviceModel.example.json");
      AWG.FIWARE.DataModels.DeviceModel deviceModelDeserialized = JsonConvert.DeserializeObject<AWG.FIWARE.DataModels.DeviceModel>(exampleDeviceModelSerialized, new FiwareNormalizedJsonConverter<AWG.FIWARE.DataModels.DeviceModel>());

      var deviceModelSerialized = JsonConvert.SerializeObject(deviceModelDeserialized, new FiwareNormalizedJsonConverter<AWG.FIWARE.DataModels.DeviceModel>());

      var jdp = new JsonDiffPatch();
      var j1 = JObject.Parse(exampleDeviceModelSerialized);
      var j2 = JObject.Parse(deviceModelSerialized);
      JToken diff = jdp.Diff(j1, j2);

      string diffstring = JsonConvert.SerializeObject(diff, Formatting.Indented);
      Assert.IsNull(diff, "differences:\n{0}", diffstring);
    }

    //check if a DeviceModel object serialization is conform to the schema definition
    [TestMethod]
    public void Serialization_IsConformToSchema()
    {
      string jsonSchema = File.ReadAllText(@"DeviceModel.schema.json");
      JSchemaUrlResolver resolver = new JSchemaUrlResolver();
      JSchema schema = JSchema.Parse(jsonSchema, resolver);

      var device = new AWG.FIWARE.DataModels.DeviceModel()
      {
        Id = "DeviceModel:test",
        BrandName = "device brand name",
        Category = new[] { "category 1" },
        ControlledProperty = new[] { "property 1", "property 2" },
        DataProvider = new Uri("http://dataprovider.of.model"),
        DateCreated = new DateTime(),
        DateModified = new DateTime(),
        Description = "i'm a dummy test device for json test",
        DeviceClass = "test device class",
        Documentation = new Uri("http://documentation.of.model"),
        EnergyLimitationClass = "E1",
        Function = new[] { "levelControl", "sensing", "onOff", "openClose", "metering", "eventNotification" },
        Image = new Uri("http://image.of.model"),
        ManufacturerName = "model manufacturer name",
        ModelName = "model name",
        Name = "devicename",
        Source = "source od data",
        SupportedProtocol = new[] { "ul20", "mqtt", "lwm2m", "http", "websocket", "onem2m", "sigfox", "lora", "nb-iot", "ec-gsm-iot", "lte-m", "cat-m", "3g", "grps" },
        SupportedUnits = new[] { "munit 1", "munit 2" },
      };
      var deviceModelSerialized = JsonConvert.SerializeObject(device, new FiwareNormalizedJsonConverter<AWG.FIWARE.DataModels.DeviceModel>());

      JObject deviceModelJObject = JObject.Parse(deviceModelSerialized);
      IList<string> messages = new List<string>();
      var isValid = deviceModelJObject.IsValid(schema, out messages);

      Assert.IsTrue(isValid, "Schema validation failed:\n\t\t\t{0}", string.Join("\n\t\t\t", messages));
    }
  }
}