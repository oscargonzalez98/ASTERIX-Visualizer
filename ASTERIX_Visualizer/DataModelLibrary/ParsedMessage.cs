using MultiCAT6.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelLibrary
{
    public class ParsedMessage
    {
        string source;
        string ICAOAddress;
        double TrackNumber;
        double timestamp;
        IMessage raw;

        double lat;
        double lon;
        double alt;

        public ParsedMessage(IMessage message)
        {
            if (message.GetType() == typeof(CAT21)) {
                CAT21 cat21 = (CAT21)message;

                this.source = "ADS-B";
                this.ICAOAddress = cat21.TargetAdress_real;
                this.timestamp = cat21.TimeofASTERIXReportTransmission_seconds;
                this.raw = message;

                this.lat = cat21.coordGeodesic.Lat * GeoUtils.RADS2DEGS;
                this.lon = cat21.coordGeodesic.Lon * GeoUtils.RADS2DEGS;
                this.alt = cat21.coordGeodesic.Height;
            }

            if(message.GetType() == typeof(CAT10))
            {
                CAT10 cat10 = (CAT10)message;

                int SAC = cat10.SAC;
                int SIC = cat10.SIC;

                if (SAC == 0 && SIC == 107)
                {
                    this.source = "MLAT";
                    this.ICAOAddress = cat10.TargetAdress_decoded;
                    this.timestamp = cat10.TimeofDay_seconds;
                    this.raw = message;

                    this.lat = cat10.coordGeodesic.Lat * GeoUtils.RADS2DEGS;
                    this.lon = cat10.coordGeodesic.Lon * GeoUtils.RADS2DEGS;
                    this.alt = cat10.coordGeodesic.Height * GeoUtils.RADS2DEGS;
                }

                else if (SAC == 0 && SIC == 7)
                {
                    this.source = "SMR";
                    this.TrackNumber = cat10.Tracknumber_value;
                    this.timestamp = cat10.TimeofDay_seconds;
                    this.raw = message;

                    this.lat = cat10.coordGeodesic.Lat * GeoUtils.RADS2DEGS;
                    this.lon = cat10.coordGeodesic.Lon * GeoUtils.RADS2DEGS;
                    this.alt = cat10.coordGeodesic.Height * GeoUtils.RADS2DEGS;
                }
            }
        }

        public string getSource()
        {
            return source;
        }

        public string getICAOAddress()
        {
            return ICAOAddress;
        }

        public double getTimestamp()
        {
            return timestamp;
        }

        public IMessage getRaw()
        {
            return raw;
        }

        public void setSource(string source)
        {
            this.source = source;
        }

        public void setICAOAddress(string Id)
        {
            this.ICAOAddress = Id;
        }

        public void setTimestamp(double timestamp)
        {
            this.timestamp = timestamp;
        }

        public void setRaw(IMessage raw)
        {
            this.raw = raw;
        }

        public double getLat()
        {
            return lat;
        }

        public double getLon()
        {
            return lon;
        }

        public double getAlt()
        {
            return alt;
        }

        public void setLat(double lat)
        {
            this.lat = lat;
        }

        public void setLon(double lon)
        {
            this.lon = lon;
        }

        public void setAlt(double alt)
        {
            this.alt = alt;
        }

        public double getTrackNumber()
        {
            return TrackNumber;
        }

        public void setTrackNumber(double trackNumber)
        {
            this.TrackNumber = trackNumber;
        }

        public override string ToString()
        {
            return $"Source: {source}, ICAO Address: {ICAOAddress}, Track Number: {TrackNumber}, Timestamp: {timestamp}, Latitude: {lat}, Longitude: {lon}, Altitude: {alt}";
        }
    }
}
