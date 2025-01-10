using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using static Igonin_Lab_6.IgoninForestVM;


namespace Igonin_Lab_6
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    IgoninForestVM forestVM = new IgoninForestVM();

    

    public MainWindow()
    {
      DataContext = forestVM;
      InitializeComponent();

      //IntPtr forest = IgoninForestVM.Create();
      //AnimalStruct an = new AnimalStruct();
      //an.Name = "Живот1";
      //an.Color = "";
      //an.ColorInx = 2;
      //an.Nutr = "";
      //an.NutrInx = 2;
      //an.Age = 11;
      //an.Weight = 2;
      //an.isPois = false;
      //an.Pois = "";
      //an.tailLenght = 0;
      //var ptr = Marshal.AllocHGlobal(Marshal.SizeOf<AnimalStruct>());
      //Marshal.StructureToPtr(an, ptr, true);
      //
      //IgoninForestVM.AddNewAnimal(forest, ptr);
      //IgoninForestVM.Get(forest, 0, ptr);
      //
      //an = (AnimalStruct)Marshal.PtrToStructure(ptr, typeof(AnimalStruct));
      //;
      //IgoninForestVM.SetAge(an, 10);
      //int an_age = IgoninForestVM.GetAge(an);
      //
      //string new_name = "Я Узбееееек!!!!";
      //IgoninForestVM.SetName(an, new_name);
      //IntPtr ptr = IgoninForestVM.GetName(an);
      //string an_name = Marshal.PtrToStringAnsi(ptr);
      //
      //int color = 1;
      //IgoninForestVM.SetColor(an, color);
      //ptr = IgoninForestVM.GetColor(an);
      //string an_color = Marshal.PtrToStringUni(ptr);
    }


    private void Button_Click_Load(object sender, RoutedEventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
        // Настройки окна диалога
      openFileDialog.Filter = "Все файлы (*.*)|*.*"; // Фильтр для отображения всех типов файлов
      openFileDialog.Title = "Выберите файл";

      openFileDialog.ShowDialog();

      string filePath = openFileDialog.FileName;
      if (filePath.Length > 0) {
        StringBuilder sb = new StringBuilder(filePath);

        forestVM.ClearTable();

        Load(sb);
        
        forestVM.Update();
        
        //an = (AnimalStruct)Marshal.PtrToStructure(ptr, typeof(AnimalStruct));
        //forestVM.AddString(filePath);

      }


    }

    private void Button_Click_Add(object sender, RoutedEventArgs e)
    {
      MessageBoxResult dialogResult = MessageBox.Show("*да - животное\n*нет - рептилия", "Добавление", MessageBoxButton.YesNo);
      if (dialogResult == MessageBoxResult.Yes) {
        IgoninDialog dialog = new IgoninDialog(ref forestVM, true, true);
        dialog.ShowDialog();
        forestVM.SelectedInx = IgoninForestVM.Count() - 1;
        forestVM.Update();
      }
      else if (dialogResult == MessageBoxResult.No) {
        IgoninDialog dialog = new IgoninDialog(ref forestVM, false, true);
        dialog.ShowDialog();
        forestVM.SelectedInx = IgoninForestVM.Count() - 1;
        forestVM.Update();
      }
      
    }

    private void Button_Click_Save(object sender, RoutedEventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      // Настройки окна диалога
      openFileDialog.Filter = "Все файлы (*.*)|*.*"; // Фильтр для отображения всех типов файлов
      openFileDialog.Title = "Выберите файл";

      openFileDialog.ShowDialog();

      string filePath = openFileDialog.FileName;
      if (filePath.Length > 0) {
        StringBuilder sb = new StringBuilder(filePath);
        Save(sb);

        //an = (AnimalStruct)Marshal.PtrToStructure(ptr, typeof(AnimalStruct));
        //forestVM.AddString(filePath);

      }
    }

    private void Button_Click_Change(object sender, RoutedEventArgs e)
    {
      if (forestVM.IsReptile.Equals(Visibility.Visible)) {
        IgoninDialog dialog = new IgoninDialog(ref forestVM, false, false);
        dialog.ShowDialog();
        forestVM.Update();
      }
      else {
        IgoninDialog dialog = new IgoninDialog(ref forestVM, true, false);
        dialog.ShowDialog();
        forestVM.Update();
      }
    }

    private void Button_Click_Delete(object sender, RoutedEventArgs e)
    {
      forestVM.ClearAtt();
      IgoninForestVM.Delete(forestVM.SelectedInx);
      forestVM.SelectedInx = forestVM.SelectedInx == 0 ? 0 : forestVM.SelectedInx > IgoninForestVM.Count() - 1 ? forestVM.SelectedInx - 1 : forestVM.SelectedInx; 
      forestVM.Update();
    }

    private void Button_Click_Clear(object sender, RoutedEventArgs e)
    {
      forestVM.ClearTable();
    }
    }
}
