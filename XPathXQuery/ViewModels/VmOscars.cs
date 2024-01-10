namespace XPathXQuery.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using XPathXQuery.Models;

public partial class VMOscars : VMBase
{
    #region Variables

    [ObservableProperty]
    private List<Models.Oscar> oscars = new List<Oscar>();

    private Repository r = new Repository();

    #endregion

    public VMOscars()
    {
    }

    public void GetPeople(List<Models.Person> lpeople) => Oscars = r.AllOscars(lpeople);

    public void GetMovies(List<Models.Movie> lmovies) => Oscars = r.AllOscars(lmovies);

}
