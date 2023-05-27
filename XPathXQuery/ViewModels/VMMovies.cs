using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using XPathXQuery.Models;

namespace XPathXQuery.ViewModels
{
    public partial class VMMovies : VMBase
    {
        #region Variables

        [ObservableProperty]
        private List<Models.Movie> movies = new List<Movie>();

        private Repository r = new Repository();

        #endregion

        public VMMovies()
        {
        }

        public void GetPeople(List<Models.Person> lpeople)
        {
            Movies = r.AllMovie(lpeople);
        }

        public void GetOscars(List<Models.Oscar> loscars)
        {
            Movies = r.AllMovie(loscars);
        }

    }
}
