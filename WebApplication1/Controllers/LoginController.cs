using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IRegistrationRepository _registrationRepository;
        public LoginController(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }
        [HttpPost]
        public async Task<IActionResult> GetLoginDetail([FromBody] LoginTbl loginTbl)
        {
            try
            {
                var result = await _registrationRepository.CheckCredential(loginTbl);

                if (result == null)
                {
                    return Unauthorized(new { Message = "Invalid username or password" });
                }

                return Ok(new { Message = "Login successful" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Data match failed");
            }
        }

    }
}
