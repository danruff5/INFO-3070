using System;
using ExercisesDAL;

namespace ExerciseViewModels
{
    public class EmployeeViewModel
    {
        private EmployeeDAO _dao;
        public string Title { get; set; }
        public string Firstname { get; set; }
        public string Lastname{ get; set; }
        public string Email { get; set; }
        public string Phoneno { get; set; }
        public string Entity64 { get; set; }
        public string EmployeeId { get; set; }
        public string DepartmentId { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        public EmployeeViewModel()
        {
            _dao = new EmployeeDAO();
        }

        public void GetBySurname()
        {
            try
            {
                Employee emp = _dao.GetEmployeeBySurname(Lastname);
                Title = emp.Title;
                Firstname = emp.Firstname;
                Lastname = emp.Lastname;
                Phoneno = emp.Phoneno;
                Email = emp.Email;
                EmployeeId = emp._id.ToString();
                DepartmentId = emp.DepartmentId.ToString();
            } catch (Exception ex)
            {
                Lastname = "not found";
            }
        }
    }
}