using System;
using System.Collections.Generic;
using System.Text;

namespace DataModelLibrary
{
    public class CAT21v23
    {
        int i = 0;
        public string[] paquete;
        public string[] arrayFSPEC;
        public string FSPEC;
        public string FSPEC_fake;
        public int data_position = 0;

        public string DataSourseIdentification = "";
        public int SAC;
        public int SIC;

        public string TargetReportDescriptor = "";
        public string DCR = "";
        public string GBS = "";
        public string SIM = "";
        public string TST = "";
        public string RAB = "";
        public string SAA = "";
        public string SPI = "";
        public string ATP = "";
        public string ARC = "";

        public string TimeofDay = "";
        public double TimeofDay_seconds;

        public string PositioninWGS_coordinates;
        public double latWGS84 = 0;
        public double lonWGS84 = 0;

        public string TargetAddress_bin = "";
        public string TargetAdress_real = "";

        public string GeometricAltitude = "";
        public double GeometricAltitude_ft;

        public string FigureofMerit = "";
        public string AC = "";
        public string MN = "";
        public string DC = "";
        public double PA;

        public string LinkTechnologyIndicator = "";
        public string DTI = "";
        public string MDS = "";
        public string UAT = "";
        public string VDL = "";
        public string OTR = "";

        public string RollAngle = "";
        public double RollAngle_degrees;

        public string FlightLevel = "";
        public double FlightLevel_FL;

        public string AirSpeed = "";
        public string IM = "";
        public double AirSpeed_velocity;
        public double AirSpeed_Mach;

        public string TrueAirSpeed = "";
        public double TrueAirSpeed_number;
        public string RE_TAS = "";

        public string MagneticHeading = "";
        public double MagneticHeading_degrees;

        public string BarometricVerticalRate = "";
        public string RE_BVR = "";
        public double BarometricVerticalRate_fmin;

        public string GeometricVerticalRate = "";
        public string RE_GVR = "";
        public double GeometricVerticalRate_fmin;

        public string GroundVector = "";
        public double GroundSpeed;
        public double TrackAngle;

        public string RateofTurn = "";
        public string TI = "";
        public double RateofTurn_deg;

        public string TargetIdentification = "";
        public string TargetIdentification_decoded = "";

        public string TimeofDayAccuracy = "";
        public double TimeofDayAccuracy_sec;

        public string TargetStatus = "";
        public string TargetStatus_decoded = "";

        public string EmitterCategory = "";
        public string ECAT = "";

        public string MetInfo = "";
        public double WindSpeed = 10e10;
        public double WindDirection = 10e10;
        public double Temperature = 10e10;
        public double Turbulence = 10e10;

        public string IntermediateStateSelectedAltitude = "";
        public string SAS = "";
        public string Source = "";
        public double Altitude;

        public string FinalStateSelectedAltitude = "";
        public string MV = "";
        public string AH = "";
        public string AM = "";
        public double FSS_Altitude;

        public string TrajectroyIntent = "";
        public int TIS;
        public int TID;
        public int NAV;
        public int NVB;
        public int REP;
        public List<int> TCA;
        public List<int> NC;
        public List<int> TCPNumber;
        public List<double> TI_Altitude;
        public List<double> TI_LatWGS84;
        public List<double> TI_LonWGS84;
        public List<String> PointType;
        public List<String> TD;
        public List<String> TRA;
        public List<String> TOA;
        public List<int> TOV;
        public List<double> TTR;




        public CAT21v23(string[] packet)
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

