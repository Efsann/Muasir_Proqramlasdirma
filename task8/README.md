# C# — I Semestr | ADO.NET + SQL Server (Windows Forms)

Tapşırıq vərəqəsinin **4 bölməsi**: Grades idarəetməsi; Courses CRUD (repository); Students əlavə etmə; LibraryDB kitab CRUD və icarə/qaytarma. C# tətbiqi iki ayrı SQL Server bazasına qoşulur.

## Lazım olanlar
- Windows, Visual Studio 2022, .NET 8 SDK, SQL Server (və ya SQL Server Express), SSMS
- `Microsoft.Data.SqlClient` NuGet paketi (`.csproj` daxilində)

## Database qurulması — SSMS-də bu ardıcıllıqla
1. **Yeni bazalar üçün** `Sql/00_UniversityDB_Create_Seed.sql` icra et. `UniversityDB` və `Faculties`, `Students`, `Courses`, `Grades` cədvəllərini yaradır, boş cədvəlləri nümunə məlumatla doldurur.
2. `Sql/01_UniversityDB_Migration.sql` icra et. `Courses.Credits` (1–10), `Students.Email` (unikal) sütunlarını təmin edir.
3. `Sql/02_LibraryDB_Create_Seed.sql` icra et. `LibraryDB` və `Authors`, `Books`, `Members`, `Loans` cədvəllərini yaradır və boş cədvəllərə nümunə məlumat əlavə edir.

Əgər əvvəlki dərsdən bazalar artıq varsa, əvvəl onların ehtiyat nüsxəsini saxla. Skriptlər cədvəlləri silmir və mövcud məlumatları əvəz etmir. İlk universitet skriptinin qiymət nümunələri tələbə/fənn adlarına əsaslanır; mövcud bazada bu adlar yoxdursa həmin nümunələr əlavə edilməyəcək.

## C# proqramını başlatmaq
1. `Db.cs` daxilində `Server=localhost` hissəsini öz SQL Server instance adına uyğun dəyiş (məsələn, `Server=.\\SQLEXPRESS`). Windows autentifikasiyası üçün `Integrated Security=True` aktivdir.
2. Visual Studio-da `ADONET-Semester1.csproj` faylını aç, NuGet Restore et və F5 bas.
3. `Grades`, `Courses`, `Students`, `LibraryDB` tab-larında əməliyyatları sına.

## Tapşırıq uyğunluğu
- **1 — Grades:** Students/Courses ComboBox, 0–100 yoxlanışı, 3 cədvəl INNER JOIN, `ExecuteScalar` + `AVG(Score * 1.0)`, əlavə/düzəlt/sil.
- **2 — Courses:** `Course` modeli (`Id`, `Title`, `Credits`); `CourseRepository` (`GetAll`, `Add`, `Update`, `Delete`, `Count`); 547 FK xətası; Credits 1–10.
- **3 — Students:** `StudentRepository.Add(Student)` parametrli INSERT, `int.TryParse`, boş ad-soyad yoxlaması, 2627/2601 təkrarlanan e-poçt xətası.
- **4 — LibraryDB:** Books CRUD + Authors JOIN/ComboBox, `GETDATE()` ilə loan, ReturnDate yeniləmə, qaytarılmamış loans siyahısı.

## GitHub
Bütün mənbə fayllarını və `Sql/` qovluğunu birlikdə yüklə. `bin/`, `obj/`, `.vs/` və `*.user` faylları `.gitignore`-dadır. Real connection string parollarını GitHub-da paylaşma.

**Qeyd:** Windows / SQL Server ilə işləmə testi bu mühitdə aparılmayıb; yerli Visual Studio və SSMS-də yoxlamaq lazımdır. Bu tədris nümunəsidir.
