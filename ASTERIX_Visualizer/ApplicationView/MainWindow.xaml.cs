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

        gmapControl.MapProvider = GMapProviders.GoogleMap;
        GMaps.Instance.Mode = AccessMode.ServerAndCache;
        gmapControl.Position = new PointLatLng(37.7749, -122.4194); // Example: San Francisco
        gmapControl.MinZoom = 2;
        gmapControl.MaxZoom = 18;
        gmapControl.Zoom = 10;

        gmapControl.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
        // lets the user drag the map
        gmapControl.CanDragMap = true;
        // lets the user drag the map with the left mouse button
        gmapControl.DragButton = MouseButton.Left;

        foreach (Beacon beacon in beacons)
        {
            double lat = beacon.getCoordinates().getLatitude();
            double lon = beacon.getCoordinates().getLongitude();

            var marker = new GMapMarker(new PointLatLng(lat, lon));
            marker.Shape = beacon.getShape();
            //marker.Offset = new Point(-20, 20); // Adjust the offset to center the marker

            gmapControl.Markers.Add(marker);

            exampleTextbox.Text += $"Lat: {lat}, Lon: {lon}\n";
        }
    }
}
