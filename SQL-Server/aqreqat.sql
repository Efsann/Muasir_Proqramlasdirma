SELECT COUNT(*) AS TotalStudents
FROM Students;
SELECT 
    AVG(Age * 1.0) AS AverageAge,
    MIN(Age) AS MinAge,
    MAX(Age) AS MaxAge
FROM Students;
SELECT 
    FacultyId, 
    COUNT(*) AS StudentCount
FROM Students
GROUP BY FacultyId;
SELECT 
    StudentId,
    AVG(Score * 1.0) AS AverageScore
FROM Grades 
GROUP BY StudentId
HAVING AVG(Score * 1.0) > 70;
SELECT 
    StudentId,
    AVG(CAST(Score AS FLOAT)) AS AverageScore
FROM Grades -- və ya Scores
GROUP BY StudentId
HAVING AVG(CAST(Score AS FLOAT)) > 70;