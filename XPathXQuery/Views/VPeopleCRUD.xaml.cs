namespace XPathXQuery.Views;

using XPathXQuery.Models;
using XPathXQuery.ViewModels;

/// <summary>
/// Lógica de interacción para VPeopleCRUD.xaml
/// </summary>
public partial class VPeopleCRUD : Window
{
    public VPeopleCRUD() => InitializeComponent();

    public VPeopleCRUD(Person person, ModeWindow mode)
    {
        InitializeComponent();
        ((VMPeopleCRUD)this.DataContext).GetPerson(person, mode);
    }

    private void acceptar(object sender, RoutedEventArgs e) => ((VMPeopleCRUD)this.DataContext).Accept();
}
