using FitCookieAI.RestComunication.GPT.Responses;
using Newtonsoft.Json;

namespace FitCookieAI.RestComunication.GPT
{
    public class GPT_RequestExecutor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public GPT_RequestExecutor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetGPTResponse> GetGPTResponseAction(string requestQuery)
        {
			using (var httpClient = new HttpClient())
            {
				GetGPTResponse _getGPTResponse = new GetGPTResponse();

                httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
                httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

                using (var response = await httpClient.GetAsync(requestQuery))
				{
					string apiResponse = await response.Content.ReadAsStringAsync();

					if (!string.IsNullOrEmpty(apiResponse))
					{
						var convert = JsonConvert.DeserializeObject<GetGPTResponse>(apiResponse);

						if (convert != null)
						{
							_getGPTResponse = convert;

                            _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                            _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                        }
					}
				}

				return _getGPTResponse;
			}	
        }
    }
}
