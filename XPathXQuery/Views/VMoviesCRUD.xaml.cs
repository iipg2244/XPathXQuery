namespace XPathXQuery.Views;

using XPathXQuery.Models;
using XPathXQuery.ViewModels;

/// <summary>
/// Lógica de interacción para VMoviesCRUD.xaml
/// </summary>
public partial class VMoviesCRUD : Window
{
    public VMoviesCRUD() => InitializeComponent();

    public VMoviesCRUD(Movie movie, ModeWindow mode)
    {
        InitializeComponent();
        ((VMMoviesCRUD)this.DataContext).GetMovie(movie, mode);
    }

    private void Accept(object sender, RoutedEventArgs e) => ((VMMoviesCRUD)this.DataContext).Accept();

    private void AddActor(object sender, RoutedEventArgs e)
    {
        if (Actors != null && Actors.SelectedItems != null)
        {
            if (Actors.SelectedItems.Count == 1)
            {
                ((VMMoviesCRUD)this.DataContext).AddActor(((Actor)Actors.SelectedItem));
            }
            else
            {
                Dialogs.GenerateMessage(MessageBoxImage.Warning, "Please select an actor first!");
            }
        }
    }

    private void RemoveActor(object sender, RoutedEventArgs e)
    {
        if (Actors2 != null && Actors2.SelectedItems != null)
        {
            if (Actors2.SelectedItems.Count == 1)
            {
                ((VMMoviesCRUD)this.DataContext).RemoveActor(((Actor)Actors2.SelectedItem));
            }
            else
            {
                Dialogs.GenerateMessage(MessageBoxImage.Warning, "Please select an actor first!");
            }
        }
    }

    private void AddDirector(object sender, RoutedEventArgs e)
    {
        if (Directors != null && Directors.SelectedItems != null)
        {
            if (Directors.SelectedItems.Count == 1)
            {
                ((VMMoviesCRUD)this.DataContext).AddDirector(((Director)Directors.SelectedItem));
            }
            else
            {
                Dialogs.GenerateMessage(MessageBoxImage.Warning, "Please select a director first!");
            }
        }
    }

    private void RemoveDirector(object sender, RoutedEventArgs e)
    {
        if (Directors2 != null && Directors2.SelectedItems != null)
        {
            if (Directors2.SelectedItems.Count == 1)
            {
                ((VMMoviesCRUD)this.DataContext).RemoveDirector(((Director)Directors2.SelectedItem));
            }
            else
            {
                Dialogs.GenerateMessage(MessageBoxImage.Warning, "Please select a director first!");
            }
        }
    }


}
