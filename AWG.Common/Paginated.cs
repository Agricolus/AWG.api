using System.Collections.Generic;

namespace AWG.Common
{
  public class Paginated<T>
  {
    public int TotalCount { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; }
    public IEnumerable<T> Items { get; set; }
  }
}