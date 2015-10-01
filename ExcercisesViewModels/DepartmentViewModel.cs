using System;
using ExercisesDAL;

namespace ExerciseViewModels
{
    public class DepartmentViewModel
    {
        private DepartmentDAO _dao;
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public DepartmentViewModel()
        {
            _dao = new DepartmentDAO();
        }

        public void GetById()
        {
            try
            {
                Department dep = _dao.GetDepartment(DepartmentId);
                DepartmentName = dep.DepartmentName;
            } catch (Exception ex)
            {
                DepartmentName = "not found";
            }
        }
    }
}
