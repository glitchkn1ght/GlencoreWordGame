using GlencoreWordGame.Config;
using GlencoreWordGame.Models.Response;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GlencoreWordGame.Service
{
    public interface IWordGameService
    {
        Task<WordResponse> CheckWordValidity(string wordToCheck);
    }
    
    public class WordGameService : IWordGameService
    {
        private readonly ILogger<WordGameService> Logger;
        private readonly HttpClient Client;
        private readonly ApiSettings ConfigApiSettings;

        public WordGameService(HttpClient httpClient, IOptions<ApiSettings> configApiSettings, ILogger<WordGameService> logger)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.Client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.ConfigApiSettings = configApiSettings.Value;
            this.Client.BaseAddress = new Uri(ConfigApiSettings.BaseURL);
        }

        public async Task<WordResponse> CheckWordValidity(string wordToCheck)
        {
            WordResponse apiResponse = new WordResponse();
            
            var rawResponse = await this.Client.GetAsync(wordToCheck);

            if (rawResponse.IsSuccessStatusCode)
            {
                this.Logger.LogInformation($"Operation=CheckWordValidity(WordGameService), Status=Success, Message=Code {rawResponse.StatusCode} received from  dictionaryAPI for word {wordToCheck}");

                apiResponse = JsonConvert.DeserializeObject<List<WordResponse>>(await rawResponse.Content.ReadAsStringAsync())[0];
                
                apiResponse.IsSuccess = true;
            }

            else
            {
                this.Logger.LogWarning($"Operation=CheckWordValidity(WordGameService), Status=Failure, Message=Code {rawResponse.StatusCode} received from  dictionaryAPI for word {wordToCheck}");

                apiResponse.Error = JsonConvert.DeserializeObject<ErrorResponse>(await rawResponse.Content.ReadAsStringAsync());
                
                apiResponse.IsSuccess = false;
            }

            return apiResponse;
        }
    }
}
