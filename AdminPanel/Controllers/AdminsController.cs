using AdminPanel.ActionFilters;
using AdminPanel.Models.Admins;
using AdminPanel.RestComunication.FitCookieAI;
using AdminPanel.RestComunication.FitCookieAI.Responses.Admins;
using AdminPanel.RestComunication.FitCookieAI.Responses.AdminStatuses;
using FitCookieAI_ApplicationService.DTOs.AdminRelated;
using GlobalVariables.Encription;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.Net.Mail;

namespace AdminPanel.Controllers
{
	[AdminsAuthenticationFilter]
	public class AdminsController : Controller
	{
        private readonly ILogger<AdminsController> _logger;
        private readonly IWebHostEnvironment webHostEnvironment;
        private IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private GetAdminsByIdResponse _getAdminsByIdResponse;
		private UpdateAdminsResponse _updateAdminsResponse;
		private GetAllAdminsResponse _getAllAdminsResponse;
        private DeleteAdminsResponse _deleteAdminsResponse;

		private GetAdminStatusesByIdResponse _getAdminStatusesByIdResponse;
		private GetAllAdminStatusesResponse _getAllAdminStatusesResponse;

        private FitCookieAI_RequestBuilder _fitCookieAIRequestBuilder;
        private FitCookieAI_RequestExecutor _fitCookieAIRequestExecutor;

        string baseFitcookieAIUri;