        public void Calculate_TargetReportDescriptor(string paquete)
        {
            string dcr = paquete[0].ToString();
            if (dcr == "0") { DCR = "No differential correction (ADS-B)"; }
            else { DCR = "Differential correction (ADS-B)"; }

            string gbs = paquete[1].ToString();
            if (gbs == "0") { GBS = "Ground Bit not set"; }
            else { GBS = "Ground Bit set"; }

            string sim = paquete[2].ToString();
            if (sim == "0") { SIM = "Actual target report "; }
            else { SIM = "Simulated target report"; }

            string tst = paquete[3].ToString();
            if (tst == "0") { TST = "Default"; }
            else { TST = "Test Target"; }

            string rab = paquete[4].ToString();
            if (rab == "0") { RAB = "Report from target transponder"; }
            else { RAB = "Report from field monitor (fixed transponder)"; }

            string saa = paquete[5].ToString();
            if (saa == "0") { SAA = "Equipement not capable to provide Selected Altitude "; }
            else { SAA = "Equipement capable to provide Selected Altitude "; }

            string spi = paquete[6].ToString();
            if(spi == "0") { SPI = "Absence of SPI "; }
            else { SPI = "Special Position Identification "; }

            string atp = paquete.Substring(8, 3);
            int atp1 = Convert.ToInt32(atp, 2);
            if (atp1 == 0) { ATP = "Non unique address"; }
            if (atp1 == 1) { ATP = "24-Bit ICAO address "; }
            if (atp1 == 2) { ATP = "Surface vehicle address "; }
            if (atp1 == 3) { ATP = "Anonymous address"; }
            if (atp1 == 4) { ATP = "Reserved for future use "; }

            string arc = paquete.Substring(11, 2);
            double arc1 = Convert.ToInt32(arc, 2);
            if (arc1 == 0) { ARC = "Unknown"; }
            if (arc1 == 1) { ARC = "25 ft"; }
            if (arc1 == 2) { ARC = "100 ft"; }
        }

        public void CalculatePositionWGS84_coordinates(string paquete)
        {
            string str1 = paquete.Substring(0, 24);
            string str2 = paquete.Substring(24, 24);

            double a1 = Calculate_ComplementoA2(str1);
            double b1 = Calculate_ComplementoA2(str2);

            latWGS84 = a1 * (180 / Math.Pow(2, 23));
            lonWGS84 = b1 * (180 / Math.Pow(2, 23));
        }

        public void Calculate_FigureofMerit(string paquete)
        {
            string ac = paquete.Substring(0, 2);
            if (ac == "00") { AC = "unknown "; }
            if (ac == "01") { AC = "ACAS not operational"; }
            if (ac == "10") { AC = "ACAS operational"; }
            if (ac == "11") { AC = "invalid "; }

            string mn = paquete.Substring(2, 2);
            if (mn == "00") { MN = "unknown "; }
            if (mn == "01") { MN = "Multiple navigational aids not operating"; }
            if (mn == "10") { MN = "Multiple navigational aids operating"; }
            if (mn == "11") { MN = "invalid "; }

            string dc = paquete.Substring(4, 2);
            if (dc == "00") { DC = "unknown "; }
            if (dc == "01") { DC = "Differential correction "; }
            if (dc == "10") { DC = "No differential correction"; }
            if (dc == "11") { DC = "invalid "; }

            string pa = paquete.Substring(12,4);
            PA = Convert.ToInt32(pa, 2);
        }

        public void Calculate_LinkTechnology(string paquete)
        {
            string dti = paquete[3].ToString();
            if (dti == "0") { DTI = "Unknown"; }
            else { DTI = "Aircraft equiped with CDTI "; }

            string mds = paquete[4].ToString();
            if (mds == "0") { MDS = "Not used."; }
            else { MDS = "Used"; }

            string uat = paquete[5].ToString();
            if (uat == "0") { UAT = "Not used"; }
            else { UAT = "Used"; }

            string vdl = paquete[6].ToString();
            if (vdl == "0") { VDL = "Not used"; }
            else { VDL = "Used"; }

            string otr = paquete[7].ToString();
            if (otr == "0") { OTR = "Not used"; }
            else { OTR = "Used"; }
        }

        public void Calculate_AirSpeed(string paquete)
        {
            string velocity = paquete.Substring(1, 15);

            if (paquete.Substring(0,1) == "0")
            {
                IM = "IAS";
                double vel1 = Convert.ToInt32(velocity);
                AirSpeed_velocity = vel1 * (1 / Math.Pow(2, 14));
            }
            else
            {
                IM = "Mach";

                AirSpeed_Mach = Convert.ToInt32(velocity);
                AirSpeed_Mach = AirSpeed_Mach * 0.001;
            }
        }

        public void Calculate_TrueAirSpeed(string paquete)
        {
            string ra1 = paquete.Substring(0,1);

            if (ra1 == "0")
            {
                RE_TAS = "Value in defined range.";
            }

            else
            {
                RE_TAS = "Value exceeds defined range.";
            }

            string tas = paquete.Substring(1, paquete.Length - 1);
            TrueAirSpeed_number = Convert.ToInt32(tas, 2);

        }

