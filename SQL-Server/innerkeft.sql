
SELECT 
    s.FirstName, 
    s.LastName, 
    f.FacultyName
FROM Students s
INNER JOIN Faculties f ON s.FacultyId = f.FacultyId;
SELECT 
    s.FirstName, 
    s.LastName, 
    f.FacultyName
FROM Students s
LEFT JOIN Faculties f ON s.FacultyId = f.FacultyId;
SELECT 
    f.FacultyName,
    COUNT(s.StudentId) AS StudentCount
FROM Faculties f
LEFT JOIN Students s ON f.FacultyId = s.FacultyId
GROUP BY f.FacultyId, f.FacultyName;