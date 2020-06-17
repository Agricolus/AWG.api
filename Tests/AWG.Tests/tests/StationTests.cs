using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AWG.Stations.api.Controllers;
using AWG.Stations.core.Query;
using MediatR;
//using AWG.Tests.data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace AWG.Tests.tests
{
  [TestClass]
  public class StationTests
  {
    [TestMethod]
    public async Task GetAllActiveStations(IMediator mediator)
    {
      var skip = int.Parse("0");
      var take = int.Parse("20");

      Assert.IsNotNull(skip, "Skip is null");
      Assert.IsNotNull(take, "Take is null");

      var result = await mediator.Send(new ListAllActiveStations() { Skip = skip, Take = take });

      Assert.IsNotNull(result, "Result is null");
    }
  }
}
