using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TMDB_demo.Extensions;
using TMDB_demo.Services.Interfaces;

namespace TMDB_demo.WebApi
{
    public class TmdbApiService : ITmdbApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration Configuration;

        public TmdbApiService(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration) 
        {
            _httpClientFactory = httpClientFactory;
            Configuration = configuration;
        }

        public async Task<T> GetData<T>(string path, Dictionary<string,string> parameters = null) {

            if (parameters == null) {
                parameters = new Dictionary<string, string>();
            }            

            path = ReplaceUrlPlaceholders(path, parameters);
            parameters.Add("api_key", Configuration.GetApiKey());

            var httpClient =_httpClientFactory.CreateClient("tmdb");            

            var url = $"/{Configuration.GetApiVersion()}/{path}";
            
            url += GetQueryParams(parameters);            

            var response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseContentRead);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var objects =  JsonSerializer.Deserialize<T>(responseJson, options);
            return objects;
        }

        private string ReplaceUrlPlaceholders(string path, Dictionary<string, string> parameters) { 
            foreach (var param in parameters){
                var possiblePlaceholder = $"{{{param.Key}}}";
                if (path.Contains(possiblePlaceholder)) {
                    path = path.Replace(possiblePlaceholder, param.Value);
                    parameters.Remove(param.Key);
                }
            }

            return path;
        }

        private string GetQueryParams(Dictionary<string, string> parameters) {
            var paramStrings = new List<string>();
            
            foreach (var param in parameters) {
                paramStrings.Add($"{param.Key}={param.Value}");
            }

            return $"?{string.Join('&', paramStrings)}";
        }
       
    }
}
