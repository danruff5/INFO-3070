using MongoDB.Driver.Linq;
using System.Linq;
using System;
using MongoDB.Bson;


namespace ExcercisesDAL
{
    public class DepartmentDAO
    {
        public Department GetDepartment (string idS)
        {
            ObjectId id = new ObjectId(idS);
            Department retDep = null;
            DbContext _ctx;

            try
            {
                _ctx = new DbContext();
                var departments = _ctx.Departments;
                var department = departments.AsQueryable<Department>().Where(dep => dep._id == id).FirstOrDefault();
                retDep = (Department)department;
            } catch (Exception ex)
            {
                Console.WriteLine("Problem " + ex.Message);
            }
            return retDep;
        }
    }
}
