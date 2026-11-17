using System.Data;
using ADONETSemester1.Models;
namespace ADONETSemester1.Repositories;

public sealed class StudentRepository
{
    public DataTable GetAll() => Db.Load(Db.University(),
        "SELECT StudentId, FirstName, LastName, Age, Email FROM Students ORDER BY StudentId");
    public void Add(Student student) => Db.Execute(Db.University(),
        "INSERT INTO Students (FirstName, LastName, Age, Email, FacultyId) VALUES (@first, @last, @age, @email, NULL)",
        Db.P("@first", student.FirstName), Db.P("@last", student.LastName),
        Db.P("@age", student.Age), Db.P("@email", student.Email));
}
