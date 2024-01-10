namespace XPathXQuery.Models;

[XmlRoot(ElementName = "directors")]
public class Director : Thing
{
    public Director()
    {
    }

    public Director(Person p)
    {
        Id = p.Id;
        Name = p.Name;
    }

    public override bool Equals(object? obj) => obj is Director director &&
               Id == director.Id &&
               Name == director.Name;

    public override int GetHashCode() => HashCode.Combine(Id, Name);
}
