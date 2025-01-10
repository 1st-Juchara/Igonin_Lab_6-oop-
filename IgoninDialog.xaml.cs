using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
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
using System.Windows.Shapes;

namespace Igonin_Lab_6
{
  /// <summary>
  /// Логика взаимодействия для IgoninDialog.xaml
  /// </summary>
  public partial class IgoninDialog : Window
  {
    IgoninForestVM forestVM;
    bool isAnimal;
    bool toAdd;
    public IgoninDialog(ref IgoninForestVM forest, bool isAnimal, bool toAdd)
    {
      InitializeComponent();
      this.forestVM = forest;
      this.isAnimal = isAnimal;
      DataContext = this.forestVM;
      if (isAnimal)
        forest.ViewReptileAtt(false);
      else
        forest.ViewReptileAtt(true);

    }

    private void Button_Click_Add(object sender, RoutedEventArgs e)
    {
      bool isRead = CheckAtt();
      if (isRead) {
        Close();
      }
      //proverkd poley
      //schitivaniye poley
      //close
    }

    private void Button_Click_Back(object sender, RoutedEventArgs e)
    {
      Close();
      //inx = inx
      //close
    }
    
    bool CheckAtt()
    {
      bool state = true;
      int color = 0;
      state = state && int.TryParse(forestVM.SelectedAnimalColor, out color);
      int nutr = 0;
      state = state && int.TryParse(forestVM.SelectedAnimalNutrition, out nutr);
      double age = 0;
      state = state && double.TryParse(forestVM.SelectedAnimalAge, out age);
      double weight = 0;
      state = state && double.TryParse(forestVM.SelectedAnimalWeight, out weight);

      double tailLen = 0;
      if (!isAnimal) {
        state = state && double.TryParse(forestVM.SelectedAnimalTail, out tailLen);
      }

      if (forestVM.SelectedAnimalName.Length == 0) {
        state = false;
        return state;
      }

      if (state) {
        IgoninForestVM.AnimalStruct anStruct = new IgoninForestVM.AnimalStruct();
        anStruct.Name = forestVM.SelectedAnimalName;
        anStruct.ColorInx = color;
        anStruct.NutrInx = nutr;
        anStruct.Age = age;
        anStruct.Weight = weight;
        anStruct.tailLenght = -1;
        if (!isAnimal) {
          anStruct.tailLenght = tailLen;
          anStruct.isPois = forestVM.SelectedAnimalPoisonous;
          if (toAdd) 
            IgoninForestVM.AddNewReptile(ref anStruct);
          else
            IgoninForestVM.Set(forestVM.SelectedInx, ref anStruct);
        }
        else {
          if (toAdd)
            IgoninForestVM.AddNewAnimal(ref anStruct);
          else
            IgoninForestVM.Set(forestVM.SelectedInx, ref anStruct);
        }

      }

      return state;
    }

  }
}
