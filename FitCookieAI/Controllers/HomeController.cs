using DocuSign.eSign.Model;
using FitCookieAI.Models;
using FitCookieAI.RestComunication.FitCookieAI;
using FitCookieAI.RestComunication.FitCookieAI.Responses.PaymentRelated.Payments;
using FitCookieAI.RestComunication.FitCookieAI.Responses.UserRelated;
using FitCookieAI.RestComunication.GPT;
using FitCookieAI.RestComunication.GPT.Responses;
using FitCookieAI.Test;
using FitCookieAI_ApplicationService.DTOs.Others;
using FitCookieAI_ApplicationService.DTOs.UserRelated;
using FitCookieAI_Data.Entities.UserRelated;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Mail;
using static FitCookieAI.Test.BaseGPTTestRequest;
using System;
using FitCookieAI.RestComunication.FitCookieAI.Responses.PasswordRecoveryTokens;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;
using FitCookieAI.RestComunication.FitCookieAI.Responses.GeneratedPlans;
using System.Drawing;

namespace FitCookieAI.Controllers
{
    public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IConfiguration _config;

		private GetGPTResponse _GPTResponse;
		private LoginUserResponse _loginUserResponse;
		private SignUpUserResponse _signUpUserResponse;
		private LogoutUserResponse _logoutUserResponse;

		private GPT_Test_Service _service;

		private GPT_RequestBuilder _GPTRequestBuilder;
		private GPT_RequestExecutor _GPTRequestExecutor;
		private FitCookieAI_RequestBuilder _fitCookieAIRequestBuilder;
		private FitCookieAI_RequestExecutor _fitCookieAIRequestExecutor;
		private ChargePaymentResponse _chargePaymentResponse;
		private VerifyUserByEmailResponse _verifyUserByEmailResponse;
		private SavePasswordRecoveryTokensResponse _savePasswordRecoveryTokensResponse;
		private GetAllPasswordRecoveryTokensResponse _getAllPasswordRecoveryTokensResponse;
		private EditUserPasswordResponse _editUserPasswordResponse;
		private SaveGeneratedPlansResponse _saveGeneratedPlansResponse;

		string baseGPTUri;
		string baseFitcookieAIUri;
		string stripePublicKey;

		private IConfiguration _configuration;
		private readonly IHttpContextAccessor _httpContextAccessor;

