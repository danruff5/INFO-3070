using MongoDB.Driver;

namespace ExcercisesDAL
{
    public class DbContext
    {
        MongoClient client;
        MongoServer server;
        MongoDatabase db;

        public DbContext ()
        {
            client = new MongoClient();
            server = client.GetServer();
            db = server.GetDatabase("HelpdeskDB");
        }

        public MongoCollection<Employee> Employees
        {
            get
            {
                return db.GetCollection<Employee>("employees");
            }
        }
    }
}