using System.Data;
using Microsoft.Data.SqlClient;
using ADONETSemester1.Models;
using ADONETSemester1.Repositories;

namespace ADONETSemester1;

public sealed class MainForm : Form
{
    private readonly CourseRepository courses = new();
    private readonly StudentRepository students = new();
    private readonly ComboBox gradeStudent = Combo(), gradeCourse = Combo();
    private readonly TextBox scoreText = new() { Width = 85 };
    private readonly Label averageLabel = new() { AutoSize = true, Text = "Orta bal: —" };
    private readonly DataGridView gradesGrid = Grid();
    private readonly TextBox courseTitle = new() { Width = 180 }, courseCredits = new() { Width = 65 };
    private readonly Label courseCount = new() { AutoSize = true };
    private readonly DataGridView coursesGrid = Grid();
    private readonly TextBox firstName = new() { Width = 120 }, lastName = new() { Width = 120 }, ageText = new() { Width = 70 }, emailText = new() { Width = 190 };
    private readonly DataGridView studentsGrid = Grid();
    private readonly ComboBox bookAuthor = Combo(), loanBook = Combo(), loanMember = Combo();
    private readonly TextBox bookTitle = new() { Width = 160 }, bookYear = new() { Width = 65 }, bookPrice = new() { Width = 85 };
    private readonly DataGridView booksGrid = Grid(), loansGrid = Grid();
    private readonly TabControl tabs = new() { Dock = DockStyle.Fill };
    private readonly Button gradeAdd = Btn("Əlavə et"), gradeUpdate = Btn("Düzəlt"), gradeDelete = Btn("Sil");
    private readonly Button courseAdd = Btn("Əlavə et"), courseUpdate = Btn("Düzəlt"), courseDelete = Btn("Sil");
    private readonly Button bookAdd = Btn("Əlavə et"), bookUpdate = Btn("Düzəlt"), bookDelete = Btn("Sil");
    private int? selectedGrade, selectedCourse, selectedBook;

    public MainForm()
    {
        Text = "ADO.NET — SQL Server | I Semestr";
        Width = 1100; Height = 780; MinimumSize = new Size(860, 600);
        Controls.Add(tabs);
        BuildGrades(); BuildCourses(); BuildStudents(); BuildLibrary();
        Shown += (_, _) => Safe(RefreshAll);
    }

