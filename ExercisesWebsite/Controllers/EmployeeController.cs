using System.Web.Http;
using System;
using ExerciseViewModels;

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

        [Route("api/employees")]
        public IHttpActionResult Put (EmployeeViewModel emp)
        {
            try
            {
                int errorNumber = emp.Update();
                switch (errorNumber)
                {
                    case 1:
                        return Ok("Employee " + emp.Lastname + " updated!");
                        break;
                    case -1:
                        return Ok("Employee" + emp.Lastname + " not updated!");
                        break;
                    case -2:
                        return Ok("Data is stale for " + emp.Lastname + ". Employee not updated!");
                        break;
                    default:
                        return Ok("Employee" + emp.Lastname + " not updated!");
                        break;
                }
            } catch (Exception ex)
            {
                return BadRequest("Update failed - " + ex.Message);
            }
        }
    }
}
