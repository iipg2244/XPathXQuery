namespace XPathXQuery.Views;

using XPathXQuery.Models;
using XPathXQuery.ViewModels;

/// <summary>
/// Lógica de interacción para VMainWindow.xaml
/// </summary>
public partial class VMainWindow : Window
{
    public VMainWindow() => InitializeComponent();

    private void GetPeopleMovie(object sender, RoutedEventArgs e)
    {
        if (Movies != null && Movies.SelectedItems != null)
        {
            if (Movies.SelectedItems.Count > 0)
            {
                VPeople window = new VPeople(Repository.FromIListToList<Movie>(Movies.SelectedItems));
                window.Show();
            }
            else
            {
                Dialogs.GenerateMessage(MessageBoxImage.Warning, "Please select movies first!");
            }
        }
    }

    private void GetPeopleOscar(object sender, RoutedEventArgs e)
    {
        if (Oscars != null && Oscars.SelectedItems != null)
        {
            if (Oscars.SelectedItems.Count > 0)
            {
                VPeople window = new VPeople(Repository.FromIListToList<Oscar>(Oscars.SelectedItems));
                window.Show();
            }
            else
            {
                Dialogs.GenerateMessage(MessageBoxImage.Warning, "Please select oscars first!");
            }
        }
    }

    private void GetMoviesPerson(object sender, RoutedEventArgs e)
    {
        if (People != null && People.SelectedItems != null)
        {
            if (People.SelectedItems.Count > 0)
            {
                VMovies window = new VMovies(Repository.FromIListToList<Person>(People.SelectedItems));
                window.Show();
            }
            else
            {
                Dialogs.GenerateMessage(MessageBoxImage.Warning, "Please select people first!");
            }
        }
    }

    private void GetMoviesOscar(object sender, RoutedEventArgs e)
    {
        if (Oscars != null && Oscars.SelectedItems != null)
        {
            if (Oscars.SelectedItems.Count > 0)
            {
                VMovies window = new VMovies(Repository.FromIListToList<Oscar>(Oscars.SelectedItems));
                window.Show();
            }
            else
            {
                Dialogs.GenerateMessage(MessageBoxImage.Warning, "Please select oscars first!");
            }
        }
    }

    private void GetOscarsPerson(object sender, RoutedEventArgs e)
    {
        if (People != null && People.SelectedItems != null)
        {
            if (People.SelectedItems.Count > 0)
            {
                VOscars window = new VOscars(Repository.FromIListToList<Person>(People.SelectedItems));
                window.Show();
            }
            else
            {
                Dialogs.GenerateMessage(MessageBoxImage.Warning, "Please select people first!");
            }
        }
    }

    private void GetOscarsMovie(object sender, RoutedEventArgs e)
    {
        if (Movies != null && Movies.SelectedItems != null)
        {
            if (Movies.SelectedItems.Count > 0)
            {
                VOscars window = new VOscars(Repository.FromIListToList<Movie>(Movies.SelectedItems));
                window.Show();
            }
            else
            {
                Dialogs.GenerateMessage(MessageBoxImage.Warning, "Please select movies first!");
            }
        }
    }

    private void GenerateXML(object sender, RoutedEventArgs e)
    {
        if (Oscars != null && Oscars.SelectedItems != null)
        {
            if (Oscars.SelectedItems.Count > 0)
            {
                ((VMMainWindow)this.DataContext).GenerateXML(Repository.FromIListToList<Oscar>(Oscars.SelectedItems));
            }
            else
            {
                Dialogs.GenerateMessage(MessageBoxImage.Warning, "Please select oscars first!");
            }
        }
    }

    private void InsertPerson(object sender, RoutedEventArgs e)
    {
        VPeopleCRUD window = new VPeopleCRUD(((Person)People.SelectedItem), ModeWindow.Create);
        window.ShowDialog();
        ((VMMainWindow)this.DataContext).PopulatePeople();
    }

    private void UpdatePerson(object sender, RoutedEventArgs e)
    {
        if (People != null && People.SelectedItems != null && People.SelectedItems.Count == 1)
        {
            VPeopleCRUD window = new VPeopleCRUD(((Person)People.SelectedItem), ModeWindow.Update);
            window.ShowDialog();
        }
        ((VMMainWindow)this.DataContext).PopulatePeople();
    }

    private void DeletePerson(object sender, RoutedEventArgs e)
    {
        if (People != null && People.SelectedItems != null)
        {
            if (People.SelectedItems.Count == 1)
            {
                ((VMMainWindow)this.DataContext).DeletePerson(((Person)People.SelectedItem));
            }
            else
            {
                Dialogs.GenerateMessage(MessageBoxImage.Warning, "Please select a person first!");
            }
        }
    }

    private void InsertMovie(object sender, RoutedEventArgs e)
    {
        VMoviesCRUD window = new VMoviesCRUD(((Movie)Movies.SelectedItem), ModeWindow.Create);
        window.ShowDialog();
        ((VMMainWindow)this.DataContext).PopulateMovies();
    }

    private void UpdateMovie(object sender, RoutedEventArgs e)
    {
        if (Movies != null && Movies.SelectedItems != null && Movies.SelectedItems.Count == 1)
        {
            VMoviesCRUD window = new VMoviesCRUD(((Movie)Movies.SelectedItem), ModeWindow.Update);
            window.ShowDialog();
        }
        ((VMMainWindow)this.DataContext).PopulateMovies();
    }

    private void DeleteMovie(object sender, RoutedEventArgs e)
    {
        if (Movies != null && Movies.SelectedItems != null)
        {
            if (Movies.SelectedItems.Count == 1)
            {
                ((VMMainWindow)this.DataContext).DeleteMovie(((Movie)Movies.SelectedItem));
            }
            else
            {
                Dialogs.GenerateMessage(MessageBoxImage.Warning, "Please select a movie first!");
            }               
        }
    }

}
