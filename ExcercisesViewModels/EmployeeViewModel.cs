using System;
using ExercisesDAL;
using System.Collections.Generic;
using System.Diagnostics;

namespace ExerciseViewModels
{
    public class EmployeeViewModel
    {
        private EmployeeDAO _dao;
        public string Id { get; set; }
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

        public void GetById(string id)
        {
            try
            {
                Employee emp = _dao.GetByID(id);
                Id = emp._id.ToString();
                Title = emp.Title;
                Firstname = emp.Firstname;
                Lastname = emp.Lastname;
                Phoneno = emp.Phoneno;
                Email = emp.Email;
                DepartmentId = emp.DepartmentId.ToString();
                StaffPicture64 = emp.StaffPicture64;
                IsTech = emp.IsTech;
                Entity64 = Convert.ToBase64String(ViewModelUtils.Serializer(emp));
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "EmployeeViewModel", "GetById");
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

        public List<EmployeeViewModel> GetAll()
        {
            List<EmployeeViewModel> viewModels = new List<EmployeeViewModel>();

            try
            {
                List<Employee> employees = _dao.GetAll();

                foreach (Employee e in employees)
                {
                    // Return only fields for display, subdequent get will other fields
                    EmployeeViewModel viewModel = new EmployeeViewModel();
                    viewModel.Id = e._id.ToString();
                    viewModel.Title = e.Title;
                    viewModel.Firstname = e.Firstname;
                    viewModel.Lastname = e.Lastname;
                    viewModels.Add(viewModel); // Add to list
                }
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "EmployeeViewModel", "GetAll");
            }
            return viewModels;
        }

        public static void ErrorRoutine(Exception e, string obj, string method)
        {
            if (e.InnerException != null)
            {
                Trace.WriteLine("Error in ViewModels, Objects = "
                    + obj
                    + ", method = "
                    + method
                    + ", inner execption message = "
                    + e.InnerException.Message
                );
                throw e.InnerException;
            }
            else
            {
                Trace.WriteLine("Error in ViewModels, object = "
                    + obj
                    + ", method = "
                    + method
                    + ", message = "
                    + e.Message
                );
                throw e;
            }
        }
    }
}