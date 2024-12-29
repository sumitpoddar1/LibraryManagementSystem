using System.Dynamic;
using System.Collections.Generic;

public class Book
{
    public int BookID { get; private set; }
    public string Title { get; private set; }
    public string Author { get; private set; }
    public bool IsBorrowed { get; private set; }

    public Book(int bookID, string title, string author )
    {
        BookID = bookID;
        Title = title;
        Author = author;
        IsBorrowed = false;
    }

    public void Borrow()
    {
        if (IsBorrowed)
            throw new InvalidOperationException("Book is already borrowed.");
        IsBorrowed = true;
    }

    public void Return()
    {
        if (!IsBorrowed)
            throw new InvalidOperationException("Book is not currently borrowed.");
        IsBorrowed = false;
    }

    public override string ToString()
    {
        return $"BookID: {BookID}, Title: {Title}, Author: {Author}, Borrowed: {IsBorrowed}";
    }
}

// Encapsulates member details and their borrowed books
public class Member
{
    public int MemberID { get; private set; }
    public string Name { get; private set; }
    private List<Book> BorrowedBooks;

    public Member(int memberID, string name)
    {
        MemberID = memberID;
        Name = name;
        BorrowedBooks = new List<Book>();
    }

    public void BorrowBook(Book book)
    {
        if (book.IsBorrowed)
            throw new InvalidOperationException("This book is already borrowed.");
        book.Borrow();
        BorrowedBooks.Add(book);
    }

    public void ReturnBook(Book book)
    {
        if (!BorrowedBooks.Contains(book))
            throw new InvalidOperationException("This book was not borrowed by this member.");
        book.Return();
        BorrowedBooks.Remove(book);
    }

    public override string ToString()
    {
        return $"MemberID: {MemberID}, Name: {Name}, Borrowed Books: {BorrowedBooks.Count}";
    }

    // Library Class
   // Manages books, members, and the borrowing/returning process.using System.Collections.Generic;

public class Library
{
    private List<Book> books = new List<Book>();
    private List<Member> members = new List<Member>();

    public void AddBook(Book book)
    {
        books.Add(book);
        Console.WriteLine($"Added book: {book.Title}");
    }

    public void AddMember(Member member)
    {
        members.Add(member);
        Console.WriteLine($"Added member: {member.Name}");
    }

    public void ListBooks()
    {
        Console.WriteLine("Books in the Library:");
        foreach (var book in books)
            Console.WriteLine(book);
    }

    public void ListMembers()
    {
        Console.WriteLine("Members in the Library:");
        foreach (var member in members)
            Console.WriteLine(member);
    }

    public void IssueBook(int bookID, int memberID)
    {
        var book = books.Find(b => b.BookID == bookID);
        var member = members.Find(m => m.MemberID == memberID);

        if (book == null) throw new InvalidOperationException("Book not found.");
        if (member == null) throw new InvalidOperationException("Member not found.");

        member.BorrowBook(book);
        Console.WriteLine($"Book '{book.Title}' issued to '{member.Name}'.");
    }

    public void ReturnBook(int bookID, int memberID)
    {
        var book = books.Find(b => b.BookID == bookID);
        var member = members.Find(m => m.MemberID == memberID);

        if (book == null || member == null)
            throw new InvalidOperationException("Book or Member not found.");

        member.ReturnBook(book);
        Console.WriteLine($"Book '{book.Title}' returned by '{member.Name}'.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Library library = new Library();

        // Add books
        library.AddBook(new Book(1, "The Great Gatsby", "F. Scott Fitzgerald"));
        library.AddBook(new Book(2, "1984", "George Orwell"));
        library.AddBook(new Book(3, "To Kill a Mockingbird", "Harper Lee"));

        // Add members
        library.AddMember(new Member(1, "Alice"));
        library.AddMember(new Member(2, "Bob"));

        // List books and members
        library.ListBooks();
        library.ListMembers();

        // Issue a book
        library.IssueBook(1, 1);

        // List books after issuing
        library.ListBooks();

        // Return a book
        library.ReturnBook(1, 1);

        // List books after returning
        library.ListBooks();
    }
}


}
