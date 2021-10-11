using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DataFramework.Entities;
using DataFramework.Repository;
using Jose;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ServiceFramework;
using ServiceFramework.Services;
using Store.API.Models;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private IConfiguration config;
        private readonly IService<Account> accountService;
        private readonly IService<AccountDetails> accountDetailsService;


        public AccountController(IConfiguration config)
        {
            this.config = config;
            this.accountService = new AccountService(new AccountRepository(config["ConnDB"]));
           this.accountDetailsService = new AccountDetailsServices(new AccountDetailsRepository(config["BetDb"]));
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("/Account/Register")]
        public AccountModel Register([FromBody] AccountModel model)
        {
            var accountModel = new AccountModel();
            Account registerAccount = new Account();
            try
            {
                if (ModelState.IsValid)
                {
                    registerAccount = accountService.GetFirst(x => x.EmailAddress == model.EmailAddress);
                    if (registerAccount is null)
                    {
                        var captureDate = DateTime.Now;
                        bool insertAccount = accountService.Insert(new Account
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            EmailAddress = model.EmailAddress,
                            PhoneNumber = model.PhoneNumber,
                            CaptureDate = captureDate
                        });
                        if (insertAccount)
                        { 
                            Account newAccount = accountService.GetFirst(x => x.EmailAddress == model.EmailAddress);
                            var accountDetails = new AccountDetails();
                            accountDetails.AccountId = newAccount.Id;
                            accountDetails.Password = new Helpers.Security().Encrypt(model.Password);
                            accountDetails.ActiveMin = Convert.ToDecimal(0);
                            accountDetails.LastLogInTime = captureDate;
                            accountDetails.CaptureDate = captureDate;

                            accountDetailsService.Insert(accountDetails);
                        }

                        accountModel.ErrorCode = 0;
                        accountModel.ErrorString = $"You have successful registered, Please procced to login!";
                    }
                    else
                    {
                        accountModel.ErrorCode = 801;
                        accountModel.ErrorString = $"Email Address {model.EmailAddress} is registered!";
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Registration Details");
                }
            }
            catch (Exception ex)
            {
                accountModel.ErrorCode = 802;
                accountModel.ErrorString = $"{ex.Message}";
            }

            return accountModel;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/Account/Login")]
        public IActionResult Login([FromBody] Login model)
        {
            IActionResult response = Unauthorized();
            try
            {
                var account = accountService.GetFirst(x => x.EmailAddress == model.EmailAddress);
                if (account == null || account.AccountDetails == null)
                    return response;

                if (account.EmailAddress == model.EmailAddress && account.AccountDetails.Password ==
                    new Helpers.Security().Encrypt(model.Password))
                {
                    var tokenStr = GenerateToken(model.EmailAddress, model.Password);
                    HttpContext.Session.SetString("token", tokenStr);
                    HttpContext.Session.SetString("EmailAddress", model.EmailAddress);
                    response = Ok(new { token = tokenStr });
                }
            }
            catch (Exception ex)
            {
                model.ErrorCode = 802;
                model.ErrorString = $"{ex.Message}";
            }

            return response;
        }

        [HttpGet]
        [Route("/Account/Logout")]
        public IActionResult Logout()
        {
            //  HttpContext.Session.Clear();
            IActionResult response = Ok();
            return response;
        }

        private string GenerateToken(object username, object password)
        {
            var TokenIssueDate = DateTime.Now;
            DateTime ExpiringTime = TokenIssueDate.AddHours(2);

            var payload = new Dictionary<string, object>()
            {
                { "iss", config["Jwt:Issuer"] }, { "sub", username }, { "jti", password },
                { "exp", ExpiringTime.Ticks }, { "iat", TokenIssueDate.Ticks }
            };

            return JWT.Encode(payload, SecretKey, JweAlgorithm.A256KW, JweEncryption.A256CBC_HS512);
        }

        private byte[] SecretKey
        {
            get
            {
                var secretKey = Encoding.ASCII.GetBytes(config["Jwt:Key"]);
                var secretKeyBytes = SHA256.Create().ComputeHash(secretKey);
                return secretKeyBytes;
            }
        }
    }
}