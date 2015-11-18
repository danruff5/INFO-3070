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
        public void CreateCollectionsShpuldReturnTrue()
        {
            DALUtilsV2 util = new DALUtilsV2();
            Assert.IsTrue(util.LoadCollections());
        }

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

        [TestMethod]
        public void UpdateViewModelTwiceShouldGenerateConcurrencyException()
        {
            int rowsUpdated = 0;
            EmployeeViewModel user1Vm = new EmployeeViewModel();
            EmployeeViewModel user2Vm = new EmployeeViewModel();

            // Simulate users getting same employee:
            user1Vm.Lastname = "Smartypants";
            user1Vm.GetBySurname();
            user2Vm.Lastname = "Smartypants";
            user2Vm.GetBySurname();

            // Change phone number for user 1:
            user1Vm.Phoneno = "555-555-5551";

            // User 1 updates:
            rowsUpdated = user1Vm.Update();

            if (rowsUpdated == 1)
            {
                // Change phone numeber for user 2:
                user2Vm.Phoneno = "555-555-5552";

                // Conncurrency exception rowsUpdated should = -2
                rowsUpdated = user2Vm.Update();
            }

            Assert.IsTrue(rowsUpdated == -2);
        }
    }
}
