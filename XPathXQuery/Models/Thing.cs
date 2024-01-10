namespace XPathXQuery.Models;

//[XmlRoot(ElementName = "movie")] && [XmlRoot(ElementName = "person")]
public class Thing : IId, IName
{
    [XmlElement(ElementName = "id")]
    public string Id
    {
        get => _id;
        set => _id = value;
    }

    [XmlElement(ElementName = "_id")]
    public string _id { get; set; } = string.Empty;
    [XmlElement(ElementName = "name")]
    public string Name { get; set; } = string.Empty;

    public Thing()
    {
    }

    public override string ToString() => $"{Id} - {Name}";

    public override bool Equals(object? obj) => obj is Thing thing &&
               Id == thing.Id &&
               _id == thing._id &&
               Name == thing.Name;

    public override int GetHashCode() => HashCode.Combine(Id, _id, Name);
}
