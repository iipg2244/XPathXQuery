namespace XPathXQuery.Models;

public interface IId
{
    string Id { get; set; }

    bool Equals(object? obj);
}
