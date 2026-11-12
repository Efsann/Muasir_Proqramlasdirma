
SELECT 
    s.FirstName, 
    s.LastName, 
    c.Title AS CourseName, 
    g.Score
FROM Grades g
INNER JOIN Students s ON g.StudentId = s.StudentId
INNER JOIN Courses c ON g.CourseId = c.CourseId;
SELECT 
    s.FirstName, 
    s.LastName, 
    c.Title AS CourseName, 
    g.Score
FROM Grades g
INNER JOIN Students s ON g.StudentId = s.StudentId
INNER JOIN Courses c ON g.CourseId = c.CourseId
WHERE c.Title = 'C#'
ORDER BY g.Score DESC;
SELECT 
    c.Title AS CourseName,
    AVG(g.Score * 1.0) AS AverageScore
FROM Courses c
INNER JOIN Grades g ON c.CourseId = g.CourseId
GROUP BY c.CourseId, c.Title;
SELECT 
    s.FirstName, 
    s.LastName
FROM Students s
LEFT JOIN Grades g ON s.StudentId = g.StudentId
WHERE g.GradeId IS NULL;