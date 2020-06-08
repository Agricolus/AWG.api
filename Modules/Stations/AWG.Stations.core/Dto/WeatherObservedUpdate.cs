using System;
using AWG.FIWARE.DataModels;
using BAMCIS.GeoJSON;

namespace AWG.Stations.core.dto
{
  public class WeatherObservedUpdate
  {
    public string RefDevice { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
    public string DataProvider { get; set; }
    public string Source { get; set; }
    public string Name { get; set; }
    public GeoJson Location { get; set; }
  }
}