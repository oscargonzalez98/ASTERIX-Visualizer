using System;
using System.Collections;
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
        private Grid shape;

        public Beacon(string code, Point2D coordinates)
        {
            Code = code;
            Coordinates = coordinates;
            shape = CreateShape();
        }

        private Grid CreateShape()
        {
            Grid panel = new Grid
            {
                HorizontalAlignment = HorizontalAlignment.Center
            };

            panel.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            panel.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            Polygon triangle = new Polygon
            {
                Points = new PointCollection(new List<System.Windows.Point>
    {
                    new System.Windows.Point(-5, 0),
                    new System.Windows.Point(5, 0),
                    new System.Windows.Point(0, -10)
    }),
                Stroke = System.Windows.Media.Brushes.Red,
                StrokeThickness = 2,
                HorizontalAlignment = HorizontalAlignment.Center
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

            Grid.SetRow(triangle, 0);
            Grid.SetRow(label, 1);

            panel.Children.Add(triangle);
            panel.Children.Add(label);


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

        public Grid getShape()
        {
            return shape;
        }

        public void setShape(Grid shape)
        {
            this.shape = shape;
        }

        public override string ToString()
        {
            return $"Beacon Code: {Code}, Coordinates: ({Coordinates.getLatitude()}, {Coordinates.getLongitude()})";
        }
    }
}
