namespace XPathXQuery.Views;

using XPathXQuery.Models;
using XPathXQuery.ViewModels;

/// <summary>
/// Lógica de interacción para VOscars.xaml
/// </summary>
public partial class VOscars : Window
{
    public VOscars() => InitializeComponent();

    public VOscars(List<Person> lpeople)
    {
        InitializeComponent();
       ( (VMOscars) this.DataContext).GetPeople(lpeople);
    }

    public VOscars(List<Movie> lmovies)
    {
        InitializeComponent();
        ((VMOscars)this.DataContext).GetMovies(lmovies);
    }

}
