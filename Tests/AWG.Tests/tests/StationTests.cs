using System.Threading.Tasks;
using AWG.Stations.core.Command;
using AWG.Stations.core.Query;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using fiware = AWG.FIWARE.DataModels;

namespace AWG.Tests.tests
{
  [TestClass]
  public class StationTests
  {
    private string fileParameter = "stations.json";
    private IMediator mediator;
    private dynamic parameters;

    public StationTests()
    {
      this.mediator = Helper.BuildMediator();
      this.parameters = Helper.Parameters(fileParameter);
    }

    [TestMethod]
    public async Task GetAllActiveStations()
    {
      var localParams = parameters["GetAllActiveStations"];

      var skip = int.Parse(localParams["skip"].ToString());
      var take = int.Parse(localParams["take"].ToString());

      var result = await mediator.Send(new ListAllActiveStations() { Skip = skip, Take = take });

      Assert.IsNotNull(result, "Result is null");
    }

    [TestMethod]
    public async Task GetStation()
    {
      var localParams = parameters["GetStation"];

      var id = localParams["id"].ToString();

      Assert.IsNotNull(id, "Id is null");

      var result = await mediator.Send(new GetStation() { Id = id });

      Assert.IsNotNull(result, "Result is null");
    }

    [TestMethod]
    public async Task GetNearestStations()
    {
      var localParams = parameters["GetNearestStations"];

      var longitude = double.Parse(localParams["longitude"].ToString());
      var latitude = double.Parse(localParams["latitude"].ToString());
      var skip = int.Parse(localParams["skip"].ToString());
      var take = int.Parse(localParams["take"].ToString());

      Assert.IsNotNull(longitude, "Longitude is null");
      Assert.IsNotNull(latitude, "Latitude is null");

      var result = await mediator.Send(new GetNearestStations() { Longitude = longitude, Latitude = latitude, Skip = skip, Take = take });

      Assert.IsNotNull(result, "Result is null");
    }

    [TestMethod]
    public async Task CreateStation()
    {
      var localParams = parameters["CreateStation"];

      fiware.Device model = JsonConvert.DeserializeObject<fiware.Device>(localParams["model"].ToString());

      Assert.IsNotNull(model, "Model is null");

      var result = await mediator.Send(new CreateStation() { Model = model, CBEnabled = false });

      Assert.IsNotNull(result, "Result is null");

      await mediator.Send(new DeleteStation() { Id = result.Id, CBEnabled = false });
    }

    [TestMethod]
    public async Task CreateStationLD()
    {
      var localParams = parameters["CreateStationLD"];

      fiware.DeviceLD model = JsonConvert.DeserializeObject<fiware.DeviceLD>(localParams["model"].ToString());

      Assert.IsNotNull(model, "Model is null");

      var result = await mediator.Send(new CreateStation() { Model = model, CBEnabled = false });

      Assert.IsNotNull(result, "Result is null");

      await mediator.Send(new DeleteStation() { Id = result.Id, CBEnabled = false });
    }

    [TestMethod]
    public async Task DeleteStation()
    {
      var localParams = parameters["DeleteStation"];

      var id = localParams["id"].ToString();

      Assert.IsNotNull(id, "Id is null");

      var result = await mediator.Send(new DeleteStation() { Id = id, CBEnabled = false });

      Assert.IsNotNull(result, "Result is null");
    }
  }
}
