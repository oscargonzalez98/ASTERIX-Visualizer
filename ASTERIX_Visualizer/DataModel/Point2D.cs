using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Point2D
    {
        private double latitude;
        private double longitude;

        public Point2D(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public double getLatitude()
        {
            return latitude;
        }

        public void setLatitude(double latitude)
        {
            this.latitude = latitude;
        }

        public double getLongitude()
        {
            return longitude;
        }

        public void setLongitude(double longitude)
        {
            this.longitude = longitude;
        }

        public override string ToString()
        {
            return $"Point: ({latitude}, {longitude})";
        }
    }
}
