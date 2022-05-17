using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimesheetBackend.Models;

namespace TimesheetBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly tuntidbContext db = new tuntidbContext();


        [HttpGet]

        public List<Employee> GetAllActive()
        {
            var employees = db.Employees.Where(e => e.Active == true);            
            return employees.ToList(); 
        }
    }
}
