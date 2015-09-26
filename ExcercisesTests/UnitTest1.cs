using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExcercisesDAL;
using ExcerciseViewModels;

namespace ExcercisesTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void EmployeeDAOReturnBySurnameShouldReturnEmployee()
        {
            EmployeeDAO dao = new EmployeeDAO();
            Employee someEmployee = dao.GetEmployeeBySurname("Smartypants");
            Assert.IsInstanceOfType(someEmployee, typeof(Employee));
        }

        [TestMethod]
        public void EmployeeViewModelReturnBySurnameShouldLoadFirstname()
        {
            EmployeeViewModel vm = new EmployeeViewModel();
            vm.Lastname = "Smartypants";
            vm.GetBySurname();
            Assert.IsTrue(vm.Firstname.Length > 0);
        }

        [TestMethod]
        public void DepartmentDAOReturnByIDShouldReturnDepartment()
        {
            DepartmentDAO dao = new DepartmentDAO();
            Department dep = dao.GetDepartment("55f1812ef748ef1f14c8eb2e");
            Assert.IsInstanceOfType(dep, typeof(Department));
        }

        [TestMethod]
        public void DepartmentViewModelReturnByIDShouldLoadDepartmentName()
        {
            DepartmentViewModel vm = new DepartmentViewModel();
            vm.DepartmentId = "55f1812ef748ef1f14c8eb2e";
            vm.GetById();
            Assert.IsTrue(vm.DepartmentName.Length > 0);
        }
    }
}
