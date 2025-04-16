using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DataModelLibrary;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace MapManagementLibrary
{
    public class CustomVehicleMarker
    {
        // Static readonly fields to load images only once
        private static readonly Bitmap blue_plane = (Bitmap)Image.FromFile(@"C:\Users\oscar\Documents\C# Projects\ASTERIX-Visualizer\ASTERIX_Visualizer\src\Images\Planes\plane_blue_small.png");
        private static readonly Bitmap red_plane = (Bitmap)Image.FromFile(@"C:\Users\oscar\Documents\C# Projects\ASTERIX-Visualizer\ASTERIX_Visualizer\src\Images\Planes\plane_red_small.png");
        private static readonly Bitmap green_plane = (Bitmap)Image.FromFile(@"C:\Users\oscar\Documents\C# Projects\ASTERIX-Visualizer\ASTERIX_Visualizer\src\Images\Planes\plane_green_small.png");
        private static readonly Bitmap white_plane = (Bitmap)Image.FromFile(@"C:\Users\oscar\Documents\C# Projects\ASTERIX-Visualizer\ASTERIX_Visualizer\src\Images\Planes\plane_white_small.png");

        private static readonly Bitmap blue_pushback = (Bitmap)Image.FromFile(@"C:\Users\oscar\Documents\C# Projects\ASTERIX-Visualizer\ASTERIX_Visualizer\src\Images\Pushbacks\bushback_blue_small.png");
        private static readonly Bitmap red_pushback = (Bitmap)Image.FromFile(@"C:\Users\oscar\Documents\C# Projects\ASTERIX-Visualizer\ASTERIX_Visualizer\src\Images\Pushbacks\bushback_red_small.png");
        private static readonly Bitmap green_pushback = (Bitmap)Image.FromFile(@"C:\Users\oscar\Documents\C# Projects\ASTERIX-Visualizer\ASTERIX_Visualizer\src\Images\Pushbacks\bushback_green_small.png");
        private static readonly Bitmap white_pushback = (Bitmap)Image.FromFile(@"C:\Users\oscar\Documents\C# Projects\ASTERIX-Visualizer\ASTERIX_Visualizer\src\Images\Pushbacks\bushback_white_small.png");

        private static readonly Bitmap blue_circle = (Bitmap)Image.FromFile(@"C:\Users\oscar\Documents\C# Projects\ASTERIX-Visualizer\ASTERIX_Visualizer\src\Images\Circles\circle_blue_small.png");
        private static readonly Bitmap red_circle = (Bitmap)Image.FromFile(@"C:\Users\oscar\Documents\C# Projects\ASTERIX-Visualizer\ASTERIX_Visualizer\src\Images\Circles\circle_red_small.png");
        private static readonly Bitmap green_circle = (Bitmap)Image.FromFile(@"C:\Users\oscar\Documents\C# Projects\ASTERIX-Visualizer\ASTERIX_Visualizer\src\Images\Circles\circle_green_small.png");
        private static readonly Bitmap white_circle = (Bitmap)Image.FromFile(@"C:\Users\oscar\Documents\C# Projects\ASTERIX-Visualizer\ASTERIX_Visualizer\src\Images\Circles\circle_white_small.png");

        public ParsedMessage m;
        public bool isPreviousMessage;
        private Bitmap icon;

        public CustomVehicleMarker(ParsedMessage m, bool isPreviousMessage)
        {
            this.m = m;
            this.isPreviousMessage = isPreviousMessage;

            // Optional: Adjust Size to match your bitmap size
            //this.Size = new Size(icon.Width, icon.Height);
        }

        public GMarkerGoogle getMarkerWithPropoerties()
        {

            this.icon = getVehicleIcon(m, isPreviousMessage);
            double lat = m.getLat();
            double lon = m.getLon();

            // Corrected instantiation of CustomVehicleMarker using the 'new' keyword
            var marker = new GMarkerGoogle(new PointLatLng(lat, lon), this.icon);

            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            marker.ToolTipText = m.getICAOAddress();

            return marker;
        }

        private Bitmap getVehicleIcon(ParsedMessage m, bool previousMessage)
        {
            if (previousMessage)
            {
                if (m.getSource() == "ADS-B")
                {
                    // Use a static method to call RotateImage  
                    return green_circle;
                }
                else if (m.getSource() == "MLAT")
                {
                    return blue_circle;
                }
                else if (m.getSource() == "SMR")
                {
                    return white_circle;
                }
            }

            if(m.getSource() == "ADS-B")
            {
                if (m.getHeading() != 0)
                {
                    return RotateImage(green_plane, (float)m.getHeading());
                }
                else
                {
                    return green_plane;
                }
            }
            else if (m.getSource() == "MLAT")
            {
                if (m.getHeading() != 0)
                {
                    return RotateImage(blue_plane, (float)m.getHeading());
                }
                else
                {
                    return blue_plane;
                }
            }
            else if (m.getSource() == "SMR")
            {
                if (m.getHeading() != 0)
                {
                    return RotateImage(white_plane, (float)m.getHeading());
                }
                else
                {
                    return white_plane;
                }
            }

            return null; // Add a return statement to handle all code paths  
        }

        private Bitmap RotateImage(Bitmap srcImage, float angle)
        {
            Bitmap rotatedImage = new Bitmap(srcImage.Width, srcImage.Height);
            rotatedImage.SetResolution(srcImage.HorizontalResolution, srcImage.VerticalResolution);

            using (Graphics g = Graphics.FromImage(rotatedImage))
            {
                g.TranslateTransform(srcImage.Width / 2f, srcImage.Height / 2f);
                g.RotateTransform(angle);
                g.TranslateTransform(-srcImage.Width / 2f, -srcImage.Height / 2f);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(srcImage, new System.Drawing.Point(0, 0));
            }

            return rotatedImage;
        }
    }
}
