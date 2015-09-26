using System.Web.Http;
using System;
using ExcerciseViewModels;

namespace ExercisesWebsite
{
    public class EmployeeController : ApiController
    {
        [Route("api/employees/{name}")]
        public IHttpActionResult Get(string name)
        {
            try
            {
                EmployeeViewModel emp = new EmployeeViewModel();
                emp.Lastname = name;
                emp.GetBySurname();
                return Ok(emp);
            } catch (Exception ex)
            {
                return BadRequest("Retrive failed - " + ex.Message);
            }
        }
    }
}
