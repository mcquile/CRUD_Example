using Npgsql;
using NpgsqlTypes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace StudentResults
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ConnectDB connectDb = new ConnectDB().OpenConnection();
            Student student = StudentCRUD.ReadStudentByID(1, connectDb.connection);
            Console.WriteLine(student.ToString());
            connectDb.CloseConnection();
        }
    }
}

