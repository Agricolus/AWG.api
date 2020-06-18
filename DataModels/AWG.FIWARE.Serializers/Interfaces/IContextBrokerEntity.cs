using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWG.FIWARE.Serializers
{
  public interface IContextBrokerEntity
  {
    string Id { get; set; }
    string Type { get; }
  }
}
