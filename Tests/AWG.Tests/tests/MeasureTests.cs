using System;
using System.Threading.Tasks;
using AWG.Measures.core.Command;
using AWG.Measures.core.Query;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using fiware = AWG.FIWARE.DataModels;

namespace AWG.Tests.tests
{
  [TestClass]
  public class MeasureTests
  {
    private string fileParameter = "measures.json";
    private IMediator mediator;
    private dynamic parameters;

    public MeasureTests()
    {
      this.mediator = Helper.BuildMediator();
      this.parameters = Helper.Parameters(fileParameter);
    }

    [TestMethod]
    public async Task PostMeasure()
    {
      var localParams = parameters["PostMeasure"];

      var model = JsonConvert.DeserializeObject<fiware.WeatherObserved>(localParams["model"].ToString());

      Assert.IsNotNull(model, "Model is null");

      var result = await mediator.Send(new AddMeasure() { Model = model });

      Assert.IsNotNull(result, "Result is null");
    }

    [TestMethod]
    public async Task PostMeasureLD()
    {
      var localParams = parameters["PostMeasureLD"];

      var model = JsonConvert.DeserializeObject<fiware.WeatherObservedLD>(localParams["model"].ToString());

      Assert.IsNotNull(model, "Model is null");

      var result = await mediator.Send(new AddMeasure() { Model = model });

      Assert.IsNotNull(result, "Result is null");
    }

    [TestMethod]
    public async Task GetLastMeasure()
    {
      try
      {
        var localParams = parameters["GetLastMeasure"];

        var stationId = localParams["stationId"].ToString();

        Assert.IsNotNull(stationId, "StationId is null");

        var result = await mediator.Send(new GetLastMeasure() { StationId = stationId });

        Assert.IsNotNull(result, "Result is null");
      }
      catch (Exception e)
      {
        Console.WriteLine(e.StackTrace);
      }
    }

    [TestMethod]
    public async Task GetLastDailyMeasure()
    {
      var localParams = parameters["GetLastDailyMeasure"];

      var stationId = localParams["stationId"].ToString();

      Assert.IsNotNull(stationId, "StationId is null");

      DateTime? fromDate = null;
      if (!String.IsNullOrEmpty(localParams["fromDate"].ToString()))
      {
        fromDate = DateTime.Parse(localParams["fromDate"].ToString());
      }

      DateTime? toDate = null;
      if (!String.IsNullOrEmpty(localParams["toDate"].ToString()))
      {
        toDate = DateTime.Parse(localParams["toDate"].ToString());
      }

      var result = await mediator.Send(new GetDailyMeasures()
      {
        StationId = stationId,
        FromDate = fromDate ?? DateTime.UtcNow,
        ToDate = toDate,
      });

      Assert.IsNotNull(result, "Result is null");
    }

    [TestMethod]
    public async Task GetLastWeeklyMeasure()
    {
      var localParams = parameters["GetLastWeeklyMeasure"];

      var stationId = localParams["stationId"].ToString();

      Assert.IsNotNull(stationId, "StationId is null");

      DateTime? fromDate = null;
      if (!String.IsNullOrEmpty(localParams["fromDate"].ToString()))
      {
        fromDate = DateTime.Parse(localParams["fromDate"].ToString());
      }

      DateTime? toDate = null;
      if (!String.IsNullOrEmpty(localParams["toDate"].ToString()))
      {
        toDate = DateTime.Parse(localParams["toDate"].ToString());
      }

      var result = await mediator.Send(new GetWeeklyMeasures()
      {
        StationId = stationId,
        FromDate = fromDate ?? DateTime.UtcNow,
        ToDate = toDate,
      });

      Assert.IsNotNull(result, "Result is null");
    }

    [TestMethod]
    public async Task GetLastMonthlyMeasure()
    {
      var localParams = parameters["GetLastMonthlyMeasure"];

      var stationId = localParams["stationId"].ToString();

      Assert.IsNotNull(stationId, "StationId is null");

      DateTime? fromDate = null;
      if (!String.IsNullOrEmpty(localParams["fromDate"].ToString()))
      {
        fromDate = DateTime.Parse(localParams["fromDate"].ToString());
      }

      DateTime? toDate = null;
      if (!String.IsNullOrEmpty(localParams["toDate"].ToString()))
      {
        toDate = DateTime.Parse(localParams["toDate"].ToString());
      }

      var result = await mediator.Send(new GetMonthlyMeasures()
      {
        StationId = stationId,
        FromDate = fromDate ?? DateTime.UtcNow,
        ToDate = toDate,
      });

      Assert.IsNotNull(result, "Result is null");
    }

    [TestMethod]
    public async Task GetMeasureList()
    {
      var localParams = parameters["GetMeasureList"];

      var stationId = localParams["stationId"].ToString();
      var fromDate = DateTime.Parse(localParams["fromDate"].ToString());

      Assert.IsNotNull(stationId, "StationId is null");
      Assert.IsNotNull(fromDate, "FromDate is null");

      DateTime? toDate = null;
      if (!String.IsNullOrEmpty(localParams["toDate"].ToString()))
      {
        toDate = DateTime.Parse(localParams["toDate"].ToString());
      }

      var result = await mediator.Send(new GetMeasuresList()
      {
        StationId = stationId,
        FromDate = fromDate,
        ToDate = toDate ?? DateTime.Now.AddDays(1).Date,
      });

      Assert.IsNotNull(result, "Result is null");
    }
  }
}
