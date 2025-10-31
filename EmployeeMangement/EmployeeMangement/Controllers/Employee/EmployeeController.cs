using Microsoft.AspNetCore.Mvc;

namespace EmployeeMangement.Controllers
{
   
    public class EmployeeController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
        public IActionResult Header()
        {
            return PartialView();
        }
        // GET: api/employees
        [HttpGet("api/employees")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(new { message = "Get all employees" });
        }

        // GET: api/employees/5
        [HttpGet("api/employee/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(new { message = $"Get employee {id}" });
        }

        // POST: api/employees
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] object employee)
        {
            return Created("", new { message = "Employee created" });
        }

        // PUT: api/employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] object employee)
        {
            return Ok(new { message = $"Employee {id} updated" });
        }

        // DELETE: api/employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return NoContent();
        }
    }
}
