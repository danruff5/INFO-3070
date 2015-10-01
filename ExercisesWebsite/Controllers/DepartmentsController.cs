using System;
using System.Web.Http;
using ExerciseViewModels;

namespace ExercisesWebsite.Controllers
{
    public class DepartmentsController : ApiController
    {
        [Route("api/departments/{id}")]
        public IHttpActionResult Get (string id)
        {
            try
            {
                DepartmentViewModel dep = new DepartmentViewModel();
                dep.DepartmentId = id;
                dep.GetById();
                return Ok(dep);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrive failed - " + ex.Message);
            }
        }
    }
}
