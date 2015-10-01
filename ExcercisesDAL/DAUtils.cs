using System;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ExercisesDAL
{
    public class DALUtils
    {
        DbContext ctx;

        /// <summary>
        /// Main Loading Method
        /// Revisions: Added Problem methods
        /// </summary>
        public bool LoadCollections()
        {
            bool createOk = false;

            try
            {
                DropAndCreateCollections();
                ctx = new DbContext();
                LoadDepartments();
                LoadEmployees();
                createOk = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return createOk;
        }

        private void DropAndCreateCollections()
        {
            MongoClient client = new MongoClient(); // connect to localhost
            MongoServer server = client.GetServer();
            MongoDatabase db = server.GetDatabase("HelpdeskDB");

            using (server.RequestStart(db))
            {

                if (db.CollectionExists("departments"))
                {
                    db.DropCollection("departments");
                }
                if (db.CollectionExists("employees"))
                {
                    db.DropCollection("employees");
                }

                db.CreateCollection("departments");
                db.CreateCollection("employees");
            }
        }

        private void LoadDepartments()
        {
            InsertDepartment("Administration");
            InsertDepartment("Sales");
            InsertDepartment("Food Services");
            InsertDepartment("Lab");
            InsertDepartment("Maintenance");
        }

        private void LoadEmployees()
        {
            InsertEmployee("Mr.", "Bigshot", "Smartypants", "(555) 555-5551", "bs@abc.com", "Administration");
            InsertEmployee("Mrs.", "Penny", "Pincher", "(555) 555-5551", "pp@abc.com", "Administration");
            InsertEmployee("Mr.", "Smoke", "Andmirrors", "(555) 555-5552", "sa@abc.com", "Sales");
            InsertEmployee("Mr.", "Sam", "Slick", "(555) 555-5552", "ss@abc.com", "Sales");
            InsertEmployee("Mr.", "Sloppy", "Joe", "(555) 555-5553", "sj@abc.com", "Food Services");
            InsertEmployee("Mr.", "Franken", "Beans", "(555) 555-5553", "fb@abc.com", "Food Services");
            InsertEmployee("Mr.", "Bunsen", "Burner", "(555) 555-5554", "bb@abc.com", "Lab");
            InsertEmployee("Ms.", "Petrie", "Dish", "(555) 555-5554", "pd@abc.com", "Lab");
            InsertEmployee("Ms.", "Mopn", "Glow", "(555) 555-5555", "mg@abc.com", "Maintenance");
            InsertEmployee("Mr.", "Spickn", "Span", "(555) 555-5555", "sps@abc.com", "Maintenance");
        }

        private void InsertDepartment(string name)
        {
            Department dep = new Department();
            dep.DepartmentName = name;
            ctx.Save<Department>(dep, "departments");
        }



        private void InsertEmployee(string title,
                                   string first,
                                   string last,
                                   string phone,
                                   string email,
                                   string dept)
        {
            Employee emp = new Employee();
            emp.Title = title;
            emp.Firstname = first;
            emp.Lastname = last;
            emp.Phoneno = phone;
            emp.Email = email;
            Department dep = ctx.Departments.AsQueryable<Department>().FirstOrDefault(d => d.DepartmentName == dept);
            emp.DepartmentId = dep._id;
            ctx.Save<Employee>(emp, "employees");
        }

    }
}
