﻿using FitCookieAI_API.Responses;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace AdminPanel.RestComunication.FitCookieAI.Responses.Payments
{
	public class DeletePaymentsResponse : BaseResponseMessage
	{
		[JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
		public string Body { get; set; }
	}
}
