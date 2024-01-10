namespace XPathXQuery.Models;

[XmlRoot(ElementName = "actors")]
public class Actor : Thing
{
    public Actor()
    {
    }

    public Actor(Person p)
    {
        Id = p.Id;
        Name = p.Name;
    }

    public override bool Equals(object? obj) => obj is Actor actor &&
               Id == actor.Id &&
               Name == actor.Name;

    public override int GetHashCode() => HashCode.Combine(Id, Name);
}