        public void Calculate_BarometricVerticalRate(string paquete)
        {
            string str1 = Convert.ToString(paquete[0]);
            if (str1 == "0")
            {
                RE_BVR = "Value in defined range.";
            }

            else
            {
                RE_BVR = "Value exceeds defined range.";
            }

            string str2 = paquete.Substring(1, 15);

            BarometricVerticalRate_fmin = Calculate_ComplementoA2(str2) * 6.25;
        }

        public void Calculate_GroundVector(string paquete)
        {
            string str1 = paquete.Substring(0, 16);
            string str2 = paquete.Substring(16, 16);

            GroundSpeed = Calculate_ComplementoA2(str1) * Math.Pow(2, -14);
            TrackAngle = Convert.ToInt32(str2, 2) * (360 / Math.Pow(2, 16));
        }

        public void Calculate_RateofTurn(string paquete)
        {
            string ti = paquete.Substring(0, 2);
            if (ti == "00") { TI = "Not available"; }
            if (ti == "01") { TI = "Left "; }
            if (ti == "10") { TI = "Right"; }
            if (ti == "11") { TI = "Straight "; }

            if(paquete.Length>8)
            {
                string RateofTurn_deg1 = paquete.Substring(8, 7);
                RateofTurn_deg = Calculate_ComplementoA2(RateofTurn_deg1) / 4;
            }
        }

        public void Compute_TargetIdentification(string paquete)
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

        public void Calculate_EmitterCategory(string paquete)
        {
            int packet1 = Convert.ToInt32(paquete, 2);
            if (packet1 == 1) { ECAT = "light aircraft <= 7000 kg  "; }
            if (packet1 == 2) { ECAT = "reserved"; }
            if (packet1 == 3) { ECAT = "7000 kg < medium aircraft  < 136000 kg "; }
            if (packet1 == 4) { ECAT = "reserved "; }
            if (packet1 == 5) { ECAT = "136000 kg <= heavy aircraft"; }
            if (packet1 == 6) { ECAT = "highly manoeuvrable (5g acceleration capability) and high speed (>400 knots cruise) "; }
            if (packet1 == 7 || packet1==8 || packet1==9) { ECAT = "reserved "; }
            if (packet1 == 10) { ECAT = "rotocraft"; }
            if (packet1 == 11) { ECAT = "glider / sailplane "; }
            if (packet1 == 12) { ECAT = "lighter-than-air"; }
            if (packet1 == 13) { ECAT = "unmanned aerial vehicle "; }
            if (packet1 == 14) { ECAT = "space / transatmospheric vehicle"; }
            if (packet1 == 15) { ECAT = "ultralight / handglider / paraglider"; }
            if (packet1 == 16) { ECAT = "parachutist / skydiver"; }
            if (packet1 == 17 || packet1 == 18 || packet1 == 19) { ECAT = "reserved "; }
            if (packet1 == 20) 
            { 
                ECAT = "surface emergency vehicle "; 
            }
            if (packet1 == 21) 
            { 
                ECAT = "surface service vehicle"; 
            }
            if (packet1 == 22) 
            {
                ECAT = "fixed ground or tethered obstruction"; 
            }
            if (packet1 == 23 || packet1 == 24) { ECAT = "reserved"; }
        }

        public void Calculate_IntermediateStateSelectedAltitude(string paquete)
        {
            string sas = paquete.Substring(0,1);
            if (sas == "0") { SAS = "No source information provided"; }
            else { SAS = "Source Information provided"; }

            string source = paquete.Substring(1, 2);
            if (source == "00") { Source = "Unknown"; }
            if (source == "01") { Source = "Aircraft Altitude "; }
            if (source == "10") { Source = "FCU/MSP Selected Altitude "; }
            if (source == "11") { Source = "FMS Selected Altitude"; }

            string alt = paquete.Substring(3,13);
            Altitude = Calculate_ComplementoA2(alt) * 25;
        }

        public void Calculate_FinalStateSelectedAltitude(string paquete)
        {
            string mv = paquete.Substring(0,1);
            if (mv == "0") { MV = "Not active"; }
            if (mv == "1"){ MV = "Active"; }

            string ah = paquete.Substring(0,1);
            if (ah == "0") { MV = "Not active"; }
            if (ah == "1") { MV = "Active"; }

            string alt = paquete.Substring(3,13);
            FSS_Altitude = Calculate_ComplementoA2(alt) * 25;
        }

