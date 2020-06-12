using BAMCIS.GeoJSON;

namespace AWG.Stations.core.dto
{
  public class WeatherObservedCBEntityUpdate
  {
    public string RefDevice { get; set; }
    public string DateCreated { get; set; }
    public string DateModified { get; set; }
    public string DataProvider { get; set; }
    public string Source { get; set; }
    public string Name { get; set; }
    public GeoJson Location { get; set; }
  }
}