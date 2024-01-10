namespace XPathXQuery.Models;

[XmlRoot(ElementName = "movie")]
public class Movie : Thing
{
    [XmlElement(ElementName = "year")]
    public int Year { get; set; } = 0;
    [XmlElement(ElementName = "rating")]
    public string Rating { get; set; } = "0";
    [XmlElement(ElementName = "runtime")]
    public int Runtime { get; set; } = 0;
    [XmlElement(ElementName = "genre")]
    public string Genre { get; set; } = string.Empty;
    [XmlElement(ElementName = "earnings_rank")]
    public int Earnings_rank { get; set; } = 0;

    public List<Actor> Actors { get; set; } = new List<Actor>();
    public List<Director> Directors { get; set; } = new List<Director>();

    public Movie()
    {
    }

    public Movie(string id, string name, int year, string rating, int runtime, string genre, 
        int earnings_rank, List<Actor> actors, List<Director> directors)
    {
        Id = id;
        Name = name;
        Year = year;
        Rating = rating;
        Runtime = runtime;
        Genre = genre;
        Earnings_rank = earnings_rank;
        Actors = actors;
        Directors = directors;
    }

    public override bool Equals(object? obj) => obj is Movie movie &&
               Id == movie.Id &&
               Name == movie.Name &&
               Year == movie.Year &&
               Rating == movie.Rating &&
               Runtime == movie.Runtime &&
               Genre == movie.Genre &&
               Earnings_rank == movie.Earnings_rank &&
               EqualityComparer<List<Actor>>.Default.Equals(Actors, movie.Actors) &&
               EqualityComparer<List<Director>>.Default.Equals(Directors, movie.Directors);

    public override int GetHashCode()
    {
        HashCode hash = new HashCode();
        hash.Add(Id);
        hash.Add(Name);
        hash.Add(Year);
        hash.Add(Rating);
        hash.Add(Runtime);
        hash.Add(Genre);
        hash.Add(Earnings_rank);
        hash.Add(Actors);
        hash.Add(Directors);
        return hash.ToHashCode();
    }

}
