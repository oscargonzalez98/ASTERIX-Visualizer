using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using DataManagementLibrary;

namespace ApplicationView;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        string beaconsDataPath = @"C:\Users\oscar\Documents\C# Projects\ASTERIX-Visualizer\ASTERIX_Visualizer\src\Mapas\Peninsula_Fijos.map";

        IDataLoader<List<(double lat, double lon)>> beaconsDataLoader = new BeaconsDataLoader();
        List<(double lat, double lon)> beaconsCoordinates = beaconsDataLoader.loadData(beaconsDataPath);

        foreach ((double lat, double lon) in beaconsCoordinates)
        {
            exampleTextbox.Text += $"Lat: {lat}, Lon: {lon}\n";
        }
    }
}
