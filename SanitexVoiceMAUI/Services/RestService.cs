using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using SanitexVoiceMAUI.Models;

namespace SanitexVoiceMAUI.Services
{
    public class RestService
    {
        private readonly HttpClient _httpClient;

        public RestService(IConfiguration configuration)
        {
            string baseUrl = configuration.GetValue<string>("VOICE_API_BASE_URL");
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        public async Task<VoiceApiResponse> GetSomeDataFromApi(VoiceApiRequest request)
        {
            try
            {

                // Serialize the data object to JSON
                var jsonData = JsonSerializer.Serialize(request);

                // Create the HTTP content with JSON data
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // Adjust the URL to your REST API endpoint
                var httpResponseMessage = await _httpClient.PostAsync("/voice/message", content);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    using var contentStream =
                        await httpResponseMessage.Content.ReadAsStreamAsync();

                    var result = await JsonSerializer.DeserializeAsync
                        <VoiceApiResponse>(contentStream);
                    var sessionId = httpResponseMessage.Headers.GetValues("X-BLS-SessionId").First();
                    var userName = httpResponseMessage.Headers.GetValues("X-BLS-UserName").First();
                    _httpClient.DefaultRequestHeaders.Remove("X-BLS-SessionId");
                    _httpClient.DefaultRequestHeaders.Remove("X-BLS-UserName");
                    _httpClient.DefaultRequestHeaders.Remove("X-BLS-DeviceName");
                    _httpClient.DefaultRequestHeaders.Add("X-BLS-SessionId", sessionId);
                    _httpClient.DefaultRequestHeaders.Add("X-BLS-UserName", userName);
                    _httpClient.DefaultRequestHeaders.Add("X-BLS-DeviceName", "3C:E9:F7:B1:F4:AE");

                    if (result != null) return result;
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle exception
                Console.WriteLine($"Error: {ex.Message}");
                 throw;
            }

            return new VoiceApiResponse();
        }
    }
}
