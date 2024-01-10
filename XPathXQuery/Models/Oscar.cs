namespace XPathXQuery.Models;

[XmlRoot(ElementName = "oscar")]
public class Oscar
{
    [XmlElement(ElementName = "year")]
    public int Year { get; set; } = 0;
    [XmlElement(ElementName = "type")]
    public string Type { get; set; } = string.Empty;
    [XmlElement(ElementName = "person")]
    public Thing? Person { get; set; } = null;
    [XmlElement(ElementName = "movie")]
    public Thing? Movie { get; set; } = null;

    public override bool Equals(object? obj) => obj is Oscar oscar &&
               Year == oscar.Year &&
               Type == oscar.Type &&
               EqualityComparer<Thing?>.Default.Equals(Person, oscar.Person) &&
               EqualityComparer<Thing?>.Default.Equals(Movie, oscar.Movie);

    public override int GetHashCode() => HashCode.Combine(Year, Type, Person, Movie);
}
