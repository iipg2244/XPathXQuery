using CommunityToolkit.Mvvm.ComponentModel;
using DocumentFormat.OpenXml.Office2013.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using XPathXQuery.Models;

namespace XPathXQuery.ViewModels
{
    public partial class VMPeople : VMBase
    {
        #region Variables

        [ObservableProperty]
        private List<Models.Person> people = new List<Models.Person>();

        private Repository r = new Repository();

        #endregion

        public VMPeople()
        {
        }

        public void GetMovies(List<Movie> lmovies)
        {
            People = r.AllPerson(lmovies);
        }

        public void GetOscars(List<Oscar> loscars)
        {
            People = r.AllPerson(loscars);
        }

    }
}
