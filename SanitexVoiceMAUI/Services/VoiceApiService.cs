using SanitexVoiceMAUI.Models;

namespace SanitexVoiceMAUI.Services
{
    public class VoiceApiService
    {
        private readonly RestService _restService;

        public VoiceApiService(RestService restService)
        {
            _restService = restService;
        }

        public async Task<VoiceApiResponse> CallVoiceMessage(string command, string data)
        {
            return await _restService.GetSomeDataFromApi(new VoiceApiRequest { CommandName = command, Data = data });
        }
    }
}
