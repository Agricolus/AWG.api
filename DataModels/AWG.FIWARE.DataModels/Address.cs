using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AWG.FIWARE.DataModels
{
  [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy), ItemNullValueHandling = NullValueHandling.Ignore)]
  public class Address
  {
    public string AddressCountry { get; set; }
    public string AddressLocality { get; set; }
    public string AddressRegion { get; set; }
    public string PostOfficeBoxNumber { get; set; }
    public string PostalCode { get; set; }
    public string StreetAddress { get; set; }
  }
}