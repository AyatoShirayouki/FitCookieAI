using FitCookieAI_API.Responses;
using FitCookieAI_ApplicationService.DTOs.AdminRelated;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace AdminPanel.RestComunication.FitCookieAI.Responses.Admins
{
    public class GetAllActiveAdminsResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public List<RefreshAdminTokenDTO> Body { get; set; }
    }
}
