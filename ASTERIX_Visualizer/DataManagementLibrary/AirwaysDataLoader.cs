using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataManagementLibrary
{
    class AirwaysDataLoader
    {
        public class BeaconsDataLoader : IDataLoader<List<List<double>>>
        {
            public List<List<double>> loadData(string filePath)
            {
                List<List<double>> airwaysList = new List<List<double>>(); // Initialize the list of airways


                string[] lines = File.ReadAllLines(filePath);
                for(int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];

                    string[] parts = Regex.Split(line.Trim(), @"\s+");



                    /*
                    if (parts.Length == 2 && parts[0] == "Polilinea")
                    {
                        i++;

                        List<double> airway = new List<double>();


                        while (i < lines.Length && parts[0] != "Polilinea")
                        {

                            line = lines[i];
                            string[] parts = Regex.Split(line.Trim(), @"\s+");


                            string coordinates = parts[1];
                            double lat_degrees = Convert.ToDouble(coordinates.Substring(0, 2));
                            double lat_minutes = Convert.ToDouble(coordinates.Substring(2, 2));
                            double lat_seconds = Convert.ToDouble(coordinates.Substring(4, 2));
                            string lat_dir = coordinates.Substring(6, 1);
                            double lon_degrees = Convert.ToDouble(coordinates.Substring(7, 3));
                            double lon_minutes = Convert.ToDouble(coordinates.Substring(10, 2));
                            double lon_seconds = Convert.ToDouble(coordinates.Substring(12, 2));
                            string lon_dir = coordinates.Substring(14, 1);
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
                            airway.Add(lat);
                            airway.Add(lon);
                            i++;
                        }


                    }
                    */
                }

                    /*
                    List<Beacon> beaconsList = new List<Beacon>();

                    foreach (string line in File.ReadAllLines(filePath))
                    {
                        string[] parts = Regex.Split(line.Trim(), @"\s+");

                        if (parts.Length == 3 && parts[0] == "Texto")
                        {
                            string coordinates = parts[1];
                            string code = parts[2];

                            double lat_degrees = Convert.ToDouble(coordinates.Substring(0, 2));
                            double lat_minutes = Convert.ToDouble(coordinates.Substring(2, 2));
                            double lat_seconds = Convert.ToDouble(coordinates.Substring(4, 2));
                            string lat_dir = coordinates.Substring(6, 1);

                            double lon_degrees = Convert.ToDouble(coordinates.Substring(7, 3));
                            double lon_minutes = Convert.ToDouble(coordinates.Substring(10, 2));
                            double lon_seconds = Convert.ToDouble(coordinates.Substring(12, 2));
                            string lon_dir = coordinates.Substring(14, 1);

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
                            Beacon beacon = new Beacon(code, point2D);

                            beaconsList.Add(beacon);
                        }
                    }
                    */

                    return new List<List<double>>();
            }
        }
    }
}