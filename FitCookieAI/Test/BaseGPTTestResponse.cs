using static FitCookieAI.Test.BaseGPTTestRequest;

namespace FitCookieAI.Test
{
	public class BaseGPTTestResponse
	{
		public string? id { get; set; }
		public string? Object { get; set; }
		public long created { get; set; }
		public string? model { get; set; }
		public List<Choice>? choices { get; set; }
		public Usage? usage { get; set; }

		public class Choice
		{
			public Message? message { get; set; }
			public long index { get; set; }
			public string? logprobs { get; set; }
			public string? finish_reason { get; set; }
		}
		public class Usage
		{
			public long prompt_tokens { get; set; }
			public long completion_tokens { get; set; }
			public long total_tokens { get; set; }
		}
	}
}