    private static ComboBox Combo() => new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 175 };
    private static DataGridView Grid() => new()
    {
        Dock = DockStyle.Fill, ReadOnly = true, MultiSelect = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        AllowUserToAddRows = false, AllowUserToDeleteRows = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    };
    private static Button Btn(string name) => new() { Text = name, AutoSize = true, Height = 30 };
    private static Label Txt(string name) => new() { Text = name, AutoSize = true, Margin = new Padding(7, 9, 2, 2) };
    private static FlowLayoutPanel Bar(params Control[] children)
    {
        var p = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, WrapContents = true, Padding = new Padding(8) };
        p.Controls.AddRange(children);
        return p;
    }
    private static TabPage Page(string name, Control content)
    {
        var t = new TabPage(name);
        t.Controls.Add(content);
        return t;
    }
    private static Control Stack(DataGridView grid, params Control[] top)
    {
        var p = new Panel { Dock = DockStyle.Fill };
        p.Controls.Add(grid);
        // Controls are docked from the bottom of the z-order upwards.
        for (var i = top.Length - 1; i >= 0; i--) p.Controls.Add(top[i]);
        return p;
    }
    private static int Key(ComboBox combo) => Convert.ToInt32(combo.SelectedValue);
    private static int CellId(DataGridView grid, string name) => Convert.ToInt32(grid.CurrentRow!.Cells[name].Value);
    private static void Bind(ComboBox combo, DataTable data, string label, string key)
    {
        combo.DataSource = data;
        combo.DisplayMember = label;
        combo.ValueMember = key;
    }
    private static void Safe(Action action)
    {
        try { action(); }
        catch (Exception ex) { MessageBox.Show(ex.Message, "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }

    // Tapşırıq 1 — qiymət idarəetməsi
    private void BuildGrades()
    {
        var bar = Bar(Txt("Tələbə:"), gradeStudent, Txt("Fənn:"), gradeCourse, Txt("Qiymət:"), scoreText,
            gradeAdd, gradeUpdate, gradeDelete, averageLabel);
        tabs.TabPages.Add(Page("1. Grades", Stack(gradesGrid, bar)));
        gradeAdd.Click += (_, _) => Safe(() => ChangeGrade("add"));
        gradeUpdate.Click += (_, _) => Safe(() => ChangeGrade("update"));
        gradeDelete.Click += (_, _) => Safe(() => ChangeGrade("delete"));
        gradeStudent.SelectedValueChanged += (_, _) => Safe(ShowAverage);
        gradesGrid.SelectionChanged += (_, _) =>
        {
            if (gradesGrid.CurrentRow == null || !gradesGrid.Columns.Contains("GradeId")) return;
            selectedGrade = CellId(gradesGrid, "GradeId");
            gradeStudent.SelectedValue = gradesGrid.CurrentRow.Cells["StudentId"].Value;
            gradeCourse.SelectedValue = gradesGrid.CurrentRow.Cells["CourseId"].Value;
            scoreText.Text = gradesGrid.CurrentRow.Cells["Score"].Value?.ToString() ?? "";
        };
    }
    private void RefreshGrades()
    {
        var oldStudent = gradeStudent.SelectedValue;
        Bind(gradeStudent, Db.Load(Db.University(),
            "SELECT StudentId, FirstName + N' ' + LastName AS FullName FROM Students ORDER BY FirstName"), "FullName", "StudentId");
        if (oldStudent is int studentId) gradeStudent.SelectedValue = studentId;
        Bind(gradeCourse, Db.Load(Db.University(),
            "SELECT CourseId, CourseName FROM Courses ORDER BY CourseName"), "CourseName", "CourseId");
        gradesGrid.DataSource = Db.Load(Db.University(), @"
            SELECT g.GradeId, s.StudentId, c.CourseId,
                   s.FirstName + N' ' + s.LastName AS StudentName,
                   c.CourseName, g.Score
            FROM Grades g
            INNER JOIN Students s ON s.StudentId = g.StudentId
            INNER JOIN Courses c ON c.CourseId = g.CourseId
            ORDER BY g.GradeId");
        gradesGrid.Columns["GradeId"].Visible = false;
        gradesGrid.Columns["StudentId"].Visible = false;
        gradesGrid.Columns["CourseId"].Visible = false;
        ShowAverage();
    }
    private void ShowAverage()
    {
        if (gradeStudent.SelectedValue is not int studentId) { averageLabel.Text = "Orta bal: —"; return; }
        object? result = Db.Scalar(Db.University(),
            "SELECT AVG(Score * 1.0) FROM Grades WHERE StudentId=@student", Db.P("@student", studentId));
        averageLabel.Text = result == null || result == DBNull.Value
            ? "Qiymət yoxdur" : $"Orta bal: {Convert.ToDecimal(result):0.00}";
    }
    private void ChangeGrade(string op)
    {
        if (op != "add" && selectedGrade == null) { MessageBox.Show("Əvvəl sətir seçin."); return; }
        if (op == "delete")
        {
            Db.Execute(Db.University(), "DELETE FROM Grades WHERE GradeId=@id", Db.P("@id", selectedGrade));
        }
        else
        {
            if (gradeStudent.SelectedValue is not int sid || gradeCourse.SelectedValue is not int cid)
            { MessageBox.Show("Tələbə və fənn seçin."); return; }
            if (!decimal.TryParse(scoreText.Text, out decimal score) || score < 0 || score > 100)
            { MessageBox.Show("Qiymət 0–100 arasında olmalıdır."); return; }
            if (op == "add")
                Db.Execute(Db.University(), "INSERT INTO Grades (StudentId, CourseId, Score) VALUES (@s,@c,@v)",
                    Db.P("@s", sid), Db.P("@c", cid), Db.P("@v", score));
            else
                Db.Execute(Db.University(), "UPDATE Grades SET StudentId=@s, CourseId=@c, Score=@v WHERE GradeId=@id",
                    Db.P("@s", sid), Db.P("@c", cid), Db.P("@v", score), Db.P("@id", selectedGrade));
        }
        selectedGrade = null;
        RefreshGrades();
    }

    // Tapşırıq 2 — Courses CRUD, repository və 547 xətası
    private void BuildCourses()
    {
        var bar = Bar(Txt("Fənn:"), courseTitle, Txt("Kredit (1–10):"), courseCredits,
            courseAdd, courseUpdate, courseDelete, courseCount);
        tabs.TabPages.Add(Page("2. Courses", Stack(coursesGrid, bar)));
        courseAdd.Click += (_, _) => Safe(() => ChangeCourse("add"));
        courseUpdate.Click += (_, _) => Safe(() => ChangeCourse("update"));
        courseDelete.Click += (_, _) => Safe(() => ChangeCourse("delete"));
        coursesGrid.SelectionChanged += (_, _) =>
        {
            if (coursesGrid.CurrentRow == null || !coursesGrid.Columns.Contains("Id")) return;
            selectedCourse = CellId(coursesGrid, "Id");
            courseTitle.Text = coursesGrid.CurrentRow.Cells["Title"].Value?.ToString() ?? "";
            courseCredits.Text = coursesGrid.CurrentRow.Cells["Credits"].Value?.ToString() ?? "";
        };
    }
    private void RefreshCourses()
    {
        coursesGrid.DataSource = courses.GetAll();
        courseCount.Text = $"Fənn sayı: {courses.Count()}";
    }
    private void ChangeCourse(string op)
    {
        if (op != "add" && selectedCourse == null) { MessageBox.Show("Əvvəl fənn seçin."); return; }
        if (op != "delete" && (string.IsNullOrWhiteSpace(courseTitle.Text) ||
            !int.TryParse(courseCredits.Text, out int credit) || credit < 1 || credit > 10))
        { MessageBox.Show("Fənn adı yazın, kredit 1–10 arasında tam ədəd olmalıdır."); return; }
        try
        {
            var course = new Course { Id = selectedCourse ?? 0, Title = courseTitle.Text.Trim(), Credits = int.TryParse(courseCredits.Text, out var n) ? n : 0 };
            if (op == "add") courses.Add(course);
            else if (op == "update") courses.Update(course);
            else courses.Delete(course.Id);
        }
        catch (SqlException ex) when (ex.Number == 547)
        {
            MessageBox.Show("Bu fənnə aid qiymətlər mövcuddur. Əvvəl əlaqəli qiymətləri silin.");
            return;
        }
        selectedCourse = null;
        RefreshCourses(); RefreshGrades();
    }

    // Tapşırıq 3 — tələbə yaratmaq, TryParse və unikal Email
    private void BuildStudents()
    {
        var add = Btn("Əlavə et");
        var bar = Bar(Txt("Ad:"), firstName, Txt("Soyad:"), lastName, Txt("Yaş:"), ageText,
            Txt("E-poçt:"), emailText, add);
        tabs.TabPages.Add(Page("3. Students", Stack(studentsGrid, bar)));
        add.Click += (_, _) => Safe(AddStudent);
    }
    private void RefreshStudents() => studentsGrid.DataSource = students.GetAll();
    private void AddStudent()
    {
        if (string.IsNullOrWhiteSpace(firstName.Text) || string.IsNullOrWhiteSpace(lastName.Text))
        { MessageBox.Show("Ad və soyad boş ola bilməz."); return; }
        if (!int.TryParse(ageText.Text, out int age) || age <= 0 || age > 120)
        { MessageBox.Show("Yaşı düzgün tam ədəd kimi daxil edin."); return; }
        if (string.IsNullOrWhiteSpace(emailText.Text))
        { MessageBox.Show("E-poçt daxil edin."); return; }
        try
        {
            students.Add(new Student { FirstName = firstName.Text.Trim(), LastName = lastName.Text.Trim(),
                Age = age, Email = emailText.Text.Trim() });
        }
        catch (SqlException ex) when (ex.Number is 2627 or 2601)
        {
            MessageBox.Show("Bu e-poçt artıq mövcuddur."); return;
        }
        RefreshStudents(); RefreshGrades();
        firstName.Clear(); lastName.Clear(); ageText.Clear(); emailText.Clear();
    }

    // Tapşırıq 4 — LibraryDB kitab CRUD + icarə və qaytarma
    private void BuildLibrary()
    {
        var bookBar = Bar(Txt("Kitab:"), bookTitle, Txt("İl:"), bookYear,
            Txt("Qiymət:"), bookPrice, Txt("Müəllif:"), bookAuthor, bookAdd, bookUpdate, bookDelete);
        var loanAdd = Btn("İcarəyə ver"), loanReturn = Btn("Qaytar");
        var loanBar = Bar(Txt("Kitab:"), loanBook, Txt("Üzv:"), loanMember, loanAdd, loanReturn);
        var container = new SplitContainer { Dock = DockStyle.Fill, Height = 650, Orientation = Orientation.Horizontal, SplitterDistance = 325 };
        container.Panel1.Controls.Add(Stack(booksGrid, bookBar));
        container.Panel2.Controls.Add(Stack(loansGrid, loanBar));
        tabs.TabPages.Add(Page("4. LibraryDB", container));
        bookAdd.Click += (_, _) => Safe(() => ChangeBook("add"));
        bookUpdate.Click += (_, _) => Safe(() => ChangeBook("update"));
        bookDelete.Click += (_, _) => Safe(() => ChangeBook("delete"));
        loanAdd.Click += (_, _) => Safe(CreateLoan);
        loanReturn.Click += (_, _) => Safe(ReturnLoan);
        booksGrid.SelectionChanged += (_, _) =>
        {
            if (booksGrid.CurrentRow == null || !booksGrid.Columns.Contains("BookId")) return;
            selectedBook = CellId(booksGrid, "BookId");
            bookTitle.Text = booksGrid.CurrentRow.Cells["Title"].Value?.ToString() ?? "";
            bookYear.Text = booksGrid.CurrentRow.Cells["PublishYear"].Value?.ToString() ?? "";
            bookPrice.Text = booksGrid.CurrentRow.Cells["Price"].Value?.ToString() ?? "";
            bookAuthor.SelectedValue = booksGrid.CurrentRow.Cells["AuthorId"].Value;
        };
    }
    private void RefreshLibrary()
    {
        Bind(bookAuthor, Db.Load(Db.Library(), "SELECT AuthorId, FullName FROM Authors ORDER BY FullName"), "FullName", "AuthorId");
        Bind(loanMember, Db.Load(Db.Library(), "SELECT MemberId, FullName FROM Members ORDER BY FullName"), "FullName", "MemberId");
        Bind(loanBook, Db.Load(Db.Library(), "SELECT BookId, Title FROM Books ORDER BY Title"), "Title", "BookId");
        booksGrid.DataSource = Db.Load(Db.Library(), @"
            SELECT b.BookId, b.Title, b.PublishYear, b.Price, b.AuthorId, a.FullName AS AuthorName
            FROM Books b INNER JOIN Authors a ON a.AuthorId = b.AuthorId ORDER BY b.BookId");
        booksGrid.Columns["AuthorId"].Visible = false;
        loansGrid.DataSource = Db.Load(Db.Library(), @"
            SELECT l.LoanId, b.Title AS BookTitle, m.FullName AS MemberName, l.LoanDate
            FROM Loans l
            INNER JOIN Books b ON b.BookId = l.BookId
            INNER JOIN Members m ON m.MemberId = l.MemberId
            WHERE l.ReturnDate IS NULL ORDER BY l.LoanDate");
    }
    private void ChangeBook(string op)
    {
        if (op != "add" && selectedBook == null) { MessageBox.Show("Əvvəl kitab seçin."); return; }
        if (op != "delete" && (string.IsNullOrWhiteSpace(bookTitle.Text) ||
            !int.TryParse(bookYear.Text, out int year) || year < 1 ||
            !decimal.TryParse(bookPrice.Text, out decimal price) || price <= 0 || bookAuthor.SelectedValue is not int))
        { MessageBox.Show("Kitab adı, il, müsbət qiymət və müəllif daxil edin."); return; }
        try
        {
            if (op == "delete") Db.Execute(Db.Library(), "DELETE FROM Books WHERE BookId=@id", Db.P("@id", selectedBook));
            else
            {
                const string insert = "INSERT INTO Books (Title, PublishYear, Price, AuthorId) VALUES (@title,@year,@price,@author)";
                const string update = "UPDATE Books SET Title=@title, PublishYear=@year, Price=@price, AuthorId=@author WHERE BookId=@id";
                Db.Execute(Db.Library(), op == "add" ? insert : update,
                    Db.P("@title", bookTitle.Text.Trim()), Db.P("@year", int.Parse(bookYear.Text)),
                    Db.P("@price", decimal.Parse(bookPrice.Text)), Db.P("@author", Key(bookAuthor)), Db.P("@id", selectedBook));
            }
        }
        catch (SqlException ex) when (ex.Number == 547)
        { MessageBox.Show("Bu kitaba aid icarə qeydləri var; kitabı silmək mümkün deyil."); return; }
        selectedBook = null;
        RefreshLibrary();
    }
    private void CreateLoan()
    {
        if (loanBook.SelectedValue is not int book || loanMember.SelectedValue is not int member)
        { MessageBox.Show("Kitab və üzv seçin."); return; }
        var active = Convert.ToInt32(Db.Scalar(Db.Library(),
            "SELECT COUNT(*) FROM Loans WHERE BookId=@book AND ReturnDate IS NULL", Db.P("@book", book)));
        if (active > 0) { MessageBox.Show("Bu kitab hələ qaytarılmayıb."); return; }
        Db.Execute(Db.Library(),
            "INSERT INTO Loans (BookId, MemberId, LoanDate, ReturnDate) VALUES (@book,@member,GETDATE(),NULL)",
            Db.P("@book", book), Db.P("@member", member));
        RefreshLibrary();
    }
    private void ReturnLoan()
    {
        if (loansGrid.CurrentRow == null) { MessageBox.Show("Qaytarılacaq icarəni seçin."); return; }
        int id = CellId(loansGrid, "LoanId");
        Db.Execute(Db.Library(), "UPDATE Loans SET ReturnDate=GETDATE() WHERE LoanId=@id AND ReturnDate IS NULL", Db.P("@id", id));
        RefreshLibrary();
    }
    private void RefreshAll()
    {
        RefreshStudents(); RefreshCourses(); RefreshGrades(); RefreshLibrary();
    }
}
