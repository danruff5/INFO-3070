using MongoDB.Driver.Linq;
using System.Linq;
using System;
using MongoDB.Kennedy;

namespace ExercisesDAL
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

        public int Update(Employee emp)
        {
            int updateOK = -1;
            try
            {
                DbContext ctx = new DbContext();
                ctx.Save<Employee>(emp, "employees");
                updateOK = 1;
            } catch (MongoConcurrencyException ex)
            {
                updateOK = -2;
                Console.WriteLine(ex.Message);
            } catch (Exception ex)
            {
                Console.WriteLine("Problem " + ex.Message);
            }

            return updateOK;
        }
    }
}