        public AdminsController(IWebHostEnvironment hostEnvironment, ILogger<AdminsController> logger, 
			IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
		{
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

            _fitCookieAIRequestBuilder = new FitCookieAI_RequestBuilder();
            _fitCookieAIRequestExecutor = new FitCookieAI_RequestExecutor(_httpContextAccessor);

			_getAdminsByIdResponse = new GetAdminsByIdResponse();
			_updateAdminsResponse = new UpdateAdminsResponse();
			_getAllAdminsResponse = new GetAllAdminsResponse();
            _deleteAdminsResponse = new DeleteAdminsResponse();

			_getAdminsByIdResponse = new GetAdminsByIdResponse();
			_getAllAdminStatusesResponse = new GetAllAdminStatusesResponse();

            webHostEnvironment = hostEnvironment;
            _logger = logger;

            baseFitcookieAIUri = _configuration.GetValue<string>("FitCookieAI_API");
        }

		[HttpGet]
		public async Task<IActionResult> Index(string error, string message)
		{
			AdminsIndexVM model = new AdminsIndexVM();

            if (!string.IsNullOrEmpty(error))
            {
                model.Error = error;
            }
            if (!string.IsNullOrEmpty(message))
            {
                model.Message = message;
            }

			_getAllAdminsResponse = await _fitCookieAIRequestExecutor.GetAllAdminsAction(_fitCookieAIRequestBuilder.GetAllAdminsRequestBuilder(baseFitcookieAIUri));

			if (_getAllAdminsResponse != null && int.Parse(_getAllAdminsResponse.Code.ToString()) == 201)
			{
				_getAllAdminStatusesResponse = await _fitCookieAIRequestExecutor.GetAllAdminStatusesAction(
					_fitCookieAIRequestBuilder.GetAllAdminStatusesRequestBuilder(baseFitcookieAIUri));

				if (_getAllAdminStatusesResponse.Code != null && int.Parse(_getAllAdminStatusesResponse.Code.ToString()) == 201)
				{
					model.AdminStatuses = _getAllAdminStatusesResponse.Body;
				}

				model.Admins = _getAllAdminsResponse.Body;
			}

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Profile(string error, string message)
		{
			ProfileVM model = new ProfileVM();

			if (!string.IsNullOrEmpty(error))
			{
				model.Error = error;
			}
			if (!string.IsNullOrEmpty(message))
			{
				model.Message = message;
			}

			_getAdminsByIdResponse = await _fitCookieAIRequestExecutor.GetAdminsByIdAction(
				_fitCookieAIRequestBuilder.GetAdminsByIdRequestBuilder(baseFitcookieAIUri, (int)this.HttpContext.Session.GetInt32("Id")));

			if (_getAdminsByIdResponse.Code != null && int.Parse(_getAdminsByIdResponse.Code.ToString()) == 201)
			{
				model.FirstName = _getAdminsByIdResponse.Body.FirstName;
				model.LastName = _getAdminsByIdResponse.Body.LastName;
				model.Email = _getAdminsByIdResponse.Body.Email;
				model.Password = StringCipher.Decrypt(_getAdminsByIdResponse.Body.Password, EncriptionVariables.PasswordEncriptionKey);
				model.ProfilePhotoName = _getAdminsByIdResponse.Body.ProfilePhotoName;
				model.DOB = _getAdminsByIdResponse.Body.DOB;
				model.Gender = _getAdminsByIdResponse.Body.Gender;

                return View(model);	
			}

            return RedirectToAction("Index", "Home", new { error = "Admin not found!"});
        }

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			if (id == 0)
			{
                return RedirectToAction("Index", "Admins", new { error = "Admin ID is missing!" });
            }

            AdminDetailsVM model = new AdminDetailsVM();

            _getAdminsByIdResponse = await _fitCookieAIRequestExecutor.GetAdminsByIdAction(
                _fitCookieAIRequestBuilder.GetAdminsByIdRequestBuilder(baseFitcookieAIUri, id));

            if (_getAdminsByIdResponse.Code != null && int.Parse(_getAdminsByIdResponse.Code.ToString()) == 201)
			{
				_getAdminStatusesByIdResponse = await _fitCookieAIRequestExecutor.GetAdminStatusesByIdAction(
					_fitCookieAIRequestBuilder.GetAdminStatusesByIdRequestBuilder(baseFitcookieAIUri, _getAdminsByIdResponse.Body.Id));

				if (_getAdminStatusesByIdResponse.Code != null && int.Parse(_getAdminStatusesByIdResponse.Code.ToString()) == 201)
				{
					model.AdminStatus = _getAdminStatusesByIdResponse.Body;
                }

				model.Admin = _getAdminsByIdResponse.Body;
			}
			else
			{
				model.Error = "Admin not found!";
            }

			return View(model);
        }

		[AdminsDeveloperStatusFilter]
		[HttpGet]
		public async Task<IActionResult> Edit(int id, string error, string message)
		{
            if (id == 0)
            {
                return RedirectToAction("Index", "Admins", new { error = "Admin ID is missing!" });
            }

			AdminsEditVM model = new AdminsEditVM();

            if (!string.IsNullOrEmpty(error))
            {
                model.Error = error;
            }
            if (!string.IsNullOrEmpty(message))
            {
                model.Message = message;
            }

            _getAdminsByIdResponse = await _fitCookieAIRequestExecutor.GetAdminsByIdAction(
                _fitCookieAIRequestBuilder.GetAdminsByIdRequestBuilder(baseFitcookieAIUri, id));

            if (_getAdminsByIdResponse.Code != null && int.Parse(_getAdminsByIdResponse.Code.ToString()) == 201)
            {
                model.Id = _getAdminsByIdResponse.Body.Id;
                model.FirstName = _getAdminsByIdResponse.Body.FirstName;
                model.LastName = _getAdminsByIdResponse.Body.LastName;
                model.Email = _getAdminsByIdResponse.Body.Email;
                model.Password = StringCipher.Decrypt(_getAdminsByIdResponse.Body.Password, EncriptionVariables.PasswordEncriptionKey);
                model.ProfilePhotoName = _getAdminsByIdResponse.Body.ProfilePhotoName;
                model.DOB = _getAdminsByIdResponse.Body.DOB;
                model.Gender = _getAdminsByIdResponse.Body.Gender;
				model.StatusId = _getAdminsByIdResponse.Body.StatusId;

                _getAllAdminStatusesResponse = await _fitCookieAIRequestExecutor.GetAllAdminStatusesAction(
					_fitCookieAIRequestBuilder.GetAllAdminStatusesRequestBuilder(baseFitcookieAIUri));

                if (_getAllAdminStatusesResponse.Code != null && int.Parse(_getAllAdminStatusesResponse.Code.ToString()) == 201)
                {
                    model.AdminStatuses = _getAllAdminStatusesResponse.Body;
                }

                return View(model);
            }

            return RedirectToAction("Index", "Home", new { error = "Admin not found!" });
        }

        [AdminsDeveloperStatusFilter]
		[HttpPost]
		public async Task<IActionResult> Edit(AdminsEditVM model)
		{
            if (model.DOB == DateTime.MinValue || string.IsNullOrEmpty(model.FirstName) || string.IsNullOrEmpty(model.LastName)
                || string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.Email) || model.Id == 0)
            {
                return RedirectToAction("Profile", "Admins", new { error = "Incorrect data, one or multiple required fields are left empty!" });
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

            AdminDTO admin = new AdminDTO();

            _getAdminsByIdResponse = await _fitCookieAIRequestExecutor.GetAdminsByIdAction(
                _fitCookieAIRequestBuilder.GetAdminsByIdRequestBuilder(baseFitcookieAIUri, model.Id));

            if (_getAdminsByIdResponse.Code != null && int.Parse(_getAdminsByIdResponse.Code.ToString()) == 201)
            {
                admin = _getAdminsByIdResponse.Body;

                admin.Password = model.Password;
                admin.Email = model.Email;
                admin.FirstName = model.FirstName;
                admin.LastName = model.LastName;
                admin.DOB = model.DOB;
                admin.Gender = model.Gender;
                admin.StatusId= model.StatusId;
            }
            else
            {
                return RedirectToAction("Edit", "Admins", new { error = "Something went wrong, Admin could not be found!", id = model.Id });
            }

            _updateAdminsResponse = await _fitCookieAIRequestExecutor.UpdateAdminsAction(admin, _fitCookieAIRequestBuilder.UpdateAdminsRequestBuilder(baseFitcookieAIUri));

            if (_updateAdminsResponse.Code != null && int.Parse(_updateAdminsResponse.Code.ToString()) == 201)
            {
                return RedirectToAction("Edit", "Admins", new { message = "Changes have been saved!", id = model.Id });
            }
            else
            {
                return RedirectToAction("Edit", "Admins", new { error = "Something went wrong, changes have not been saved!", id = model.Id });
            }
        }

        [HttpPost]
		public async Task<IActionResult> Update(ProfileVM model)
		{
			if (model.DOB == DateTime.MinValue || string.IsNullOrEmpty(model.FirstName) || string.IsNullOrEmpty(model.LastName)
				|| string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.Email))
			{
                return RedirectToAction("Profile", "Admins", new { error = "Incorrect data, one or multiple required fields are left empty!" });
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

            AdminDTO admin = new AdminDTO();

            _getAdminsByIdResponse = await _fitCookieAIRequestExecutor.GetAdminsByIdAction(
				_fitCookieAIRequestBuilder.GetAdminsByIdRequestBuilder(baseFitcookieAIUri, (int)this.HttpContext.Session.GetInt32("Id")));

			if (_getAdminsByIdResponse.Code != null && int.Parse(_getAdminsByIdResponse.Code.ToString()) == 201)
			{
				admin = _getAdminsByIdResponse.Body;
				if (StringCipher.Decrypt(admin.Password, EncriptionVariables.PasswordEncriptionKey) != model.Password)
				{
                    return RedirectToAction("Profile", "Admins", new { error = "Incorrect password, can't perform update!" });
                }

                if (!string.IsNullOrEmpty(model.NewPassword))
                {
                    model.Password = model.NewPassword;
                }

				admin.Password = model.Password;
                admin.Email = model.Email;
                admin.FirstName = model.FirstName;
				admin.LastName = model.LastName;
				admin.DOB = model.DOB;
				admin.Gender= model.Gender;
			}
			else
			{
                return RedirectToAction("Profile", "Admins", new { error = "Something went wrong, Admin could not be found!" });
            }

            _updateAdminsResponse = await _fitCookieAIRequestExecutor.UpdateAdminsAction(admin, _fitCookieAIRequestBuilder.UpdateAdminsRequestBuilder(baseFitcookieAIUri));
			
			if (_updateAdminsResponse.Code != null && int.Parse(_updateAdminsResponse.Code.ToString()) == 201)
			{
				return RedirectToAction("Profile", "Admins", new {message = "Changes have been saved!"});
			}
			else
			{
                return RedirectToAction("Profile", "Admins", new { error = "Something went wrong, changes have not been saved!" });
            }
        }

        [HttpPost]
		async Task<IActionResult> EditAdminPhotoAction(AdminsEditVM model)
        {
			string stringFileName = EditFile(model);

			AdminDTO admin = new AdminDTO();

			if (!string.IsNullOrEmpty(stringFileName) || model.Id != 0)
			{
				_getAdminsByIdResponse = await _fitCookieAIRequestExecutor.GetAdminsByIdAction(
					_fitCookieAIRequestBuilder.GetAdminsByIdRequestBuilder(baseFitcookieAIUri, model.Id));

				if (_getAdminsByIdResponse.Code != null && int.Parse(_getAdminsByIdResponse.Code.ToString()) == 201)
				{
					admin = _getAdminsByIdResponse.Body;

					var pathOldImage = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\DASHMIN\Admins\ProfilePhotos", admin.ProfilePhotoName);
					if (System.IO.File.Exists(pathOldImage))
					{
						System.IO.File.Delete(pathOldImage);
					}

					admin.Password = StringCipher.Decrypt(_getAdminsByIdResponse.Body.Password, EncriptionVariables.PasswordEncriptionKey);
					admin.ProfilePhotoName = stringFileName;

					_updateAdminsResponse = await _fitCookieAIRequestExecutor.UpdateAdminsAction(admin, _fitCookieAIRequestBuilder.UpdateAdminsRequestBuilder(baseFitcookieAIUri));

					if (_updateAdminsResponse.Code != null && int.Parse(_updateAdminsResponse.Code.ToString()) == 201)
					{
						return RedirectToAction("Edit", "Admins", new { message = "Changes have been saved!" });
					}
					else
					{
						return RedirectToAction("Edit", "Admins", new { error = "Something went wrong, changes have not been saved!" });
					}
				}
				else
				{
					return RedirectToAction("Edit", "Admins", new { error = "Something went wrong, Admin could not be found!" });
				}
			}
			else
			{
				return RedirectToAction("Edit", "Admins", new { error = "Something went wrong, file couldn't be uploaded!" });
			}
		}

        [AdminsDeveloperStatusFilter]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Admins", new { error = "Admin ID is missing!" });
            }

