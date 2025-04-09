using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsPresentation;
using System.Windows;
using System.Windows.Input;
using DataManagementLibrary;
using DataModel;
using GMap.NET.WindowsForms.Markers;
using System.Windows.Forms;
using System.Xml.Linq;
using GMap.NET.WindowsForms;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MapManagementLibrary
{
    public class CustomMapView : GMap.NET.WindowsForms.GMapControl
    {
        private GMap.NET.WindowsForms.GMapOverlay beaconsOverlay;
        private GMap.NET.WindowsForms.GMapOverlay airwaysOverlay;

        public CustomMapView()
        {
            // Custom initialization or default settings
            CustomGMapControl_LoadMap();
            CustomGMapControl_LoadBeacons();
            CustomGMapControl_LoadAirways();

            this.Refresh();
        }

        private void CustomGMapControl_LoadMap()
        {
            this.MapProvider = GMapProviders.GoogleMap;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;

            this.Position = new PointLatLng(42, 2);
            this.MinZoom = 2;
            this.MaxZoom = 18;
            this.Zoom = 5;

            this.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.CanDragMap = true;

            // Fix for CS0266: Explicitly convert System.Windows.Input.MouseButton to System.Windows.Forms.MouseButtons
            this.DragButton = ConvertMouseButton(MouseButton.Left);
        }

        private void CustomGMapControl_LoadBeacons()
        {
            beaconsOverlay = new GMap.NET.WindowsForms.GMapOverlay("");

            string beaconsDataPath = @"C:\Users\oscar\Documents\C# Projects\ASTERIX-Visualizer\ASTERIX_Visualizer\src\Mapas\Peninsula_Fijos.map";

            IDataLoader<List<Beacon>> beaconsDataLoader = new BeaconsDataLoader();
            List<Beacon> beacons = beaconsDataLoader.loadData(beaconsDataPath);

            foreach (Beacon beacon in beacons)
            {
                double lat = beacon.getCoordinates().getLatitude();
                double lon = beacon.getCoordinates().getLongitude();

                var marker = new CustomMarker(new PointLatLng(lat, lon), beacon.getCode());
                

                beaconsOverlay.Markers.Add(marker);
            }

            this.beaconsOverlay.IsVisibile = true; // Ensure the overlay is visible

            this.Overlays.Add(beaconsOverlay); // Add the overlay to the map
        }

        private void CustomGMapControl_LoadAirways()
        {
            string airwaysDataPath = @"C:\Users\oscar\Documents\C# Projects\ASTERIX-Visualizer\ASTERIX_Visualizer\src\Mapas\Peninsula_Aerovias.map";

            airwaysOverlay = new GMap.NET.WindowsForms.GMapOverlay("");

            // Explicitly cast AirwaysDataLoader to IDataLoader<List<List<double>>>
            IDataLoader<List<List<Point2D>>> airwaysDataLoader = new AirwaysDataLoader();

            List<List<Point2D>> airways = airwaysDataLoader.loadData(airwaysDataPath);
            
            foreach(List<Point2D> airway in airways)
            {
                List<PointLatLng> gmapPoints = airway.Select(p => new PointLatLng(p.getLatitude(), p.getLongitude())).ToList();

                // Create a route (polyline)
                GMap.NET.WindowsForms.GMapRoute route = new GMap.NET.WindowsForms.GMapRoute(gmapPoints, "");
                System.Drawing.Color routeColor = System.Drawing.Color.FromArgb(80, 20, 120, 255); // Set the color of the route
                route.Stroke = new System.Drawing.Pen(routeColor, 5); // Set the color and width of the route

                // Add the route to the overlay
                airwaysOverlay.Routes.Add(route);
            }
            
            this.airwaysOverlay.IsVisibile = true; // Ensure the overlay is visible
            this.Overlays.Add(airwaysOverlay); // Add the routes overlay to the map
        }

        // Helper method to convert System.Windows.Input.MouseButton to System.Windows.Forms.MouseButtons
        private System.Windows.Forms.MouseButtons ConvertMouseButton(MouseButton button)
        {
            return button switch
            {
                MouseButton.Left => System.Windows.Forms.MouseButtons.Left,
                MouseButton.Right => System.Windows.Forms.MouseButtons.Right,
                MouseButton.Middle => System.Windows.Forms.MouseButtons.Middle,
                _ => System.Windows.Forms.MouseButtons.None,
            };
        }
    }
}