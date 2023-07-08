namespace FitCookieAI.Test
{
	public class BaseGPTTestRequest
	{
		public string? model { get; set; }
		public List<Message>? messages { get; set; }
		public long max_tokens { get; set; }
		public double temperature { get; set; }

		public class Message
		{
			public string role { get; set; }
			public string content { get; set; }
		}
	}
}
