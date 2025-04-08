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
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace ApplicationView;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        // Initialize the custom map
        CustomMapView customMap = new CustomMapView();

        // Wrap CustomMapView in a WindowsFormsHost to make it compatible with UIElement
        formsHost = new WindowsFormsHost();
        formsHost.Child = customMap;

        // Set the WindowsFormsHost as the content of the Grid
        this.gridHost.Children.Add(formsHost);

        /*
        // Custom initialization or default settings
        // Create the interop host control.
        System.Windows.Forms.Integration.WindowsFormsHost host =
            new System.Windows.Forms.Integration.WindowsFormsHost();

        // Create the MaskedTextBox control.
        MaskedTextBox mtbDate = new MaskedTextBox("00/00/0000");

        // Assign the MaskedTextBox control as the host control's child.
        host.Child = mtbDate;

        // Add the interop host control to the Grid
        // control's collection of child controls.
        this.grid1.Children.Add(host);

        */

        string beaconsDataPath = @"C:\Users\oscar\Documents\C# Projects\ASTERIX-Visualizer\ASTERIX_Visualizer\src\Mapas\Peninsula_Fijos.map";

        IDataLoader<List<Beacon>> beaconsDataLoader = new BeaconsDataLoader();
        List<Beacon> beacons = beaconsDataLoader.loadData(beaconsDataPath);
        /*
        foreach (Beacon beacon in beacons)
        {
            double lat = beacon.getCoordinates().getLatitude();
            double lon = beacon.getCoordinates().getLongitude();

            var marker = new GMapMarker(new PointLatLng(lat, lon));
            marker.Shape = beacon.getShape();

            customMap.Manager.Markers.Add(marker);

            customMap.Markers.Add(marker);

            exampleTextbox.Text += $"Lat: {lat}, Lon: {lon}\n";
        }
        */
    }

    private void exampleTextbox_TextChanged(object sender, TextChangedEventArgs e)
    {

    }
}
