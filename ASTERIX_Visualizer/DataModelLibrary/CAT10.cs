using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text.RegularExpressions;
using MultiCAT6.Utils;
using DotNetMatrix;
using System.Runtime;

namespace DataModelLibrary
{
    public class CAT10
    {
        // Centro de coordenadas SMR
        double LatSMR = 41 + (17.0 / 60.0) + (44.226 / 3600);
        double LonSMR = 02 + (05.0 / 60.0) + (42.411 / 3600);

        // Centro de coordenadas MLAT
        double LatMLAT = 41 + (17.0 / 60.0) + (49.426 / 3600);
        double LonMLAT = 02 + (04.0 / 60.0) + (42.410 / 3600);
        //double LatMLAT = 41.297063;
        //double LonMLAT = 2.078447;

        // Coordenadas ARP
        double LatARP = 41 + (17.0 / 60.0) + (49.426 / 3600);
        double LonARP = 02 + (04.0 / 60.0) + (42.410 / 3600);

        double E = Math.Sqrt(0.00669437999013);
        double A = 6378137.0;

        GeoUtils GeoUtils1;

        int i = 0;
        public string[] paquete;
        public string[] arrayFSPEC;
        public string FSPEC;
        public string FSPEC_fake;
        public int data_position = 0;

        public string DataSourceIdentifier = "";
        public int SIC;
        public int SAC;

        public string MessageType = "";
        public string MessageType_decodified = "";

        public string TargetReportDescriptor = "";
        public string TYP = "";
        public string DCR = "";
        public string CHN = "";
        public string GBS = "";
        public string CRT = "";
        public string SIM = "";
        public string TST = "";
        public string RAB = "";
        public string LOP = "";
        public string TOT = "";
        public string SPI = "";

        public string TimeofDay = "";
        public double TimeofDay_seconds;
        public double timetotal;

        public string PositioninWGS84_coordinates = "";
        public double latWGS84 = 0;
        public double lonWGS84 = 0;

        public string MeasuredPositioninPolarCoordinates = "";
        public double Theta;
        public double Rho;

        public string PositioninCartesianCoordinates = "";
        public double X_cartesian;
        public double Y_cartesian;

        public string CalculatedTrackVelocityinPolarCoordinates = "";
        public double GroundSpeed;
        public double TrackAngle;

        public CoordinatesWGS84 coordGeodesic;
        public CoordinatesUVH coordStereographic;
        public CoordinatesXYZ coordSystemCartesian;

        public string CalculatedTrackVelocityinCartesianCoordinates = "";
        public double Vx_cartesian;
        public double Vy_cartesian;

        public string TrackNumber = "";
        public double Tracknumber_value;

        public string TrackStatus = "";
        public string CNF = "";
        public string TRE = "";
        public string CST = "";
        public string MAH = "";
        public string TCC = "";
        public string STH = "";
        public string TOM = "";
        public string DOU = "";
        public string MRS = "";
        public string GHO = "";

        public string Mode3ACodeinOctal = "";
        public string V = "";
        public string G = "";
        public string L = "";
        public string Mode3ACodeinOctal_decodified = "";

        public string TargetAdress = "";
        public string TargetAdress_decoded = "";

        public string TargetIdentification = "";
        public string TargetIdentification_decoded = "";
        public string STI = "";

        public string ModeSMBData = "";
        public string[] REP_MS;
        public string[] MBDATA;
        public string[] BDS1;
        public string[] BDS2;

        public string VehicleFleetIdentification = "";
        public string VFI = "";

        public string FlightLevelInBinaryRepresentation = "";
        public string V_fl = "";
        public string G_fl = "";
        public double FlightLevel;

        public string MeasuredHeight = "";
        public double MeasuredHeight_ft;

        public string TargetSizeOrientation = "";
        public double Length;
        public double Orientation;
        public double Width;

        public string SystemStatus = "";
        public string NOGO = "";
        public string OVL = "";
        public string TSV = "";
        public string DIV = "";
        public string TTF = "";

        public string PreProgrammedMessage = "";
        public string TRB = "";
        public string MSG = "";

        public string StandardDeviationofPosition = "";
        public double StdDeviation_x;
        public double StdDeviation_y;
        public double StdDeviation_xy;

        public string Presence = "";
        public int REP;
        public double[] DRHO;
        public double[] DTHETA;

        public string AmplitudeofPrimaryPlot = "";
        public double AmplitudeofPrimaryPlot_value;

        public string CalculatedAcceleration = "";
        public double CalculatedAcceleration_X;
        public double CalculatedAcceleration_Y;

        public string zone = "";
        public double errorX;
        public double errorY;
        public double errorXY;
        public CoordinatesXYZ coordSystemCartesianReference;



        public CAT10(string[] packet)
        {
            this.paquete = packet;
        }

