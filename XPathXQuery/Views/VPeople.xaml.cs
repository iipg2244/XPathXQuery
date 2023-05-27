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
    /// Lógica de interacción para VPeople.xaml
    /// </summary>
    public partial class VPeople : Window
    {
        public VPeople()
        {
            InitializeComponent();
        }

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
}
