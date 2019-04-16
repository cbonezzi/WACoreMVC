using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WACoreMVC.Models;

namespace WACoreMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult User()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44345/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/values/users").Result;
            var model = response.Content.ReadAsAsync<IList<UserModel>>();

            var userList = new List<UserModel>();
            userList.AddRange(model.Result);

            return View(userList);
        }

        public IActionResult EditUser(string userId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44345/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/values/" + userId).Result;
            var model = response.Content.ReadAsAsync<UserModel>();

            return View(model.Result);
        }

        public async void SaveUser(UserModel model)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44345/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var json = JsonConvert.SerializeObject(model);
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("UserId", model.UserId),
            });
            content.Headers.ContentType.MediaType = "application/json";
            await client.PostAsync("api/values/", content);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
