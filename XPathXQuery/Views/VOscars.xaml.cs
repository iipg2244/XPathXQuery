using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using XPathXQuery.Models;
using XPathXQuery.ViewModels;

namespace XPathXQuery.Views
{
    /// <summary>
    /// Lógica de interacción para VOscars.xaml
    /// </summary>
    public partial class VOscars : Window
    {
        public VOscars()
        {
            InitializeComponent();
        }

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
}
