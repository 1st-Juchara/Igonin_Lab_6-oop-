using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Igonin_Lab_6
{

  public class IgoninForestVM : ViewModelBase
  {

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct AnimalStruct
    {
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
      public string Name;
      [MarshalAs(UnmanagedType.I4)]
      public int ColorInx;
      [MarshalAs(UnmanagedType.I4)]
      public int NutrInx;
      [MarshalAs(UnmanagedType.R8)]
      public double Age;
      [MarshalAs(UnmanagedType.R8)]
      public double Weight;

      [MarshalAs(UnmanagedType.Bool)]
      public bool isPois;
      [MarshalAs(UnmanagedType.R8)]
      public double tailLenght;
    }

    [DllImport("IgoninForestDll.dll")]
    public static extern int Count();

    [DllImport("IgoninForestDll.dll")]
    public static extern void AddNewAnimal(ref AnimalStruct anStruct);

    [DllImport("IgoninForestDll.dll")]
    public static extern void AddNewReptile(ref AnimalStruct anStruct);

    [DllImport("IgoninForestDll.dll")]
    public static extern void Delete(int inx);

    [DllImport("IgoninForestDll.dll")]
    public static extern void Set(int inx, ref AnimalStruct anStruct);

    [DllImport("IgoninForestDll.dll")]
    public static extern void Get(int inx, ref AnimalStruct anStruct);

    [DllImport("IgoninForestDll.dll", CharSet = CharSet.Ansi)]
    public static extern void Save(StringBuilder anStruct);

    [DllImport("IgoninForestDll.dll", CharSet = CharSet.Ansi)]
    public static extern void Load(StringBuilder anStruct);

    [DllImport("IgoninForestDll.dll")]
    public static extern void Clear();


    public ObservableCollection<string> AnNames { get; set; } = new ObservableCollection<string>();

    private int selectedInx = -1;
    public int SelectedInx { get => selectedInx; set => SelectedIndexChanged(value); }

    private string selectedAnimalName = "";
    public string SelectedAnimalName { get => selectedAnimalName; set => Set(ref selectedAnimalName, value); }

    private string selectedAnimalColor = "";
    public string SelectedAnimalColor { get => selectedAnimalColor; set => Set(ref selectedAnimalColor, value); }

    private string selectedAnimalNutrition = "";
    public string SelectedAnimalNutrition { get => selectedAnimalNutrition; set => Set(ref selectedAnimalNutrition, value); }

    private string selectedAnimalAge = "";
    public string SelectedAnimalAge { get => selectedAnimalAge; set => Set(ref selectedAnimalAge, value); }

    private string selectedAnimalWeight = "";
    public string SelectedAnimalWeight { get => selectedAnimalWeight; set => Set(ref selectedAnimalWeight, value); }

    private bool selectedAnimalPoisonous;
    public bool SelectedAnimalPoisonous { get => selectedAnimalPoisonous; set => Set(ref selectedAnimalPoisonous, value); }
    private string selectedAnimalTail = "";
    public string SelectedAnimalTail { get => selectedAnimalTail; set => Set(ref selectedAnimalTail, value); }

    private Visibility isReptile = Visibility.Collapsed;
    public Visibility IsReptile { get => isReptile; set => Set(ref isReptile, value); }




    public IgoninForestVM()
    {
    }


    public void SelectedIndexChanged(int selInx)
    {
      if (selInx >= 0) {
        selectedInx = selInx;
        if (selectedInx == 0 && AnNames.Count == 0) {
          ClearAtt();
        }
        CheckAtt();
      }
    }

    public void Update()
    {
      AnNames.Clear();

      int anCount = Count();
      if (anCount > 0) {
        for (int i = 0; i < anCount; i++) {
          AnimalStruct an = new AnimalStruct();
          Get(i, ref an);
          AnNames.Add(an.Name);
        }
      }
    }

    public void CheckAtt()
    {
      AnimalStruct an = new AnimalStruct();
      Get(SelectedInx, ref an);
      SelectedAnimalName = an.Name;
      SelectedAnimalColor = an.ColorInx.ToString();
      SelectedAnimalNutrition = an.NutrInx.ToString();
      SelectedAnimalAge = an.Age.ToString();
      SelectedAnimalWeight = an.Weight.ToString();
      if (an.tailLenght >= 0) {
        ViewReptileAtt(true);
        SelectedAnimalPoisonous = an.isPois;
        SelectedAnimalTail = an.tailLenght.ToString();
      }
      else
        ViewReptileAtt(false);
    }

    public void ClearAtt()
    {
      SelectedAnimalName = "";
      SelectedAnimalColor = "";
      SelectedAnimalNutrition = "";
      SelectedAnimalAge = "";
      SelectedAnimalWeight = "";
      SelectedAnimalPoisonous = false;
      SelectedAnimalTail = "";
    }

    public void ViewReptileAtt(bool view)
    {
      if (view)
        IsReptile = Visibility.Visible;
      else 
        IsReptile = Visibility.Collapsed;
    }

    public void ClearTable()
    {
      SelectedInx = -1;
      AnNames.Clear();
      ClearAtt();
      Clear();
    }

  }
}