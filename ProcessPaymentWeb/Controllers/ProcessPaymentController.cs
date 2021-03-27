using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProcessPaymentWeb.Models;
using ProcessPaymentWeb.Utility;

namespace ProcessPaymentWeb.Controllers
{
    public class ProcessPaymentController : Controller
    {
        private IConfiguration _Configure;
        private readonly IOptions<MySettingsModel> appsettings;
        string apiBaseUrl;

         public ProcessPaymentController(IConfiguration configuration)
        {
            _Configure = configuration;
            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
        }

         
        public IActionResult ProcessPaymentView()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessPaymentAsync(ProcessPayment  processPayment)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(processPayment), Encoding.UTF8, "application/json");

                    using (var Response = await client.PostAsync("http://localhost:4789/api/ProcessPay/Create", content))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            //TempData["Profile"] = JsonConvert.SerializeObject(user);

                            //return RedirectToAction("Profile");

                        }
                        else
                        {
                            // ModelState.Clear();
                            //ModelState.AddModelError(string.Empty, "Username or Password is Incorrect");
                            return Json("ProcessPaymentView");
                        }

                    }

                }
            }
                return RedirectToAction("ProcessPaymentView");
        }
    }
}