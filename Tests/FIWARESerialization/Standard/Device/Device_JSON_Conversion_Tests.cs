using System;
using System.Collections.Generic;
using System.IO;
using BAMCIS.GeoJSON;
using JsonDiffPatchDotNet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace Standard.Device
{
  [TestClass]
  public class Device_JSON_Conversion_Tests
  {
    [TestMethod]
    public void Deserialization_Serialization_EndToEnd()
    {
      string exampleDeviceSerialized = System.IO.File.ReadAllText(@"Device.example.json");
      AWG.FIWARE.DataModels.Device deviceDeserialized = JsonConvert.DeserializeObject<AWG.FIWARE.DataModels.Device>(exampleDeviceSerialized);

      var deviceSerialized = JsonConvert.SerializeObject(deviceDeserialized);

      var jdp = new JsonDiffPatch();
      var j1 = JObject.Parse(exampleDeviceSerialized);
      var j2 = JObject.Parse(deviceSerialized);
      JToken diff = jdp.Diff(j1, j2);

      string diffstring = JsonConvert.SerializeObject(diff, Formatting.Indented);
      Assert.IsNull(diff, "differences:\n{0}", diffstring);
    }

    //check if a Device object serialization is conform to the schema definition
    [TestMethod]
    public void Serialization_IsConformToSchema()
    {
      string jsonSchema = File.ReadAllText(@"Device.schema.json");
      JSchemaUrlResolver resolver = new JSchemaUrlResolver();
      JSchema schema = JSchema.Parse(jsonSchema, resolver);

      var device = new AWG.FIWARE.DataModels.Device()
      {
        Id = "Device:test",
        BatteryLevel = null,
        Category = new[] { "sensor" },
        Configuration = "{'test': 'configuration value'}",
        ControlledAsset = new[] { "asset 1", "asset 2" },
        ControlledProperty = new[] { "property 1", "property 2" },
        DateFirstUsed = new DateTime(),
        DateInstalled = new DateTime(),
        DateLastCalibration = new DateTime(),
        DateLastValueReported = new DateTime(),
        DateManufactured = new DateTime(),
        DataProvider = "data provider field",
        DateCreated = new DateTime(),
        DateModified = new DateTime(),
        Description = "i'm a dummy test device for json test",
        DeviceState = "i'm ok, ty",
        FirmwareVersion = "1.0.0.",
        HardwareVersion = "1.0.0.",
        IpAddress = new[] { "0.0.0.0", "1.1.1.1" },
        Location = new Point(new Position(0, 0)),
        MacAddress = new[] { "0.0.0.0", "1.1.1.1" },
        Mcc = "it",
        Mnc = "whoknows",
        Name = "devicename",
        OsVersion = "1.2.3",
        Owner = new[] { "me", "you", "everybody" },
        Provider = "provider name",
        Value = "device value"
      };
      var deviceSerialized = JsonConvert.SerializeObject(device);

      JObject deviceJObject = JObject.Parse(deviceSerialized);
      IList<ValidationError> messages = new List<ValidationError>();
      var isValid = deviceJObject.IsValid(schema, out messages);

      Assert.IsTrue(isValid, "Schema validation failed:\n\t\t\t{0}\nJSON:{1}", string.Join("\n\t\t\t", messages), JsonConvert.SerializeObject(deviceJObject, Formatting.Indented));
    }
  }
}