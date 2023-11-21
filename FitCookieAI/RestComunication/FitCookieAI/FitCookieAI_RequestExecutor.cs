using FitCookieAI.RestComunication.FitCookieAI.Responses.GeneratedPlans;
using FitCookieAI.RestComunication.FitCookieAI.Responses.PasswordRecoveryTokens;
using FitCookieAI.RestComunication.FitCookieAI.Responses.PaymentRelated.Payments;
using FitCookieAI.RestComunication.FitCookieAI.Responses.UserRelated;
using FitCookieAI_ApplicationService.DTOs.Others;
using FitCookieAI_ApplicationService.DTOs.UserRelated;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace FitCookieAI.RestComunication.FitCookieAI
{
    public class FitCookieAI_RequestExecutor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public FitCookieAI_RequestExecutor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        //Users
        public async Task<SignUpUserResponse> SignUpAction(UserDTO request, string requestQuery)
        {
            using (var httpClient = new HttpClient())
            {
                SignUpUserResponse _signUpResponse = new SignUpUserResponse();

                httpClient.BaseAddress = new Uri(requestQuery);
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
                        var convert = JsonConvert.DeserializeObject<SignUpUserResponse>(apiResponse);

                        if (convert != null)
                        {
                            _signUpResponse = convert;
                        }
                    }
                }

                return _signUpResponse;
            }
        }
        public async Task<LoginUserResponse> LoginAction(string requestQuery)
        {
            using (var httpClient = new HttpClient())
            {
                LoginUserResponse _loginUserResponse = new LoginUserResponse();

                using (var response = await httpClient.GetAsync(requestQuery))
                {
                    if (response.Headers.FirstOrDefault(x => x.Key == "token").Value != null ||
                        response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value != null)
                    {
                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());

                        string apiResponse = await response.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(apiResponse))
                        {
                            var convert = JsonConvert.DeserializeObject<LoginUserResponse>(apiResponse);

                            if (convert != null)
                            {
                                _loginUserResponse = convert;
                            }
                        }
                    }
                }

                return _loginUserResponse;
            }

        }
        public async Task<LogoutUserResponse> LogoutAction(string requestQuery)
        {
            using (var httpClient = new HttpClient())
            {
                LogoutUserResponse _logoutResponse = new LogoutUserResponse();

                using (var response = await httpClient.GetAsync(requestQuery))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(apiResponse))
                    {
                        var convert = JsonConvert.DeserializeObject<LogoutUserResponse>(apiResponse);

                        if (convert != null)
                        {
                            _logoutResponse = convert;
                        }
                    }
                }

                return _logoutResponse;
            }
        }
        public async Task<UpdateUsersResponse> UpdateUsersAction(UserDTO request, string requestQuery)
        {
            using (var httpClient = new HttpClient())
            {
                UpdateUsersResponse _updateUserResponse = new UpdateUsersResponse();

                httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
                httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

                httpClient.BaseAddress = new Uri(requestQuery);
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
                        var convert = JsonConvert.DeserializeObject<UpdateUsersResponse>(apiResponse);

                        if (convert != null)
                        {
                            _updateUserResponse = convert;

                            _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                            _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                        }
                    }
                }
                return _updateUserResponse;
            }
        }
        public async Task<VerifyUserByEmailResponse> VerifyUserByEmailAction(string requestQuery)
        {
            using (var httpClient = new HttpClient())
            {
                VerifyUserByEmailResponse _verifyUserByEmailResponse = new VerifyUserByEmailResponse();

                using (var response = await httpClient.GetAsync(requestQuery))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(apiResponse))
                    {
                        var convert = JsonConvert.DeserializeObject<VerifyUserByEmailResponse>(apiResponse);

                        if (convert != null)
                        {
                            _verifyUserByEmailResponse = convert;
                        }
                    }
                }

                return _verifyUserByEmailResponse;
            }
        }
        public async Task<EditUserPasswordResponse> EditUserPasswordAction(string requestQuery)
        {
            using (var httpClient = new HttpClient())
            {
                EditUserPasswordResponse _editUserPasswordResponse = new EditUserPasswordResponse();

                using (var response = await httpClient.GetAsync(requestQuery))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(apiResponse))
                    {
                        var convert = JsonConvert.DeserializeObject<EditUserPasswordResponse>(apiResponse);

                        if (convert != null)
                        {
                            _editUserPasswordResponse = convert;
                        }
                    }
                }
                return _editUserPasswordResponse;
            }
        }


        //PasswordRecoveryTokens
        public async Task<GetAllPasswordRecoveryTokensResponse> GetAllPasswordRecoveryTokensAction(string requestQuery)
		{
			using (var httpClient = new HttpClient())
			{
				GetAllPasswordRecoveryTokensResponse _getAllPasswordRecoveryTokensRespponse = new GetAllPasswordRecoveryTokensResponse();

				using (var response = await httpClient.GetAsync(requestQuery))
				{
					string apiResponse = await response.Content.ReadAsStringAsync();

					if (!string.IsNullOrEmpty(apiResponse))
					{
						var convert = JsonConvert.DeserializeObject<GetAllPasswordRecoveryTokensResponse>(apiResponse);

						if (convert != null)
						{
							_getAllPasswordRecoveryTokensRespponse = convert;
						}
					}
				}

				return _getAllPasswordRecoveryTokensRespponse;
			}
		}

		public async Task<DeletePasswordRecoveryTokensResponse> DeletePasswordRecoveryTokensAction(string requestQuery)
		{
			using (var httpClient = new HttpClient())
			{
				DeletePasswordRecoveryTokensResponse _deletePasswordRecoveryTokensResponse = new DeletePasswordRecoveryTokensResponse();

				httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
				httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

				using (var response = await httpClient.DeleteAsync(requestQuery))
				{
					string apiResponse = await response.Content.ReadAsStringAsync();

					if (!string.IsNullOrEmpty(apiResponse))
					{
						var convert = JsonConvert.DeserializeObject<DeletePasswordRecoveryTokensResponse>(apiResponse);

						if (convert != null)
						{
							_deletePasswordRecoveryTokensResponse = convert;

							_session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
							_session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
						}
					}
				}

				return _deletePasswordRecoveryTokensResponse;
			}
		}

        public async Task<SavePasswordRecoveryTokensResponse> SavePasswordRecoveryTokensAction(PasswordRecoveryTokenDTO request, string requestQuery)
        {
            using (var httpClient = new HttpClient())
            {
                SavePasswordRecoveryTokensResponse _savePasswordRecoveryTokensResponse = new SavePasswordRecoveryTokensResponse();

                httpClient.BaseAddress = new Uri(requestQuery);
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
                        var convert = JsonConvert.DeserializeObject<SavePasswordRecoveryTokensResponse>(apiResponse);

                        if (convert != null)
                        {
                            _savePasswordRecoveryTokensResponse = convert;
                        }
                    }
                }
                return _savePasswordRecoveryTokensResponse;
            }
        }

		public async Task<SendEmailResponse> SendEmailAction(string requestQuery)
		{
			using (var httpClient = new HttpClient())
			{
				SendEmailResponse _sendEmailResponse = new SendEmailResponse();

				httpClient.BaseAddress = new Uri(requestQuery);
				httpClient.DefaultRequestHeaders.Accept.Clear();
				httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				using (var response = await httpClient.PostAsync(requestQuery,null))
				{
					string apiResponse = await response.Content.ReadAsStringAsync();

					if (!string.IsNullOrEmpty(apiResponse))
					{
						var convert = JsonConvert.DeserializeObject<SendEmailResponse>(apiResponse);

						if (convert != null)
						{
							_sendEmailResponse = convert;
						}
					}
				}
				return _sendEmailResponse;
			}
		}

		public async Task<GetPasswordRecoveryTokensByIdResponse> GetPasswordRecoveryTokensByIdAction(string requestQuery)
		{
			using (var httpClient = new HttpClient())
			{
				GetPasswordRecoveryTokensByIdResponse _getPasswordRecoveryTokensByIdResponse = new GetPasswordRecoveryTokensByIdResponse();

				httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
				httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

				using (var response = await httpClient.GetAsync(requestQuery))
				{
					string apiResponse = await response.Content.ReadAsStringAsync();

					if (!string.IsNullOrEmpty(apiResponse))
					{
						var convert = JsonConvert.DeserializeObject<GetPasswordRecoveryTokensByIdResponse>(apiResponse);

						if (convert != null)
						{
							_getPasswordRecoveryTokensByIdResponse = convert;

							_session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
							_session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
						}
					}
				}

				return _getPasswordRecoveryTokensByIdResponse;
			}
		}

		//Payments
		public async Task<ChargePaymentResponse> ChargePaymentsAction(PaymentDTO request, string requestQuery)
		{
			using (var httpClient = new HttpClient())
			{
				ChargePaymentResponse _savePaymentResponse = new ChargePaymentResponse();

				httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
				httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

				httpClient.BaseAddress = new Uri(requestQuery);
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
						var convert = JsonConvert.DeserializeObject<ChargePaymentResponse>(apiResponse);

						if (convert != null)
						{
							_savePaymentResponse = convert;

							_session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
							_session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
						}
					}
				}
				return _savePaymentResponse;
			}
		}

		//GeneratedPlans
		public async Task<SaveGeneratedPlansResponse> SaveGeneratedPlansAction(GeneratedPlanDTO request, string requestQuery)
		{
			using (var httpClient = new HttpClient())
			{
				SaveGeneratedPlansResponse _saveGeneratedPlansResponse = new SaveGeneratedPlansResponse();

				httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
				httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

				httpClient.BaseAddress = new Uri(requestQuery);
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
						var convert = JsonConvert.DeserializeObject<SaveGeneratedPlansResponse>(apiResponse);

						if (convert != null)
						{
							_saveGeneratedPlansResponse = convert;

							_session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
							_session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
						}
					}
				}
				return _saveGeneratedPlansResponse;
			}
		}
	}
}
