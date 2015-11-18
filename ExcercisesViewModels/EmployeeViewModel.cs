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
        public string StaffPicture64 { get; set; }
        public bool IsTech { get; set; }

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
                Entity64 = Convert.ToBase64String(ViewModelUtils.Serializer(emp));
                StaffPicture64 = emp.StaffPicture64;
            } catch (Exception ex)
            {
                Lastname = "not found";
            }
        }

        public int Update()
        {
            int empsUpdated = 0;

            try
            {
                Employee emp = (Employee)ViewModelUtils.Deserializer(Convert.FromBase64String(Entity64));
                emp.Title = Title;
                emp.Firstname = Firstname;
                emp.Lastname = Lastname;
                emp.Phoneno = Phoneno;
                emp.Email = Email;
                emp.StaffPicture64 = StaffPicture64;
                empsUpdated = _dao.Update(emp);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                empsUpdated = -1;
            }

            return empsUpdated;
        }
    }
}