using MongoDB.Driver.Linq;
using System.Linq;
using System;
using MongoDB.Kennedy;
using System.Collections.Generic;
using MongoDB.Bson;

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
                DALUtilsV2.ErrorRoutine(ex, "EmployeeDAO", "GetEmployeeBySurname");
            }

            return retEmp;
        }

        public Employee GetByID(string id)
        {
            Employee retEmp = null;
            ObjectId ID = new ObjectId(id);
            DbContext _ctx;

            try
            {
                _ctx = new DbContext();
                retEmp = _ctx.Employees.AsQueryable().FirstOrDefault(e => e._id == ID);
            }
            catch (Exception ex)
            {
                DALUtilsV2.ErrorRoutine(ex, "EmployeeDAO", "GetById");
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
                DALUtilsV2.ErrorRoutine(ex, "EmployeeDAO", "Update");
            }

            return updateOK;
        }

        public List<Employee> GetAll()
        {
            List<Employee> allEmps = new List<Employee>();

            try
            {
                DbContext ctx = new DbContext();
                allEmps = ctx.Employees.AsQueryable().ToList();
            }
            catch (Exception ex)
            {
                DALUtilsV2.ErrorRoutine(ex, "EmployeeDAO", "GetAll");
            }

            return allEmps;
        }
    }
}