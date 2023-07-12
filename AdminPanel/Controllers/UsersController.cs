using AdminPanel.ActionFilters;
using AdminPanel.Models.Admins;
using AdminPanel.Models.Users;
using AdminPanel.RestComunication.FitCookieAI;
using AdminPanel.RestComunication.FitCookieAI.Responses.Admins;
using AdminPanel.RestComunication.FitCookieAI.Responses.AdminStatuses;
using AdminPanel.RestComunication.FitCookieAI.Responses.Users;
using FitCookieAI_ApplicationService.DTOs.AdminRelated;
using FitCookieAI_ApplicationService.DTOs.UserRelated;
using GlobalVariables.Encription;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace AdminPanel.Controllers
{
	[AdminsAuthenticationFilter]
	public class UsersController : Controller
	{
        private readonly ILogger<UsersController> _logger;
        private readonly IWebHostEnvironment webHostEnvironment;
        private IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        GetAllUsersResponse _getAllUsersResponse;
        GetUsersByIdResponse _getUsersByIdResponse;
        UpdateUsersResponse _updateUsersResponse;
        DeleteUsersResponse _deleteUsersResponse;

        private FitCookieAI_RequestBuilder _fitCookieAIRequestBuilder;
        private FitCookieAI_RequestExecutor _fitCookieAIRequestExecutor;

        string baseFitcookieAIUri;

        public UsersController(IWebHostEnvironment hostEnvironment, ILogger<UsersController> logger,
           IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

            _fitCookieAIRequestBuilder = new FitCookieAI_RequestBuilder();
            _fitCookieAIRequestExecutor = new FitCookieAI_RequestExecutor(_httpContextAccessor);

            _getAllUsersResponse = new GetAllUsersResponse();
            _getUsersByIdResponse = new GetUsersByIdResponse();
            _updateUsersResponse = new UpdateUsersResponse();
            _deleteUsersResponse = new DeleteUsersResponse();

            webHostEnvironment = hostEnvironment;
            _logger = logger;

            baseFitcookieAIUri = _configuration.GetValue<string>("FitCookieAI_API");
        }

        [HttpGet]
        public async Task<IActionResult> Index(string error, string message)
		{
            UsersIndexVM model = new UsersIndexVM();

            if (!string.IsNullOrEmpty(error))
            {
                model.Error = error;
            }
            if (!string.IsNullOrEmpty(message))
            {
                model.Message = message;
            }

            _getAllUsersResponse = await _fitCookieAIRequestExecutor.GetAllUsersAction(_fitCookieAIRequestBuilder.GetAllUsersRequestBuilder(baseFitcookieAIUri));

            if (_getAllUsersResponse.Code != null && int.Parse(_getAllUsersResponse.Code.ToString()) == 201)
            {
                model.Users = _getAllUsersResponse.Body;
            }

			return View(model);
		}

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Users", new { error = "User ID is missing!" });
            }

            UserDetailsVM model = new UserDetailsVM();

            _getUsersByIdResponse = await _fitCookieAIRequestExecutor.GetUsersByIdAction(_fitCookieAIRequestBuilder.GetUsersByIdRequestBuilder(baseFitcookieAIUri, id));

            if (_getUsersByIdResponse.Code != null && int.Parse(_getUsersByIdResponse.Code.ToString()) == 201)
            {
                model.User = _getUsersByIdResponse.Body;
            }
            else
            {
                model.Error = "User not found!";
            }

			return View(model);
		}

        [HttpGet]
        public async Task<IActionResult> Edit(int id, string error, string message)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Users", new { error = "User ID is missing!" });
            }

            UsersEditVM model = new UsersEditVM();

            if (!string.IsNullOrEmpty(error))
            {
                model.Error = error;
            }
            if (!string.IsNullOrEmpty(message))
            {
                model.Message = message;
            }

            _getUsersByIdResponse = await _fitCookieAIRequestExecutor.GetUsersByIdAction(
                _fitCookieAIRequestBuilder.GetUsersByIdRequestBuilder(baseFitcookieAIUri, id));

            if (_getUsersByIdResponse.Code != null && int.Parse(_getUsersByIdResponse.Code.ToString()) == 201)
            {
                model.Id = _getUsersByIdResponse.Body.Id;
                model.FirstName = _getUsersByIdResponse.Body.FirstName;
                model.LastName = _getUsersByIdResponse.Body.LastName;
                model.Email = _getUsersByIdResponse.Body.Email;
                model.Password = StringCipher.Decrypt(_getUsersByIdResponse.Body.Password, EncriptionVariables.PasswordEncriptionKey);
                model.DOB = _getUsersByIdResponse.Body.DOB;
                model.Gender = _getUsersByIdResponse.Body.Gender;
                model.PhoneNumber = _getUsersByIdResponse.Body.PhoneNumber;

                return View(model);
            }

            return RedirectToAction("Index", "Home", new { error = "User not found!" });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UsersEditVM model)
        {
            if (model.DOB == DateTime.MinValue || string.IsNullOrEmpty(model.FirstName) || string.IsNullOrEmpty(model.LastName)
                || string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.Email) || model.Id == 0)
            {
                return RedirectToAction("Edit", "Users", new { error = "Incorrect data, one or multiple required fields are left empty!" });
            }
            try
            {
                if (!string.IsNullOrEmpty(model.Email))
                {
                    MailAddress m = new MailAddress(model.Email);
                }
            }
            catch (FormatException)
            {
                ModelState.AddModelError("Email-format", "Email is not in correct format.");
                return View(model);
            }

            UserDTO User = new UserDTO();

            _getUsersByIdResponse = await _fitCookieAIRequestExecutor.GetUsersByIdAction(
                _fitCookieAIRequestBuilder.GetUsersByIdRequestBuilder(baseFitcookieAIUri, model.Id));

            if (_getUsersByIdResponse.Code != null && int.Parse(_getUsersByIdResponse.Code.ToString()) == 201)
            {
                User = _getUsersByIdResponse.Body;

                User.Password = model.Password;
                User.Email = model.Email;
                User.FirstName = model.FirstName;
                User.LastName = model.LastName;
                User.DOB = model.DOB;
                User.Gender = model.Gender;
                User.PhoneNumber = model.PhoneNumber;
            }
            else
            {
                return RedirectToAction("Edit", "Users", new { error = "Something went wrong, User could not be found!", id = model.Id });
            }

            _updateUsersResponse = await _fitCookieAIRequestExecutor.UpdateUsersAction(User, _fitCookieAIRequestBuilder.UpdateUsersRequestBuilder(baseFitcookieAIUri));

            if (_updateUsersResponse.Code != null && int.Parse(_updateUsersResponse.Code.ToString()) == 201)
            {
                return RedirectToAction("Edit", "Users", new { message = "Changes have been saved!", id = model.Id });
            }
            else
            {
                return RedirectToAction("Edit", "Users", new { error = "Something went wrong, changes have not been saved!", id = model.Id });
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) 
            {
                return RedirectToAction("Index", "Users", new { error = "User ID is missing!" });
            }

            _deleteUsersResponse = await _fitCookieAIRequestExecutor.DeleteUsersAction(_fitCookieAIRequestBuilder.DeleteUsersByIdRequestBuilder(baseFitcookieAIUri, id));

            if (_deleteUsersResponse.Code != null && int.Parse(_deleteUsersResponse.Code.ToString()) == 201)
            {
                return RedirectToAction("Index", "Users", new { message = "User has been deleted!"});
            }
            else
            {
                return RedirectToAction("Index", "Users", new { error = "Something went wrong, user hasn't been deleted!" });
            }
        }
    }
}
