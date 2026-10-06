using System;
using System.Collections.Generic;
class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    private bool isAvailable;
    public Book(string title, string author, string isbn)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        isAvailable = true;
    }
    public void BorrowBook()
    {
        if (isAvailable)
        {
            isAvailable = false;
            Console.WriteLine($"'{Title}' kitabı götürüldü.");
        }
        else
        {
            Console.WriteLine($"'{Title}' kitabı artıq başqasındadır!");
        }
    }
    public void ReturnBook()
    {
        isAvailable = true;
        Console.WriteLine($"'{Title}' kitabı kitabxanaya qaytarıldı.");
    }
    public void DisplayInfo()
    {
        string status = isAvailable ? "Mövcuddur" : "Götürülüb";
        Console.WriteLine($"Kitab: {Title} | Müəllif: {Author} | ISBN: {ISBN} | Status: {status}");
    }
}
class Program
{
    static void Main()
    {
        List<Book> library = new List<Book>
        {
            new Book("1984", "George Orwell", "970451524935"),
            new Book("Xəmsə", "Nizami Gəncəvi", "9789952341234"),
            new Book("Şərq Qatarında Qətl", "Agatha Christie", "9700620731")
        };
        library[0].BorrowBook();
        library[2].BorrowBook();
        foreach (var book in library)
        {
            book.DisplayInfo();
        }
    }
}
