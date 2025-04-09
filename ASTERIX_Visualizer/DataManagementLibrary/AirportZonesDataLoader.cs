using DataModel;
using GMap.NET.MapProviders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace DataManagementLibrary
{
    public class AirportZonesDataLoader : IDataLoader<List<List<Point2D>>>
    {
        public List<List<Point2D>> loadData(string filePath)
        {
            var resultLines = new List<List<Point2D>>();
            var lines = File.ReadAllLines(filePath);

            bool found = false;
            int i = 0;
            for(i = 0; i < lines.Length && found == false; i++)
            {
                string line = lines[i];
                var trimmedLine = line.Trim();

                if (trimmedLine.StartsWith("Polilinea"))
                {
                    found = true;
                }
            }

            string[] lineasArray = lines.Take(i).ToArray();          // Elements 0 to index - 1
            string[] polilineasArray = lines.Skip(i).ToArray();        // Elements index to end

            List<Point2D> currentPolyline = null;
            int expectedCount = 0;
            int currentCount = 0;

            foreach (var line in lineasArray) {
                var trimmedLine = line.Trim();

                //Debug.WriteLine("Trimmed line: " + trimmedLine);

                if (trimmedLine.StartsWith("Linea"))
                {
                    string[] parts = trimmedLine.Split(' ');

                    double lat_degrees1 = Convert.ToDouble(parts[1].Substring(0, 2));
                    double lat_minutes1 = Convert.ToDouble(parts[1].Substring(2, 2));
                    double lat_seconds1 = Convert.ToDouble(parts[1].Substring(4, 2)) + Convert.ToDouble(parts[1].Substring(6, 3)) / 1000;
                    string lat_dir1 = parts[1].Substring(9, 1);

                    double lon_degrees1 = Convert.ToDouble(parts[2].Substring(0, 3));
                    double lon_minutes1 = Convert.ToDouble(parts[2].Substring(3, 2));
                    double lon_seconds1 = Convert.ToDouble(parts[2].Substring(5, 2)) + Convert.ToDouble(parts[2].Substring(7, 3)) / 1000;
                    string lon_dir1 = parts[2].Substring(10, 1);

                    double lat_degrees2 = Convert.ToDouble(parts[3].Substring(0, 2));
                    double lat_minutes2 = Convert.ToDouble(parts[3].Substring(2, 2));
                    double lat_seconds2 = Convert.ToDouble(parts[3].Substring(4, 2)) + Convert.ToDouble(parts[3].Substring(6, 3)) / 1000;
                    string lat_dir2 = parts[3].Substring(9, 1);

                    double lon_degrees2 = Convert.ToDouble(parts[4].Substring(0, 3));
                    double lon_minutes2 = Convert.ToDouble(parts[4].Substring(3, 2));
                    double lon_seconds2 = Convert.ToDouble(parts[4].Substring(5, 2)) + Convert.ToDouble(parts[4].Substring(7, 3)) / 1000;
                    string lon_dir2 = parts[4].Substring(10, 1);

                    double lat1 = lat_degrees1 + (lat_minutes1 / 60) + (lat_seconds1 / 3600);
                    double lon1 = lon_degrees1 + (lon_minutes1 / 60) + (lon_seconds1 / 3600);

                    double lat2 = lat_degrees2 + (lat_minutes2 / 60) + (lat_seconds2 / 3600);
                    double lon2 = lon_degrees2 + (lon_minutes2 / 60) + (lon_seconds2 / 3600);

                    if (lat_dir1 == "W")
                    {
                        lat1 *= -1;
                    }

                    if (lon_dir1 == "S")
                    {
                        lon1 *= -1;
                    }

                    if (lat_dir2 == "W")
                    {
                        lat2 *= -1;
                    }

                    if (lon_dir2 == "S")
                    {
                        lon2 *= -1;
                    }

                    Point2D point1 = new Point2D(lat1, lon1);
                    Point2D point2 = new Point2D(lat2, lon2);

                    resultLines.Add(new List<Point2D> { point1, point2 });

                    //Debug.WriteLine("lat_degrees: " + parts[2].Substring(0, 3));
                    //Debug.WriteLine("lat_minutes: " + parts[2].Substring(3, 2));
                    //Debug.WriteLine("lat_seconds: " + parts[2].Substring(5, 2));
                    //Debug.WriteLine("lat_seconds: " + parts[2].Substring(7, 3));
                    //Debug.WriteLine("lat_dir: " + parts[2].Substring(10, 1));


                }
            }

            foreach (var line in polilineasArray)
            {
                var trimmedLine = line.Trim();

                if (trimmedLine.StartsWith("Polilinea"))
                {
                    if (currentPolyline != null && currentPolyline.Count != expectedCount)
                    {
                        //Debug.WriteLine("Warning: Expected " + expectedCount + " points, but found " + currentPolyline.Count);
                    }

                    // Start a new polyline
                    var match = Regex.Match(trimmedLine, @"Polilinea\s+(\d+)");
                    if (match.Success)
                    {
                        expectedCount = int.Parse(match.Groups[1].Value);
                        currentPolyline = new List<Point2D>();
                        resultLines.Add(currentPolyline);

                        currentCount = 0;
                    }
                }
                else if (currentPolyline != null)
                {
                    try
                    {
                        double lat_degrees = Convert.ToDouble(trimmedLine.Substring(0, 2));
                        double lat_minutes = Convert.ToDouble(trimmedLine.Substring(2, 2));
                        double lat_seconds = Convert.ToDouble(trimmedLine.Substring(4, 2)) + Convert.ToDouble(trimmedLine.Substring(6, 3)) / 1000;
                        string lat_dir = trimmedLine.Substring(9, 1);

                        //Debug.WriteLine("lat_degrees: " + trimmedLine.Substring(0, 2) + "  lat_minutes: " + trimmedLine.Substring(2, 2) + " lat_seconds: " + trimmedLine.Substring(4, 2) + " lat_seconds: " + trimmedLine.Substring(6, 3) + " lat_dir: " + trimmedLine.Substring(9, 1));

                        double lon_degrees = Convert.ToDouble(trimmedLine.Substring(10, 4));
                        double lon_minutes = Convert.ToDouble(trimmedLine.Substring(14, 2));
                        double lon_seconds = Convert.ToDouble(trimmedLine.Substring(16, 2)) + Convert.ToDouble(trimmedLine.Substring(18, 3)) / 1000;
                        string lon_dir = trimmedLine.Substring(21, 1);

                        //Debug.WriteLine("lon_degrees: " + trimmedLine.Substring(10, 4) + "  lon_minutes: " + trimmedLine.Substring(14, 2) + " lon_seconds: " + trimmedLine.Substring(16, 2) + " lon_seconds: " + trimmedLine.Substring(18, 3) + " lon_dir: " + trimmedLine.Substring(21, 1));

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
                    } catch
                    {
                        //Debug.WriteLine("Error parsing line: " + trimmedLine);
                        //Debug.WriteLine("Current polyline count: " + currentPolyline.Count);
                        //Debug.WriteLine("Expected count: " + expectedCount);
                    }

                    currentCount++;
                }
            }
            return resultLines;
        }
    }
}
