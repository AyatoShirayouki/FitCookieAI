using AdminPanel.ActionFilters;
using AdminPanel.Models;
using AdminPanel.Models.Home;
using AdminPanel.RestComunication.FitCookieAI;
using AdminPanel.RestComunication.FitCookieAI.Responses.Admins;
using AdminPanel.RestComunication.FitCookieAI.Responses.AdminStatuses;
using AdminPanel.RestComunication.FitCookieAI.Responses.Users;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdminPanel.Controllers
{
	[AdminsAuthenticationFilter]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment webHostEnvironment;
        private IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

		private GetAllAdminsResponse _getAllAdminsResponse;
		private GetAllUsersResponse _getAllUsersResponse;
		private GetAllActiveAdminsResponse _getAllActiveAdminsResponse;
		private GetAllActiveUsersResponse _getAllActiveUsersResponse;
		private GetAllAdminStatusesResponse _getAllAdminStatusesResponse;

        private FitCookieAI_RequestBuilder _fitCookieAIRequestBuilder;
        private FitCookieAI_RequestExecutor _fitCookieAIRequestExecutor;

        string baseFitcookieAIUri;

        public HomeController(IWebHostEnvironment hostEnvironment, ILogger<HomeController> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
		{
			_logger = logger;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            webHostEnvironment = hostEnvironment;

            _fitCookieAIRequestBuilder = new FitCookieAI_RequestBuilder();
            _fitCookieAIRequestExecutor = new FitCookieAI_RequestExecutor(_httpContextAccessor);

			_getAllActiveAdminsResponse = new GetAllActiveAdminsResponse();
			_getAllActiveUsersResponse = new GetAllActiveUsersResponse();
			_getAllAdminsResponse = new GetAllAdminsResponse();
			_getAllAdminStatusesResponse = new GetAllAdminStatusesResponse();
			_getAllUsersResponse = new GetAllUsersResponse();

            baseFitcookieAIUri = _configuration.GetValue<string>("FitCookieAI_API");
        }

		public async Task<IActionResult> Index(string error)
		{
			HomeIndexVM model = new HomeIndexVM();
			if (!string.IsNullOrEmpty(error))
			{
				model.Error = error;
			}

			_getAllAdminsResponse = await _fitCookieAIRequestExecutor.GetAllAdminsAction(_fitCookieAIRequestBuilder.GetAllAdminsRequestBuilder(baseFitcookieAIUri));
			_getAllUsersResponse = await _fitCookieAIRequestExecutor.GetAllUsersAction(_fitCookieAIRequestBuilder.GetAllUsersRequestBuilder(baseFitcookieAIUri));
			_getAllActiveAdminsResponse = await _fitCookieAIRequestExecutor.GetAllActiveAdminsAction(_fitCookieAIRequestBuilder.GetAllActiveAdminsRequestBuilder(baseFitcookieAIUri));
			_getAllActiveUsersResponse = await _fitCookieAIRequestExecutor.GetAllActiveUsersAction(_fitCookieAIRequestBuilder.GetAllActiveUsersRequestBuilder(baseFitcookieAIUri));
			_getAllAdminStatusesResponse = await _fitCookieAIRequestExecutor.GetAllAdminStatusesAction(_fitCookieAIRequestBuilder.GetAllAdminStatusesRequestBuilder(baseFitcookieAIUri));

			if ((_getAllAdminsResponse.Code != null && int.Parse(_getAllAdminsResponse.Code.ToString()) == 201) &&
				(_getAllUsersResponse.Code != null && int.Parse(_getAllUsersResponse.Code.ToString()) == 201) && 
				(_getAllActiveAdminsResponse.Code != null && int.Parse(_getAllActiveAdminsResponse.Code.ToString()) == 201) &&
				(_getAllActiveUsersResponse.Code != null && int.Parse(_getAllActiveUsersResponse.Code.ToString()) == 201) &&
				(_getAllAdminStatusesResponse.Code != null && int.Parse(_getAllAdminStatusesResponse.Code.ToString()) == 201))
			{
				model.Users = _getAllUsersResponse.Body;
				model.Admins = _getAllAdminsResponse.Body;
				model.RefreshAdminTokens = _getAllActiveAdminsResponse.Body;
				model.RefreshUserTokens = _getAllActiveUsersResponse.Body;
				model.AdminStatuses = _getAllAdminStatusesResponse.Body;
			}

			return View(model);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}