using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapManagementLibrary
{
    public class CustomBeaconMarker : GMapMarker
    {
        public Brush FillBrush = Brushes.Red;
        //public Brush FillBrush = new SolidBrush(Color.FromArgb(10, 100, 100, 255));
        public Pen BorderPen = Pens.Transparent;
        public string Label { get; set; }
        public Font LabelFont { get; set; } = new Font("Arial", 10, FontStyle.Regular);
        public Brush LabelBrush { get; set; } = Brushes.Black;

        public CustomBeaconMarker(PointLatLng p, string label) : base(p)
        {
            Label = label;
            Size = new Size(20, 20);
            Offset = new Point(-Size.Width / 2, -Size.Height / 2);
        }

        public override void OnRender(Graphics g)
        {
            // Triangle points relative to LocalPosition
            Point p1 = new Point(LocalPosition.X, LocalPosition.Y - Size.Height / 2);
            Point p2 = new Point(LocalPosition.X - Size.Width / 2, LocalPosition.Y + Size.Height / 2);
            Point p3 = new Point(LocalPosition.X + Size.Width / 2, LocalPosition.Y + Size.Height / 2);

            Point[] trianglePoints = { p1, p2, p3 };

            // Draw the triangle
            g.FillPolygon(FillBrush, trianglePoints);
            g.DrawPolygon(BorderPen, trianglePoints);

            // Measure text and draw label centered below triangle
            SizeF labelSize = g.MeasureString(Label, LabelFont);
            PointF labelPos = new PointF(
                LocalPosition.X - labelSize.Width / 2,
                LocalPosition.Y + Size.Height / 2 + 2); // A little padding below triangle

            g.DrawString(Label, LabelFont, LabelBrush, labelPos);
        }
    }
}
