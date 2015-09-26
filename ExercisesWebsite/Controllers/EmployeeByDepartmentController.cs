using System;
using System.Web.Http;
using ExcerciseViewModels;

namespace ExercisesWebsite.Controllers
{
    public class EmployeeByDepartmentController : ApiController
    {
        [Route("api/employees/department/{department}")]
        public IHttpActionResult Get (string department)
        {
            try
            {
                EmployeeByDepartmentViewModel emp = new EmployeeByDepartmentViewModel();
                emp.department = department;
                emp.Get();
                return Ok(emp);
            } catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }
    }
}
