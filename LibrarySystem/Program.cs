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
    private string issuenumber;
    public string IssueNumberr => issuenumber;

    public Magazine(int id, string title, string issuenumber) : base(id, title, ItemType.Novels)
    {
        this.issuenumber = issuenumber;
    }
    public override string GetDetails() => $"Magazine: {Title}, IssueNumber: {issuenumber}";
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