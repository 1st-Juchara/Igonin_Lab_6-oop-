using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igonin_Lab_6
{
  internal class IgoninForestVM : ViewModelBase
  {
    private ObservableCollection<string> strings = new ObservableCollection<string>();
    public ObservableCollection<string> Strings {  get => strings; set => strings = value; }

    private bool checkBind = false;
    public bool CheckBind { get => checkBind; set => Set(ref checkBind, value); }

    private int selectedInx = 0;
    public int SelectedInx { get => selectedInx; set => SelectedIndexChanged(value); }

    private string selectedAnimalName = "";
    public string SelectedAnimalName { get => selectedAnimalName; set => Set(ref  selectedAnimalName, value); }




    public IgoninForestVM() 
    {
      Strings.Add("First Line");

      Strings.Add("Second Line");

      Strings.Add("Third Line");

    }
    public void AddString()
    {
      Strings.Add($"Line number: {Strings.Count + 1}");
    }

    public void CheckBindCheck()
    { 
      CheckBind = !CheckBind;
    }

    public void SelectedIndexChanged(int selInx)
    {
      selectedInx = selInx;
      SelectedAnimalName = Strings[selectedInx];

    }
  }
}