        public void Calculate_FSPEC(string[] paquete)
        {
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

            while (FSPEC_fake.Length < (7 * 8))
            {
                FSPEC_fake = String.Concat(FSPEC_fake, "0");
            }

            data_position = 1 + 2 + ((FSPEC.Length) / 8);

            //-------------------------------------------------------------------------------------------------------------
            // Aqui porocesamos los parametros que hemos encontrado en el FSPEC
            //-------------------------------------------------------------------------------------------------------------


            if (Char.ToString(FSPEC_fake[0]) == "1") // 1 I021/010 Data Source Identification
            {

                string string1 = Convert.ToString(paquete[data_position]);
                string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                string1 = AddZeros(string1);

                string string2 = Convert.ToString(paquete[data_position + 1]);
                string2 = Convert.ToString(Convert.ToInt32(string2, 16), 2);
                string2 = AddZeros(string2);

                data_position = data_position + 2;

                DataSourseIdentification = String.Concat(string1, string2);

                Calculate_DataSourceIdentification(DataSourseIdentification);

            }  // 1 I021/010 Data Source Identification

            if (Char.ToString(FSPEC_fake[1]) == "1") // 2 I021/040 Target Report Descriptor
            {
                string string1 = Convert.ToString(paquete[data_position]);
                string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                string1 = AddZeros(string1);

                string string2 = Convert.ToString(paquete[data_position + 1]);
                string2 = Convert.ToString(Convert.ToInt32(string2, 16), 2);
                string2 = AddZeros(string2);

                TargetReportDescriptor = String.Concat(string1, string2);

                data_position = data_position + 2;

                Calculate_TargetReportDescriptor(TargetReportDescriptor);

            }// 2 I021/040 Target Report Descriptor

            if (Char.ToString(FSPEC_fake[2]) == "1") // 3 I010 / 140 Time of Day
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

                double a = Convert.ToInt32(TimeofDay, 2);
                TimeofDay_seconds = a / 128;

            }// 3 I010 / 140 Time of Day

            if (Char.ToString(FSPEC_fake[3]) == "1") // 4 I021/131 Position in WGS-84 co-ordinates
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

                PositioninWGS_coordinates = String.Concat(string1, string2, string3, string4, string5, string6);

                data_position = data_position + 6;

                CalculatePositionWGS84_coordinates(PositioninWGS_coordinates);

            } // 4 I021/131 Position in WGS-84 co-ordinates

            if (Char.ToString(FSPEC_fake[4]) == "1") // 5 I021/080 Target Address
            {

                string hexa = "";

                i = 0;
                while (i < 3)
                {
                    string string1 = Convert.ToString(paquete[data_position + i]);
                    char a = string1[0];
                    char b = string1[1];
                    hexa = string.Concat(hexa, a);
                    hexa = string.Concat(hexa, b);
                    string string2 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                    string2 = AddZeros(string2);
                    TargetAddress_bin = String.Concat(TargetAddress_bin, string2);
                    i = i + 1;
                }

                data_position = data_position + 3;

                TargetAdress_real = hexa;

            }// 5 I021/080 Target Address

            if (Char.ToString(FSPEC_fake[5]) == "1") // 6 I021/140 Geometric Altitude
            {
                string string1 = Convert.ToString(paquete[data_position]);
                string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                string1 = AddZeros(string1);

                string string2 = Convert.ToString(paquete[data_position + 1]);
                string2 = Convert.ToString(Convert.ToInt32(string2, 16), 2);
                string2 = AddZeros(string2);

                string string3 = Convert.ToString(paquete[data_position + 2]);
                string3 = Convert.ToString(Convert.ToInt32(string3, 16), 2);
                string3 = AddZeros(string2);

                data_position = data_position + 3;

                GeometricAltitude = String.Concat(string1, string2, string3);
                GeometricAltitude_ft = Calculate_ComplementoA2(GeometricAltitude) * 6.25;

            }// 6 I021/140 Geometric Altitude

