using MongoDB.Driver;
using MongoDB.Kennedy;

namespace ExercisesDAL
{
    public class DbContext : ConcurrentDataContext
    {
        public DbContext (string databaseName = "HelpdeskDB", 
            string serverName = "localhost"
        ) : base(databaseName, serverName) { }

        public MongoCollection<Employee> Employees
        {
            get
            {
                return this.Db.GetCollection<Employee>("employees");
            }
        }

        public MongoCollection<Department> Departments
        {
            get
            {
                return this.Db.GetCollection<Department>("departments");
            }
        }
    }
}