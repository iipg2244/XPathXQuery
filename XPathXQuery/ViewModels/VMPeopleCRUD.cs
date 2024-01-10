namespace XPathXQuery.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using XPathXQuery.Models;
using Person = XPathXQuery.Models.Person;

public partial class VMPeopleCRUD : VMBase
{
    #region Variables

    private ModeWindow _mode = ModeWindow.Create;
    private string _idOld = string.Empty;

    [ObservableProperty]
    private string id = string.Empty;

    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private string pob = string.Empty;

    [ObservableProperty]
    private string data = string.Empty;

    [ObservableProperty]
    private bool actor = false;

    [ObservableProperty]
    private bool director = false;

    private Repository r = new Repository();

    #endregion

    public VMPeopleCRUD()
    {
        Id = string.Empty;
        Name = string.Empty;
        Data = string.Empty;
        Pob = string.Empty;
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
