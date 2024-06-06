using CrudAppWithWebApi.Models;
using DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using NuGet.Protocol;
using System.Web;
using System.Configuration;
using System.Diagnostics;
using System.Text.Json.Serialization;


namespace CrudAppWithWebApi.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _environment;
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            List<Employee> employees = new List<Employee>();    
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44313/");
            HttpResponseMessage response = await client.GetAsync("api/employees");
            if (response.IsSuccessStatusCode)
            {
                var results = response.Content.ReadAsStringAsync().Result; 
                employees = JsonConvert.DeserializeObject<List<Employee>>(results);
            }
            return View(employees);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Employee employee = await GetEmployeeByID(id);
            return View(employee);
        }

        private static async Task<Employee> GetEmployeeByID(int id)
        {
            Employee employee = new Employee();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44313/");
            HttpResponseMessage response = await client.GetAsync($"api/employees/{id}");
            if (response.IsSuccessStatusCode)
            {
                var results = response.Content.ReadAsStringAsync().Result;
                employee = JsonConvert.DeserializeObject<Employee>(results);
            }
            return employee;
        }


        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            Employee employee = new Employee();
            List<Country> list = new List<Country>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44313/");

                HttpResponseMessage response1 = await client.GetAsync("api/Country");
                if (response1.IsSuccessStatusCode)
                {
                    var results = await response1.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<Country>>(results);
                }
                ViewBag.Country = list;

                if (id > 0)
                {
                    ViewBag.btn = "Update";
                    HttpResponseMessage response = await client.GetAsync($"api/employees/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        var results = await response.Content.ReadAsStringAsync();
                        employee = JsonConvert.DeserializeObject<Employee>(results);
                    }
                }
            }

            return View(employee);
        }


        [HttpPost]
        public async Task<IActionResult> Create(Employee emp)
        {
            string filename = uploadFile(emp);
            emp.imageFileName = filename;

            emp.image = null;
            if (!ModelState.IsValid)
            {
                return View(emp);
            }
            else
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:44313/");
                var response = await client.PostAsJsonAsync("api/Employees", emp);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View();

            }
        }

        public string uploadFile(Employee empp)
        {
            string fileName = null;
            if (empp.image != null)
            {
                string uploadDir = Path.Combine(_environment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + empp.image.FileName;
                string FilePath = Path.Combine(uploadDir, fileName);
                using (var filestream = new FileStream(FilePath, FileMode.Create))
                {
                    empp.image.CopyTo(filestream);
                }
            }
            return fileName;
        }


        public async Task<IActionResult> Delete(int id)
        {
            Employee employee = new Employee();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44313/");
            HttpResponseMessage response = await client.DeleteAsync($"api/employees/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(employee);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id) 
        {
            Employee employee = await GetEmployeeByID(id);
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee emp)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44313/");
            var response = await client.PutAsJsonAsync($"api/employees/{emp.Id}", emp);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
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