            if (Char.ToString(FSPEC_fake[6]) == "1") // 7 I021/090 Figure of Merit
            {
                string string1 = Convert.ToString(paquete[data_position]);
                string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                string1 = AddZeros(string1);

                string string2 = Convert.ToString(paquete[data_position + 1]);
                string2 = Convert.ToString(Convert.ToInt32(string2, 16), 2);
                string2 = AddZeros(string2);

                data_position = data_position + 2;

                FigureofMerit = String.Concat(string1, string2);
                Calculate_FigureofMerit(FigureofMerit);

            }// 7 I021/090 Figure of Merit

            if (Char.ToString(FSPEC_fake[7]) == "1") // FX - Field extension indicator 
            {
            }// FX

            if (Char.ToString(FSPEC_fake[8]) == "1") // 8 I021/210 Link technology
            {
                string string1 = Convert.ToString(paquete[data_position]);
                string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                string1 = AddZeros(string1);

                data_position = data_position + 1;

                LinkTechnologyIndicator = String.Concat(string1);
                Calculate_LinkTechnology(LinkTechnologyIndicator);

            }// 8 I021/210 Link technology

            if (Char.ToString(FSPEC_fake[9]) == "1") // 9 I021/230 Roll Angle
            {
                i = 0;
                while (i < 2)
                {
                    string string1 = Convert.ToString(paquete[data_position + i]);
                    string string2 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                    string2 = AddZeros(string2);
                    RollAngle = String.Concat(RollAngle, string2);
                    i = i + 1;
                }

                data_position = data_position + 2;

                RollAngle_degrees = Calculate_ComplementoA2(RollAngle) * 0.01;
            }// 9 I021/230 Roll Angle

            if (Char.ToString(FSPEC_fake[10]) == "1") // 10 I021/145 Flight Level
            {
                i = 0;
                while (i < 2)
                {
                    string string1 = Convert.ToString(paquete[data_position + i]);
                    string string2 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                    string2 = AddZeros(string2);
                    FlightLevel = String.Concat(FlightLevel, string2);
                    i = i + 1;
                }

                data_position = data_position + 2;

                FlightLevel_FL = (Calculate_ComplementoA2(FlightLevel)) / 4;
            }// 10 I021/145 Flight Level

            if (Char.ToString(FSPEC_fake[11]) == "1") // 11 I021/150 Air Speed 
            {
                i = 0;
                while (i < 2)
                {
                    string string1 = Convert.ToString(paquete[data_position + i]);
                    string string2 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                    string2 = AddZeros(string2);
                    AirSpeed = String.Concat(AirSpeed, string2);
                    i = i + 1;
                }

                data_position = data_position + 2;

                Calculate_AirSpeed(AirSpeed);

            } // 11 I021/150 Air Speed

            if (Char.ToString(FSPEC_fake[12]) == "1") // 12 I021/151 True Air Speed
            {
                i = 0;
                while (i < 2 && data_position < paquete.Length)
                {
                    string string1 = Convert.ToString(paquete[data_position + i]);
                    string string2 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                    string2 = AddZeros(string2);
                    TrueAirSpeed = String.Concat(TrueAirSpeed, string2);
                    i = i + 1;
                    data_position = data_position + 1;
                }

                Calculate_TrueAirSpeed(TrueAirSpeed);

            } // 12 I021/151 True Air Speed

            if (Char.ToString(FSPEC_fake[13]) == "1") // 13 I021/152 Magnetic Heading
            {
                i = 0;
                while (i < 2)
                {
                    string string1 = Convert.ToString(paquete[data_position + i]);
                    string string2 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                    string2 = AddZeros(string2);
                    MagneticHeading = String.Concat(MagneticHeading, string2);
                    i = i + 1;
                }

                data_position = data_position + 2;

                MagneticHeading_degrees = (Convert.ToInt32(MagneticHeading, 2)) * (360 / Math.Pow(2, 16));
            }// 13 I021/152 Magnetic Heading

            if (Char.ToString(FSPEC_fake[14]) == "1") // 14 I021/155 Barometric Vertical Rate
            {
                i = 0;
                while (i < 2)
                {
                    string string1 = Convert.ToString(paquete[data_position + i]);
                    string string2 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                    string2 = AddZeros(string2);
                    BarometricVerticalRate = String.Concat(BarometricVerticalRate, string2);
                    i = i + 1;
                }

                data_position = data_position + 2;

                Calculate_BarometricVerticalRate(BarometricVerticalRate);

            }// 14 I021/155 Barometric Vertical Rate

