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
        private GMap.NET.WindowsForms.GMapOverlay airportZonesOverlay;

        public CustomMapView()
        {
            // Custom initialization or default settings
            CustomGMapControl_LoadMap();
            CustomGMapControl_LoadBeacons();
            CustomGMapControl_LoadAirways();
            CustomGMapControl_LoadAirportZones();

            this.Refresh();
        }

        private void CustomGMapControl_LoadMap()
        {
            this.MapProvider = GMapProviders.GoogleMap;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;

            this.Position = new PointLatLng(42, 2);
            this.MinZoom = 2;
            this.MaxZoom = 18;
            this.Zoom = 10;

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

        private void CustomGMapControl_LoadAirportZones()
        {
            string airportBarcelonaZonesDataPath = @"C:\Users\oscar\Documents\C# Projects\ASTERIX-Visualizer\ASTERIX_Visualizer\src\Mapas\Aeropuerto_Barcelona.map";
            string airportBarcelonaNewZonesDataPath = @"C:\Users\oscar\Documents\C# Projects\ASTERIX-Visualizer\ASTERIX_Visualizer\src\Mapas\Aeropuerto_Barcelonanue.map";
            string airportBarcelonaParkingPath = @"C:\Users\oscar\Documents\C# Projects\ASTERIX-Visualizer\ASTERIX_Visualizer\src\Mapas\BCN_Aparcamientos.map";
            string airportBarcelonaServiceRoadsPath = @"C:\Users\oscar\Documents\C# Projects\ASTERIX-Visualizer\ASTERIX_Visualizer\src\Mapas\BCN_CarreterasServicio.map";
            string airportBarcelonaBuildingsPath = @"C:\Users\oscar\Documents\C# Projects\ASTERIX-Visualizer\ASTERIX_Visualizer\src\Mapas\BCN_Edificios.map";
            string airportBarcelonaParterresPath = @"C:\Users\oscar\Documents\C# Projects\ASTERIX-Visualizer\ASTERIX_Visualizer\src\Mapas\BCN_Parterres.map";
            string airportBarcelonaRunwaysPath = @"C:\Users\oscar\Documents\C# Projects\ASTERIX-Visualizer\ASTERIX_Visualizer\src\Mapas\BCN_Pistas.map";
            string airportBarcelonaMovementZonesPath = @"C:\Users\oscar\Documents\C# Projects\ASTERIX-Visualizer\ASTERIX_Visualizer\src\Mapas\BCN_ZonasMovimiento.map";

            airportZonesOverlay = new GMap.NET.WindowsForms.GMapOverlay("");

            IDataLoader<List<List<Point2D>>> airportZonesDataLoader = new AirportZonesDataLoader();
            List<List<Point2D>> airportBarcelonaZones = airportZonesDataLoader.loadData(airportBarcelonaZonesDataPath);
            List<List<Point2D>> airportBarcelonaZonesNew = airportZonesDataLoader.loadData(airportBarcelonaNewZonesDataPath);
            List<List<Point2D>> airportBarcelonaParking = airportZonesDataLoader.loadData(airportBarcelonaParkingPath);
            List<List<Point2D>> airportBarcelonaServiceRoads = airportZonesDataLoader.loadData(airportBarcelonaServiceRoadsPath);
            List<List<Point2D>> airportBarcelonaBuildings = airportZonesDataLoader.loadData(airportBarcelonaBuildingsPath);
            List<List<Point2D>> airportBarcelonaParterres = airportZonesDataLoader.loadData(airportBarcelonaParterresPath);
            List<List<Point2D>> airportBarcelonaRunways = airportZonesDataLoader.loadData(airportBarcelonaRunwaysPath);
            List<List<Point2D>> airportBarcelonaMovementZones = airportZonesDataLoader.loadData(airportBarcelonaMovementZonesPath);

            airportZonesOverlay = printRoutesInOverlay(airportZonesOverlay, airportBarcelonaZones, System.Drawing.Color.FromArgb(255, 255, 99, 132), 10);
            airportZonesOverlay = printRoutesInOverlay(airportZonesOverlay, airportBarcelonaZonesNew, System.Drawing.Color.FromArgb(255, 54, 162, 235), 10);
            airportZonesOverlay = printRoutesInOverlay(airportZonesOverlay, airportBarcelonaParking, System.Drawing.Color.FromArgb(255, 75, 192, 192), 10);
            airportZonesOverlay = printRoutesInOverlay(airportZonesOverlay, airportBarcelonaServiceRoads, System.Drawing.Color.FromArgb(255, 255, 159, 64), 10);
            airportZonesOverlay = printRoutesInOverlay(airportZonesOverlay, airportBarcelonaBuildings, System.Drawing.Color.FromArgb(255, 153, 102, 255), 10);
            airportZonesOverlay = printRoutesInOverlay(airportZonesOverlay, airportBarcelonaParterres, System.Drawing.Color.FromArgb(255, 255, 205, 86), 10);
            airportZonesOverlay = printRoutesInOverlay(airportZonesOverlay, airportBarcelonaRunways, System.Drawing.Color.FromArgb(255, 128, 128), 10);
            airportZonesOverlay = printRoutesInOverlay(airportZonesOverlay, airportBarcelonaMovementZones, System.Drawing.Color.FromArgb(255, 165, 42, 42), 10);

            this.airportZonesOverlay.IsVisibile = true; // Ensure the overlay is visible
            this.Overlays.Add(airportZonesOverlay); // Add the routes overlay to the map
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

        private GMapOverlay printRoutesInOverlay(GMapOverlay overlay, List<List<Point2D>> routes, System.Drawing.Color c, int thickness)
        {
            foreach (List<Point2D> line in routes)
            {
                List<PointLatLng> gmapPoints = line.Select(p => new PointLatLng(p.getLatitude(), p.getLongitude())).ToList();

                // Create a route (polyline)
                GMap.NET.WindowsForms.GMapRoute route = new GMap.NET.WindowsForms.GMapRoute(gmapPoints, "");
                //System.Drawing.Color routeColor = System.Drawing.Color.FromArgb(255, 255, 182, 0); // Set the color of the route  Orange
                System.Drawing.Color routeColor = c;
                route.Stroke = new System.Drawing.Pen(routeColor, thickness); // Set the color and width of the route

                // Add the route to the overlay
                overlay.Routes.Add(route);
            }

            return overlay;
        }
    }
}