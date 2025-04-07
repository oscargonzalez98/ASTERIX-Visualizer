using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsPresentation;
using System.Windows;
using System.Windows.Input;

namespace MapManagementLibrary
{
    public class CustomMapView : GMapControl
    {
        public CustomMapView()
        {
            // Custom initialization or default settings
            this.Loaded += CustomGMapControl_Loaded;
        }

        private void CustomGMapControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.MapProvider = GMapProviders.GoogleMap;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;

            this.Position = new PointLatLng(42, 2);
            this.MinZoom = 2;
            this.MaxZoom = 18;
            this.Zoom = 5;

            this.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.CanDragMap = true;
            this.DragButton = MouseButton.Left;
        }
    }
}