            if (Char.ToString(FSPEC_fake[15]) == "1") // FX - Field extension indicator 
            {
            }// FX

            if (Char.ToString(FSPEC_fake[16]) == "1") // 15 I021/157 Geometric Vertical Rate
            {
                i = 0;
                while (i < 2)
                {
                    string string1 = Convert.ToString(paquete[data_position + i]);
                    string string2 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                    string2 = AddZeros(string2);
                    GeometricVerticalRate = String.Concat(GeometricVerticalRate, string2);
                    i = i + 1;
                }

                data_position = data_position + 2;

                GeometricVerticalRate_fmin = Calculate_ComplementoA2(GeometricVerticalRate) * 6.5;

            }// 15 I021/157 Geometric Vertical Rate

            if (Char.ToString(FSPEC_fake[17]) == "1") // 16 I021/160 Ground Vector
            {
                i = 0;
                while (i < 4)
                {
                    string string1 = Convert.ToString(paquete[data_position + i]);
                    string string2 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                    string2 = AddZeros(string2);
                    GroundVector = String.Concat(GroundVector, string2);
                    i = i + 1;
                }

                data_position = data_position + 4;

                Calculate_GroundVector(GroundVector);

            }// 16 I021/160 Ground Vector

            if (Char.ToString(FSPEC_fake[18]) == "1") // 17 I021/165 Rate of Turn
            {
                // primero leemos el primer paquete y lo pasamos a binario

                string string_packet = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                string_packet = AddZeros(string_packet);

                if ((Convert.ToString(string_packet[7])) == "0") // si ultima posicion es un 0 guardamos el octeto y pasamos al siguiente
                {
                    RateofTurn = string_packet;
                    data_position = data_position + 1;
                }
                if ((Convert.ToString(string_packet[7])) == "1") // si ultimo valor =1 hacemos un bucle que vaya concatenando todos los octetos acabados en 1
                {
                    i = 0;
                    data_position = data_position + 1;
                    while ((Convert.ToString(string_packet[string_packet.Length - 1])) == "1" && string_packet.Length < 16)
                    {
                        string string_packet2 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                        string_packet2 = AddZeros(string_packet2);
                        string_packet = string.Concat(string_packet, string_packet2);
                        data_position = data_position + 1;
                        i = i + 1;
                    }
                }

                RateofTurn = string_packet;

                Calculate_RateofTurn(RateofTurn);

            }// 17 I021/165 Rate of Turn

            if (Char.ToString(FSPEC_fake[19]) == "1") // 18 I021/170 Target Identification
            {
                i = 0;
                while (i < 6)
                {
                    string string1 = Convert.ToString(paquete[data_position + i]);
                    string string2 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                    string2 = AddZeros(string2);
                    TargetIdentification = String.Concat(TargetIdentification, string2);
                    i = i + 1;
                }

                data_position = data_position + 6;

                Compute_TargetIdentification(TargetIdentification);
                string a = TargetIdentification_decoded;
            } // 18 I021/170 Target Identification

            if (Char.ToString(FSPEC_fake[20]) == "1") // 19 I021/095 Velocity Accuracy
            {

            }// 19 I021/095 Velocity Accuracy

            if (Char.ToString(FSPEC_fake[21]) == "1") // 20 I021/032 Time of day accuracy
            {
                string string1 = Convert.ToString(paquete[data_position]);
                string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                string1 = AddZeros(string1);

                data_position = data_position + 1;

                TimeofDayAccuracy = String.Concat(string1);
                TimeofDayAccuracy_sec = Convert.ToInt32(TimeofDayAccuracy, 2) / 256;

            }// 20 I021/032 Time of day accuracy

            if (Char.ToString(FSPEC_fake[22]) == "1") // 21 I021/200 Target Status
            {
                string string1 = Convert.ToString(paquete[data_position]);
                string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                string1 = AddZeros(string1);

                data_position = data_position + 1;

                TargetStatus = String.Concat(string1);
                int targetstatus = Convert.ToInt32(TargetStatus, 2);

                if (targetstatus == 0) { TargetStatus_decoded = "No emergency / not reported"; }
                if (targetstatus == 1) { TargetStatus_decoded = "General emergency "; }
                if (targetstatus == 2) { TargetStatus_decoded = "Lifeguard / medical "; }
                if (targetstatus == 3) { TargetStatus_decoded = "Minimum fuel"; }
                if (targetstatus == 4) { TargetStatus_decoded = "No communications"; }
                if (targetstatus == 5) { TargetStatus_decoded = "Unlawful interference"; }

            }// 21 I021/200 Target Status

