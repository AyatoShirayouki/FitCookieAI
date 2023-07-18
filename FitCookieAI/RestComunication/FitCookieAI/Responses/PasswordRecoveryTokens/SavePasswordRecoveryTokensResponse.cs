using FitCookieAI_API.Responses;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace FitCookieAI.RestComunication.FitCookieAI.Responses.PasswordRecoveryTokens
{
	public class SavePasswordRecoveryTokensResponse : BaseResponseMessage
	{
		[JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
		public string Body { get; set; }
	}
}
