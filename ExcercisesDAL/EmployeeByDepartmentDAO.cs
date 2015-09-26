using MongoDB.Driver.Linq;
using System.Linq;
using System;
using MongoDB.Bson;


namespace ExcercisesDAL
{
    public class EmployeeByDepartmentDAO
    {
        public Employee[] GetEmployees (string dept)
        {
            DbContext _ctx;
            Employee[] employeesInDept = null;

            try
            {
                _ctx = new DbContext();
                var employees = _ctx.Employees;
                var departments = _ctx.Departments;
                var department = departments.AsQueryable<Department>().FirstOrDefault(dep => dep.DepartmentName == dept);

                employeesInDept = employees.AsQueryable<Employee>().Where(emp => emp.DepartmentId == department._id).ToArray();

            } catch (Exception ex)
            {
                Console.WriteLine("Problem " + ex.Message);
            }

            return employeesInDept;
        }
    }
}
