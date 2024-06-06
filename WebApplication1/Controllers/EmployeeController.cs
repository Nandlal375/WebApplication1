using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
            try
            {
                return Ok(await _employeeRepository.GetEmployees());
                //200
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving Data");
            }
        }


        [HttpGet("{id:int}")]

        public async Task<ActionResult<Employee>> GetEmployee(int id) 
        {
            try
            {
                var result = await _employeeRepository.GetEmployee(id);
                if (result == null)
                {
                    return NotFound();
                    //404
                }
                else
                {
                    return result;
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving Data");
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            try
            {
                if (employee == null)
                { 
                 return BadRequest();
                    //400
                }
                var result = await _employeeRepository.AddEmployee(employee);
                return CreatedAtAction(nameof(GetEmployee), new { id = result.Id }, result);
                // Ok("Data insert succfelilly");
                //201
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in insert");

            }
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee employee)
        {
            try
            {
                if (id != employee.Id)
                {
                    return BadRequest("Id Mismatch");
                    // 400
                }
                var result = await _employeeRepository.GetEmployee(id);
                if (result == null)
                {
                    return NotFound($"Employee Id {id} Not Found"); 
                    //404
                }
                //return Ok("Update Data Succeeffully");
                // 200
                return await _employeeRepository.UpdateEmployee(employee);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in insert");

            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmp(int id)
        {
            try
            {
                var result = await _employeeRepository.GetEmployee(id);
                if (result == null)
                {
                    return NotFound($"Employee Id {id} Not Found");
                    //404
                }
                return await _employeeRepository.DeleteEmployee(id);
                

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in insert");

            }
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Employee>>> Search(string name)
        {
            try
            {
                var result = await _employeeRepository.Search(name);
                if (result.Any())
                {
                  return Ok(result);
                }
                else
                {
                  return NotFound($"{name} is not found");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in insert");

            }
        }
    }
}