            _deleteAdminsResponse = await _fitCookieAIRequestExecutor.DeleteAdminsAction(_fitCookieAIRequestBuilder.DeleteAdminsByIdRequestBuilder(baseFitcookieAIUri, id));

            if (_deleteAdminsResponse.Code != null && int.Parse(_deleteAdminsResponse.Code.ToString()) == 201)
            {
                return RedirectToAction("Index", "Admins", new { message = "Admin has been deleted!" });
            }
            else
            {
                return RedirectToAction("Index", "Admins", new { error = "Something went wrong, Admin hasn't been deleted!" });
            }
        }

        [HttpPost]
		public async Task<IActionResult> UploadFileAction(ProfileVM model)
		{
            string stringFileName = UploadFile(model);

			AdminDTO admin = new AdminDTO();

			if (!string.IsNullOrEmpty(stringFileName))
			{
                _getAdminsByIdResponse = await _fitCookieAIRequestExecutor.GetAdminsByIdAction(
					_fitCookieAIRequestBuilder.GetAdminsByIdRequestBuilder(baseFitcookieAIUri, (int)this.HttpContext.Session.GetInt32("Id")));

                if (_getAdminsByIdResponse.Code != null && int.Parse(_getAdminsByIdResponse.Code.ToString()) == 201)
                {
                    admin = _getAdminsByIdResponse.Body;

                    var pathOldImage = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\DASHMIN\Admins\ProfilePhotos", admin.ProfilePhotoName);
                    if (System.IO.File.Exists(pathOldImage))
                    {
                        System.IO.File.Delete(pathOldImage);
                    }

                    admin.Password = StringCipher.Decrypt(_getAdminsByIdResponse.Body.Password, EncriptionVariables.PasswordEncriptionKey);
                    admin.ProfilePhotoName = stringFileName;

                    _updateAdminsResponse = await _fitCookieAIRequestExecutor.UpdateAdminsAction(admin, _fitCookieAIRequestBuilder.UpdateAdminsRequestBuilder(baseFitcookieAIUri));

                    if (_updateAdminsResponse.Code != null && int.Parse(_updateAdminsResponse.Code.ToString()) == 201)
                    {
                        return RedirectToAction("Profile", "Admins", new { message = "Changes have been saved!" });
                    }
                    else
                    {
                        return RedirectToAction("Profile", "Admins", new { error = "Something went wrong, changes have not been saved!" });
                    }
                }
				else
				{
                    return RedirectToAction("Profile", "Admins", new { error = "Something went wrong, Admin could not be found!" });
                }
            }
			else
			{
                return RedirectToAction("Profile", "Admins", new { error = "Something went wrong, file couldn't be uploaded!" });
            }
        }


        public string EditFile(AdminsEditVM model)
        {
			string fileName = null;
			if (model.FileName != null)
			{
				string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "DASHMIN", "Admins", "ProfilePhotos");
				fileName = Guid.NewGuid().ToString() + "-" + model.FileName.FileName;
				string filePath = Path.Combine(uploadDir, fileName);
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					model.FileName.CopyTo(fileStream);
				}
			}

			return fileName;
		}

        private string UploadFile(ProfileVM model)
		{
			string fileName = null;
			if (model.FileName != null)
			{
                string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "DASHMIN", "Admins", "ProfilePhotos");
				fileName = Guid.NewGuid().ToString() + "-" + model.FileName.FileName;
				string filePath = Path.Combine(uploadDir, fileName);
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					model.FileName.CopyTo(fileStream);
				}
			}

			return fileName;
		}
	}
}
