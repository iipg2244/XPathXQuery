using CommunityToolkit.Mvvm.ComponentModel;
using DocumentFormat.OpenXml.Bibliography;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using XPathXQuery.Models;
using Person = XPathXQuery.Models.Person;

namespace XPathXQuery.ViewModels
{
    public partial class VMPeopleCRUD : VMBase
    {
        #region Variables

        private ModeWindow _mode = ModeWindow.Create;
        private string _idOld = "";

        [ObservableProperty]
        private string id = "";

        [ObservableProperty]
        private string name = "";

        [ObservableProperty]
        private string pob = "";

        [ObservableProperty]
        private string data = "";

        [ObservableProperty]
        private bool actor = false;

        [ObservableProperty]
        private bool director = false;

        private Repository r = new Repository();

        #endregion

        public VMPeopleCRUD()
        {
            Id = "";
            Name = "";
            Data = "";
            Pob = "";
            Actor = false;
            Director = false;
        }

        public void GetPerson(Models.Person person, ModeWindow mode)
        {
            _mode = mode;
            if (_mode == ModeWindow.Update)
            {
                _idOld = person.Id;
                Id = person.Id;
                Name = person.Name;
                Data = person.Dob;
                Pob = person.Pob;
                Actor = person.HasActed;
                Director = person.HasDirected;
            }
        }

        public void Accept()
        {
            try
            {
                Person person = new Models.Person(Id, Name, Data, Pob, Actor, Director);
                if (_mode == ModeWindow.Update)
                {
                    r.UpdatePerson(person, _idOld);
                }
                else
                {
                    r.InsertPerson(person);
                    _mode = ModeWindow.Update;
                }
                GetPerson(person, _mode);
                Dialogs.GenerateMessage(System.Windows.MessageBoxImage.Information, "Person accepted successfully.");
            }
            catch (Exception)
            {
                Dialogs.GenerateMessage(System.Windows.MessageBoxImage.Error, "Error accepting the person!");
            }        
        }

    }
}
