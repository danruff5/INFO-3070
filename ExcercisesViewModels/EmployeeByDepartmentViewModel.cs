using System;
using ExcercisesDAL;

namespace ExcerciseViewModels
{
    public class EmployeeByDepartmentViewModel
    {
        private EmployeeByDepartmentDAO _dao;
        public string department { get; set; }
        public Employee[] employees { get; set; }

        public EmployeeByDepartmentViewModel()
        {
            _dao = new EmployeeByDepartmentDAO();
        }

        public void Get()
        {
            try
            {
                employees = _dao.GetEmployees(department);
            } catch (Exception ex)
            {
                employees[0] = new Employee("not found");
            }
        }
    }
}