        public string AddZeros(string octeto)
        {
            while (octeto.Length < 8)
            {
                octeto = octeto.Insert(0, "0");
            }

            return octeto;
        }
        public double Calculate_ComplementoA2(string bits)
        {
            if (bits == "1")
                return -1;
            if (bits == "0")
                return 0;
            else
            {
                if (Convert.ToString(bits[0]) == "0")
                {
                    int num = Convert.ToInt32(bits, 2);
                    return Convert.ToSingle(num);
                }
                else
                {
                    //elimino primer bit
                    string bitss = bits.Substring(1, bits.Length - 1);

                    //creo nuevo string cambiando 0s por 1s y viceversa
                    string newbits = "";
                    int i = 0;
                    while (i < bitss.Length)
                    {
                        if (Convert.ToString(bitss[i]) == "1")
                            newbits = newbits + "0";
                        if (Convert.ToString(bitss[i]) == "0")
                            newbits = newbits + "1";
                        i++;
                    }

                    //convertimos a int
                    double num = Convert.ToInt32(newbits, 2);

                    return -(num + 1);
                }
            }
        }
        public void Calculate_DataSourceIdentification(string paquete)
        {
            string string1 = paquete.Substring(0, 8);
            string string2 = paquete.Substring(8, 8);

            SAC = Convert.ToInt32(string1, 2);
            SIC = Convert.ToInt32(string2, 2);

        }
        public void Calculate_MessageType(string paquete)
        {
            int int1 = Convert.ToInt32(paquete, 2);
            if (int1 == 1) { MessageType_decodified = "Target Report"; }
            if (int1 == 2) { MessageType_decodified = "Start of Update Cycle"; }
            if (int1 == 3) { MessageType_decodified = "Periodic Status Message"; }
            if (int1 == 4) { MessageType_decodified = "Event-triggered Status Message"; }
        }
        public void Calculate_TargetReportDescriptor(string paquete)
        {
            string typ = paquete.Substring(0, 3);

            if (typ == "000") { TYP = "SSR multilateration."; }
            if (typ == "001") { TYP = "Mode S multilateration."; }
            if (typ == "010") { TYP = "ADS-B."; }
            if (typ == "011") { TYP = "PSR."; }
            if (typ == "100") { TYP = "Magnetic Loop System."; }
            if (typ == "101") { TYP = "HF multilateration."; }
            if (typ == "110") { TYP = "Not defined."; }
            if (typ == "111") { TYP = "Other types."; }

            string dcr1 = paquete.Substring(3, 1);
            if (dcr1 == "0") { DCR = "No differential correction (ADS-B)."; }
            else { DCR = "Differential correction (ADS-B)."; }

            string chn1 = paquete.Substring(4, 1);
            if (chn1 == "0") { CHN = "Chain 1."; }
            else { CHN = "Chain 2."; }

            string gbs1 = paquete.Substring(5, 1);
            if (gbs1 == "0") { GBS = "Transponder Ground bit not set."; }
            else { GBS = "Transponder Ground bit set."; }

            string crt1 = paquete.Substring(6, 1);
            if (crt1 == "0") { CRT = "No Corrupted reply in multilateration."; }
            else { CRT = "Corrupted reply in multilateration."; }


            if (paquete.Length > 8)
            {
                string sim1 = paquete.Substring(8, 1);
                if (sim1 == "0") { SIM = "Actual target report."; }
                else { SIM = "Simulated target report."; }

                string tst1 = paquete.Substring(9, 1);
                if (tst1 == "0") { TST = "Default."; }
                else { TST = "Test target."; }

                string rab1 = paquete.Substring(10, 1);
                if (rab1 == "0") { RAB = "Report from target transponder."; }
                else { RAB = "Report from field monitor (fixed transponder)."; }

                string lop = paquete.Substring(11, 2);
                if (lop == "00") { LOP = "Undeterminated."; }
                if (lop == "01") { LOP = "Loop start."; }
                else { LOP = "Loop finish."; }

                string tot = paquete.Substring(13, 2);
                if (tot == "00") { TOT = "Undetermined."; }
                if (tot == "01") { TOT = "Aircraft."; }
                if (tot == "10")
                {
                    TOT = "Ground Vehicle.";
                }
                if (tot == "11") { TOT = "Helicopter."; }

                if (paquete.Length > 16)
                {
                    string spi1 = paquete.Substring(16, 1); ;
                    if (spi1 == "0") { SPI = "Absence of SPI"; }
                    else { SPI = "Special Position Identification."; }
                }
            }
        }
        public void Calculate_PositionWGS84_coordinates(string paquete)
        {
            string str1 = paquete.Substring(0, 32);
            string str2 = paquete.Substring(32, 32);

            double a1 = Calculate_ComplementoA2(str1);
            double b1 = Calculate_ComplementoA2(str2);

            latWGS84 = a1 * (180 / Math.Pow(2, 31));
            lonWGS84 = b1 * (180 / Math.Pow(2, 31));
        }
        public void Calculate_TrackStatus(string paquete)
        {
            string cnf = Convert.ToString(paquete[0]);
            if (cnf == "0") { CNF = "Confirmed track."; }
            else { CNF = "Track initialisation phase."; }

            string tre = Convert.ToString(paquete[1]);
            if (tre == "0") { TRE = "Default."; }
            else { TRE = "Last report for a track."; }

            string cst = paquete.Substring(2, 2);

            if (cst == "00") { CST = "No extrapolation."; }
            if (cst == "01") { CST = "Predictable extrapolation due to sensor refresh period."; }
            if (cst == "10") { CST = " Predictable extrapolation in masked area."; }
            if (cst == "11") { CST = "Extrapolation due to unpredictable absence of detection."; }

            string mah = Convert.ToString(paquete[4]);
            if (mah == "0") { MAH = "Default."; }
            else { MAH = "Horizontal manoeuvre."; }

            string tcc = Convert.ToString(paquete[5]);
            if (tcc == "0") { TCC = "Tracking performed in 'Sensor Plane', i.e. neither slant range correction nor projection was applied."; }
            else { TCC = "Slant range correction and a suitable projection technique are used to track in a 2D.reference plane, tangential to the earth model at the Sensor Site co-ordinates."; }

            string sth = Convert.ToString(paquete[6]);
            if (sth == "0") { STH = "Measured position."; }
            else { STH = "Smoothed position."; }

            if (paquete.Length > 8)
            {
                string tom = paquete.Substring(8, 2);
                if (tom == "00") { TOM = "Unknown type of movement."; }
                if (tom == "01") { TOM = "Taking-off."; }
                if (tom == "10") { TOM = "Landing."; }
                if (tom == "11") { TOM = "Other types of movement."; }

                string dou = paquete.Substring(10, 3);

                if (dou == "000") { DOU = "No doubt."; }
                if (dou == "001") { DOU = " Doubtful correlation (undetermined reason)."; }
                if (dou == "010") { DOU = " Doubtful correlation in clutter."; }
                if (dou == "011") { DOU = " Loss of accuracy."; }
                if (dou == "100") { DOU = " Loss of accuracy in clutter."; }
                if (dou == "101") { DOU = "Unstable track."; }
                if (dou == "110") { DOU = "Previously coasted."; }

                string mrs = paquete.Substring(13, 2);

                if (mrs == "00") { MRS = "Merge or split indication undetermined."; }
                if (mrs == "01") { MRS = "Track merged by association to plot."; }
                if (mrs == "10") { MRS = "Track merged by non-association to ."; }
                if (mrs == "11") { MRS = "Split track."; }

                if (paquete.Length > 16)
                {
                    string gho = Convert.ToString(paquete[16]);
                    if (gho == "0") { GHO = "default."; }
                    else { GHO = "Ghost track."; }
                }


            }

        }
        public void Calculate_Mode3ACode_octal(string paquete)
        {
            string v1 = paquete.Substring(0, 1);
            if (v1 == "0") { V = "Code validated."; }
            if (v1 == "1") { V = "Code not validated."; }

            string g1 = paquete.Substring(1, 1);
            if (g1 == "0") { G = "Default."; }
            if (g1 == "1") { G = "Garbled code."; }

            string l1 = paquete.Substring(2, 1);
            if (l1 == "0") { L = "Mode-3/A code derived from the reply of the transponder."; }
            if (l1 == "1") { L = "Mode-3/A code not extracted during the last scan."; }

            string str1 = paquete.Substring(4, 12);
            Mode3ACodeinOctal_decodified = Convert.ToString(Convert.ToInt32(str1, 2), 8);
            while (Mode3ACodeinOctal_decodified.Length < 4)
            {
                Mode3ACodeinOctal_decodified = String.Concat("0", Mode3ACodeinOctal_decodified);
            }
        }
        public void Calculate_TargetIdentification(string paquete)
        {
            int i = 0;
            while (i < 8)
            {
                string str1 = paquete.Substring(i * 6, 6);

                int b1 = Convert.ToInt32(Convert.ToString(str1[5]));
                int b2 = Convert.ToInt32(Convert.ToString(str1[4]));
                int b3 = Convert.ToInt32(Convert.ToString(str1[3]));
                int b4 = Convert.ToInt32(Convert.ToString(str1[2]));
                int b5 = Convert.ToInt32(Convert.ToString(str1[1]));
                int b6 = Convert.ToInt32(Convert.ToString(str1[0]));

                if (b4 == 0 && b3 == 0 && b2 == 0 && b1 == 1 && b5 == 0 && b6 == 0)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "A");
                }
                if (b4 == 0 && b3 == 0 && b2 == 1 && b1 == 0 && b5 == 0 && b6 == 0)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "B");
                }
                if (b4 == 0 && b3 == 0 && b2 == 1 && b1 == 1 && b5 == 0 && b6 == 0)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "C");
                }
                if (b4 == 0 && b3 == 1 && b2 == 0 && b1 == 0 && b5 == 0 && b6 == 0)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "D");
                }
                if (b4 == 0 && b3 == 1 && b2 == 0 && b1 == 1 && b5 == 0 && b6 == 0)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "E");
                }
                if (b4 == 0 && b3 == 1 && b2 == 1 && b1 == 0 && b5 == 0 && b6 == 0)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "F");
                }
                if (b4 == 0 && b3 == 1 && b2 == 1 && b1 == 1 && b5 == 0 && b6 == 0)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "G");
                }
                if (b4 == 1 && b3 == 0 && b2 == 0 && b1 == 0 && b5 == 0 && b6 == 0)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "H");
                }
                if (b4 == 1 && b3 == 0 && b2 == 0 && b1 == 1 && b5 == 0 && b6 == 0)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "I");
                }
                if (b4 == 1 && b3 == 0 && b2 == 1 && b1 == 0 && b5 == 0 && b6 == 0)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "J");
                }
                if (b4 == 1 && b3 == 0 && b2 == 1 && b1 == 1 && b5 == 0 && b6 == 0)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "K");
                }
                if (b4 == 1 && b3 == 1 && b2 == 0 && b1 == 0 && b5 == 0 && b6 == 0)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "L");
                }
                if (b4 == 1 && b3 == 1 && b2 == 0 && b1 == 1 && b5 == 0 && b6 == 0)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "M");
                }
                if (b4 == 1 && b3 == 1 && b2 == 1 && b1 == 0 && b5 == 0 && b6 == 0)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "N");
                }
                if (b4 == 1 && b3 == 1 && b2 == 1 && b1 == 1 && b5 == 0 && b6 == 0)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "O");
                }
                if (b4 == 0 && b3 == 0 && b2 == 0 && b1 == 0 && b5 == 1 && b6 == 0)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "P");
                }
                if (b4 == 0 && b3 == 0 && b2 == 0 && b1 == 1 && b5 == 1 && b6 == 0)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "Q");
                }
                if (b4 == 0 && b3 == 0 && b2 == 1 && b1 == 0 && b5 == 1 && b6 == 0)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "R");
                }
                if (b4 == 0 && b3 == 0 && b2 == 1 && b1 == 1 && b5 == 1 && b6 == 0)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "S");
                }
                if (b4 == 0 && b3 == 1 && b2 == 0 && b1 == 0 && b5 == 1 && b6 == 0)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "T");
                }
                if (b4 == 0 && b3 == 1 && b2 == 0 && b1 == 1 && b5 == 1 && b6 == 0)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "U");
                }
                if (b4 == 0 && b3 == 1 && b2 == 1 && b1 == 0 && b5 == 1 && b6 == 0)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "V");
                }
                if (b4 == 0 && b3 == 1 && b2 == 1 && b1 == 1 && b5 == 1 && b6 == 0)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "W");
                }
                if (b4 == 1 && b3 == 0 && b2 == 0 && b1 == 0 && b5 == 1 && b6 == 0)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "X");
                }
                if (b4 == 1 && b3 == 0 && b2 == 0 && b1 == 1 && b5 == 1 && b6 == 0)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "Y");
                }
                if (b4 == 1 && b3 == 0 && b2 == 1 && b1 == 0 && b5 == 1 && b6 == 0)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "Z");
                }
                if (b4 == 0 && b3 == 0 && b2 == 0 && b1 == 0 && b5 == 1 && b6 == 1)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "0");
                }
                if (b4 == 0 && b3 == 0 && b2 == 0 && b1 == 1 && b5 == 1 && b6 == 1)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "1");
                }
                if (b4 == 0 && b3 == 0 && b2 == 1 && b1 == 0 && b5 == 1 && b6 == 1)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "2");
                }
                if (b4 == 0 && b3 == 0 && b2 == 1 && b1 == 1 && b5 == 1 && b6 == 1)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "3");
                }
                if (b4 == 0 && b3 == 1 && b2 == 0 && b1 == 0 && b5 == 1 && b6 == 1)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "4");
                }
                if (b4 == 0 && b3 == 1 && b2 == 0 && b1 == 1 && b5 == 1 && b6 == 1)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "5");
                }
                if (b4 == 0 && b3 == 1 && b2 == 1 && b1 == 0 && b5 == 1 && b6 == 1)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "6");
                }
                if (b4 == 0 && b3 == 1 && b2 == 1 && b1 == 1 && b5 == 1 && b6 == 1)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "7");
                }
                if (b4 == 1 && b3 == 0 && b2 == 0 && b1 == 0 && b5 == 1 && b6 == 1)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "8");
                }
                if (b4 == 1 && b3 == 0 && b2 == 0 && b1 == 1 && b5 == 1 && b6 == 1)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "9");
                }
                if (b4 == 0 && b3 == 0 && b2 == 0 && b1 == 0 && b5 == 0 && b6 == 1)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "");
                }
                if (b4 == 0 && b3 == 0 && b2 == 0 && b1 == 0 && b5 == 0 && b6 == 0)
                {
                    TargetIdentification_decoded = String.Concat(TargetIdentification_decoded, "");
                }

                i = i + 1;

            }

        }

        public int Calculate_ModeSMBData(string[] paquete, int pos)
        {
            //contador dels octets q ocupa
            int cont = 1;

            //el primer octet és el número de missatges
            int REP = int.Parse(paquete[pos], System.Globalization.NumberStyles.HexNumber);

            //creamos los vectores:
            this.MBDATA = new string[REP];
            this.BDS1 = new string[REP];
            this.BDS2 = new string[REP];

            //agafem les dades
            int i = 0;
            while (i < REP)
            {
                //MB Data
                string mbdata = "";

                int j = 0;
                while (j < 7) //7 octets
                {
                    mbdata = mbdata + paquete[pos + cont + j];

                    j++;
                }

                this.MBDATA[i] = Convert.ToString(Convert.ToInt32(mbdata, 2), 16).PadLeft(8, '0');

                //BDS1 & BDS2
                string octet8 = Convert.ToString(Convert.ToInt32(paquete[pos + cont + 7], 16), 2).PadLeft(8, '0');

                this.BDS1[i] = octet8.Substring(0, 4);
                this.BDS2[i] = octet8.Substring(4, 4);

                cont = cont + 8;

                i++;
            }

            return cont;
        }

        public void Calculate_VehicleFleetIdentification(string paquete)
        {
            int vfi1 = Convert.ToInt32(paquete, 2);
            if (vfi1 == 0) { VFI = "Unknown."; }
            if (vfi1 == 1) { VFI = "ATC equipment maintenance."; }
            if (vfi1 == 2) { VFI = "Airport maintenance."; }
            if (vfi1 == 3) { VFI = "Fire."; }
            if (vfi1 == 4) { VFI = "Bird scarer."; }
            if (vfi1 == 5) { VFI = "Snow plough."; }
            if (vfi1 == 6) { VFI = "Runway sweeper."; }
            if (vfi1 == 7) { VFI = "Emergency."; }
            if (vfi1 == 8) { VFI = "Police."; }
            if (vfi1 == 9) { VFI = "Bus."; }
            if (vfi1 == 10) { VFI = "Tug(push/tow)."; }
            if (vfi1 == 11) { VFI = "Grass cuttert."; }
            if (vfi1 == 12) { VFI = "Fuel."; }
            if (vfi1 == 13) { VFI = "Baggage."; }
            if (vfi1 == 14) { VFI = "Catering."; }
            if (vfi1 == 15) { VFI = "Aircraft maintenance."; }
            if (vfi1 == 16) { VFI = "Flyco(follow me)."; }
        }
        public void Calculate_TargetSizeOrientation(string paquete)
        {
            string length = paquete.Substring(0, 7);
            Length = Convert.ToInt32(length, 2);

            if (paquete.Length > 8)
            {
                string orientation = paquete.Substring(8, 7);
                Orientation = Convert.ToInt32(orientation, 2) * 360 / 128;

                if (paquete.Length > 16)
                {
                    string width = paquete.Substring(16, 7);
                    Width = Convert.ToInt32(width, 2);
                }
            }
        }
        public void Calculate_SystemStatus(string paquete)
        {
            string nogo = paquete.Substring(0, 2);
            if (nogo == "00") { NOGO = "Operational."; }
            if (nogo == "01") { NOGO = "Degraded."; }
            if (nogo == "10") { NOGO = "NOGO"; }

            string ovl = paquete.Substring(2, 1);
            if (ovl == "0") { OVL = "No overload."; }
            else { OVL = "Overload."; }

            string tsv = paquete.Substring(3, 1);
            if (tsv == "0") { TSV = "Valid."; }
            else { TSV = "Invalid."; }

            string div = paquete.Substring(4, 1);
            if (div == "0") { DIV = "Normal operation."; }
            else { DIV = "Diversity degraded."; }

            string ttf = paquete.Substring(5, 0);
            if (ttf == "0") { TTF = "Test Target Operative."; }
            else { TTF = "Test Target Failure."; }
        }

        public int ComputePresence(string[] paquete, int pos) // Data Item I010/280
        {
            //contador
            int cont = 1;

            //el primer octet és el número de diferències
            int REP = int.Parse(paquete[pos], System.Globalization.NumberStyles.HexNumber);

            //creamos los vectores
            this.DRHO = new double[REP];
            this.DTHETA = new double[REP];

            //agafem les dades
            int i = 0;
            while (i < REP)
            {
                //agafem els octets en string de bits:
                string octetRHO = Convert.ToString(Convert.ToInt32(paquete[pos + cont], 16), 2).PadLeft(8, '0');
                string octetoDTHETA = Convert.ToString(Convert.ToInt32(paquete[pos + cont + 1], 16), 2).PadLeft(8, '0');

                //agafem el num fent el complement a2:
                double rho = this.Calculate_ComplementoA2(octetRHO);
                double dtheta = this.Calculate_ComplementoA2(octetoDTHETA);

                //multipliquem per la resolució
                this.DRHO[i] = rho * 1;
                this.DTHETA[i] = dtheta * 0.15;

                this.Presence = this.DRHO[i] + " m, " + this.DTHETA[i] + "º \n";

                cont = cont + 2;

                i++;
            }

            return cont;
        }


        public void Calculate_FSPEC(string[] paquete)
        {
            GeoUtils1 = new GeoUtils( E, A, new CoordinatesWGS84(LatARP * GeoUtils.DEGS2RADS, LonARP * GeoUtils.DEGS2RADS, 0));

            int j = 3;
            bool found = false;

            while (found == false)
            {
                FSPEC = Convert.ToString(Convert.ToInt32(paquete[j], 16), 2);// Convertir de hex a binario paquete [3]
                FSPEC = AddZeros(FSPEC);

                if (Char.ToString(FSPEC[FSPEC.Length - 1]) == "1")
                {
                    while (Char.ToString(FSPEC[FSPEC.Length - 1]) != "0")
                    {
                        j = j + 1;
                        string parte2 = Convert.ToString(Convert.ToInt32(paquete[j], 16), 2);
                        parte2 = AddZeros(parte2);

                        FSPEC = String.Concat(FSPEC, parte2);
                    }

                    found = true;
                }

                found = true;
            }

            FSPEC_fake = FSPEC;

            while (FSPEC_fake.Length < (4 * 8))
            {
                FSPEC_fake = String.Concat(FSPEC_fake, "0");
            }

            data_position = 1 + 2 + ((FSPEC.Length) / 8);

            //-------------------------------------------------------------------------------------------------------------
            // Aqui porocesamos los parametros que hemos encontrado en el FSPEC
            //-------------------------------------------------------------------------------------------------------------

            if (FSPEC.Length > 0)
            {
                if (Char.ToString(FSPEC_fake[0]) == "1") // 1 I021/010 Data Source Identification
                {

                    string string1 = Convert.ToString(paquete[data_position]);
                    string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                    string1 = AddZeros(string1);

                    string string2 = Convert.ToString(paquete[data_position + 1]);
                    string2 = Convert.ToString(Convert.ToInt32(string2, 16), 2);
                    string2 = AddZeros(string2);

                    DataSourceIdentifier = String.Concat(string1, string2);

                    data_position = data_position + 2;

                    Calculate_DataSourceIdentification(DataSourceIdentifier);

                } // 1 I021/010 Data Source Identification

                if (Char.ToString(FSPEC_fake[1]) == "1") // 2 I010/000 Message Type 
                {

                    string string1 = Convert.ToString(paquete[data_position]);
                    string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                    string1 = AddZeros(string1);

                    MessageType = string1;

                    data_position = data_position + 1;

                    Calculate_MessageType(MessageType);

                }// 2 I010/000 Message Type 

                if (Char.ToString(FSPEC_fake[2]) == "1") // 3 I010/020 Target Report Descriptor  
                {
                    // primero leemos el primer paquete y lo pasamos a binario

                    string string_packet = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                    string_packet = AddZeros(string_packet);

                    if ((Convert.ToString(string_packet[7])) == "0") // si ultima posicion es un 0 guardamos el octeto y pasamos al siguiente
                    {
                        TargetReportDescriptor = string_packet;
                        data_position = data_position + 1;
                    }
                    if ((Convert.ToString(string_packet[7])) == "1" && string_packet.Length < 24) // si ultimo valor =1 hacemos un bucle que vaya concatenando todos los octetos acabados en 1
                    {
                        i = 0;
                        data_position = data_position + 1;
                        while ((Convert.ToString(string_packet[string_packet.Length - 1])) == "1")
                        {
                            string string_packet2 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                            string_packet2 = AddZeros(string_packet2);
                            string_packet = string.Concat(string_packet, string_packet2);
                            data_position = data_position + 1;
                            i = i + 1;
                        }
                    }

                    TargetReportDescriptor = string_packet;

                    Calculate_TargetReportDescriptor(TargetReportDescriptor);

                } // 3 I010/020 Target Report Descriptor  

                if (Char.ToString(FSPEC_fake[3]) == "1") // 4 I010 / 140 Time of Day
                {

                    string string1 = Convert.ToString(paquete[data_position]);
                    string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                    string1 = AddZeros(string1);

                    string string2 = Convert.ToString(paquete[data_position + 1]);
                    string2 = Convert.ToString(Convert.ToInt32(string2, 16), 2);
                    string2 = AddZeros(string2);

                    string string3 = Convert.ToString(paquete[data_position + 2]);
                    string3 = Convert.ToString(Convert.ToInt32(string3, 16), 2);
                    string3 = AddZeros(string3);

                    data_position = data_position + 3;

                    TimeofDay = String.Concat(string1, string2, string3);

                    TimeofDay_seconds = Convert.ToInt32(TimeofDay, 2);
                    TimeofDay_seconds = TimeofDay_seconds / 128;

                    timetotal = TimeofDay_seconds;

                    int a = 1;

                }// 4 I010 / 140 Time of Day

                if (Char.ToString(FSPEC_fake[4]) == "1") // 5 I010 / 041  Position in WGS-84 Co-ordinates 
                {
                    string string1 = Convert.ToString(paquete[data_position]);
                    string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                    string1 = AddZeros(string1);

                    string string2 = Convert.ToString(paquete[data_position + 1]);
                    string2 = Convert.ToString(Convert.ToInt32(string2, 16), 2);
                    string2 = AddZeros(string2);

                    string string3 = Convert.ToString(paquete[data_position + 2]);
                    string3 = Convert.ToString(Convert.ToInt32(string3, 16), 2);
                    string3 = AddZeros(string3);

                    string string4 = Convert.ToString(paquete[data_position + 3]);
                    string4 = Convert.ToString(Convert.ToInt32(string4, 16), 2);
                    string4 = AddZeros(string4);

                    string string5 = Convert.ToString(paquete[data_position + 4]);
                    string5 = Convert.ToString(Convert.ToInt32(string5, 16), 2);
                    string5 = AddZeros(string5);

                    string string6 = Convert.ToString(paquete[data_position + 5]);
                    string6 = Convert.ToString(Convert.ToInt32(string6, 16), 2);
                    string6 = AddZeros(string6);

                    string string7 = Convert.ToString(paquete[data_position + 6]);
                    string7 = Convert.ToString(Convert.ToInt32(string7, 16), 2);
                    string7 = AddZeros(string7);

                    string string8 = Convert.ToString(paquete[data_position + 7]);
                    string8 = Convert.ToString(Convert.ToInt32(string8, 16), 2);
                    string8 = AddZeros(string8);

                    PositioninWGS84_coordinates = String.Concat(string1, string2, string3, string4, string5, string6, string7, string8);

                    data_position = data_position + 8;

                    Calculate_PositionWGS84_coordinates(PositioninWGS84_coordinates);

                } // 5 I010 / 041  Position in WGS-84 Co-ordinates 

                if (Char.ToString(FSPEC_fake[5]) == "1") // 6 I010/040  Measured Position in Polar Co-ordinates 
                {

                    string string1 = Convert.ToString(paquete[data_position]);
                    string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                    string1 = AddZeros(string1);

                    string string2 = Convert.ToString(paquete[data_position + 1]);
                    string2 = Convert.ToString(Convert.ToInt32(string2, 16), 2);
                    string2 = AddZeros(string2);

                    string string3 = Convert.ToString(paquete[data_position + 2]);
                    string3 = Convert.ToString(Convert.ToInt32(string3, 16), 2);
                    string3 = AddZeros(string3);

                    string string4 = Convert.ToString(paquete[data_position + 3]);
                    string4 = Convert.ToString(Convert.ToInt32(string4, 16), 2);
                    string4 = AddZeros(string4);

                    data_position = data_position + 4;

                    MeasuredPositioninPolarCoordinates = String.Concat(string1, string2, string3, string4);

                    string rho1 = (MeasuredPositioninPolarCoordinates.Substring(0, 16));
                    string theta1 = (MeasuredPositioninPolarCoordinates.Substring(16, 16));

                    Rho = Convert.ToInt32(rho1, 2);
                    Theta = Convert.ToInt32(theta1, 2) * 360 / (Math.Pow(2, 16));

                    //// Calculamos Coordenadas WGS84
                    //CoordinatesPolar coordRadarSpherical = new CoordinatesPolar(Rho, Theta*GeoUtils.DEGS2RADS,0);
                    //CoordinatesXYZ coordRadarCartesian = GeoUtils1.change_radar_spherical2radar_cartesian(coordRadarSpherical);
                    //CoordinatesXYZ coordGeocentric = GeoUtils1.change_radar_cartesian2geocentric(new CoordinatesWGS84(LatSMR * GeoUtils.DEGS2RADS, LonSMR * GeoUtils.DEGS2RADS), coordRadarCartesian);
                    //coordinatesWGS84 = GeoUtils1.change_geocentric2geodesic(coordGeocentric);

                    //// Calculamos Coordenadas Stereograficas
                    //CoordinatesXYZ coordSystemCartesian = GeoUtils1.change_radar_cartesian2system_cartesian(new CoordinatesWGS84(LatSMR * GeoUtils.DEGS2RADS, LonSMR * GeoUtils.DEGS2RADS), coordRadarCartesian);
                    //coordinatesUVH = GeoUtils1.change_system_cartesian2stereographic(coordSystemCartesian);

                }// 6 I010/040  Measured Position in Polar Co-ordinates 

                if (Char.ToString(FSPEC_fake[6]) == "1") // 7 I010/042  Position in Cartesian Co-ordinates
                {
                    string string1 = Convert.ToString(paquete[data_position]);
                    string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                    string1 = AddZeros(string1);

                    string string2 = Convert.ToString(paquete[data_position + 1]);
                    string2 = Convert.ToString(Convert.ToInt32(string2, 16), 2);
                    string2 = AddZeros(string2);

                    string string3 = Convert.ToString(paquete[data_position + 2]);
                    string3 = Convert.ToString(Convert.ToInt32(string3, 16), 2);
                    string3 = AddZeros(string3);

                    string string4 = Convert.ToString(paquete[data_position + 3]);
                    string4 = Convert.ToString(Convert.ToInt32(string4, 16), 2);
                    string4 = AddZeros(string4);

                    PositioninCartesianCoordinates = String.Concat(string1, string2, string3, string4);

                    data_position = data_position + 4;

                    string xcartesian = (PositioninCartesianCoordinates.Substring(0, 16));
                    string ycartesian = (PositioninCartesianCoordinates.Substring(16, 16));

                    X_cartesian = Calculate_ComplementoA2(xcartesian);
                    Y_cartesian = Calculate_ComplementoA2(ycartesian);

                    //// Calculamos Coordenadas WGS84
                    //CoordinatesXYZ coordRadarCartesian = new CoordinatesXYZ(X_cartesian, Y_cartesian, 0);
                    //CoordinatesXYZ coordGeocentric = GeoUtils1.change_radar_cartesian2geocentric(new CoordinatesWGS84(LatARP*GeoUtils.DEGS2RADS, LonARP*GeoUtils.DEGS2RADS), coordRadarCartesian);
                    //coordinatesWGS84 = GeoUtils1.change_geocentric2geodesic(coordGeocentric);

                    //// Calculamos Coordenadas Stereograficas
                    //CoordinatesXYZ coordSystemCartesian = GeoUtils1.change_radar_cartesian2system_cartesian(new CoordinatesWGS84(LatARP * GeoUtils.DEGS2RADS, LonARP * GeoUtils.DEGS2RADS), coordRadarCartesian);
                    //coordinatesUVH = GeoUtils1.change_system_cartesian2stereographic(coordSystemCartesian);

                }// 7 I010/042  Position in Cartesian Co-ordinates

                if (Char.ToString(FSPEC_fake[7]) == "1") // 8 - _FX
                {

                } // FX

                if (FSPEC.Length > 8)
                {
                    if (Char.ToString(FSPEC_fake[8]) == "1") // 8 I010/200  Calculated Track Velocity in Polar Co-ordinates
                    {
                        string string1 = Convert.ToString(paquete[data_position]);
                        string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                        string1 = AddZeros(string1);

                        string string2 = Convert.ToString(paquete[data_position + 1]);
                        string2 = Convert.ToString(Convert.ToInt32(string2, 16), 2);
                        string2 = AddZeros(string2);

                        string string3 = Convert.ToString(paquete[data_position + 2]);
                        string3 = Convert.ToString(Convert.ToInt32(string3, 16), 2);
                        string3 = AddZeros(string3);

                        string string4 = Convert.ToString(paquete[data_position + 3]);
                        string4 = Convert.ToString(Convert.ToInt32(string4, 16), 2);
                        string4 = AddZeros(string4);

                        CalculatedTrackVelocityinPolarCoordinates = String.Concat(string1, string2, string3, string4);

                        data_position = data_position + 4;

                        string gs1 = CalculatedTrackVelocityinPolarCoordinates.Substring(0, 16);
                        string ta1 = CalculatedTrackVelocityinPolarCoordinates.Substring(16, 16);

                        GroundSpeed = Convert.ToInt32(gs1, 2) * (Math.Pow(2, -14));
                        TrackAngle = Convert.ToInt32(ta1, 2) * 360 / (Math.Pow(2, 16));


                    }// 8 I010/200  Calculated Track Velocity in Polar Co-ordinates

                    if (Char.ToString(FSPEC_fake[9]) == "1") // 9 I010/202  Calculated Track Velocity in Cartesian Coord
                    {
                        string string1 = Convert.ToString(paquete[data_position]);
                        string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                        string1 = AddZeros(string1);

                        string string2 = Convert.ToString(paquete[data_position + 1]);
                        string2 = Convert.ToString(Convert.ToInt32(string2, 16), 2);
                        string2 = AddZeros(string2);

                        string string3 = Convert.ToString(paquete[data_position + 2]);
                        string3 = Convert.ToString(Convert.ToInt32(string3, 16), 2);
                        string3 = AddZeros(string3);

                        string string4 = Convert.ToString(paquete[data_position + 3]);
                        string4 = Convert.ToString(Convert.ToInt32(string4, 16), 2);
                        string4 = AddZeros(string4);

                        CalculatedTrackVelocityinCartesianCoordinates = String.Concat(string1, string2, string3, string4);
                        data_position = data_position + 4;

                        string vx = CalculatedTrackVelocityinCartesianCoordinates.Substring(0, 16);
                        string vy = CalculatedTrackVelocityinCartesianCoordinates.Substring(16, 16);

                        Vx_cartesian = Calculate_ComplementoA2(vx) * 0.25;
                        Vy_cartesian = Calculate_ComplementoA2(vy) * 0.25;
                    } // 9 I010/202  Calculated Track Velocity in Cartesian Coord

                    if (Char.ToString(FSPEC_fake[10]) == "1") // 10 I010/161  Track Number 
                    {
                        string string1 = Convert.ToString(paquete[data_position]);
                        string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                        string1 = AddZeros(string1);

                        string string2 = Convert.ToString(paquete[data_position + 1]);
                        string2 = Convert.ToString(Convert.ToInt32(string2, 16), 2);
                        string2 = AddZeros(string2);

                        TrackNumber = String.Concat(string1, string2);

                        data_position = data_position + 2;

                        string tn = TrackNumber.Substring(4, 12);
                        Tracknumber_value = Convert.ToInt32(tn, 2);

                    } // 10 I010/161  Track Number 

                    if (Char.ToString(FSPEC_fake[11]) == "1") // 11 I010/170  Track Status 
                    {
                        // primero leemos el primer paquete y lo pasamos a binario

                        string string_packet = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                        string_packet = AddZeros(string_packet);

                        if ((Convert.ToString(string_packet[7])) == "0") // si ultima posicion es un 0 guardamos el octeto y pasamos al siguiente
                        {
                            TrackStatus = string_packet;
                            data_position = data_position + 1;
                        }
                        if ((Convert.ToString(string_packet[7])) == "1") // si ultimo valor =1 hacemos un bucle que vaya concatenando todos los octetos acabados en 1
                        {
                            i = 0;
                            data_position = data_position + 1;
                            while ((Convert.ToString(string_packet[string_packet.Length - 1])) == "1" && string_packet.Length < 24)
                            {
                                string string_packet2 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                                string_packet2 = AddZeros(string_packet2);
                                string_packet = string.Concat(string_packet, string_packet2);
                                data_position = data_position + 1;
                                i = i + 1;
                            }
                        }

                        TrackStatus = string_packet;

                        Calculate_TrackStatus(TrackStatus);

                    }// 11 I010/170  Track Status 

                    if (Char.ToString(FSPEC_fake[12]) == "1") // 12  I010/060  Mode-3/A Code in Octal Representation 
                    {
                        string string1 = Convert.ToString(paquete[data_position]);
                        string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                        string1 = AddZeros(string1);

                        string string2 = Convert.ToString(paquete[data_position + 1]);
                        string2 = Convert.ToString(Convert.ToInt32(string2, 16), 2);
                        string2 = AddZeros(string2);

                        Mode3ACodeinOctal = String.Concat(string1, string2);

                        data_position = data_position + 2;

                        Calculate_Mode3ACode_octal(Mode3ACodeinOctal);

                    }// 12  I010/060  Mode-3/A Code in Octal Representation 

                    if (Char.ToString(FSPEC_fake[13]) == "1") // 13  I010 / 220 Target Adress 
                    {
                        string string1 = Convert.ToString(paquete[data_position]);
                        string str1 = string1;
                        string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                        string1 = AddZeros(string1);

                        string string2 = Convert.ToString(paquete[data_position + 1]);
                        string str2 = string2;
                        string2 = Convert.ToString(Convert.ToInt32(string2, 16), 2);
                        string2 = AddZeros(string2);

                        string string3 = Convert.ToString(paquete[data_position + 2]);
                        string str3 = string3;
                        string3 = Convert.ToString(Convert.ToInt32(string3, 16), 2);
                        string3 = AddZeros(string3);

                        TargetAdress = String.Concat(string1, string2, string3);

                        data_position = data_position + 3;

                        TargetAdress_decoded = String.Concat(str1, str2, str3);

                    }// 13  I010 / 220 Target Adress 

                    if (Char.ToString(FSPEC_fake[14]) == "1") // 14  I010/245 Target Identification   
                    {
                        string string1 = Convert.ToString(paquete[data_position]);
                        string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                        string1 = AddZeros(string1);

                        string string2 = Convert.ToString(paquete[data_position + 1]);
                        string2 = Convert.ToString(Convert.ToInt32(string2, 16), 2);
                        string2 = AddZeros(string2);

                        string string3 = Convert.ToString(paquete[data_position + 2]);
                        string3 = Convert.ToString(Convert.ToInt32(string3, 16), 2);
                        string3 = AddZeros(string3);

                        string string4 = Convert.ToString(paquete[data_position + 3]);
                        string4 = Convert.ToString(Convert.ToInt32(string4, 16), 2);
                        string4 = AddZeros(string4);

                        string string5 = Convert.ToString(paquete[data_position + 4]);
                        string5 = Convert.ToString(Convert.ToInt32(string5, 16), 2);
                        string5 = AddZeros(string5);

                        string string6 = Convert.ToString(paquete[data_position + 5]);
                        string6 = Convert.ToString(Convert.ToInt32(string6, 16), 2);
                        string6 = AddZeros(string6);

                        string string7 = Convert.ToString(paquete[data_position + 6]);
                        string7 = Convert.ToString(Convert.ToInt32(string7, 16), 2);
                        string7 = AddZeros(string7);

                        TargetIdentification = String.Concat(string1, string2, string3, string4, string5, string6, string7);

                        data_position = data_position + 7;

                        string sti1 = TargetIdentification.Substring(0, 2);
                        if (sti1 == "00") { STI = "Callsign or registration downlinked from transponder."; }
                        if (sti1 == "01") { STI = "Callsign not downlinked from transponder."; }
                        else { STI = "Registration not downlinked from transponder."; }

                        string todecode = TargetIdentification.Substring(2, 48);

                        Calculate_TargetIdentification(todecode);

                    } // 14  I010/245 Target Identification 

                    if (Char.ToString(FSPEC_fake[15]) == "1")  // FX - Field Extension Indicator
                    {

                    } // FX

                    if (FSPEC.Length > 16)
                    {
                        if (Char.ToString(FSPEC_fake[16]) == "1") // 15  I010/250  Mode S MB Data
                        {
                            data_position = data_position + Calculate_ModeSMBData(paquete, data_position);

                        }// 15  I010/250  Mode S MB Data

                        if (Char.ToString(FSPEC_fake[17]) == "1") // 16  I010/300 Vehicle Fleet Identification 
                        {
                            string string1 = Convert.ToString(paquete[data_position]);
                            string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                            string1 = AddZeros(string1);

                            data_position = data_position + 1;

                            VehicleFleetIdentification = string1;
                            Calculate_VehicleFleetIdentification(VehicleFleetIdentification);
                        } // 16  I010/300 Vehicle Fleet Identification 

                        if (Char.ToString(FSPEC_fake[18]) == "1") // 17  I010/090 Flight Level in Binary Representation 
                        {
                            string string1 = Convert.ToString(paquete[data_position]);
                            string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                            string1 = AddZeros(string1);

                            string string2 = Convert.ToString(paquete[data_position + 1]);
                            string2 = Convert.ToString(Convert.ToInt32(string2, 16), 2);
                            string2 = AddZeros(string2);


                            data_position = data_position + 2;

                            FlightLevelInBinaryRepresentation = String.Concat(string1, string2);

                            string v1 = FlightLevelInBinaryRepresentation.Substring(0, 1);
                            if (v1 == "0") { V_fl = "Code validated."; }
                            else { V_fl = "Code not validated."; }

                            string g1 = FlightLevelInBinaryRepresentation.Substring(1, 1);
                            if (g1 == "0") { G_fl = "Default."; }
                            else { G_fl = "Garbled code."; }

                            string fl = FlightLevelInBinaryRepresentation.Substring(2, 14);
                            FlightLevel = Calculate_ComplementoA2(fl) * 0.25;

                        }// 17  I010/090 Flight Level in Binary Representation 

                        if (Char.ToString(FSPEC_fake[19]) == "1") // 18  I010/091 Measured Height 
                        {
                            string string1 = Convert.ToString(paquete[data_position]);
                            string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                            string1 = AddZeros(string1);

                            string string2 = Convert.ToString(paquete[data_position]);
                            string2 = Convert.ToString(Convert.ToInt32(string2, 16), 2);
                            string2 = AddZeros(string2);


                            data_position = data_position + 2;

                            MeasuredHeight = String.Concat(string1, string2);

                            MeasuredHeight_ft = Calculate_ComplementoA2(MeasuredHeight) * 6.26;

                        }// 18  I010/091 Measured Height 

                        if (Char.ToString(FSPEC_fake[20]) == "1") // 19  I010/270 Target Size & Orientation
                        {
                            // primero leemos el primer paquete y lo pasamos a binario

                            string string_packet = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                            string_packet = AddZeros(string_packet);

                            if ((Convert.ToString(string_packet[7])) == "0") // si ultima posicion es un 0 guardamos el octeto y pasamos al siguiente
                            {
                                TargetSizeOrientation = string_packet;
                                data_position = data_position + 1;
                            }
                            if ((Convert.ToString(string_packet[7])) == "1") // si ultimo valor =1 hacemos un bucle que vaya concatenando todos los octetos acabados en 1
                            {
                                i = 0;
                                data_position = data_position + 1;
                                while ((Convert.ToString(string_packet[string_packet.Length - 1])) == "1" && string_packet.Length < 24)
                                {
                                    string string_packet2 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                                    string_packet2 = AddZeros(string_packet2);
                                    string_packet = string.Concat(string_packet, string_packet2);
                                    data_position = data_position + 1;
                                    i = i + 1;
                                }
                            }

                            TargetSizeOrientation = string_packet;

                            Calculate_TargetSizeOrientation(TargetSizeOrientation);
                        }// 19  I010/270 Target Size & Orientation

                        if (Char.ToString(FSPEC_fake[21]) == "1") // 20  I010/550 System Status
                        {
                            string string1 = Convert.ToString(paquete[data_position]);
                            string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                            string1 = AddZeros(string1);


                            data_position = data_position + 1;

                            SystemStatus = string1;

                            Calculate_SystemStatus(SystemStatus);
                        } // 20  I010/550 System Status

                        if (Char.ToString(FSPEC_fake[22]) == "1") // 21  I010/310 Pre-programmed Message
                        {
                            string string1 = Convert.ToString(paquete[data_position]);
                            string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                            string1 = AddZeros(string1);

                            data_position = data_position + 1;

                            PreProgrammedMessage = string1;

                            string trb = PreProgrammedMessage.Substring(0, 1);
                            if (trb == "0") { TRB = "Default."; }
                            else { TRB = "In Trouble."; }

                            string msg = PreProgrammedMessage.Substring(1, 7);
                            int msg1 = Convert.ToInt32(msg, 2);
                            if (msg1 == 1) { MSG = "Towing aircraft."; }
                            if (msg1 == 2) { MSG = "“Follow me” operation."; }
                            if (msg1 == 3) { MSG = "Runway check."; }
                            if (msg1 == 4) { MSG = "Emergency operation (fire, medical…)."; }
                            if (msg1 == 5) { MSG = "Work in progress (maintenance, birds scarer, sweepers…)"; }

                        }// 21  I010/310 Pre-programmed Message

                        if (Char.ToString(FSPEC_fake[23]) == "1")  // FX - Field Extension Indicator
                        {

                        } // FX

                        if (FSPEC.Length > 24)
                        {
                            if (Char.ToString(FSPEC_fake[24]) == "1") // 22 I010/500   Standard Deviation of Position 
                            {
                                string string1 = Convert.ToString(paquete[data_position]);
                                string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                                string1 = AddZeros(string1);

                                string string2 = Convert.ToString(paquete[data_position + 1]);
                                string2 = Convert.ToString(Convert.ToInt32(string2, 16), 2);
                                string2 = AddZeros(string2);

                                string string3 = Convert.ToString(paquete[data_position + 2]);
                                string3 = Convert.ToString(Convert.ToInt32(string3, 16), 2);
                                string3 = AddZeros(string3);

                                string string4 = Convert.ToString(paquete[data_position + 3]);
                                string4 = Convert.ToString(Convert.ToInt32(string4, 16), 2);
                                string4 = AddZeros(string4);

                                StandardDeviationofPosition = String.Concat(string1, string2, string3, string4);

                                data_position = data_position + 4;

                                string sdx = StandardDeviationofPosition.Substring(0, 8);
                                string sdy = StandardDeviationofPosition.Substring(8, 8);
                                string sdxy = StandardDeviationofPosition.Substring(16, 16);

                                StdDeviation_x = Convert.ToInt32(sdx, 2) * 0.25;
                                StdDeviation_y = Convert.ToInt32(sdy, 2) * 0.25;
                                StdDeviation_xy = Convert.ToInt32(sdxy, 2) * 0.25;
                            } // 22 I010/500   Standard Deviation of Position 

                            if (Char.ToString(FSPEC_fake[25]) == "1") // 23  I010/280  Presence 
                            {
                                data_position = data_position + ComputePresence(paquete, data_position);
                            }// 23  I010/280  Presence 

                            if (Char.ToString(FSPEC_fake[26]) == "1") // 24  I010/131  Amplitude of Primary Plot 
                            {
                                string string1 = Convert.ToString(paquete[data_position]);
                                string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                                string1 = AddZeros(string1);

                                AmplitudeofPrimaryPlot = string1;

                                data_position = data_position + 1;

                                AmplitudeofPrimaryPlot_value = Convert.ToInt32(AmplitudeofPrimaryPlot, 2);
                            }// 24  I010/131  Amplitude of Primary Plot 

                            if (Char.ToString(FSPEC_fake[27]) == "1") // 25  I010/210  Calculated Acceleration 
                            {
                                string string1 = Convert.ToString(paquete[data_position]);
                                string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                                string1 = AddZeros(string1);

                                string string2 = Convert.ToString(paquete[data_position + 1]);
                                string2 = Convert.ToString(Convert.ToInt32(string2, 16), 2);
                                string2 = AddZeros(string2);

                                CalculatedAcceleration = String.Concat(string1, string2);

                                data_position = data_position + 2;

                                CalculatedAcceleration_X = Calculate_ComplementoA2(CalculatedAcceleration.Substring(0, 8)) * 0.25;
                                CalculatedAcceleration_Y = Calculate_ComplementoA2(CalculatedAcceleration.Substring(8, 8)) * 0.25;
                            } // 25  I010/210  Calculated Acceleration 
                        }
                    }
                }
            }

            // Una vez calculados todos los parametros calculamos las coordenadas en los diferentes sistemas de Referencia

            // SMR
            if(this.SAC == 0 && this.SIC == 7)
            {
                // Calculamos Coordenadas Geodesic
                CoordinatesPolar coordRadarSpherical = new CoordinatesPolar(Rho, Theta * GeoUtils.DEGS2RADS, 0);
                CoordinatesXYZ coordRadarCartesian = GeoUtils1.change_radar_spherical2radar_cartesian(coordRadarSpherical);
                CoordinatesXYZ coordGeocentric = GeoUtils1.change_radar_cartesian2geocentric(new CoordinatesWGS84(LatSMR * GeoUtils.DEGS2RADS, LonSMR * GeoUtils.DEGS2RADS, 0), coordRadarCartesian);
                coordGeodesic = GeoUtils1.change_geocentric2geodesic(coordGeocentric);

                // Calculamos Coordenadas System Cartesian
                coordSystemCartesian = GeoUtils1.change_geocentric2system_cartesian(coordGeocentric);

                // Calculamos Coordenadas Stereographic
                coordStereographic = GeoUtils1.change_system_cartesian2stereographic(coordSystemCartesian);
            }

            // MLAT
            if (this.SAC == 0 && this.SIC == 107)
            { 
                // Calculamos Coordenadas Geodesic
                CoordinatesXYZ coordRadarCartesian = new CoordinatesXYZ(X_cartesian, Y_cartesian, this.FlightLevel * 100 * GeoUtils.FEET2METERS);
                CoordinatesXYZ coordGeocentric = GeoUtils1.change_radar_cartesian2geocentric(new CoordinatesWGS84(LatMLAT * GeoUtils.DEGS2RADS, LonMLAT * GeoUtils.DEGS2RADS), coordRadarCartesian);
                coordGeodesic = GeoUtils1.change_geocentric2geodesic(coordGeocentric);

                // Calculamos Coordenadas System Cartesian
                coordSystemCartesian = GeoUtils1.change_geocentric2system_cartesian(coordGeocentric);

                // Calculamos Coordenadas Stereographic
                coordStereographic = GeoUtils1.change_system_cartesian2stereographic(coordSystemCartesian);
            }
        }
    }
}
