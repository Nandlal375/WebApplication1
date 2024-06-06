using DataAccessLayer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;

namespace CrudAppWithWebApi.Controllers
{
    public class LoginController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetLoginDetail(LoginTbl loginTbl1)
        {
            try
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, loginTbl1.username));
                claims.Add(new Claim(ClaimTypes.Name, loginTbl1.username));
                if (loginTbl1.username == "anil")
                {
                    claims.Add(new Claim(ClaimTypes.Role, "anil"));
                }
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                HttpContext.SignInAsync(claimsPrincipal);



                string message = await GetLoginDetail1(loginTbl1);
                TempData["Message"] = message;
                HttpContext.Session.SetString("username", loginTbl1.username);
                
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
               
            }
            return RedirectToAction("Index", "Login");
        }

        public async Task<string> GetLoginDetail1(LoginTbl loginTbl)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44313/");
                HttpResponseMessage response = await client.PostAsJsonAsync("api/login", loginTbl);
                if (response.IsSuccessStatusCode)  
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    dynamic responseObj = JsonConvert.DeserializeObject(responseData);
                    return responseObj.message;

                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    dynamic errorObj = JsonConvert.DeserializeObject(errorResponse);
                    return errorObj.message;             
                }
            }
        }

        public IActionResult Logout()
        { 
           HttpContext.Session.Clear();
          return RedirectToAction("Index", "Login");
        }
    }
}
