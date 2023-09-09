using Authorisation.Configuration;
using Authorisation.DTOs;
using Authorisation.Services;
using Azure;
using GPT_3_Web_API.Services;
using GPT_Web_API.Requests;
using GPT_Web_API.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GPT_Web_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GPTController : ControllerBase
	{
		private readonly ILogger<GPTController> _logger;
		private readonly IConfiguration _config;
		private GPT_Service _service;

        private readonly RefreshUserTokenService _usersAuthentication;
        private readonly TokenValidationParameters _tokenValidationParams;
        private TokenRequestDTO tokenRequest;
		private BaseGPTResponse _response;

        public GPTController(ILogger<GPTController> logger, IConfiguration config, GPT_Service service, IOptionsMonitor<JwtConfig> optionsMonitor, TokenValidationParameters tokenValidationParams)
		{
			_logger = logger;
			_config = config;
			_service = service;

            _tokenValidationParams = tokenValidationParams;
            _usersAuthentication = new RefreshUserTokenService(optionsMonitor, _tokenValidationParams);

			_response = new BaseGPTResponse();
            tokenRequest = new TokenRequestDTO();
        }

		[HttpGet]
		[Route("GetGPTResponse")]
		public async Task<IActionResult> GetGPTResponse(string input, [FromHeader] string token, [FromHeader] string refreshToken)
		{
            if (token != null && refreshToken != null)
			{
                if (!string.IsNullOrEmpty(input))
                {
                    tokenRequest.Token = token;
                    tokenRequest.RefreshToken = refreshToken;

                    JwtResult jwtUserToken = await _usersAuthentication.RefreshUserToken(tokenRequest);

                    if (jwtUserToken.JwtSuccess == true)
                    {
                        BaseGPTRequest request = new BaseGPTRequest();

                        request.model = _config.GetValue<string>("OpenAI_API_Request_Model");
                        request.prompt = input;
                        request.max_tokens = 3000;
                        request.temperature = 0;

                        using (var httpClient = new HttpClient())
                        {
                            _response = await _service.GetGPTResponse(httpClient, request);
                        }

                        if (_response != null)
                        {
                            _response.Code = 201;

                            HttpContext.Response.Headers.Add("token", jwtUserToken.Token);
                            HttpContext.Response.Headers.Add("refreshToken", jwtUserToken.RefreshToken);

                            _response.JwtSuccess = jwtUserToken.JwtSuccess;
                            _response.JwtErrors = jwtUserToken.JwtErrors;
                            //return new JsonResult(_response);
                            //data = leaveTypes
                        }
                        else
                        {
                            _response.Code = 500;
                            _response.Error = "GPT returned null!";
                        }
                    }
                    else if (jwtUserToken.JwtSuccess != true)
                    {
                        _response.Code = 200;

                        _response.JwtSuccess = jwtUserToken.JwtSuccess;
                        _response.JwtErrors = jwtUserToken.JwtErrors;
                    }
                }
                else
                {
                    _response.Code = 400;
                    _response.Error = "Missing data - Input cannot be null or empty!";
                }
            }
            else
            {
                _response.Code = 400;
                _response.Error = "Missing data - Token or refresh token is null or invalid!";
            }

            return new JsonResult(_response);
        }
	}
}
