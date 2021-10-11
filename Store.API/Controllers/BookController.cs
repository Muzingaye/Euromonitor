using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using DataFramework.Entities;
using DataFramework.Repository;
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
   // [EnableCors("CorsPolicy")]
    public class BookController : ControllerBase
    {
        private IConfiguration config;
        private readonly IService<Product> productService;
        private readonly IService<Account> accountService;
        private readonly IService<Subscription> subscibeService;
        private readonly ProductInSubscriptionService prodSubscibeService;

        public BookController(IConfiguration config)
        {
            this.config = config;
            this.productService = new ProductServices(new ProductRepository(config["ConnDB"]));
            this.accountService = new AccountService(new AccountRepository(config["ConnDB"]));
            this.subscibeService = new SubscriptionServices(new SubscriptionRepository(config["ConnDB"]));
            this.prodSubscibeService = new ProductInSubscriptionService(new ProductInSubscriptionRepository(config["ConnDB"]));
        }

        [HttpGet]
        [Route("/Product/AvailableBooks")]
        [AllowAnonymous]
        public IList<Product> GetBooks()
        {
            List<Product> products = null;

            try
            {
                products = productService.GetAll().ToList(); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return products;
        }


        [HttpPost]
      ///  [AllowAnonymous] // Do not allow unonymous this for Unit testing only
        [Route("/Product/SubscribeToBook")]
        public SubscriptionModel SubscribeToBook(List<Subscription> subscriptions)
        {
            var orderModel = new SubscriptionModel();
            try
            {
                if (HttpContext.Session.GetString("EmailAddress") != null)
                {
                    var loggInUsser =
                        accountService.GetFirst(x => x.EmailAddress == HttpContext.Session.GetString("EmailAddress"));

                    //Save Order
                    subscibeService.InsertRange(subscriptions);
                    foreach (var subscription in subscriptions)
                    {
                        ProductInSubscription addInSubscription = new ProductInSubscription();
                        addInSubscription.AccountId = loggInUsser.Id;
                        addInSubscription.OrderId = subscription.Id;
                        addInSubscription.ProductId = subscription.Id;
                        addInSubscription.CaptureDate = DateTime.Now;
                        prodSubscibeService.Insert(addInSubscription);
                    }
                   
                    orderModel.ErrorCode = 0;
                    orderModel.ErrorString = "You have successful processed your order";
                }
            }
            catch (Exception ex)
            {
                orderModel.ErrorCode = 800;
                orderModel.ErrorString = ex.Message;
            }

            return orderModel;
        }



        [HttpPost]
        [Route("/Product/UnSubscribe")]
        public SubscriptionModel UnSubscribe(List<Subscription> subscriptions)
        {
            var orderModel = new SubscriptionModel();
            try
            {
                if (HttpContext.Session.GetString("EmailAddress") != null)
                {
                    var loggInUsser =
                        accountService.GetFirst(x => x.EmailAddress == HttpContext.Session.GetString("EmailAddress"));

                    foreach (var subscription in subscriptions)
                    {
                        ProductInSubscription addInSubscription = new ProductInSubscription();
                        addInSubscription.AccountId = loggInUsser.Id;
                        addInSubscription.OrderId = subscription.Id;
                        addInSubscription.ProductId = subscription.Id;
                        addInSubscription.CaptureDate = DateTime.Now;
                        prodSubscibeService.Delete(addInSubscription);
                    }

                    orderModel.ErrorCode = 0;
                    orderModel.ErrorString = "You have successful processed your order";
                }
            }
            catch (Exception ex)
            {
                orderModel.ErrorCode = 800;
                orderModel.ErrorString = ex.Message;
            }

            return orderModel;
        }
    }
}
