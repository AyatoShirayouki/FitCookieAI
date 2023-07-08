using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace FitCookieAI.Test
{
	public class GPT_Test_Service
	{
		private readonly IConfiguration _config;

		public GPT_Test_Service(IConfiguration config)
		{
			_config = config;
		}

		public async Task<BaseGPTTestResponse> GetGPTResponse(HttpClient httpClient, BaseGPTTestRequest request)
		{
			BaseGPTTestResponse? resultResponse = new BaseGPTTestResponse();

			httpClient.DefaultRequestHeaders.Add("authorization", "Bearer " + _config.GetValue<string>("OpenAI_API_Key"));
			httpClient.BaseAddress = new Uri(_config.GetValue<string>("OpenAI_API_Address"));
			httpClient.DefaultRequestHeaders.Accept.Clear();
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var content = JsonConvert.SerializeObject(request);
			var buffer = System.Text.Encoding.UTF8.GetBytes(content);
			var byteContent = new ByteArrayContent(buffer);
			byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			using (var response = await httpClient.PostAsync("", byteContent))
			{
				string apiResponse = await response.Content.ReadAsStringAsync();

				if (!string.IsNullOrEmpty(apiResponse))
				{
					resultResponse = JsonConvert.DeserializeObject<BaseGPTTestResponse>(apiResponse);
				}
			}

			return resultResponse;
		}
	}
}
