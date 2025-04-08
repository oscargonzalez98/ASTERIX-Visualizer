using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsPresentation;
using System.Windows;
using System.Windows.Input;
using DataManagementLibrary;
using DataModel;
using GMap.NET.WindowsForms.Markers;
using System.Windows.Forms;

namespace MapManagementLibrary
{
    public class CustomMapView : GMap.NET.WindowsForms.GMapControl
    {
        private GMap.NET.WindowsForms.GMapOverlay beaconsOverlay;

        public CustomMapView()
        {
            // Custom initialization or default settings
            CustomGMapControl_LoadMap();
            CustomGMapControl_LoadBeacons();
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
            beaconsOverlay = new GMap.NET.WindowsForms.GMapOverlay("beacons");

            string beaconsDataPath = @"C:\Users\oscar\Documents\C# Projects\ASTERIX-Visualizer\ASTERIX_Visualizer\src\Mapas\Peninsula_Fijos.map";

            IDataLoader<List<Beacon>> beaconsDataLoader = new BeaconsDataLoader();
            List<Beacon> beacons = beaconsDataLoader.loadData(beaconsDataPath);

            foreach (Beacon beacon in beacons)
            {
                double lat = beacon.getCoordinates().getLatitude();
                double lon = beacon.getCoordinates().getLongitude();

                // Use GMap.NET.WindowsForms.GMapMarker instead of GMap.NET.WindowsPresentation.GMapMarker
                var marker = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(
                    new PointLatLng(lat, lon),
                    GMarkerGoogleType.red_dot
                );

                beaconsOverlay.Markers.Add(marker);
            }

            this.beaconsOverlay.IsVisibile = true; // Ensure the overlay is visible

            this.Overlays.Add(beaconsOverlay); // Add the overlay to the map
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