using Npgsql;

namespace StudentResults;

internal class StudentCRUD
{
    /// <summary>
    /// Selects student entry by ID from database and returns a Student object
    /// </summary>
    /// <param name="id">int</param>
    /// <param name="connection">NpgsqlConnection</param>
    /// <returns>Student</returns>
    public static Student ReadStudentByID(int id,NpgsqlConnection connection)
    {
        string commandText = $@"SELECT * FROM ""{Constants._StudentTableName}"" WHERE ""StudentID"" = @id";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
        {
            cmd.Parameters.AddWithValue("id", id);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    Student student = Student.MapToStudent(reader);
                    return student;
                }
        }
        return new Student(0,"", "", DateTime.Now);
    }

    /// <summary>
    /// Inserts a student into Students table of StudentResultDB
    /// </summary>
    /// <param name="student">Student</param>
    /// <param name="connection">NpgsqlConnection</param>
    public static void CreateStudent(Student student, NpgsqlConnection connection)
    {
        string commandText = $@"INSERT INTO ""{Constants._StudentTableName}"" (""FirstName"", ""LastName"", ""DateOfBirth"") VALUES (@firstName, @lastName, @dateOfBirth)";
        using (var cmd = new NpgsqlCommand(commandText, connection))
        {
            cmd.Parameters.AddWithValue("firstName", student.firstName);
            cmd.Parameters.AddWithValue("lastName", student.lastName);
            cmd.Parameters.AddWithValue("dateOfBirth", student.dateOfBirth);

            cmd.ExecuteNonQuery();
        }
    }

    /// <summary>
    /// Updates the student entry corresponding to the Student instance passed as an argument
    /// </summary>
    /// <param name="student">Student</param>
    /// <param name="connection">NpgsqlConnection</param>
    public static void UpdateStudent(Student student, NpgsqlConnection connection)
    {
        var commandText = $@"UPDATE ""{Constants._StudentTableName}""
                SET ""FirstName"" = @firstName, ""LastName"" = @lastName, ""DateOfBirth"" = @dateOfBirth
                WHERE ""StudentID"" = @studentID";

        using (var cmd = new NpgsqlCommand(commandText, connection))
        {
            cmd.Parameters.AddWithValue("studentID", student.id);
            cmd.Parameters.AddWithValue("firstName", student.firstName);
            cmd.Parameters.AddWithValue("lastName", student.lastName);
            cmd.Parameters.AddWithValue("dateOfBirth", student.dateOfBirth);
                
            cmd.ExecuteNonQueryAsync();
        }
    }

    /// <summary>
    /// Deletes a student entry from Students table in StudentResultsDB corresponding to the ID provided
    /// </summary>
    /// <param name="id">int</param>
    /// <param name="connection">NpgsqlConnection</param>
    public static void DeleteStudentByID(int id, NpgsqlConnection connection)
    {
        string commandText = $@"DELETE FROM ""{Constants._StudentTableName}"" WHERE ""StudentID"" = @id";
        using (var cmd = new NpgsqlCommand(commandText, connection))
        {
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQueryAsync();
        }
    }
}