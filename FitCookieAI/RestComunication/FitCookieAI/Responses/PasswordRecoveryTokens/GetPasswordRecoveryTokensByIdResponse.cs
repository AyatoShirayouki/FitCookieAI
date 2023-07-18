using FitCookieAI_API.Responses;
using FitCookieAI_ApplicationService.DTOs.Others;
using FitCookieAI_ApplicationService.DTOs.UserRelated;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace FitCookieAI.RestComunication.FitCookieAI.Responses.PasswordRecoveryTokens
{
	public class GetPasswordRecoveryTokensByIdResponse : BaseResponseMessage
	{
		[JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
		public PasswordRecoveryTokenDTO Body { get; set; }
	}
}