        private static readonly Random random = new Random();
        private const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IConfiguration config)
		{
			_configuration = configuration;
			_httpContextAccessor = httpContextAccessor;

			_GPTRequestBuilder= new GPT_RequestBuilder();
			_GPTRequestExecutor = new GPT_RequestExecutor(_httpContextAccessor);
			_fitCookieAIRequestBuilder = new FitCookieAI_RequestBuilder();
			_fitCookieAIRequestExecutor = new FitCookieAI_RequestExecutor(_httpContextAccessor);

			_logger = logger;
			_GPTResponse = new GetGPTResponse();
			_loginUserResponse = new LoginUserResponse();
			_signUpUserResponse	= new SignUpUserResponse();
			_logoutUserResponse= new LogoutUserResponse();
			_chargePaymentResponse = new ChargePaymentResponse();
			_verifyUserByEmailResponse = new VerifyUserByEmailResponse();
			_savePasswordRecoveryTokensResponse = new SavePasswordRecoveryTokensResponse();
			_getAllPasswordRecoveryTokensResponse = new GetAllPasswordRecoveryTokensResponse();
			_editUserPasswordResponse = new EditUserPasswordResponse();
			_saveGeneratedPlansResponse = new SaveGeneratedPlansResponse();

			_config= config;
			_service = new GPT_Test_Service(_config);
			
			baseGPTUri = _configuration.GetValue<string>("GPT_API");
			baseFitcookieAIUri = _configuration.GetValue<string>("FitCookieAI_API");
			stripePublicKey = _configuration.GetSection("Stripe")["PublicKey"];
		}

		public IActionResult Index()
		{
			this.HttpContext.Session.SetString("PublicKey", stripePublicKey);

			return View();
		}

        [HttpPost]
        public async Task<JsonResult> Login(LoginVM model)
		{
			if (!ModelState.IsValid)
			{
				return Json(new {code = 400, message = "Login input is invalid (missing data)!" });
			}

			_loginUserResponse = await _fitCookieAIRequestExecutor.LoginAction(_fitCookieAIRequestBuilder.LoginRequestBuilder(baseFitcookieAIUri, model.Email, model.Password));

			if (_loginUserResponse.Code != null && int.Parse(_loginUserResponse.Code.ToString()) == 201)
			{
				this.HttpContext.Session.SetString("Email", _loginUserResponse.Body.Email);
				this.HttpContext.Session.SetInt32("Id", _loginUserResponse.Body.Id);
				this.HttpContext.Session.SetString("FirstName", _loginUserResponse.Body.FirstName);
				this.HttpContext.Session.SetString("LastName", _loginUserResponse.Body.LastName);

				return Json(new { code = 201, message = "SLogin was successful!", names = _loginUserResponse.Body.FirstName + " " + _loginUserResponse.Body.LastName });
			}

			return Json(new { code = 200, message = "A user with this set of credentials doesn't exist, please check your" });
		}

        [HttpPost]
        public async Task<JsonResult> SignUp(SignUpVM model)
		{
            string pattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}$";
            Regex regex = new Regex(pattern);
			/*
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.FirstName) || string.IsNullOrEmpty(model.LastName) ||
				string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.ConfirmPassword) || string.IsNullOrEmpty(model.Gender) ||
				model.DOB == DateTime.MinValue || string.IsNullOrEmpty(model.PhoneNumber))
			{
				return Json(new { code = 400, message = "SignUp input is invalid (missing data)!" });
			}
			*/
			if (string.IsNullOrEmpty(model.Email))
			{
				return Json(new { code = 400, message = "Sign Up input is invalid Email is missing!" });
			}
			if (string.IsNullOrEmpty(model.FirstName))
			{
				return Json(new { code = 400, message = "Sign Up input is invalid First Name is missing!" });
			}
			if (string.IsNullOrEmpty(model.LastName))
			{
				return Json(new { code = 400, message = "Sign Up input is invalid Last Name is missing!" });
			}
			if (
				string.IsNullOrEmpty(model.Password))
			{
				return Json(new { code = 400, message = "Sign Up input is invalid Password is missing!" });
			}
			if (string.IsNullOrEmpty(model.ConfirmPassword))
			{
				return Json(new { code = 400, message = "Sign Up input is invalid Confirm Pasword is missing!" });
			}
			if (string.IsNullOrEmpty(model.Gender))
			{
				return Json(new { code = 400, message = "Sign Up input is invalid (Gender is missing!" });
			}
			if (model.DOB == DateTime.MinValue)
			{
				return Json(new { code = 400, message = "Sign Up input is invalid DOB is missing!" });
			}
			if (string.IsNullOrEmpty(model.PhoneNumber))
			{
				return Json(new { code = 400, message = "Sign Up input is invalid Phone number is missing!" });
			}
			if (model.Password != model.ConfirmPassword)
			{
				return Json(new { code = 400, message = "SignUp input is invalid (password != confirm pasword)!" });
			}
            if (!regex.IsMatch(model.Password) || model.Password.Length < 6 || model.Password.Length > 20)
            {
                return Json(new { code = 400, message = "SignUp input is invalid password must contain at least one number and one uppercase and lowercase letter, and at least 8 or more characters!" });
            }

            UserDTO user = new UserDTO
			{
				Email= model.Email,
				FirstName= model.FirstName,
				LastName= model.LastName,
				Password= model.Password,
				Gender= model.Gender,
				DOB= model.DOB,
				PhoneNumber = model.PhoneNumber
			};

			_signUpUserResponse = await _fitCookieAIRequestExecutor.SignUpAction(user, _fitCookieAIRequestBuilder.SignUpRequestBuilder(baseFitcookieAIUri));

			if (_signUpUserResponse.Code != null && int.Parse(_signUpUserResponse.Code.ToString()) == 201)
			{
				_loginUserResponse = await _fitCookieAIRequestExecutor.LoginAction(_fitCookieAIRequestBuilder.LoginRequestBuilder(baseFitcookieAIUri, model.Email, model.Password));

				if (_loginUserResponse.Code != null && int.Parse(_loginUserResponse.Code.ToString()) == 201)
				{
					this.HttpContext.Session.SetString("Email", _loginUserResponse.Body.Email);
					this.HttpContext.Session.SetInt32("Id", _loginUserResponse.Body.Id);
					this.HttpContext.Session.SetString("FirstName", _loginUserResponse.Body.FirstName);
					this.HttpContext.Session.SetString("LastName", _loginUserResponse.Body.LastName);

					return Json(new { code = 201, message = "SignUp and Login were successful!", names = _loginUserResponse.Body.FirstName + " " + _loginUserResponse.Body.LastName });
				}

				return Json(new { code = 200, message = "SignUp was successful but Login failed!" });
			}

			return Json(new { code = 200, message = "SignUp failed!" });
		}

		[HttpPost]
        public async Task<JsonResult> GenerateShoppingBasket(string input)
		{
            if (string.IsNullOrEmpty(this.HttpContext.Session.GetString("Email")))
            {
                return Json(new { code = 400, message = "User is not authenticated!" });
            }

            if (string.IsNullOrEmpty(input))
            {
                return Json(new { code = 400, message = "Input data was invalid!" });
            }

			string resultHeader = "<div class=\"text-center\">\r\n\t\t\t\t" +
				"<h5 class=\"text-primary-gradient fw-medium\">Products</h5>\r\n\t\t\t\t" +
				"<h1 class=\"mb-5\">Your shopping basket!</h1>\r\n\t\t\t</div>";

			string GPT_Input = $"Please examine the following list of ingredients: {input}. For each ingredient," +
				$" identify the fundamental grocery item that needs to be purchased from a supermarket. " +
				$"Please exclude any preparation methods (like 'steamed', 'diced', 'grilled' etc.) and convert the quantities" +
				$" to either grams or kilograms. Present the results in an HTML table with an id of 'shopping_basket_table'," +
				$" containing columns for 'Product' and 'Quantity'.";

			string result = "";

            string requestQuery = _GPTRequestBuilder.PostGPTINputRequestBuilder(baseGPTUri, GPT_Input);
            _GPTResponse = await _GPTRequestExecutor.GetGPTResponseAction(requestQuery);

            if (_GPTResponse.Code != null && int.Parse(_GPTResponse.Code.ToString()) == 201)
            {
				result += resultHeader;
                result += _GPTResponse.choices[0].text + "</br>";
            }

            if (!string.IsNullOrEmpty(result))
            {
                GeneratedPlanDTO generatedPlan = new GeneratedPlanDTO
                {
                    Plan = "Shopping basket",
                    CreatedAt = DateTime.Now,
                    UserId = (int)this.HttpContext.Session.GetInt32("Id")
                };
                _saveGeneratedPlansResponse = await _fitCookieAIRequestExecutor.SaveGeneratedPlansAction(generatedPlan,
                    _fitCookieAIRequestBuilder.SaveGeneratedPlansRequestBuilder(baseFitcookieAIUri));

                return Json(result);
            }
            else
            {
                return Json(new { code = int.Parse(_GPTResponse.Code.ToString()), message = _GPTResponse.Error.ToString() });
            }
        }

        [HttpPost]
        public async Task<JsonResult> SubmitInput(SubmitInputVM model)
		{
			if (string.IsNullOrEmpty(this.HttpContext.Session.GetString("Email")))
			{
                return Json(new { code = 400, message = "User is not authenticated!" });
            }

			if (model.Height == 0 || model.Weight == 0 || model.TargetWeight == 0 || string.IsNullOrEmpty(model.Ocupation) ||
				string.IsNullOrEmpty(model.BMI) || string.IsNullOrEmpty(model.HealthGoal) || string.IsNullOrEmpty(model.ActivityLevel) ||
                string.IsNullOrEmpty(model.Ocupation) || (!string.IsNullOrEmpty(model.DietaryRestrictions) && model.DietaryRestrictions.Length > 150) 
				|| (!string.IsNullOrEmpty(model.FoodPreferences) && model.FoodPreferences.Length > 150))
			{
				return Json(new { code = 400,  message = "Input data was invalid!" });
            }

			string dietaryRestrictions = "";
			string foodPreferences = "";
			if (!string.IsNullOrEmpty(model.DietaryRestrictions))
			{
				dietaryRestrictions = $"My dietary restrictions/preferences are: {model.DietaryRestrictions}, ";
			}
			if (!string.IsNullOrEmpty(model.FoodPreferences))
			{
				foodPreferences = $"My food preferences are: {model.FoodPreferences}, ";
			}

			TimeSpan age = DateTime.Now.Subtract(model.DOB);
			int years = (int)(age.TotalDays / 365.25);

			string result = "";

			string input1 = $"As a profesional dietitian recomend me a diverse diet plan with a large variety of delicious meals meals which must be different for every day of the week, " +
				$"I am a {model.Gender}, {years} years old, I weigh {model.Weight} kilograms, my target weight is {model.TargetWeight} kilograms, my height is {model.Height} meters, my BMI is {model.BMI}, my activity level is {model.ActivityLevel}" +
				dietaryRestrictions + foodPreferences + $", my healt goal is to {model.HealthGoal}, and my oocupations is {model.Ocupation}. " +
				$"Return the result as a HTML table with id=diet_plan_table in html format with colums: day of the week and meals(here you should include the quantity of each part of the meal in grams)";

			string input2 = $"As a profesional dietitian recomend me a list of supplements, how much of them should i take per day and briefly explain the benefits of each of them, " +
				$"(strenght building and health related) which would help me based on my needs in html format as a html table with id=supplements_table and with collumns Supplement, Description and Dose per day. " 
				+ $"I am a {model.Gender}, {years} years old, I weigh {model.Weight} kilograms, my target weight is {model.TargetWeight} kilograms, my height is {model.Height} meters, my BMI is {model.BMI}, my activity level is {model.ActivityLevel}" +
				dietaryRestrictions + foodPreferences + $", my healt goal is to {model.HealthGoal}, and my oocupations is {model.Ocupation}.";
			/*
			BaseGPTTestRequest request = new BaseGPTTestRequest();
			BaseGPTTestResponse response = new BaseGPTTestResponse();

			request.model = _config.GetValue<string>("OpenAI_API_Request_Model");

			Message message1 = new Message
			{
				role = "assistant",
				content = "You are a profesional Dietitian specialized in creating custom diet plans."
			};

			Message message2 = new Message
			{
				role = "user",
				content = input
			};

			request.messages = new List<Message> { message1, message2 };

			request.max_tokens = 8000;
			request.temperature = 0;

			using (var httpClient = new HttpClient())
			{
				response = await _service.GetGPTResponse(httpClient, request);
			}
			*/

			string supplementsHeader = "<div class=\"text-center\">\r\n\t\t\t\t" +
				"<h5 class=\"text-primary-gradient fw-medium\">Supplements</h5>\r\n\t\t\t\t" +
				"<h1 class=\"mb-5\">Your recomended supplements!</h1>\r\n\t\t\t</div>";

			string shoppingBasketLoadingContainer = "<div id=\"loading-container_shopping_basket\" class=\"row py-5\">\r\n\t" +
				"<div class=\"container py-5 px-lg-5\">\r\n\t\t<div class=\"row justify-content-center text-center\">\r\n\t\t\t" +
				"<div class=\"col-lg-12 justify-content-center\">\r\n\t\t\t\t<div class=\"justify-content-center\">\r\n\t\t\t\t\t" +
				"<h5 class=\"required\">Your shopping basket is being generated!</h5>\r\n\t\t\t\t\t<p class=\"required\">" +
				"This could take between 30 to 60 seconds</p>\r\n\t\t\t\t</div>\r\n\t\t\t\t<div id=\"loading-animation\" class=\"justify-content-center\">\r\n\t\t\t\t\t" +
				"<img src=\"./img/fitcookieloading_gif.gif\" alt=\"Loading...\" />\r\n\t\t\t\t</div>\r\n\t\t\t</div>\r\n\t\t</div>\r\n\t</div>\r\n</div>";

            string requestQuery = _GPTRequestBuilder.PostGPTINputRequestBuilder(baseGPTUri, input1);
			_GPTResponse = await _GPTRequestExecutor.GetGPTResponseAction(requestQuery);

			if (_GPTResponse.Code != null && int.Parse(_GPTResponse.Code.ToString()) == 201)
			{
                result += _GPTResponse.choices[0].text + "</br>";
            }

            requestQuery = _GPTRequestBuilder.PostGPTINputRequestBuilder(baseGPTUri, input2);
            _GPTResponse = await _GPTRequestExecutor.GetGPTResponseAction(requestQuery);

            if (_GPTResponse.Code != null && int.Parse(_GPTResponse.Code.ToString()) == 201)
            {
				result += supplementsHeader;
                result += _GPTResponse.choices[0].text + "</br>";
            }

			if (!string.IsNullOrEmpty(result))
			{
				GeneratedPlanDTO generatedPlan = new GeneratedPlanDTO
				{
					Plan = "Diet Plan",
					CreatedAt = DateTime.Now,
					UserId = (int)this.HttpContext.Session.GetInt32("Id")
				};
				_saveGeneratedPlansResponse = await _fitCookieAIRequestExecutor.SaveGeneratedPlansAction(generatedPlan, 
					_fitCookieAIRequestBuilder.SaveGeneratedPlansRequestBuilder(baseFitcookieAIUri));

				result += shoppingBasketLoadingContainer + "</br>";

                return Json(result);
            }
			else
			{
                return Json(new { code = int.Parse(_GPTResponse.Code.ToString()), message = _GPTResponse.Error.ToString() });
            }
		}

		[HttpPost]
		public async Task<JsonResult> StripePayment(PaymentDTO paymentData)
		{
			if (paymentData != null)
			{
				paymentData.Currency = "usd";
				paymentData.UserId = (int)this.HttpContext.Session.GetInt32("Id");
				 
				_chargePaymentResponse = await _fitCookieAIRequestExecutor
					.ChargePaymentsAction(paymentData, _fitCookieAIRequestBuilder.ChargePaymentsRequestBuilder(baseFitcookieAIUri));

				if (_chargePaymentResponse.Code != null && int.Parse(_chargePaymentResponse.Code.ToString()) == 201)
				{
					return Json(new { status = 201, response = _chargePaymentResponse.Body });
				}
				else
				{
					return Json(new { status = int.Parse(_chargePaymentResponse.Code.ToString()), response = _chargePaymentResponse.Error });
				}
			}
			else
			{
				return Json(new {status = 400, response = new {message = "Input data was invalid!" } });
			}
		}

        [HttpGet]
        public async Task<IActionResult> RestorePasswordStep3()
        {
			if (this.HttpContext.Session.GetInt32("PasswordRecoveryTokenId") == null || this.HttpContext.Session.GetInt32("PasswordRecoveryTokenId") == 0)
			{
                return RedirectToAction("RestorePasswordStep2", "Home");
            }

            EditPasswordRestorePasswordVM model = new EditPasswordRestorePasswordVM();

            return View(model);
        }

		[HttpPost]
        public async Task<IActionResult> RestorePasswordStep3(EditPasswordRestorePasswordVM model)
		{
            if (this.HttpContext.Session.GetInt32("PasswordRecoveryTokenId") == null || this.HttpContext.Session.GetInt32("PasswordRecoveryTokenId") == 0)
            {
                return RedirectToAction("RestorePasswordStep2", "Home");
            }

            string pattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}$";
            Regex regex = new Regex(pattern);

            if (!ModelState.IsValid)
			{
				return View(model);
			}

			if (model.Password != model.ConfirmPassword)
			{
                ModelState.AddModelError("Password-error", "Password has to match Confirm Password!");
                return View(model);
            }

			if (!regex.IsMatch(model.Password) || model.Password.Length < 6 || model.Password.Length > 20)
			{
                ModelState.AddModelError("Password-error", "Password must contain at least one number and one uppercase and lowercase letter, and between 6 and 20 characters");
                return View(model);
            }

			_editUserPasswordResponse = await _fitCookieAIRequestExecutor.EditUserPasswordAction(
				_fitCookieAIRequestBuilder.EditUserPasswordRequestBuilder(
					baseFitcookieAIUri, model.Password, (int)this.HttpContext.Session.GetInt32("PasswordRecoveryTokenId")));

			if (_editUserPasswordResponse.Code != null && int.Parse(_editUserPasswordResponse.Code.ToString()) == 201)
			{
                this.HttpContext.Session.Remove("PasswordRecoveryTokenId");
				return RedirectToAction("Index", "Home");
            }
			else
			{
                ModelState.AddModelError("Password-error", "Something went wrong, server is nor responding!");
                return View(model);
            }
		}

        [HttpGet]
        public async Task<IActionResult> RestorePasswordStep2()
        {
            CheckCodeRestorePasswordVM model = new CheckCodeRestorePasswordVM();

            return View(model);
        }

		[HttpPost]
        public async Task<IActionResult> RestorePasswordStep2(CheckCodeRestorePasswordVM model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			_getAllPasswordRecoveryTokensResponse = await _fitCookieAIRequestExecutor.GetAllPasswordRecoveryTokensAction(
				_fitCookieAIRequestBuilder.GetAllPasswordRecoveryTokensRequestBuilder(baseFitcookieAIUri));

			if (_getAllPasswordRecoveryTokensResponse.Code != null && int.Parse(_getAllPasswordRecoveryTokensResponse.Code.ToString()) == 201)
			{
				PasswordRecoveryTokenDTO token = _getAllPasswordRecoveryTokensResponse.Body.Where(t => t.Code == model.Code).FirstOrDefault();

				if (token == null)
				{
                    ModelState.AddModelError("Code-error", "The code you've entered is incorrect or doesn't exist!");
                    return View(model);
                }
				else
				{
					if (DateTime.Now > token.Start && DateTime.Now < token.End)
					{
						this.HttpContext.Session.SetInt32("PasswordRecoveryTokenId", token.Id);
                        return RedirectToAction("RestorePasswordStep3", "Home");
                    }
					else
					{
                        ModelState.AddModelError("Code-error", "The code you've entered is expired!");
                        return View(model);
					}
				}
			}
			else
			{
                ModelState.AddModelError("Code-error", "Something went wrong, server is nor responding!");
                return View(model);
			}
		}

        [HttpGet]
		public async Task<IActionResult> RestorePassword()
		{
            CheckEmailRestorePasswordVM model = new CheckEmailRestorePasswordVM();

            return View(model);
		}

		[HttpPost]
        public async Task<IActionResult> RestorePassword(CheckEmailRestorePasswordVM model)
        {
			if (!ModelState.IsValid)
			{
                return View(model);
            }

			_verifyUserByEmailResponse = await _fitCookieAIRequestExecutor.VerifyUserByEmailAction(
				_fitCookieAIRequestBuilder.VerifyUserByEmailRequestBuilder(baseFitcookieAIUri, model.Email));

			if (_verifyUserByEmailResponse.Code != null && int.Parse(_verifyUserByEmailResponse.Code.ToString()) == 201)
			{
				string code = GeneratePasswordRecoveryCode(5);

				PasswordRecoveryTokenDTO token = new PasswordRecoveryTokenDTO
				{
					Code = code,
					UserId = int.Parse(_verifyUserByEmailResponse.Body)
				};

				_savePasswordRecoveryTokensResponse = await _fitCookieAIRequestExecutor.SavePasswordRecoveryTokensAction(token,
					_fitCookieAIRequestBuilder.SavePasswordRecoveryTokensRequestBuilder(baseFitcookieAIUri));

				if (_savePasswordRecoveryTokensResponse.Code != null && int.Parse(_savePasswordRecoveryTokensResponse.Code.ToString()) == 201)
				{
                    await SendEmailAsync(model.Email, "FitCookieAI Password restore.", $"Your recovery code is: {code}");

                    return RedirectToAction("RestorePasswordStep2", "Home");
                }
				else
				{
                    ModelState.AddModelError("Email-error", "Something went wrong, your password recovery code cannot be sent!");
                    return View(model);
				}
            }
			else
			{
                ModelState.AddModelError("Email-error", "Something went wrong, server is nor responding!");
                return View(model);
            }
        }

        public static async Task<string> SendEmailAsync(string email, string subject, string messageContent)
		{
            string user = "fitcookieai@gmail.com";
            string pass = "ivleaifvsclnwbxe";
            string smtpServer = "smtp.gmail.com";

            bool ssl = true;

            SmtpClient smtpClient = new SmtpClient();
            NetworkCredential basicCredential = new NetworkCredential(user, pass);
            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress(user);
            smtpClient.Host = smtpServer;
            if (ssl)
            {
                smtpClient.EnableSsl = true;
                smtpClient.Port = 587;
            }
            else
            {
                smtpClient.Port = 26;
            }
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = basicCredential;

            message.From = fromAddress;
            message.Subject = subject;
            //Set IsBodyHtml to true means you can send HTML email.
            /*
            message.IsBodyHtml = true;
            message.Body = "<h1>Hello, this is a demo ... ..</h1>";
            */
            message.IsBodyHtml = false;
            message.Body = messageContent;

            message.To.Add(email);

            try
            {
                smtpClient.Send(message);
                return "OK";
            }
            catch (Exception ex)
            {
                //Error, could not send the message
                return ex.ToString();
            }
        }

        public static string GeneratePasswordRecoveryCode(int length)
        {
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public IActionResult Privacy()
		{
			return View();
		}

		[HttpPost]
        public async Task<JsonResult> IsInKioskMode(bool IsInKioskMode)
        {
			string mode = "";

			if (IsInKioskMode == true)
			{
				HttpContext.Session.SetString("Kiosk", "true");
				mode = "Kiosk mode";
				return Json(mode);
			}
			else 
			{
				HttpContext.Session.SetString("Kiosk", "false");
				mode = "Normal mode";
				return Json(mode);
			}
        }

		[HttpPost]
		public async Task<JsonResult> Logout()
		{
			int userId = (int)this.HttpContext.Session.GetInt32("Id");
			_logoutUserResponse = await _fitCookieAIRequestExecutor.LogoutAction(_fitCookieAIRequestBuilder.LogoutRequestBuilder(baseFitcookieAIUri, userId));


			if (_logoutUserResponse != null && int.Parse(_logoutUserResponse.Code.ToString()) == 201)
			{
				this.HttpContext.Session.Remove("Token");
				this.HttpContext.Session.Remove("RefreshToken");
				this.HttpContext.Session.Remove("Id");
				this.HttpContext.Session.Remove("Email");
				this.HttpContext.Session.Remove("FirstName");
				this.HttpContext.Session.Remove("LastName");

				return Json(new { code = 201, message = "Logout Successful!" });
			}
			else
			{
				return Json(new { code = 200, message = "Logout failed!" });
			}
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}