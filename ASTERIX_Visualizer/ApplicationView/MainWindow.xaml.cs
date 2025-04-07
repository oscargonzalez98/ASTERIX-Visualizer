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
using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsPresentation;
using static GMap.NET.Entity.OpenStreetMapRouteEntity;

using DataModel;
using MapManagementLibrary;

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

        IDataLoader<List<Beacon>> beaconsDataLoader = new BeaconsDataLoader();
        List<Beacon> beacons = beaconsDataLoader.loadData(beaconsDataPath);

        foreach (Beacon beacon in beacons)
        {
            double lat = beacon.getCoordinates().getLatitude();
            double lon = beacon.getCoordinates().getLongitude();

            var marker = new GMapMarker(new PointLatLng(lat, lon));
            marker.Shape = beacon.getShape();
            //marker.Offset = new Point(-20, 20); // Adjust the offset to center the marker

            customMap.Markers.Add(marker);

            exampleTextbox.Text += $"Lat: {lat}, Lon: {lon}\n";
        }
    }
}
