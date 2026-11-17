using System.Data;
using ADONETSemester1.Models;
namespace ADONETSemester1.Repositories;

public sealed class CourseRepository
{
    public DataTable GetAll() => Db.Load(Db.University(),
        "SELECT CourseId AS Id, CourseName AS Title, Credits FROM Courses ORDER BY CourseId");
    public int Count() => Convert.ToInt32(Db.Scalar(Db.University(), "SELECT COUNT(*) FROM Courses"));
    public void Add(Course course) => Db.Execute(Db.University(),
        "INSERT INTO Courses (CourseName, Credits) VALUES (@title, @credits)",
        Db.P("@title", course.Title), Db.P("@credits", course.Credits));
    public void Update(Course course) => Db.Execute(Db.University(),
        "UPDATE Courses SET CourseName=@title, Credits=@credits WHERE CourseId=@id",
        Db.P("@title", course.Title), Db.P("@credits", course.Credits), Db.P("@id", course.Id));
    public void Delete(int id) => Db.Execute(Db.University(),
        "DELETE FROM Courses WHERE CourseId=@id", Db.P("@id", id));
}
