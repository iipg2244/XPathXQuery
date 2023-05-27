using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

public class VMBase : ObservableObject
{
    public List<object> _listBackUp = new List<object>();

    public void CreateBackUp(object item)
    {
        _listBackUp.Add(item);
    }

    public void CreateBackUp(int index, object item)
    {
        try
        {
            _listBackUp.Insert(index, item);
            if (_listBackUp.Count - 1 != index)
                _listBackUp.RemoveAt(index + 1);
        }
        catch (Exception)
        {
            CreateBackUp(item);
        }
    }

    public object? Restore(int index)
    {
        try
        {
            var item = _listBackUp.ElementAt(index);
            return item;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
