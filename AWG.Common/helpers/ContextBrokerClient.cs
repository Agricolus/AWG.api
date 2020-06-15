using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AWG.Common.Enums;
using AWG.FIWARE.Serializers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AWG.Common.Helpers
{
  public class ContextBrokerClient
  {
    private readonly HttpClient client;
    private string apiVersion;

    public ContextBrokerClient(string service, string servicePath, string url, string apiVersion = "v2")
    {
      this.client = new HttpClient();
      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      client.DefaultRequestHeaders.Add("fiware-service", service);
      client.DefaultRequestHeaders.Add("fiware-servicepath", servicePath);
      this.client.BaseAddress = new Uri($"{url}");
      this.apiVersion = apiVersion;
    }

    public async Task<IEnumerable<T>> ListEntities<T>(string id = null,
                                                      string type = null,
                                                      string idPattern = null,
                                                      string typePattern = null,
                                                      string[] q = null,
                                                      string[] mq = null,
                                                      string georel = null,
                                                      string geometry = null,
                                                      double[][] coords = null,
                                                      int? limit = null,
                                                      int? offset = null,
                                                      string[] metadata = null,
                                                      string[] orderBy = null,
                                                      AttributesFormatEnum options = AttributesFormatEnum.normalized) where T : class, new()
    {

      string uri = $"{apiVersion}/entities";
      Uri requestUri;
      Uri.TryCreate(client.BaseAddress, uri, out requestUri);
      var retrieveEntityUri = new UriBuilder(requestUri);
      NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
      if (id != null)
        queryString.Add("id", id);
      if (type != null)
        queryString.Add("type", type);
      if (idPattern != null)
        queryString.Add("idPattern", idPattern);
      if (typePattern != null)
        queryString.Add("typePattern", typePattern);
      if (q != null)
        queryString.Add("q", string.Join(";", q));
      if (mq != null)
        queryString.Add("mq", string.Join(";", mq));
      if (georel != null)
        queryString.Add("georel", georel);
      if (geometry != null)
        queryString.Add("geometry", geometry);
      if (coords != null)
        queryString.Add("coords", string.Join(";", coords.Select(xy => string.Join(",", xy))));
      if (limit.HasValue)
        queryString.Add("limit", limit.Value.ToString());
      if (offset.HasValue)
        queryString.Add("offset", offset.Value.ToString());
      if (metadata != null)
        queryString.Add("metadata", string.Join(",", mq));
      if (orderBy != null)
        queryString.Add("orderBy", string.Join(",", orderBy));

      queryString.Add("options", Enum.GetName(typeof(AttributesFormatEnum), options));
      retrieveEntityUri.Query = queryString.ToString();
      var response = await client.GetAsync(retrieveEntityUri.Uri);

      var responsetext = await response.Content.ReadAsStringAsync();
      IEnumerable<T> deserialized = null; ;
      if (options == AttributesFormatEnum.normalized)
        deserialized = JsonConvert.DeserializeObject<IEnumerable<T>>(responsetext, new FiwareNormalizedJsonConverter<T>());
      else
        deserialized = JsonConvert.DeserializeObject<IEnumerable<T>>(responsetext);

      return deserialized;
    }

    public async Task<string> CreateEntity<T>(T entityObject, AttributesFormatEnum options = AttributesFormatEnum.normalized) where T : class, new()
    {
      string entityString = null;
      if (options == AttributesFormatEnum.normalized)
        entityString = JsonConvert.SerializeObject(entityObject, new FiwareNormalizedJsonConverter<T>());
      else if (options == AttributesFormatEnum.keyValues)
        entityString = JsonConvert.SerializeObject(entityObject, new JsonSerializerSettings
        {
          ContractResolver = new DefaultContractResolver
          {
            NamingStrategy = new CamelCaseNamingStrategy()
          }
        });
      else
        throw new Exception($"attribute format not supported for this operation: {(AttributesFormatEnum)options}");

      var response = await client.PostAsync($"{apiVersion}/entities?options={options}", new StringContent(entityString, Encoding.UTF8, "application/json"));

      if (response.StatusCode == System.Net.HttpStatusCode.Created)
      {
        return response.Headers.Location.ToString().Split('/').Last().Split('?').First();
      }
      var responsetext = await response.Content.ReadAsStringAsync();
      throw new Exception($"entity creation failed with code: {response.StatusCode} - {responsetext}");
    }

    public async Task AppendEntity<T>(string entityId, T entityObject, string entityType = null, bool strictAppend = false) where T : class, new()
    {
      string uri = $"{apiVersion}/entities/{entityId}/attrs";
      Uri requestUri;
      Uri.TryCreate(client.BaseAddress, uri, out requestUri);
      var retrieveEntityUri = new UriBuilder(requestUri);
      NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
      if (entityType != null)
        queryString.Add("type", entityType);
      if (strictAppend)
        queryString.Add("options", "append");
      retrieveEntityUri.Query = queryString.ToString();

      var entityString = JsonConvert.SerializeObject(entityObject, new FiwareNormalizedJsonConverter<T>());

      var response = await client.PostAsync(retrieveEntityUri.Uri, new StringContent(entityString, Encoding.UTF8, "application/json"));

      if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
      {
        var responsetext = await response.Content.ReadAsStringAsync();
        throw new Exception($"entity update failed with code: {response.StatusCode} - {responsetext}"); ;
      }
    }

    public async Task UpdateEntity<T>(string entityId, T entityObject, string entityType = null, AttributesFormatEnum options = AttributesFormatEnum.normalized) where T : class, new()
    {
      string uri = $"{apiVersion}/entities/{entityId}/attrs";
      Uri requestUri;
      Uri.TryCreate(client.BaseAddress, uri, out requestUri);
      var retrieveEntityUri = new UriBuilder(requestUri);
      NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
      if (entityType != null)
        queryString.Add("type", entityType);

      String entityString = null;
      if (options == AttributesFormatEnum.keyValues)
      {
        queryString.Add("options", "keyValues");
        entityString = JsonConvert.SerializeObject(entityObject, new JsonSerializerSettings
        {
          ContractResolver = new DefaultContractResolver
          {
            NamingStrategy = new CamelCaseNamingStrategy()
          }
        });
      }
      else
      {
        entityString = JsonConvert.SerializeObject(entityObject, new FiwareNormalizedJsonConverter<T>());
      }
      retrieveEntityUri.Query = queryString.ToString();
      var response = await client.PatchAsync(retrieveEntityUri.Uri, new StringContent(entityString, Encoding.UTF8, "application/json"));

      if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
      {
        var responsetext = await response.Content.ReadAsStringAsync();
        throw new Exception($"entity update failed with code: {response.StatusCode} - {responsetext}"); ;
      }
    }

    public async Task RemoveEntity(string entityId, string entityType = null)
    {
      string uri = $"{apiVersion}/entities/{entityId}";
      Uri requestUri;
      Uri.TryCreate(client.BaseAddress, uri, out requestUri);
      var retrieveEntityUri = new UriBuilder(requestUri);
      NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
      if (entityType != null)
        queryString.Add("type", entityType);

      retrieveEntityUri.Query = queryString.ToString();

      var response = await client.DeleteAsync(retrieveEntityUri.Uri);

      if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
      {
        var responsetext = await response.Content.ReadAsStringAsync();
        throw new Exception($"entity removal failed with code: {response.StatusCode} - {responsetext}");
      }
    }

    public async Task<T> RetrieveEntity<T>(string entityId, string entityType = null, string[] entityAttributes = null) where T : class, new()
    {
      string uri = $"{apiVersion}/entities/{entityId}";
      Uri requestUri;
      Uri.TryCreate(client.BaseAddress, uri, out requestUri);
      var retrieveEntityUri = new UriBuilder(requestUri);
      NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
      if (entityType != null)
        queryString.Add("type", entityType);
      if (entityAttributes != null)
        queryString.Add("attrs", string.Join(",", entityAttributes));

      retrieveEntityUri.Query = queryString.ToString();

      var response = await client.GetAsync(retrieveEntityUri.Uri);
      if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
      {
        return null;
      }
      var responsetext = await response.Content.ReadAsStringAsync();

      var deserialized = JsonConvert.DeserializeObject<T>(responsetext, new FiwareNormalizedJsonConverter<T>());

      return deserialized;
    }

    public async Task<IEnumerable<Subscription>> ListSubscriptions()
    {
      var response = await client.GetAsync($"{apiVersion}/subscriptions");

      var responsetext = await response.Content.ReadAsStringAsync();

      var deserialized = JsonConvert.DeserializeObject<IEnumerable<Subscription>>(responsetext);

      return deserialized;
    }

    public async Task<Subscription> GetSubscription(string subscriptionId)
    {
      var response = await client.GetAsync($"{apiVersion}/subscriptions/{subscriptionId}");

      if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
      {
        return null;
      }

      var responsetext = await response.Content.ReadAsStringAsync();

      var deserialized = JsonConvert.DeserializeObject<Subscription>(responsetext);

      return deserialized;
    }

    public async Task<string> CreateSubscription(Subscription sub)
    {
      var contentString = JsonConvert.SerializeObject(sub);

      var response = await client.PostAsync($"{apiVersion}/subscriptions", new StringContent(contentString, Encoding.UTF8, "application/json"));

      if (response.StatusCode == System.Net.HttpStatusCode.Created)
      {
        return response.Headers.Location.ToString().Split('/').Last();
      }
      var responsetext = await response.Content.ReadAsStringAsync();
      throw new Exception($"subscription creation failed with code: {response.StatusCode} - {responsetext}");
    }

    public async Task UpdateSubscription(string subscriptionId, DateTime expire)
    {
      var expireObject = new
      {
        expires = expire
      };
      var contentString = JsonConvert.SerializeObject(expireObject);
      var response = await client.PatchAsync($"{apiVersion}/subscriptions/{subscriptionId}", new StringContent(contentString, Encoding.UTF8, "application/json"));

      if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
      {
        var responsetext = await response.Content.ReadAsStringAsync();
        throw new Exception($"subscription update failed with code: {response.StatusCode} - {responsetext}");
      }
    }

    public async Task DeleteSubscription(string subscriptionId)
    {
      var response = await client.DeleteAsync($"{apiVersion}/subscriptions/{subscriptionId}");

      if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
      {
        var responsetext = await response.Content.ReadAsStringAsync();
        throw new Exception($"subscription delete failed with code: {response.StatusCode} - {responsetext}");
      }
    }
  }
}
