using MongoDB.Bson;

namespace ExcercisesDAL
{
    public class Employee
    {
        public ObjectId _id { get; set; }
        public ObjectId DepartmentId { get; set; }
        public string Title { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phoneno { get; set; }
        public string Entity64 { get; set; }

        public Employee (string name)
        {
            Firstname = name;
        }
    }
}