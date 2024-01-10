namespace XPathXQuery.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using XPathXQuery.Models;

public partial class VMMoviesCRUD : VMBase
{
    #region Variables

    private ModeWindow _mode = ModeWindow.Create;
    private Models.Movie _movieOld = new Movie();

    [ObservableProperty]
    private string id = string.Empty;

    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private string year = string.Empty;

    [ObservableProperty]
    private string rating = string.Empty;

    [ObservableProperty]
    private string runtime = string.Empty;

    [ObservableProperty]
    private string genre = string.Empty;

    [ObservableProperty]
    private string earnings_rank = string.Empty;

    [ObservableProperty]
    private ObservableCollection<Models.Actor> _actors = new ObservableCollection<Actor>();

    [ObservableProperty]
    private ObservableCollection<Models.Actor> _actors2 = new ObservableCollection<Actor>();

    [ObservableProperty]
    private ObservableCollection<Models.Director> _directors = new ObservableCollection<Director>();

    [ObservableProperty]
    private ObservableCollection<Models.Director> _directors2 = new ObservableCollection<Director>();

    private Repository r = new Repository();

    #endregion

    public VMMoviesCRUD()
    {
        Id = string.Empty;
        Name = string.Empty;
        Year = string.Empty;
        Rating = string.Empty;
        Runtime = string.Empty;
        Genre = string.Empty;
        Earnings_rank = string.Empty;
        PopulatePeople();
    }

    public void GetMovie(Models.Movie movie, ModeWindow mode)
    {
        _mode = mode;
        Actors2 = new ObservableCollection<Actor>();
        Directors2 = new ObservableCollection<Director>();
        if (_mode == ModeWindow.Update)
        {
            _movieOld = movie;
            Id = movie.Id;
            Name = movie.Name;
            Year = movie.Year.ToString();
            Rating = movie.Rating;
            Runtime = movie.Runtime.ToString();
            Genre = movie.Genre;
            Earnings_rank = movie.Earnings_rank.ToString();
            movie.Actors.ForEach(x => Actors2.Add(x));
            movie.Directors.ForEach(x => Directors2.Add(x));
            PopulatePeople();
        }
    }

    public void Accept()
    {
        try
        {
            Movie movie = new Models.Movie(Id, Name, Convert.ToInt32(Year), Rating, Convert.ToInt32(Runtime), Genre, Convert.ToInt32(Earnings_rank), Actors2.ToList(), Directors2.ToList());
            if (_mode == ModeWindow.Update)
            {
                r.UpdateMovie(movie, _movieOld);
            }
            else
            {
                r.InsertMovie(movie);
                _mode = ModeWindow.Update;
            }
            GetMovie(movie, _mode);
            Dialogs.GenerateMessage(System.Windows.MessageBoxImage.Information, "Movie accepted successfully.");
        }
        catch (Exception)
        {
            Dialogs.GenerateMessage(System.Windows.MessageBoxImage.Error, "Error accepting the movie!");
        }
    }

    public void AddActor(Actor p)
    {
        Actors2.Add(p);
        Actors.Remove(p);
    }

    public void RemoveActor(Actor p)
    {
        Actors2.Remove(p);
        Actors.Add(p);
    }

    public void AddDirector(Director p)
    {
        Directors2.Add(p);
        Directors.Remove(p);
    }

    public void RemoveDirector(Director p)
    {
        Directors2.Remove(p);
        Directors.Add(p);
    }

    public void PopulatePeople()
    {
        Repository.Connect();
        Actors = new ObservableCollection<Actor>();
        foreach (Person person in r.SearchPerson().Where(x => x.HasActed))
        {
            Actors.Add(new Actor(person));
        }
        Directors = new ObservableCollection<Director>();
        foreach (Person person in r.SearchPerson().Where(x => x.HasDirected))
        {
            Directors.Add(new Director(person));
        }
        if (_mode == ModeWindow.Update)
        {
            foreach (Actor actor in Actors2)
            {
                Actors.Remove(actor);
            }
            foreach (Director director in Directors2)
            {
                Directors.Remove(director);
            }
        }
    }

}
