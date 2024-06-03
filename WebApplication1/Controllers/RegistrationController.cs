using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationRepository _registration;
        public RegistrationController(IRegistrationRepository registration)
        {
                _registration = registration;
        }
        [HttpGet] 
        public async Task<ActionResult> GetRegistration()
        {
            try
            {
                return Ok(await _registration.GetRegistrationList());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving Data");
            }

        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Registration>> GetRegistrationListById(int id)
        {
            try
            {
                var getById = await _registration.GetRegistrationListById(id);
                if (getById == null)
                {
                    return NotFound();
                }
                else
                {
                    return getById;
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieve data");
            }
        }

        [HttpPost]
        public async Task<IActionResult>AddRegistrationList(Registration registration)
        {
            try
            {
                if (registration == null)
                {
                    return BadRequest();
                }
                var addresult = await _registration.AddRegistrationList(registration);
                return CreatedAtAction(nameof(GetRegistrationListById), new {id = addresult.Pid }, addresult);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro r of insert data");
            }

        }

     
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Registration>> UpdateRegistrationList(int id, Registration registration)
        {
            try
            {
                if (registration.Pid == 0)
                {
                    return BadRequest($"This {registration.Pid} Id Mismatch");
                }
                var result = await _registration.GetRegistrationListById(registration.Pid);
                if (result == null)
                {
                    return NotFound("Record is mismatch");
                }
                return await _registration.UpdateRegistrationList(registration);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in insert");
            }
        }






        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Registration>> Deleterow(int id)
        {
            try
            {
                var result = await _registration.DeleteUser(id);
                if (result == null)
                {
                    return NotFound($"this id {id} is not found");
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in insert");
            }
        }
    }
}
