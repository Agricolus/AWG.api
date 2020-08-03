using System;
using System.Collections.Generic;
using AWG.Common.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace AWG.Common.Helpers
{
  [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy), ItemNullValueHandling = NullValueHandling.Ignore)]
  public class Subscription
  {
    public string Id { get; set; }
    public string Description { get; set; }
    public Subject Subject { get; set; }
    public Notification Notification { get; set; }
    [JsonConverter(typeof(IsoDateTimeConverter))]
    public DateTime? Expires { get; set; } = null;
    [JsonConverter(typeof(StringEnumConverter))]
    public StatusEnum? Status { get; set; } = null;
    public long? Throttling { get; set; }
  }

  [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy), ItemNullValueHandling = NullValueHandling.Ignore)]
  public class Notification
  {
    public Http Http { get; set; }
    public HttpCustom HttpCustom { get; set; }
    public string[] Attrs { get; set; } = null;
    public string[] ExceptAttrs { get; set; } = null;
    [JsonConverter(typeof(StringEnumConverter))]
    public AttributesFormatEnum? AttrsFormat { get; set; } = AttributesFormatEnum.normalized;
    public string[] Metadata { get; set; } = null;
    public long? TimesSent { get; set; } = null;
    [JsonConverter(typeof(IsoDateTimeConverter))]
    public DateTime? LastNotification { get; set; } = null;
    [JsonConverter(typeof(IsoDateTimeConverter))]
    public DateTime? LastFailure { get; set; } = null;
    [JsonConverter(typeof(IsoDateTimeConverter))]
    public DateTime? LastSuccess { get; set; } = null;
  }

  [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy), ItemNullValueHandling = NullValueHandling.Ignore)]
  public class Http
  {
    public Uri Url { get; set; }
  }

  [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy), ItemNullValueHandling = NullValueHandling.Ignore)]
  public class HttpCustom
  {
    public Uri Url { get; set; }
    public Dictionary<string, string> Headers { get; set; }
    public Dictionary<string, string> Qs { get; set; }
    public string Payload { get; set; }
  }


  [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy), ItemNullValueHandling = NullValueHandling.Ignore)]
  public class Subject
  {
    public Entity[] Entities { get; set; }
    public Condition Condition { get; set; }
  }

  [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy), ItemNullValueHandling = NullValueHandling.Ignore)]
  public class Condition
  {
    public string[] Attrs { get; set; }
    public Expression Expression { get; set; }
  }

  [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy), ItemNullValueHandling = NullValueHandling.Ignore)]
  public class Expression
  {
    public string Q { get; set; }
    public string Mq { get; set; }
    public string Georel { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public GeometryEnum Geometry { get; set; }
    public double[] Coords { get; set; }
  }

  [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy), ItemNullValueHandling = NullValueHandling.Ignore)]
  public class Entity
  {
    public string Id { get; set; }
    public string IdPattern { get; set; }
    public string Type { get; set; }
    public string TypePattern { get; set; }
  }
}
