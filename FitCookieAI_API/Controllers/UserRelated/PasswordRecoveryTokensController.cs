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
using System.Net.Mail;
using System.Net;
using System;

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

		private static readonly Random random = new Random();
		private const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

		public PasswordRecoveryTokensController(IOptionsMonitor<JwtConfig> optionsMonitor, TokenValidationParameters tokenValidationParams, IHttpContextAccessor httpContextAccessor)
		{
			_tokenValidationParams = tokenValidationParams;
			_authentication = new RefreshAdminTokenService(optionsMonitor, _tokenValidationParams);

			response = new BaseResponseMessage();
			tokenRequest = new TokenRequestDTO();
		}

		[HttpGet]
		[Route("GetAll")]
		public async Task<IActionResult> GetAll()
		{
			List<PasswordRecoveryTokenDTO> tokens = await PasswordRecoveryTokensManagementService.GetAll();

			if (tokens.Count != 0)
			{
                response.Code = 201;
				response.Body = tokens;
            }
			else
			{
                response.Code = 200;
                response.Error = "No tokens found!";
            }

			return new JsonResult(response);
		}

		[HttpPost]
		[Route("Save")]
		public async Task<IActionResult> Save(PasswordRecoveryTokenDTO passwordRecoveryTokenDTO)
		{
			if (passwordRecoveryTokenDTO == null)
			{
				response.Code = 400;
				response.Error = "Missing data - PasswordRecoveryToken is null or invalid!";

				return new JsonResult(response);
			}
			else
			{
				response.Code = 201;
				await PasswordRecoveryTokensManagementService.Save(passwordRecoveryTokenDTO);
				response.Body = "Token has been saved!";
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


		[HttpPost]
		[Route("SendEmail")]
		public async Task<IActionResult> SendEmail(string email, string subject, string messageContent)
		{
			string user = "fitcookieai@gmail.com";
			string pass = "ivleaifvsclnwbxe";
			string smtpServer = "smtp.gmail.com";

			bool ssl = true;

			SmtpClient smtpClient = new SmtpClient();
			NetworkCredential basicCredential = new NetworkCredential(user, pass);
			MailMessage message = new MailMessage();
			MailAddress fromAddress = new MailAddress(user);
			smtpClient.Host = smtpServer;
			if (ssl)
			{
				smtpClient.EnableSsl = true;
				smtpClient.Port = 587;
			}
			else
			{
				smtpClient.Port = 26;
			}
			smtpClient.UseDefaultCredentials = false;
			smtpClient.Credentials = basicCredential;

			message.From = fromAddress;
			message.Subject = subject;
			//Set IsBodyHtml to true means you can send HTML email.
			/*
            message.IsBodyHtml = true;
            message.Body = "<h1>Hello, this is a demo ... ..</h1>";
            */
			message.IsBodyHtml = false;
			message.Body = messageContent;

			message.To.Add(email);

			try
			{
				smtpClient.Send(message);

				response.Code = 201;
				response.Body = "OK";
				return new JsonResult(response);
			}
			catch (Exception ex)
			{
				response.Code = 500;
				response.Body = "An error has occured: " + ex.Message + "\n Email has not been sent!";
				response.Error = ex.ToString();
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
