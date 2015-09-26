using System;
using System.Web.Http;
using ExcerciseViewModels;

namespace ExercisesWebsite.Controllers
{
    public class EmployeeController : ApiController
    {
        [Route("api/employees/{name}")]
        public IHttpActionResult Get (string name)
        {
            try
            {
                EmployeeViewModel emp = new EmployeeViewModel();
                emp.Lastname = name;
                emp.GetBySurname();
                return Ok(emp);
            } catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }
    }
}
