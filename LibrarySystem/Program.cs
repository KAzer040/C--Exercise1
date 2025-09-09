using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public enum ItemType { Novels, Magazine, TextBook }

public abstract class LibraryItem
{
    private int id;
    private string title;

    public int Id => id;
    public string Title => title;
    public ItemType Type { get; }

    public LibraryItem(int id, string title, ItemType type)
    {
        this.id = id;
        this.title = title;
        this.Type = type;
    }
    public abstract string GetDetails();
}

public class Novels : LibraryItem
{
    private string author;
    public string Author => author;

    public Novels(int id, string title, string author) : base(id, title, ItemType.Novels)
    {
        this.author = author;
    }
    public override string GetDetails() => $"Novel: {Title}, Author: {Author}";
}

public class Magazine : LibraryItem
{
    private readonly int issueNumber;   
    public int IssueNumber => issueNumber;

    public Magazine(int id, string title, int issueNumber)  
        : base(id, title, ItemType.Magazine)
        => this.issueNumber = issueNumber;

    public override string GetDetails()
        => $"Magazine: {Title}, Issue Number: {IssueNumber}";
}
public class TextBook : LibraryItem
{
    private string publisher;
    public string Publisher => publisher;

    public TextBook(int id, string title, string publisher) : base(id, title, ItemType.TextBook)
    {
        this.publisher = publisher;
    }
    public override string GetDetails() => $"Textbook: {Title}, Publisher: {publisher}";
}

public class Member
{
    private string name;
    private List<LibraryItem> borrowedItems = new();

    public Member(string name)
    {
        this.name = name;
    }
    public string Name => name;
    public string BorrowItem(LibraryItem item)
    {
        if (borrowedItems.Count >= 3)
            return "You cannot borrow more than 3 items.";
        borrowedItems.Add(item);
        return $"Item '{item.Title}' has been added to {name}'s borrowed books.";
    }

    public string RetrunItems(LibraryItem item)
    {
        if (!borrowedItems.Contains(item))
            return $"Item '{item.Title}' was not in the list of borrowed items.";
        borrowedItems.Remove(item);
        return $"Item '{item.Title}' has been successfully returned.";

    }
    public List<LibraryItem> GetBorrowedItems() => borrowedItems;
}

public class LibraryManager
    {
        private List<LibraryItem> catalog = new();
        private List<Member> members = new();

        public void AddItem(LibraryItem item) => catalog.Add(item);
        public void RegisterMember(Member member) => members.Add(member);

        public void ShowCatalog()
        {
            Console.WriteLine("Library Catalog:");
            catalog.ForEach(i => Console.WriteLine(i.GetDetails()));
        }

        public LibraryItem? FindItemById(int id) => catalog.Find(i => i.Id == id);
        public Member? FindMemberByName(string name) => members.Find(m => m.Name == name);
    }

internal class Program
    {
        static void Main()
        {
            LibraryManager library = new();

            library.AddItem(new Novels(1, ""A Tale of Two Cities", "Charles Dickens"));
            library.AddItem(new Magazine(2, "SCIENCE", 42));
            library.AddItem(new TextBook(3, "Biology", "Culture"));

            Member alice = new("Alice");
            Member bob = new("Bob");
            library.RegisterMember(alice);
            library.RegisterMember(bob);

            library.ShowCatalog();

            for (int id = 1; id <= 3; id++)
            {
                var item = library.FindItemById(id);
                if (item != null) Console.WriteLine(alice.BorrowItem(item));
            }

            var newNovel = new Novels(4, "The Million Pound Note", "Mark Twain");
            library.AddItem(newNovel);
            var item4 = library.FindItemById(4);
            Console.WriteLine(alice.BorrowItem(item4));
        }
    }

