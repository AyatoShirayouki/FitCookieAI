using FitCookieAI_API.Responses;
using FitCookieAI_ApplicationService.DTOs.AdminRelated;
using FitCookieAI_ApplicationService.DTOs.UserRelated;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace AdminPanel.RestComunication.FitCookieAI.Responses.Admins
{
    public class GetAllActiveUsersResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public List<RefreshUserTokenDTO> Body { get; set; }
    }
}
