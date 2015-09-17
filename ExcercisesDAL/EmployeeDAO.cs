using MongoDB.Driver.Linq;
using System.Linq;
using System;

namespace ExcercisesDAL
{
    public class EmployeeDAO
    {
        public Employee GetEmployeeBySurname(string name)
        {
            Employee retEmp = null;
            DbContext _ctx;

            try
            {
                _ctx = new DbContext();
                var employees = _ctx.Employees;
                var employee = employees.AsQueryable<Employee>().Where(emp => emp.Lastname == name).FirstOrDefault();
                retEmp = (Employee)employee;
            }catch (Exception ex)
            {
                Console.WriteLine("Problem " + ex.Message);
            }

            return retEmp;
        }
    }
}