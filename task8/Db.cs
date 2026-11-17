using System.Data;
using Microsoft.Data.SqlClient;

namespace ADONETSemester1;

public static class Db
{
    // SQL Server Express üçün Data Source=.\\SQLEXPRESS ilə əvəz edə bilərsiniz.
    // SQL Server autentifikasiyasından istifadə edirsinizsə connection string-i dəyişin.
    private const string Server = @"Server=localhost;Integrated Security=True;TrustServerCertificate=True;";
    public static SqlConnection University() => new(Server + "Database=UniversityDB;");
    public static SqlConnection Library() => new(Server + "Database=LibraryDB;");

    public static DataTable Load(SqlConnection connection, string sql, params SqlParameter[] parameters)
    {
        using (connection)
        using (var command = new SqlCommand(sql, connection))
        {
            command.Parameters.AddRange(parameters);
            connection.Open();
            using var reader = command.ExecuteReader();
            var table = new DataTable();
            table.Load(reader);
            return table;
        }
    }

    public static int Execute(SqlConnection connection, string sql, params SqlParameter[] parameters)
    {
        using (connection)
        using (var command = new SqlCommand(sql, connection))
        {
            command.Parameters.AddRange(parameters);
            connection.Open();
            return command.ExecuteNonQuery();
        }
    }

    public static object? Scalar(SqlConnection connection, string sql, params SqlParameter[] parameters)
    {
        using (connection)
        using (var command = new SqlCommand(sql, connection))
        {
            command.Parameters.AddRange(parameters);
            connection.Open();
            return command.ExecuteScalar();
        }
    }

    public static SqlParameter P(string name, object? value) => new(name, value ?? DBNull.Value);
}
