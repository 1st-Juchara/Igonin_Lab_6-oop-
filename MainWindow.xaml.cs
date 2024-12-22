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


namespace Igonin_Lab_6
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    IgoninForestVM forest = new IgoninForestVM();
    
    [DllImport("IgoninForestDll.dll")]
    public static extern IntPtr Create();

    [DllImport("IgoninForestDll.dll")]
    public static extern void SetAge(IntPtr animal, int newAge);

    [DllImport("IgoninForestDll.dll")]
    public static extern int GetParams(IntPtr animal);

    [DllImport("IgoninForestDll.dll", CharSet = CharSet.Unicode)]
    public static extern IntPtr GetName(IntPtr animal);

    [DllImport("IgoninForestDll.dll", CharSet = CharSet.Unicode)]
    public static extern void SetName(IntPtr animal, string newName);

    [DllImport("IgoninForestDll.dll")]
    public static extern void FreeMemory(IntPtr ptr);

    public MainWindow()
    {
      DataContext = forest;
      InitializeComponent();
      IntPtr an = Create();
      SetAge(an, 10);
      int an_age = GetParams(an);

      string new_name = "Ime4ko";
      SetName(an, new_name);
      IntPtr ptr = GetName(an);
      string an_name = Marshal.PtrToStringUni(ptr);
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      forest.AddString();
    }
  }
}