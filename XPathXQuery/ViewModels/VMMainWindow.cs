namespace XPathXQuery.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using XPathXQuery.Models;

public partial class VMMainWindow : VMBase
{
    #region Variables

    [ObservableProperty]
    private string filter1 = string.Empty;

    [ObservableProperty]
    private string filter2 = string.Empty;

    [ObservableProperty]
    private string filter3 = string.Empty;

    [ObservableProperty]
    private string filter4 = string.Empty;

    [ObservableProperty]
    private List<Models.Person> personas = new List<Models.Person>();

    [ObservableProperty]
    private List<Models.Movie> peliculas = new List<Models.Movie>();

    [ObservableProperty]
    private List<Models.Oscar> oscars = new List<Models.Oscar>();

    private Repository r = new Repository();

    #endregion

    public VMMainWindow()
    {
        Filter1 = string.Empty;
        Filter2 = string.Empty;
        Filter3 = string.Empty;
        Filter4 = string.Empty;
        PopulatePeople();
        PopulateMovies();
        PopulateOscars();
    }

    #region OnChanged

    partial void OnFilter1Changed(string value)
    {
        PopulatePeople();
    }

    partial void OnFilter2Changed(string value)
    {
        PopulateMovies();
    }

    partial void OnFilter3Changed(string value)
    {
        PopulateOscars();
    }

    partial void OnFilter4Changed(string value)
    {
        PopulateOscars();
    }

    #endregion

    public void PopulatePeople()
    {
        if (Filter1 != null)
        {
            Repository.Connect();
            Personas = r.SearchPerson(Filter1);
        }
    }

    public void PopulateMovies()
    {
        if (Filter2 != null)
        {
            Repository.Connect();
            Peliculas = r.SearchMovie(Filter2);
        }
    }

    public void PopulateOscars()
    {
        if (Filter3 != null && Filter4 != null)
        {
            Repository.Connect();
            Oscars = r.SearchOscars(Filter3, Filter4);
        }
    }

    public void GenerateXML(List<Oscar> loscars)
    {
        string? path = Repository.SelectPath("Select destination directory");
        if (!string.IsNullOrEmpty(path))
        {
            r.GenerateFile(loscars, path);
        }
    }

    public void DeletePerson(Models.Person person)
    {
        if (Dialogs.GenerateConfirmation($"The person {person.Name} will be deleted, do you want to continue?", "Delete Person", System.Windows.MessageBoxImage.Warning))
        {
            r.DeletePerson(person);
            PopulatePeople();
        }
    }

    public void DeleteMovie(Models.Movie movie)
    {
        if (Dialogs.GenerateConfirmation($"The movie {movie.Name} will be deleted, do you want to continue?", "Delete Movie", System.Windows.MessageBoxImage.Warning))
        {
            r.DeleteMovie(movie);
            PopulateMovies();
        }
    }

}
