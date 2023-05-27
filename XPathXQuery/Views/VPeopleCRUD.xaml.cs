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
    /// Lógica de interacción para VPeopleCRUD.xaml
    /// </summary>
    public partial class VPeopleCRUD : Window
    {
        public VPeopleCRUD()
        {
            InitializeComponent();
        }

        public VPeopleCRUD(Person person, ModeWindow mode)
        {
            InitializeComponent();
            ((VMPeopleCRUD)this.DataContext).GetPerson(person, mode);
        }

        private void acceptar(object sender, RoutedEventArgs e)
        {
            ((VMPeopleCRUD)this.DataContext).Accept();
        }
    }
}
