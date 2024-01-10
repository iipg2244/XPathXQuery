namespace XPathXQuery.Views;

using XPathXQuery.Models;
using XPathXQuery.ViewModels;

/// <summary>
/// Lógica de interacción para VPeople.xaml
/// </summary>
public partial class VPeople : Window
{
    public VPeople() => InitializeComponent();

    public VPeople(List<Movie> lmovies)
    {
        InitializeComponent();
       ( (VMPeople) this.DataContext).GetMovies(lmovies);
    }

    public VPeople(List<Oscar> loscars)
    {
        InitializeComponent();
        ((VMPeople)this.DataContext).GetOscars(loscars);
    }

}
