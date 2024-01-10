namespace XPathXQuery.Views;

using XPathXQuery.Models;
using XPathXQuery.ViewModels;

/// <summary>
/// Lógica de interacción para VMovies.xaml
/// </summary>
public partial class VMovies : Window
{
    public VMovies() => InitializeComponent();

    public VMovies(List<Person> lpeople)
    {
        InitializeComponent();
       ( (VMMovies) this.DataContext).GetPeople(lpeople);
    }

    public VMovies(List<Oscar> loscars)
    {
        InitializeComponent();
        ((VMMovies)this.DataContext).GetOscars(loscars);
    }

}
