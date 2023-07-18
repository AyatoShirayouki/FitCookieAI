using Authorisation.Configuration;
using Authorisation.DTOs;
using Authorisation.Services;
using FitCookieAI_API.Responses;
using FitCookieAI_ApplicationService.DTOs.Others;
using FitCookieAI_ApplicationService.DTOs.UserRelated;
using FitCookieAI_ApplicationService.Implementations.Others;
using FitCookieAI_ApplicationService.Implementations.UserRelated;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FitCookieAI_API.Controllers.UserRelated
{
	[Route("api/[controller]")]
	[Produces("application/json")]
	[ApiController]
	public class PasswordRecoveryTokensController : ControllerBase
	{
		private readonly RefreshAdminTokenService _authentication;
		private readonly TokenValidationParameters _tokenValidationParams;

		private BaseResponseMessage response;
		private TokenRequestDTO tokenRequest;

		public PasswordRecoveryTokensController(IOptionsMonitor<JwtConfig> optionsMonitor, TokenValidationParameters tokenValidationParams, IHttpContextAccessor httpContextAccessor)
		{
			_tokenValidationParams = tokenValidationParams;
			_authentication = new RefreshAdminTokenService(optionsMonitor, _tokenValidationParams);

			response = new BaseResponseMessage();
			tokenRequest = new TokenRequestDTO();
		}

		[HttpGet]
		[Route("GetAll")]
		public async Task<IActionResult> GetAll([FromHeader] string token, [FromHeader] string refreshToken)
		{
			if (token != null && refreshToken != null)
			{
				tokenRequest.Token = token;
				tokenRequest.RefreshToken = refreshToken;

				JwtResult jwtAdminToken = await _authentication.RefreshAdminToken(tokenRequest);

				if (jwtAdminToken.JwtSuccess == true)
				{
					response.Code = 201;

					response.Body = await PasswordRecoveryTokensManagementService.GetAll();

					HttpContext.Response.Headers.Add("token", jwtAdminToken.Token);
					HttpContext.Response.Headers.Add("refreshToken", jwtAdminToken.RefreshToken);

					response.JwtSuccess = jwtAdminToken.JwtSuccess;
					response.JwtErrors = jwtAdminToken.JwtErrors;
				}
				else if (jwtAdminToken.JwtSuccess != true)
				{
					response.Code = 200;

					response.JwtSuccess = jwtAdminToken.JwtSuccess;
					response.JwtErrors = jwtAdminToken.JwtErrors;
				}
			}
			else
			{
				response.Code = 400;
				response.Error = "Missing data - Token or refresh token is null or invalid!";
			}
			return new JsonResult(response);
		}

		[HttpPost]
		[Route("Save")]
		public async Task<IActionResult> Save(PasswordRecoveryTokenDTO passwordRecoveryTokenDTO, [FromHeader] string token, [FromHeader] string refreshToken)
		{
			if (passwordRecoveryTokenDTO == null || token == null || refreshToken == null)
			{
				response.Code = 400;
				response.Error = "Missing data - PaymentPlan and/or token and/or refresh token is null or invalid!";

				return new JsonResult(response);
			}
			else
			{
				tokenRequest.Token = token;
				tokenRequest.RefreshToken = refreshToken;

				JwtResult jwtAdminToken = await _authentication.RefreshAdminToken(tokenRequest);

				if (jwtAdminToken.JwtSuccess == true)
				{
					response.Code = 201;

					await PasswordRecoveryTokensManagementService.Save(passwordRecoveryTokenDTO);
					response.Body = "Token has been saved!";

					HttpContext.Response.Headers.Add("token", jwtAdminToken.Token);
					HttpContext.Response.Headers.Add("refreshToken", jwtAdminToken.RefreshToken);

					response.JwtSuccess = jwtAdminToken.JwtSuccess;
					response.JwtErrors = jwtAdminToken.JwtErrors;
				}
				else if (jwtAdminToken.JwtSuccess != true)
				{
					response.Code = 200;

					response.JwtSuccess = jwtAdminToken.JwtSuccess;
					response.JwtErrors = jwtAdminToken.JwtErrors;
				}
			}

			return new JsonResult(response);
		}

		[HttpGet]
		[Route("GetById")]
		public async Task<IActionResult> GetById(int id, [FromHeader] string token, [FromHeader] string refreshToken)
		{
			if (id == 0 || token == null || refreshToken == null)
			{
				response.Code = 400;
				response.Error = "Missing data - Id and/or token data is incorrect or null";

				return new JsonResult(response);
			}
			else
			{
				tokenRequest.Token = token;
				tokenRequest.RefreshToken = refreshToken;

				JwtResult jwtAdminToken = await _authentication.RefreshAdminToken(tokenRequest);

				if (jwtAdminToken.JwtSuccess == true)
				{
					response.Code = 201;

					response.Body = await PasswordRecoveryTokensManagementService.GetById(id);

					HttpContext.Response.Headers.Add("token", jwtAdminToken.Token);
					HttpContext.Response.Headers.Add("refreshToken", jwtAdminToken.RefreshToken);

					response.JwtSuccess = jwtAdminToken.JwtSuccess;
					response.JwtErrors = jwtAdminToken.JwtErrors;
				}
				else if (jwtAdminToken.JwtSuccess != true)
				{
					response.Code = 200;

					response.JwtSuccess = jwtAdminToken.JwtSuccess;
					response.JwtErrors = jwtAdminToken.JwtErrors;
				}
				return new JsonResult(response);
			}
		}

		[HttpDelete]
		[Route("Delete")]
		public async Task<IActionResult> Delete(int id, [FromHeader] string token, [FromHeader] string refreshToken)
		{
			if (id == 0 || token == null || refreshToken == null)
			{
				response.Code = 400;
				response.Error = "Missing data - Id and/or token data is incorrect or null";

				return new JsonResult(response);
			}
			else
			{
				tokenRequest.Token = token;
				tokenRequest.RefreshToken = refreshToken;

				JwtResult jwtAdminToken = await _authentication.RefreshAdminToken(tokenRequest);

				if (jwtAdminToken.JwtSuccess == true)
				{
					response.Code = 201;

					await PasswordRecoveryTokensManagementService.Delete(id);
					response.Body = "Token has been deleted!";

					HttpContext.Response.Headers.Add("token", jwtAdminToken.Token);
					HttpContext.Response.Headers.Add("refreshToken", jwtAdminToken.RefreshToken);

					response.JwtSuccess = jwtAdminToken.JwtSuccess;
					response.JwtErrors = jwtAdminToken.JwtErrors;
				}
				else if (jwtAdminToken.JwtSuccess != true)
				{
					response.Code = 201;

					response.JwtSuccess = jwtAdminToken.JwtSuccess;
					response.JwtErrors = jwtAdminToken.JwtErrors;
				}
			}
			return new JsonResult(response);
		}
	}
}
