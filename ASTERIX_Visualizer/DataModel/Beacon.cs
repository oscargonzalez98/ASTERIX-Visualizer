using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DataModel
{
    public class Beacon
    {
        private string Code;
        private Point2D Coordinates;

        private UIElement shape;

        public Beacon(string code, Point2D coordinates)
        {
            Code = code;
            Coordinates = coordinates;
            shape = CreateShape();
        }

        private UIElement CreateShape()
        {
            Polygon triangle = new Polygon
            {
                Points = new PointCollection(new List<System.Windows.Point>
                {
                    new System.Windows.Point(-5, 0),
                    new System.Windows.Point(5, 0),
                    new System.Windows.Point(0, -10)
                }),
                Stroke = System.Windows.Media.Brushes.Red,
                StrokeThickness = 2
            };

            TextBlock label = new TextBlock
            {
                Text = getCode(),
                Background = Brushes.Transparent,
                Foreground = Brushes.Black,
                Padding = new Thickness(4),
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            // Stack them
            StackPanel panel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            return panel;
        }

        public string getCode()
        {
            return Code;
        }

        public void setCode(string code)
        {
            Code = code;
        }

        public Point2D getCoordinates()
        {
            return Coordinates;
        }

        public void setCoordinates(Point2D coordinates)
        {
            Coordinates = coordinates;
        }

        public override string ToString()
        {
            return $"Beacon Code: {Code}, Coordinates: ({Coordinates.getLatitude()}, {Coordinates.getLongitude()})";
        }
    }
}
