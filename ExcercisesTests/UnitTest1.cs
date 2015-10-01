using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExerciseViewModels;
using ExercisesDAL;

namespace ExcercisesTests
{
    [TestClass]
    public class UnitTest1
    {
        /*[TestMethod]
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
        }*/

        /*[TestMethod]
        public void CreateCollectionsShpuldReturnTrue()
        {
            DALUtils util = new DALUtils();
            Assert.IsTrue(util.LoadCollections());
        }*/

        [TestMethod]
        public void UpdateTwiceShouldGenerateConcurrencyException()
        {
            int rowsUpdated = 0;
            EmployeeDAO dao = new EmployeeDAO();

            // Simulate two users getting same employee
            Employee user1Employee = dao.GetEmployeeBySurname("Smartypants");
            Employee user2Employee = dao.GetEmployeeBySurname("Smartypants");

            // Change in user number 1
            user1Employee.Phoneno = "555-555-5551";
            rowsUpdated = dao.Update(user1Employee);

            // User 1 updated
            if (rowsUpdated == 1)
            {
                // Change in user number 2
                user2Employee.Phoneno = "555-555-5552";
                // NOOO!!! Concurrency Exception!
                rowsUpdated = dao.Update(user2Employee);
            }

            Assert.IsTrue(rowsUpdated == -2);
        }
    }
}