            if (Char.ToString(FSPEC_fake[23]) == "1") // FX - Field extension indicator 
            {
            }// FX

            if (Char.ToString(FSPEC_fake[24]) == "1") // 22 I021/020 Emitter Category
            {
                string string1 = Convert.ToString(paquete[data_position]);
                string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                string1 = AddZeros(string1);

                data_position = data_position + 1;

                EmitterCategory = String.Concat(string1);
                Calculate_EmitterCategory(EmitterCategory);

            }// 22 I021/020 Emitter Category

            if (Char.ToString(FSPEC_fake[25]) == "1") // 23 I021/220 Met report
            {
                // primero leemos el primer paquete y lo pasamos a binario

                string string_packet = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                string_packet = AddZeros(string_packet);

                string indexpacket = string_packet;
                data_position = data_position + 1;
                MetInfo = indexpacket;

                if(indexpacket.Substring(7,1)=="1")
                {

                    if (indexpacket.Substring(0,1) == "1")
                    {
                        string string1 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                        string string2 = Convert.ToString(Convert.ToInt32(paquete[data_position + 1], 16), 2);
                        string ws = String.Concat(string1, string2);
                        MetInfo = String.Concat(MetInfo, ws);

                        WindSpeed = Convert.ToInt32(ws, 2);

                        data_position = data_position + 2;
                    }

                    if (indexpacket.Substring(1,1) == "1")
                    {
                        string string1 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                        string string2 = Convert.ToString(Convert.ToInt32(paquete[data_position + 1], 16), 2);
                        string wd = String.Concat(string1, string2);
                        MetInfo = String.Concat(MetInfo, wd);

                        WindDirection = Convert.ToInt32(wd, 2);

                        data_position = data_position + 2;
                    }

                    if (indexpacket.Substring(2,1) == "1")
                    {
                        string string1 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                        string string2 = Convert.ToString(Convert.ToInt32(paquete[data_position + 1], 16), 2);
                        string temp = String.Concat(string1, string2);
                        MetInfo = String.Concat(MetInfo, temp);

                        Temperature = Calculate_ComplementoA2(temp)/4;

                        data_position = data_position + 2;
                    }

                    if (indexpacket.Substring(3,1) == "1")
                    {
                        string string1 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                        string turb = string1;
                        MetInfo = String.Concat(MetInfo, turb);

                        Turbulence = Convert.ToInt32(turb, 2);

                        data_position = data_position + 2;
                    }
                }
            }// 23 I021/220 Met report

            if (Char.ToString(FSPEC_fake[26]) == "1") // 24 I021/146 Intermediate State Selected Altitude
            {
                string string1 = Convert.ToString(paquete[data_position]);
                string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                string1 = AddZeros(string1);

                string string2 = Convert.ToString(paquete[data_position + 1]);
                string1 = Convert.ToString(Convert.ToInt32(string2, 16), 2);
                string1 = AddZeros(string2);

                data_position = data_position + 2;

                IntermediateStateSelectedAltitude = String.Concat(string1, string2);
                Calculate_IntermediateStateSelectedAltitude(IntermediateStateSelectedAltitude);

            }// 24 I021/146 Intermediate State Selected Altitude

            if (Char.ToString(FSPEC_fake[27]) == "1") // 25 I021/148 Final State Selected Altitude
            {
                string string1 = Convert.ToString(paquete[data_position]);
                string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                string1 = AddZeros(string1);

                string string2 = Convert.ToString(paquete[data_position + 1]);
                string1 = Convert.ToString(Convert.ToInt32(string2, 16), 2);
                string1 = AddZeros(string2);

                data_position = data_position + 2;

                FinalStateSelectedAltitude = String.Concat(string1, string2);
                Calculate_FinalStateSelectedAltitude(FinalStateSelectedAltitude);

            }// 25 I021/148 Final State Selected Altitude
        }
    }
}
