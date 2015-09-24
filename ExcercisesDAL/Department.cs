using MongoDB.Bson;

namespace ExcercisesDAL
{
    public class Department
    {
        public ObjectId _id { get; set; }
        public string DepartmentName { get; set; }
    }
}
