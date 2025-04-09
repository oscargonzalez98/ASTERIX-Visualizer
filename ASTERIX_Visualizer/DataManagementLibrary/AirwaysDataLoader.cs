using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using System.Diagnostics;


namespace DataManagementLibrary
{
    public class AirwaysDataLoader : IDataLoader<List<List<Point2D>>>
    {
        public List<List<Point2D>> loadData(string filePath)
        {
            var result = new List<List<Point2D>>();
            var lines = File.ReadAllLines(filePath);

            List<Point2D> currentPolyline = null;
            int expectedCount = 0;
            int currentCount = 0;

            foreach (var line in lines)
            {
                var trimmedLine = line.Trim();

                if (trimmedLine.StartsWith("Polilinea"))
                {
                    if (currentPolyline != null && currentPolyline.Count != expectedCount)
                    {
                        Debug.WriteLine("Warning: Expected " + expectedCount + " points, but found " + currentPolyline.Count);
                    }

                    // Start a new polyline
                    var match = Regex.Match(trimmedLine, @"Polilinea\s+(\d+)");
                    if (match.Success)
                    {
                        expectedCount = int.Parse(match.Groups[1].Value);
                        currentPolyline = new List<Point2D>();
                        result.Add(currentPolyline);

                        currentCount = 0;
                    }
                }
                else if (currentPolyline != null)
                {
                    double lat_degrees = Convert.ToDouble(trimmedLine.Substring(0, 2));
                    double lat_minutes = Convert.ToDouble(trimmedLine.Substring(2, 2));
                    double lat_seconds = Convert.ToDouble(trimmedLine.Substring(4, 2));
                    string lat_dir = trimmedLine.Substring(6, 1);

                    double lon_degrees = Convert.ToDouble(trimmedLine.Substring(7, 3));
                    double lon_minutes = Convert.ToDouble(trimmedLine.Substring(10, 2));
                    double lon_seconds = Convert.ToDouble(trimmedLine.Substring(12, 2));
                    string lon_dir = trimmedLine.Substring(14, 1);

                    double lat = lat_degrees + (lat_minutes / 60) + (lat_seconds / 3600);
                    double lon = lon_degrees + (lon_minutes / 60) + (lon_seconds / 3600);

                    if (lat_dir == "W")
                    {
                        lat *= -1;
                    }

                    if (lon_dir == "S")
                    {
                        lon *= -1;
                    }

                    Point2D point2D = new Point2D(lat, lon);
                    currentPolyline.Add(point2D);

                    currentCount++;
                }
            }

            foreach(List<Point2D> airway in result)
            {
                if (airway.Count != expectedCount)
                {
                    Debug.WriteLine("Polyline loaded with " + airway.Count + " points.");
                    foreach (Point2D point in airway)
                    {
                        Debug.WriteLine(point.ToString());
                    }
                }
            }

            return result;
        }
    }
}