using DataAccessLayer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Microsoft.Build.Evaluation;
using Microsoft.AspNetCore.Authorization;

namespace CrudAppWithWebApi.Controllers
{
    //[Authorize(Roles = "anil")]
    public class RegistrationController : Controller
    {

        public async Task<IActionResult> Index(int id)
        {
            if (id > 0)
            {
                ViewBag.btn = "Update";
                Registration registration = await GetresultById(id);
                return View(registration);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<ActionResult> Index(Registration registration)
        {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:44313/");
                var response = await client.PostAsJsonAsync("api/Registration", registration);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Login");
                }                                                                               
                return View();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Registration>>> Getresult()
        {
            var username = HttpContext.Session.GetString("username");

            if (username == null)
            {
                return RedirectToAction("Index", "Login");
            }

            HttpClient httpClient = new HttpClient();
          httpClient.BaseAddress = new Uri("https://localhost:44313/");
          var getresult = await httpClient.GetAsync("api/Registration");
            if (getresult.IsSuccessStatusCode)
            { 
                var result = getresult.Content.ReadAsStringAsync().Result;
                var getAlldetail = JsonConvert.DeserializeObject<IEnumerable<Registration>>(result);
                return View(getAlldetail);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        private static async Task<Registration> GetresultById(int id)
        {
            Registration registration = new Registration();
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44313/");
            HttpResponseMessage response = await httpClient.GetAsync($"api/Registration/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                registration = JsonConvert.DeserializeObject<Registration>(data);
            }
            return registration;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRegistrationList1(Registration registration)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44313/");

                HttpResponseMessage response = await client.PutAsJsonAsync($"api/Registration/{registration.Pid}", registration);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Getresult");
                }
                else
                {
                    // Optionally log the error details
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    // Log the error (example using a logger, if available)
                    // _logger.LogError($"Failed to update registration: {errorMessage}");

                    // Optionally, pass the error message to the view
                    ViewBag.ErrorMessage = "An error occurred while updating the registration.";
                    return View("Error"); // Assuming you have an Error view
                }
            }
        }

        public async Task<ActionResult<Registration>> Delete(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:44313/");
                HttpResponseMessage response = await client.DeleteAsync($"api/Registration/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Getresult");
                }
                else
                {
                    return View(response);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
