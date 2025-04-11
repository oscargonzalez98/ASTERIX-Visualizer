using System;
using System.Collections.Generic;
using System.Text;

using System.Text.RegularExpressions;
using DotNetMatrix;
using MultiCAT6.Utils;

namespace DataModelLibrary
{
    public class CAT21 : IMessage
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

        public string DataSourseIdentification = "";
        public int SAC;
        public int SIC;

        public string TargetReportDescriptor = "";
        public string ATP = "";
        public string ARC = "";
        public string RC = "";
        public string RAB = "";
        public string DCR = "";
        public string GBS = "";
        public string SIM = "";
        public string TST = "";
        public string SAA = "";
        public string CL = "";
        public string IPC = "";
        public string NOGO = "";
        public string CPR = "";
        public string LDPJ = "";
        public string RCF = "";

        public string TrackNumber = "";
        public int TrackNumber_number;

        public string ServiceIdentification = "";
        public int ServiceIdentification_number;

        public string TimeofApplicability_Position = "";
        public double TimeofApplicability_Position_seconds;

        public string PositioninWGS_coordinates = "";
        public double latWGS84 = 0;
        public double lonWGS84 = 0;

        public string PositioninWGS_HRcoordinates = "";
        public double latWGS84_HR = 0;
        public double lonWGS84_HR = 0;

        public CoordinatesWGS84 coordGeodesic;
        public CoordinatesUVH coordStereographic;
        public CoordinatesXYZ coordSystemCartesian;

        public string TimeofApplicability_Velocity = "";
        public double TimeofApplicability_Velocity_seconds;

        public string AirSpeed = "";
        public string IM = "";
        public double AirSpeed_velocity;
        public double AirSpeed_Mach;

        public string TrueAirSpeed = "";
        public double TrueAirSpeed_number;
        public string RE_TAS = "";

        public string TargetAddress_bin = "";
        public string TargetAdress_real = "";

        public string TimeofMessageReception_Position = "";
        public double TimeofMessageReception_Position_seconds;

        public string TimeofMessageReception_HRPosition = "";
        public double TimeofMessageReception_HRPosition_seconds;
        public string FSI1 = "";

        public string TimeofMessageReception_Velocity = "";
        public double TimeofMessageReception_Velocity_seconds;
        public string FSI2 = "";

        public string TimeofMessageReception_HRVelocity = "";
        public double TimeofMessageReception_HRVelocity_seconds;

        public string GeometricHeight = "";
        public double GeometricHeight_feet;

        public string QualityIndicators = "";
        public int NACv;
        public int NUCp;
        public int NIC_Baro;
        public string SIL = "";
        public int NACp;
        public int SDA;
        public int GVA;
        public int PIC;

        public string MOPSVersion = "";
        public string VNS = "";
        public string VN = "";
        public string LTT = "";

        public string Mode3ACode_bin = "";
        public string Mode3ACode_oct = "";

        public string RollAngle = "";
        public double RollAngle_degrees;

        public string FlightLevel = "";
        public double FlightLevel_FL;

        public string MagneticHeading = "";
        public double MagneticHeading_degrees;

        public string TargetStatus = "";
        public string ICF = "";
        public string LNAV = "";
        public string PS = "";
        public string SS = "";

        public string BarometricVerticalRate = "";
        public string RE_BVR = "";
        public double BarometricVerticalRate_fmin;

        public string GeometricVerticalRate = "";
        public string RE_GVR = "";
        public double GeometricVerticalRate_fmin;

        public string AirborneGoundVector = "";
        public string RE_AGV = "";
        public double GroundSpeed;
        public double TrackAngle;

        public string TrackAngleRate = "";
        public double TrackAngleRate_degs;

        public string TimeofASTERIXReportTransmission = "";
        public double TimeofASTERIXReportTransmission_seconds;

        public string TargetIdentification = "";
        public string TargetIdentification_decoded = "";

        public string EmitterCategory = "";
        public string ECAT = "";

        public string MetInformation = "";
        public double WindSpeed;
        public double WindDirection;
        public double Temperature;
        public double Turbulence;

        public string SelectedAltitude = "";
        public string SAS = "";
        public string Source = "";
        public double SelectedAltitude_ft;

        public string FinalStateSelectedAltitude = "";
        public string MV = "";
        public string AH = "";
        public string AM = "";
        public double FinalStateSelectedAltitude_ft;

        public string TrajectoryIntent = "";
        public string NAV = "";
        public string NVB = "";
        public int REP;
        public string TCA = "";
        public string NC = "";
        public int TCP;
        public double TI_ALtitude;
        public double TI_Latitude;
        public double TI_Longitude;
        public string PointType = "";
        public string TD = "";
        public string TRA = "";
        public string TOA = "";
        public int TOV;
        public double TTR;

        public string ServiceManagement = "";
        public double RP;

        public string AircraftOperationalStatus = "";
        public string RA = "";
        public string TC = "";
        public string TS = "";
        public string ARV = "";
        public string CDTI_A = "";
        public string TCAS = "";
        public string SA = "";

        public string SurfaceCapabilitiesandCharacteristicas = "";
        public string POA = "";
        public string CDTI_S = "";
        public string B2low = "";
        public string RAS = "";
        public string IDENT = "";
        public string Length = "";
        public string Width = "";

        public string ModeSMBData = "";
        public string[] REP_MS;
        public string[] MBDATA;
        public string[] BDS1;
        public string[] BDS2;

        public string ACASResolutionAdvisoryReport = "";
        public int TYP;
        public int STYP;
        public int ARA_ACAS;
        public int RAC;
        public int RAT;
        public int MTE;
        public int TTI;
        public int TID;

        public string MessageAmplitude = "";
        public double MessageAmplitude_dBm;

        public string ReceiverID = "";
        public int ReceiverID_number;

        public string DataAges = "";
        public double AOS = -1;
        public double TRD = -1;
        public double M3A = -1;
        public double QI = -1;
        public double TI = -1;
        public double MAM = -1;
        public double GH = -1;
        public double FL = -1;
        public double ISA = -1;
        public double FSA = -1;
        public double AS = -1;
        public double TAS = -1;
        public double MH = -1;
        public double BVR = -1;
        public double GVR = -1;
        public double GV = -1;
        public double TAR = -1;
        public double TI_DA = -1;
        public double TS_DA = -1;
        public double MET = -1;
        public double ROA = -1;
        public double ARA = -1;
        public double SCC = -1;



        public CAT21(string[] packet)
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
            string str1 = paquete.Substring(0, 3);
            int atp1 = Convert.ToInt32(str1, 2);

            if (atp1 == 0)
            {
                ATP = "24-Bit ICAO address.";
            }

            if (atp1 == 1)
            {
                ATP = "Duplicate address.";
            }

            if (atp1 == 2)
            {
                ATP = "Surface vehicle address.";
            }

            if (atp1 == 3)
            {
                ATP = "Anonymous address.";
            }

            str1 = paquete.Substring(3, 2);
            int arc1 = Convert.ToInt32(str1, 2);

            if (arc1 == 0)
            {
                ARC = "25 ft.";
            }

            if (arc1 == 1)
            {
                ARC = "100 ft.";
            }

            if (arc1 == 2)
            {
                ARC = "Unknown.";
            }

            if (arc1 == 3)
            {
                ARC = "Invalid .";
            }

            string rc1 = paquete.Substring(5, 1);

            if (rc1 == "0")
            {
                RC = "Default.";
            }

            if (rc1 == "1")
            {
                RC = "Range Check passed, CPR Validation pending.";
            }

            string rab1 = paquete.Substring(6, 1);

            if (rab1 == "0")
            {
                RAB = "Report from target transponder.";
            }

            if (rab1 == "1")
            {
                RAB = "Report from field monitor (fixed transponder).";
            }

            if (paquete.Length > 8)
            {
                string dcr1 = paquete.Substring(8, 1);

                if (dcr1 == "0")
                {
                    DCR = "No differential correction (ADS-B).";
                }

                if (dcr1 == "1")
                {
                    DCR = "Differential correction (ADS-B)";
                }

                string gbs1 = paquete.Substring(9, 1);

                if (gbs1 == "0")
                {
                    GBS = "Ground Bit not set.";
                }

                if (gbs1 == "1")
                {
                    GBS = "Ground Bit set.";
                }

                string sim1 = paquete.Substring(10, 1);

                if (sim1 == "0")
                {
                    SIM = "Actual target report.";
                }

                if (sim1 == "1")
                {
                    SIM = "Simulated target report.";
                }

                string tst1 = paquete.Substring(11, 1);

                if (tst1 == "0")
                {
                    TST = "Default.";
                }

                if (tst1 == "1")
                {
                    TST = "Test Target.";
                }

                string saa1 = paquete.Substring(12, 1);

                if (saa1 == "0")
                {
                    SAA = "Equipment capable to provide Selected Altitude.";
                }

                if (saa1 == "1")
                {
                    SAA = "Equipment not capable to provide Selected Altitude.";
                }

                str1 = paquete.Substring(13, 2);
                int cl1 = Convert.ToInt32(str1, 2);

                if (cl1 == 0)
                {
                    CL = "Report valid.";
                }

                if (cl1 == 1)
                {
                    CL = "Report suspect.";
                }

                if (cl1 == 2)
                {
                    CL = "No information.";
                }

                if (paquete.Length > 16)
                {
                    string ipc1 = paquete.Substring(18, 1);

                    if (ipc1 == "0")
                    {
                        IPC = "Independent Position Check = 0 default";
                    }

                    if (ipc1 == "1")
                    {
                        IPC = "Independent Position Check failed.";
                    }

                    string nogo1 = paquete.Substring(18, 1);

                    if (nogo1 == "0")
                    {
                        NOGO = "NOGO-bit not set.";
                    }

                    if (nogo1 == "1")
                    {
                        NOGO = "NOGO-bit set.";
                    }

                    string cpr1 = paquete.Substring(19, 1);

                    if (cpr1 == "0")
                    {
                        CPR = "CPR Validation correct.";
                    }

                    if (cpr1 == "1")
                    {
                        CPR = "CPR Validation failed.";
                    }

                    string ldpj1 = paquete.Substring(20, 1);

                    if (ldpj1 == "0")
                    {
                        LDPJ = "LDPJ not detected.";
                    }

                    if (ldpj1 == "1")
                    {
                        LDPJ = "LDPJ detected.";
                    }

                    string rcf1 = paquete.Substring(21, 1);

                    if (rcf1 == "0")
                    {
                        RCF = "Default.";
                    }

                    if (rcf1 == "1")
                    {
                        RCF = "Range Check failed .";
                    }

                }
            }


        }
        public void Calculate_TrackNumber(string paquete)
        {
            string string1 = paquete.Substring(4, 12);
            TrackNumber_number = Convert.ToInt32(string1, 2);
        }
        public void Calculate_TimeofAppliability_Position(string paquete)
        {
            double time = Convert.ToInt32(paquete, 2);
            TimeofApplicability_Position_seconds = time / 128;

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
        public void CalculatePositionWGS84_HRcoordinates(string paquete)
        {

            string str1 = paquete.Substring(0, 32);
            string str2 = paquete.Substring(32, 32);

            double a1 = Calculate_ComplementoA2(str1);
            double b1 = Calculate_ComplementoA2(str2);

            latWGS84_HR = a1 * (180 / Math.Pow(2, 30));
            lonWGS84_HR = b1 * (180 / Math.Pow(2, 30));


        }
        public void Calculate_TimeofAppliability_Velocity(string paquete)
        {
            double time = Convert.ToInt32(paquete, 2);
            TimeofApplicability_Position_seconds = time * (1 / 128);
        }
        public void Calculate_AirSpeed(string paquete)
        {

            string velocity = paquete.Substring(1, 15);

            if (Convert.ToInt32(paquete[0]) == 0)
            {
                IM = "IAS";
                double vel1 = Convert.ToInt32(velocity);
                TimeofApplicability_Position_seconds = vel1 * (1 / Math.Pow(2, 14));

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
            string ra1 = Convert.ToString(paquete[0]);

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
        public void Calculate_TimeofMessageReception_HRPosition(string paquete)
        {
            string fsi1 = paquete.Substring(0, 2);
            int fsi2 = Convert.ToInt32(fsi1, 2);

            if (fsi2 == 11)
            {
                FSI1 = "Reserved.";
            }

            if (fsi2 == 10)
            {
                string seconds = Convert.ToString(TimeofMessageReception_Position_seconds);
                FSI1 = "TOMRp whole seconds = " + seconds + " Whole seconds -1.";
            }

            if (fsi2 == 01)
            {
                string seconds = Convert.ToString(TimeofMessageReception_Position_seconds);
                FSI1 = "TOMRp whole seconds = " + seconds + " Whole seconds +1.";
            }

            if (fsi2 == 00)
            {
                string seconds = Convert.ToString(TimeofMessageReception_Position_seconds);
                FSI1 = "TOMRp whole seconds = " + seconds + " Whole seconds.";
            }

            string str1 = paquete.Substring(2, 30);
            int seconds1 = Convert.ToInt32(str1, 2);
            TimeofMessageReception_HRPosition_seconds = seconds1 * (Math.Pow(2, -30));

        }
        public void Calculate_TimeofMessageReception_HRVelocity(string paquete)
        {
            string fsi1 = paquete.Substring(0, 2);
            int fsi2 = Convert.ToInt32(fsi1, 2);

            if (fsi2 == 11)
            {
                FSI2 = "Reserved.";
            }

            if (fsi2 == 10)
            {
                string seconds = Convert.ToString(TimeofMessageReception_Velocity_seconds);
                FSI2 = "TOMRp whole seconds = " + seconds + " Whole seconds -1.";
            }

            if (fsi2 == 01)
            {
                string seconds = Convert.ToString(TimeofMessageReception_Velocity_seconds);
                FSI2 = "TOMRp whole seconds = " + seconds + " Whole seconds +1.";
            }

            if (fsi2 == 00)
            {
                string seconds = Convert.ToString(TimeofMessageReception_Velocity_seconds);
                FSI2 = "TOMRp whole seconds = " + seconds + " Whole seconds.";
            }

            string str1 = paquete.Substring(2, 30);
            int seconds1 = Convert.ToInt32(str1, 2);
            TimeofMessageReception_HRVelocity_seconds = seconds1 * (Math.Pow(2, -30));

        }

        public void Calculate_QualityIndicators(string paquete)
        {
            if (paquete.Length > 0)
            {
                string str1 = paquete.Substring(0, 3);
                string str2 = paquete.Substring(3, 4);

                NACv = Convert.ToInt32(str1, 2);
                NUCp = Convert.ToInt32(str2, 2);

                if (paquete.Length > 8)
                {
                    string str3 = Convert.ToString(paquete[8]);
                    NIC_Baro = Convert.ToInt32(str3);

                    string str5 = paquete.Substring(11, 4);
                    NACp = Convert.ToInt32(str5, 2);

                    string sil1 = paquete.Substring(9, 2);
                    SIL = Convert.ToInt32(sil1, 2).ToString();

                    if (paquete.Length > 16)
                    {
                        string str6 = paquete.Substring(18, 1);
                        if (str6 == "0") { SIL = String.Concat(SIL, "/", "Measured per flight-hour."); }
                        else { SIL = String.Concat(SIL, "/", "Measured per sample."); }

                        string str7 = paquete.Substring(19, 2);
                        SDA = Convert.ToInt32(str7, 2);

                        string str8 = paquete.Substring(21, 2);
                        GVA = Convert.ToInt32(str8, 2);

                        if (paquete.Length > 24)
                        {
                            string str9 = paquete.Substring(24, 4);
                            PIC = Convert.ToInt32(str9, 2);
                        }
                    }
                }
            }
        }


        public void Calculate_MOPSVersion(string paquete)
        {
            string vns1 = Convert.ToString(paquete[1]);
            if (vns1 == "0")
            {
                VNS = "The MOPS Version is supported by the GS.";
            }

            else
            {
                VNS = "The MOPS Version is not supported by the GS.";
            }

            string vn = paquete.Substring(2, 3);
            int vn1 = Convert.ToInt32(vn, 2);
            if (vn1 == 0)
            {
                VN = "ED102/DO-260 [Ref. 8].";
            }

            if (vn1 == 1)
            {
                VN = "DO-260A [Ref. 9].";
            }

            if (vn1 == 2)
            {
                VN = "ED102A/DO-260B [Ref. 11].";
            }


            string ltt = paquete.Substring(5, 3);
            int ltt1 = Convert.ToInt32(ltt, 2);
            if (ltt1 == 0)
            {
                LTT = "Other.";
            }

            if (ltt1 == 1)
            {
                LTT = "UAT";
            }

            if (ltt1 == 2)
            {
                LTT = "1090 ES.";
            }

            if (ltt1 == 4)
            {
                LTT = "VDL 4.";
            }

            if (ltt1 == 4 || ltt1 == 5 || ltt1 == 6 || ltt1 == 7)
            {
                LTT = "Not Assigned";
            }

        }
        public void Calculate_TargetStatus(string paquete)
        {
            string icf1 = paquete.Substring(0, 1);
            if (icf1 == "0")
            {
                ICF = "No intent change active.";
            }

            if (icf1 == "1")
            {
                ICF = "Intent change active.";
            }

            string lnav1 = paquete.Substring(1, 1);
            if (icf1 == "0")
            {
                LNAV = "LNAV Mode engaged.";
            }

            else if (icf1 == "1")
            {
                LNAV = "LNAV Mode not engaged.";
            }

            string ps = paquete.Substring(3, 3);
            int ps1 = Convert.ToInt32(ps, 2);
            if (ps1 == 0)
            {
                PS = "No emergency / not reported.";
            }

            if (ps1 == 1)
            {
                PS = "General emergency.";
            }

            if (ps1 == 2)
            {
                PS = "Lifeguard / medical emergency.";
            }

            if (ps1 == 3)
            {
                PS = "Minimum fuel.";
            }

            if (ps1 == 4)
            {
                PS = "No communications.";
            }

            if (ps1 == 5)
            {
                PS = "Unlawful interference.";
            }

            if (ps1 == 6)
            {
                PS = "“Downed” Aircraft.";
            }

            string ss = paquete.Substring(6, 2);
            int ss1 = Convert.ToInt32(ss, 2);
            if (ss1 == 0)
            {
                SS = "No condition reported.";
            }

            if (ss1 == 1)
            {
                SS = "Permanent Alert (Emergency condition).";
            }

            if (ss1 == 2)
            {
                SS = "Temporary Alert (change in Mode 3/A Code other than emergency).";
            }

            if (ss1 == 3)
            {
                SS = "SPI set.";
            }
        }
        public void Calculate_AirborneGroundVector(string paquete)
        {
            string str1 = paquete.Substring(0, 16);
            string str2 = paquete.Substring(16, 16);

            if (str1[0] == Convert.ToChar("0"))
            {
                RE_AGV = "Value in defined range.";
            }

            else
            {
                RE_AGV = "Value exceeds defined range.";
            }

            string str3 = str1.Substring(1, 15);

            GroundSpeed = Convert.ToInt32(str3, 2) * Math.Pow(2, -14);
            TrackAngle = Convert.ToInt32(str2, 2) * (360 / Math.Pow(2, 16));
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
        public void Calculate_GeometricVerticalRate(string paquete)
        {
            string str1 = Convert.ToString(paquete[0]);
            if (str1 == "0")
            {
                RE_GVR = "Value in defined range.";
            }

            else
            {
                RE_GVR = "Value exceeds defined range.";
            }

            string str2 = paquete.Substring(1, 15);

            GeometricVerticalRate_fmin = Calculate_ComplementoA2(str2) * 6.25;
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
            int ecat = Convert.ToInt32(paquete, 2);
            if (ecat == 0)
            {
                ECAT = "No ADS-B Emitter Category Information.";
            }
            if (ecat == 1)
            {
                ECAT = "Light aircraft <= 15500 lbs.";
            }
            if (ecat == 2)
            {
                ECAT = "15500 lbs < small aircraft <75000 lbs.";
            }
            if (ecat == 3)
            {
                ECAT = "75000 lbs < medium a/c  < 300000 lbs.";
            }
            if (ecat == 4)
            {
                ECAT = "High Vortex Large.";
            }
            if (ecat == 5)
            {
                ECAT = "300000 lbs <= heavy aircraft.";
            }
            if (ecat == 6)
            {
                ECAT = "Highly manoeuvrable (5g acceleration capability) and high speed (>400 knots cruise).";
            }
            if (ecat == 7 || ecat == 8 || ecat == 9)
            {
                ECAT = "Reserved";
            }
            if (ecat == 10)
            {
                ECAT = "Rotocraft.";
            }
            if (ecat == 11)
            {
                ECAT = "Glider/Sailplane.";
            }
            if (ecat == 12)
            {
                ECAT = "Lighter-than-air.";
            }
            if (ecat == 13)
            {
                ECAT = "Unmanned aerial vehicle.";
            }
            if (ecat == 14)
            {
                ECAT = "Space/Transatmospheric Vehicle.";
            }
            if (ecat == 15)
            {
                ECAT = "Ultralight/Handglider/Paraglider.";
            }
            if (ecat == 16)
            {
                ECAT = "Parachutist/Skydiver.";
            }
            if (ecat == 17 || ecat == 18 || ecat == 19)
            {
                ECAT = "Reserved.";
            }
            if (ecat == 20)
            {
                ECAT = "Surface emergency vehicle";
            }
            if (ecat == 21)
            {
                ECAT = "Surface service vehicle.";
            }
            if (ecat == 22)
            {
                ECAT = "Fixed ground or tethered obstruction.";
            }
            if (ecat == 23)
            {
                ECAT = "Cluster obstacle.";
            }
            if (ecat == 24)
            {
                ECAT = "Line obstacle.";
            }


        }
        public void Calculate_MetInformation(string paquete)
        {

            int counter = 8;
            string ws = "";
            string wd = "";
            string temp = "";
            string turb = "";

            if (Convert.ToString(paquete[0]) == "1")
            {
                ws = paquete.Substring(counter, 16);
                counter = counter + 16;

                WindSpeed = Convert.ToInt32(ws, 2);
            }

            if (Convert.ToString(paquete[1]) == "1")
            {
                wd = paquete.Substring(counter, 16);
                counter = counter + 16;

                WindDirection = Convert.ToInt32(wd, 2);
            }

            if (Convert.ToString(paquete[2]) == "1")
            {
                temp = paquete.Substring(counter, 16);
                counter = counter + 16;

                Temperature = Calculate_ComplementoA2(temp) * 0.25;
            }

            if (Convert.ToString(paquete[3]) == "1")
            {
                turb = paquete.Substring(counter, 8);
                counter = counter + 8;

                Turbulence = Convert.ToInt32(turb, 2);
            }


        }
        public void Calculate_SelectedAltitude(string paquete)
        {
            string sas = Convert.ToString(paquete[0]);
            if (sas == "0")
            {
                SAS = "No source information provided.";
            }

            if (sas == "1")
            {
                SAS = "Source information provided.";
            }

            string source1 = Convert.ToString(paquete[1]);
            string source2 = Convert.ToString(paquete[2]);

            if (source1 == "0" && source2 == "0")
            {
                Source = "Unknown";
            }

            if (source1 == "0" && source2 == "1")
            {
                Source = "Aircraft Altitude (Holding Altitude).";
            }

            if (source1 == "1" && source2 == "0")
            {
                Source = "MCP/FCU Selected Altitude.";
            }
            if (source1 == "1" && source2 == "1")
            {
                Source = "FMS Selected Altitude.";
            }

            string alt = paquete.Substring(3, 13);
            SelectedAltitude_ft = Calculate_ComplementoA2(alt) * 25;

        }
        public void Calculate_FinalStateSelectedAltitude(string paquete)
        {
            string mv1 = Convert.ToString(paquete[0]);
            if (mv1 == "0")
            {
                MV = "Not actice or unknown.";
            }
            else
            {
                MV = "Active.";
            }

            string ah1 = Convert.ToString(paquete[1]);
            if (ah1 == "0")
            {
                AH = "Not actice or unknown.";
            }
            else
            {
                AH = "Active.";
            }

            string am1 = Convert.ToString(paquete[1]);
            if (am1 == "0")
            {
                AM = "Not actice or unknown.";
            }
            else
            {
                AM = "Active.";
            }

            string alt1 = paquete.Substring(3, 13);
            FinalStateSelectedAltitude_ft = Calculate_ComplementoA2(alt1) * 25;
        }
        public void Calculate_TrajectoryIntent(string paquete)
        {
            int counter = 8;
            string trajectoryintentstatus = "";
            string trajectoryintentdata = "";

            if (Convert.ToString(paquete[0]) == "1")
            {
                trajectoryintentstatus = paquete.Substring(counter, 8);
                counter = counter + 8;

                string str1 = Convert.ToString(trajectoryintentstatus[0]);
                if (str1 == "0")
                {
                    NAV = "Trajectory Intent Data is available for this aircraft.";
                }

                else
                {
                    NAV = "Trajectory Intent Data is not available for this aircraft.";
                }

                string str2 = Convert.ToString(trajectoryintentstatus[0]);
                if (str2 == "0")
                {
                    NVB = "Trajectory Intent Data is valid.";
                }

                else
                {
                    NVB = "Trajectory Intent Data is not valid.";
                }
            }

            if (Convert.ToString(paquete[1]) == "1")
            {
                trajectoryintentdata = paquete.Substring(counter, 128);
                counter = counter + 128;

                string rep1 = trajectoryintentdata.Substring(0, 8);
                string tca1 = trajectoryintentdata.Substring(8, 1);
                string nc1 = trajectoryintentdata.Substring(9, 1);
                string tcp1 = trajectoryintentdata.Substring(10, 6);
                string alt1 = trajectoryintentdata.Substring(16, 16);
                string lat1 = trajectoryintentdata.Substring(32, 24);
                string lon1 = trajectoryintentdata.Substring(56, 24);
                string pointtype1 = trajectoryintentdata.Substring(80, 4);
                string td1 = trajectoryintentdata.Substring(84, 2);
                string tra1 = trajectoryintentdata.Substring(86, 1);
                string toa1 = trajectoryintentdata.Substring(87, 1);
                string tov1 = trajectoryintentdata.Substring(88, 24);
                string ttr1 = trajectoryintentdata.Substring(112, 16);

                REP = Convert.ToInt32(rep1, 2);

                if (Convert.ToString(tca1) == "0")
                {
                    TCA = "TCP number available";
                }
                else
                {
                    TCA = "TCP number not available.";
                }

                if (Convert.ToString(nc1) == "0")
                {
                    NC = "TCP compliance.";

                }
                else
                {
                    NC = "TCP non-compliance.";
                }

                TCP = Convert.ToInt32(tcp1, 2);
                TI_ALtitude = Calculate_ComplementoA2(alt1) * 10;
                TI_Latitude = Calculate_ComplementoA2(lat1) * 180 / (Math.Pow(2, 23));
                TI_Longitude = Calculate_ComplementoA2(lon1) * 180 / (Math.Pow(2, 23));

                int int1 = Convert.ToInt32(pointtype1);
                if (int1 == 0)
                {
                    PointType = "Unknown";
                }
                if (int1 == 1)
                {
                    PointType = "Fly by Waypoint (LT)";
                }
                if (int1 == 2)
                {
                    PointType = "Fly over waypoint (LT).";
                }
                if (int1 == 3)
                {
                    PointType = "Hold pattern (LT).";
                }
                if (int1 == 4)
                {
                    PointType = "Procedure hold (LT).";
                }
                if (int1 == 5)
                {
                    PointType = "Procedure turn (LT).";
                }
                if (int1 == 6)
                {
                    PointType = "RF leg (LT).";
                }
                if (int1 == 7)
                {
                    PointType = "Top of climb (VT).";
                }
                if (int1 == 8)
                {
                    PointType = "Top of descent (VT).";
                }
                if (int1 == 9)
                {
                    PointType = "Start of level (VT).";
                }
                if (int1 == 10)
                {
                    PointType = "Cross-over altitude (VT).";
                }
                if (int1 == 11)
                {
                    PointType = "Transition altitude (VT).";
                }

                string str1 = Convert.ToString(td1[0]);
                string str2 = Convert.ToString(td1[1]);

                if (str1 == "0" && str2 == "8")
                {
                    TD = "N/A";
                }
                if (str1 == "0" && str2 == "1")
                {
                    TD = "Turn right.";
                }
                if (str1 == "1" && str2 == "0")
                {
                    TD = "Turn left.";
                }
                else
                {
                    TD = "No turn.";
                }

                if (tra1 == "0")
                {
                    TRA = "TTR not available.";
                }
                else
                {
                    TRA = "TTR available.";
                }

                TOV = Convert.ToInt32(tov1, 2);
                TTR = Convert.ToInt32(ttr1, 2) * 0.01;
            }
        }
        public void Calculate_AircraftOperationalStatus(string paquete)
        {

            string ra1 = paquete.Substring(1, 1);

            if (ra1 == "1")
            {
                RA = "TCAS RA active";
            }

            else
            {
                RA = "TCAS II or ACAS RA not active";
            }


            string TC1 = Convert.ToString((paquete.Substring(1, 2)));

            if (Convert.ToInt32(TC1, 2) == 0)
            {
                TC = "No capability for Trajectory Change Reports.";
            }

            if (Convert.ToInt32(TC1, 2) == 1)
            {
                TC = "Support for TC+0 reports only.";
            }

            if (Convert.ToInt32(TC1, 2) == 2)
            {
                TC = "Support for multiple TC reports.";
            }

            if (Convert.ToInt32(TC1, 2) == 3)
            {
                TC = "Reserved.";
            }

            string ta1 = paquete.Substring(3, 1);

            if (ta1 == "0")
            {
                TS = "No capability to support Target State Reports.";
            }

            if (ta1 == "1")
            {
                TS = "Capable of supporting target State Reports.";
            }

            string arv1 = paquete.Substring(4, 1);

            if (arv1 == "0")
            {
                ARV = "No capability to generate ARV-reports.";
            }

            if (arv1 == "1")
            {
                ARV = "Capable of generate ARV-reports.";
            }

            string cdti_a1 = paquete.Substring(5, 1);

            if (cdti_a1 == "0")
            {
                CDTI_A = "CDTI not operational.";
            }

            if (cdti_a1 == "1")
            {
                CDTI_A = "CDTI operational.";
            }

            string tcas1 = paquete.Substring(6, 1);

            if (tcas1 == "0")
            {
                TCAS = "TCAS operational.";
            }

            if (tcas1 == "1")
            {
                TCAS = "TCAS not operational.";
            }

            string sa1 = paquete.Substring(7, 1);

            if (sa1 == "0")
            {
                SA = "Antenna Diversity.";
            }

            if (sa1 == "1")
            {
                SA = "Single Antenna only.";
            }
        }
        public void Calculate_SurfaceCapabilitiesandCharacterístics(string paquete)
        {
            string poa1 = Convert.ToString(paquete[3]);
            if (poa1 == "0")
            {
                POA = "Position transmitted is not ADS-B position reference point.";
            }
            else
            {
                POA = "Position transmitted is the ADS-B position reference point ";
            }

            string cdtis1 = Convert.ToString(paquete[4]);
            if (cdtis1 == "0")
            {
                CDTI_S = "CDTI not operational.";
            }
            else
            {
                CDTI_S = "CDTI operational.";
            }

            string b2low1 = Convert.ToString(paquete[5]);
            if (b2low1 == "0")
            {
                B2low = "≥ 70 Watts.";
            }
            else
            {
                B2low = "< 70 Watts.";
            }

            string ras1 = Convert.ToString(paquete[6]);
            if (ras1 == "0")
            {
                RAS = "Aircraft not receiving ATC-services.";
            }
            else
            {
                RAS = "Aircraft receiving ATC services.";
            }

            string ident1 = paquete.Substring(6, 2);
            if (ident1 == "0")
            {
                IDENT = "IDENT switch not active.";
            }
            else
            {
                IDENT = "IDENT switch active.";
            }

            if (paquete.Length > 8)
            {
                int int2 = Convert.ToInt32(paquete.Substring(12, 4), 2);

                if (VN == "DO-260A [Ref. 9].")
                {
                    if (int2 == 0)
                    {
                        Width = "W < 11.5 ";
                        Length = "L < 15";
                    }
                    if (int2 == 1)
                    {
                        Width = "W < 23";
                        Length = "L < 15";
                    }
                    if (int2 == 2)
                    {
                        Width = "W < 28.5";
                        Length = "L < 25";
                    }
                    if (int2 == 3)
                    {
                        Width = "W < 28.5";
                        Length = "L < 25";
                    }
                    if (int2 == 4)
                    {
                        Width = "W < 33";
                        Length = "L < 35";
                    }
                    if (int2 == 5)
                    {
                        Width = "W < 38";
                        Length = "L < 35";
                    }
                    if (int2 == 6)
                    {
                        Width = "W < 39.5";
                        Length = "L < 45";
                    }
                    if (int2 == 7)
                    {
                        Width = "W < 45";
                        Length = "L < 45";
                    }
                    if (int2 == 8)
                    {
                        Width = "W < 45";
                        Length = "L < 55";
                    }
                    if (int2 == 9)
                    {
                        Width = "W < 52";
                        Length = "L < 55";
                    }
                    if (int2 == 10)
                    {
                        Width = "W < 59.5";
                        Length = "L < 65";
                    }
                    if (int2 == 11)
                    {
                        Width = "W < 67";
                        Length = "L < 65";
                    }
                    if (int2 == 12)
                    {
                        Width = "W < 72.5";
                        Length = "L < 75";
                    }
                    if (int2 == 13)
                    {
                        Width = "W < 80";
                        Length = "L < 75";
                    }
                    if (int2 == 14)
                    {
                        Width = "W < 80";
                        Length = "L < 85";
                    }
                    if (int2 == 15)
                    {
                        Width = "W > 80";
                        Length = "L < 85";
                    }

                }

                if (VN == "ED102A/DO-260B [Ref. 11].")
                {
                    if (int2 == 0)
                    {
                        Width = "W < 11.5 ";
                        Length = "L < 15";
                    }
                    if (int2 == 1)
                    {
                        Width = "W < 23";
                        Length = "L < 15";
                    }
                    if (int2 == 2)
                    {
                        Width = "W < 28.5";
                        Length = "L < 25";
                    }
                    if (int2 == 3)
                    {
                        Width = "W < 28.5";
                        Length = "L < 25";
                    }
                    if (int2 == 4)
                    {
                        Width = "W < 33";
                        Length = "L < 35";
                    }
                    if (int2 == 5)
                    {
                        Width = "W < 38";
                        Length = "L < 35";
                    }
                    if (int2 == 6)
                    {
                        Width = "W < 39.5";
                        Length = "L < 45";
                    }
                    if (int2 == 7)
                    {
                        Width = "W < 45";
                        Length = "L < 45";
                    }
                    if (int2 == 8)
                    {
                        Width = "W < 45";
                        Length = "L < 55";
                    }
                    if (int2 == 9)
                    {
                        Width = "W < 52";
                        Length = "L < 55";
                    }
                    if (int2 == 10)
                    {
                        Width = "W < 59.5";
                        Length = "L < 65";
                    }
                    if (int2 == 11)
                    {
                        Width = "W < 67";
                        Length = "L < 65";
                    }
                    if (int2 == 12)
                    {
                        Width = "W < 72.5";
                        Length = "L < 75";
                    }
                    if (int2 == 13)
                    {
                        Width = "W < 80";
                        Length = "L < 75";
                    }
                    if (int2 == 14)
                    {
                        Width = "W < 80";
                        Length = "L < 85";
                    }
                    if (int2 == 15)
                    {
                        Width = "W > 80";
                        Length = "L < 85";
                    }
                }
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
        public void Calculate_ACASResolutionAdvisoryReport(string paquete)
        {
            string typ1 = paquete.Substring(0, 5);
            TYP = Convert.ToInt32(typ1, 2);

            string styp1 = paquete.Substring(5, 3);
            STYP = Convert.ToInt32(styp1, 2);

            string ara1 = paquete.Substring(8, 14);
            ARA_ACAS = Convert.ToInt32(ara1, 2);

            string rac1 = paquete.Substring(22, 4);
            RAC = Convert.ToInt32(rac1, 2);

            string rat1 = paquete.Substring(26, 1);
            RAT = Convert.ToInt32(rat1);

            string mte = paquete.Substring(27, 1);
            MTE = Convert.ToInt32(mte, 2);

            string tti1 = paquete.Substring(28, 2);
            TTI = Convert.ToInt32(tti1, 2);

            string tid1 = paquete.Substring(30, 26);
            TID = Convert.ToInt32(tid1, 2);
        }
        public void Calculate_ReceiverID(string paquete)
        {
            ReceiverID_number = Convert.ToInt32(paquete);
        }
        public int Calculate_Data_Ages(string DataAges, string[] paquete, int data_position)
        {
            while (DataAges.Length < 32)
            {
                DataAges = String.Concat(DataAges, "0");
            }

            if (Convert.ToString(DataAges[0]) == "1")
            {
                string aos1 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                data_position = data_position + 1;
                AOS = Convert.ToInt32(aos1, 2) * 0.1;
            }

            if (Convert.ToString(DataAges[1]) == "1")
            {
                string trd1 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                data_position = data_position + 1;
                TRD = Convert.ToInt32(trd1, 2) * 0.1;
            }

            if (Convert.ToString(DataAges[2]) == "1")
            {
                string m3a1 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                data_position = data_position + 1;
                M3A = Convert.ToInt32(m3a1, 2) * 0.1;
            }

            if (Convert.ToString(DataAges[3]) == "1")
            {
                string qi1 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                data_position = data_position + 1;
                QI = Convert.ToInt32(qi1, 2) * 0.1;
            }

            if (Convert.ToString(DataAges[4]) == "1")
            {
                string ti1 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                data_position = data_position + 1;
                TI = Convert.ToInt32(ti1, 2) * 0.1;
            }

            if (Convert.ToString(DataAges[5]) == "1")
            {
                string mam1 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                data_position = data_position + 1;
                MAM = Convert.ToInt32(mam1, 2) * 0.1;
            }

            if (Convert.ToString(DataAges[6]) == "1")
            {
                string gh1 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                data_position = data_position + 1;
                GH = Convert.ToInt32(gh1, 2) * 0.1;
            }

            if (Convert.ToString(DataAges[8]) == "1")
            {
                string fl1 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                data_position = data_position + 1;
                FL = Convert.ToInt32(fl1, 2) * 0.1;
            }

            if (Convert.ToString(DataAges[9]) == "1")
            {
                string isa1 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                data_position = data_position + 1;
                ISA = Convert.ToInt32(isa1, 2) * 0.1;
            }

            if (Convert.ToString(DataAges[10]) == "1")
            {
                string fsa1 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                data_position = data_position + 1;
                FSA = Convert.ToInt32(fsa1, 2) * 0.1;
            }

            if (Convert.ToString(DataAges[11]) == "1")
            {
                string as1 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                data_position = data_position + 1;
                AS = Convert.ToInt32(as1, 2) * 0.1;
            }

            if (Convert.ToString(DataAges[12]) == "1")
            {
                string tas1 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                data_position = data_position + 1;
                TAS = Convert.ToInt32(tas1, 2) * 0.1;
            }

            if (Convert.ToString(DataAges[13]) == "1")
            {
                string mh1 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                data_position = data_position + 1;
                MH = Convert.ToInt32(mh1, 2) * 0.1;
            }

            if (Convert.ToString(DataAges[14]) == "1")
            {
                string bvr1 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                data_position = data_position + 1;
                BVR = Convert.ToInt32(bvr1, 2) * 0.1;
            }

            if (Convert.ToString(DataAges[16]) == "1")
            {
                string gvr1 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                data_position = data_position + 1;
                GVR = Convert.ToInt32(gvr1, 2) * 0.1;
            }

            if (Convert.ToString(DataAges[17]) == "1")
            {
                string gv1 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                data_position = data_position + 1;
                GV = Convert.ToInt32(gv1, 2) * 0.1;
            }

            if (Convert.ToString(DataAges[18]) == "1")
            {
                string tar1 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                data_position = data_position + 1;
                TAR = Convert.ToInt32(tar1, 2) * 0.1;
            }

            if (Convert.ToString(DataAges[19]) == "1")
            {
                string ti1 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                data_position = data_position + 1;
                TI_DA = Convert.ToInt32(ti1, 2) * 0.1;
            }

            if (Convert.ToString(DataAges[20]) == "1")
            {
                string ts1 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                data_position = data_position + 1;
                TS_DA = Convert.ToInt32(ts1, 2) * 0.1;
            }

            if (Convert.ToString(DataAges[21]) == "1")
            {
                string met1 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                data_position = data_position + 1;
                MET = Convert.ToInt32(met1, 2) * 0.1;
            }

            if (Convert.ToString(DataAges[22]) == "1")
            {
                string roa1 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                data_position = data_position + 1;
                ROA = Convert.ToInt32(roa1, 2) * 0.1;
            }

            if (Convert.ToString(DataAges[24]) == "1")
            {
                string ara1 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                data_position = data_position + 1;
                ARA = Convert.ToInt32(ara1, 2) * 0.1;
            }

            if (Convert.ToString(DataAges[25]) == "1")
            {
                string scc1 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                data_position = data_position + 1;
                SCC = Convert.ToInt32(scc1, 2) * 0.1;
            }

            return data_position;
        }



        public void Calculate_FSPEC(string[] paquete)
        {
            GeoUtils1 = new GeoUtils( E , A , new CoordinatesWGS84(LatARP * GeoUtils.DEGS2RADS, LonARP * GeoUtils.DEGS2RADS));

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

                    data_position = data_position + 2;

                    DataSourseIdentification = String.Concat(string1, string2);

                    Calculate_DataSourceIdentification(DataSourseIdentification);

                }  // 1 I021/010 Data Source Identification

                if (Char.ToString(FSPEC_fake[1]) == "1") // 2 I021/040 Target Report Descriptor
                {

                    // primero leemos el primer paquete y lo pasamos a binario

                    string string_packet = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                    string_packet = AddZeros(string_packet);

                    if ((Convert.ToString(string_packet[7])) == "0") // si ultima posicion es un 0 guardamos el octeto y pasamos al siguiente
                    {
                        TargetReportDescriptor = string_packet;
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

                    TargetReportDescriptor = string_packet;

                    Calculate_TargetReportDescriptor(TargetReportDescriptor);

                }// 2 I021/040 Target Report Descriptor

                if (Char.ToString(FSPEC_fake[2]) == "1") // 3 I021/161 Track Number 
                {
                    string string1 = Convert.ToString(paquete[data_position]);
                    string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                    string1 = AddZeros(string1);

                    string string2 = Convert.ToString(paquete[data_position + 1]);
                    string2 = Convert.ToString(Convert.ToInt32(string2, 16), 2);
                    string2 = AddZeros(string2);


                    TrackNumber = String.Concat(string1, string2);

                    data_position = data_position + 2;

                    Calculate_TrackNumber(TrackNumber);
                }// 3 I021/161 Track Number 

                if (Char.ToString(FSPEC_fake[3]) == "1") // 4 I021/015 Service Identification 
                {
                    string string1 = Convert.ToString(paquete[data_position]);
                    string1 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                    string1 = AddZeros(string1);

                    ServiceIdentification = String.Concat(string1);

                    data_position = data_position + 1;

                    ServiceIdentification_number = Convert.ToInt32(ServiceIdentification, 2);

                } // 4 I021/015 Service Identification 

                if (Char.ToString(FSPEC_fake[4]) == "1") // 5 I021/071 Time of Applicability for Position
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

                    TimeofApplicability_Position = String.Concat(string1, string2, string3);

                    data_position = data_position + 3;

                    Calculate_TimeofAppliability_Position(TimeofApplicability_Position);
                }// 5 I021/071 Time of Applicability for Position

                if (Char.ToString(FSPEC_fake[5]) == "1") // 6 I021/130 Position in WGS-84 coordinates
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

                    //coordinatesWGS84 = new CoordinatesWGS84(latWGS84*GeoUtils.DEGS2RADS, lonWGS84*GeoUtils.DEGS2RADS);
                    //CoordinatesXYZ coordGeocentric = GeoUtils1.change_geodesic2geocentric(coordinatesWGS84);
                    //CoordinatesXYZ coordSystemCartesian = GeoUtils1.change_geocentric2system_cartesian(coordGeocentric);
                    //coordinatesUVH = GeoUtils1.change_system_cartesian2stereographic(coordSystemCartesian);


                }// 6 I021/130 Position in WGS-84 coordinates

                if (Char.ToString(FSPEC_fake[6]) == "1") // 7 I021/131 Position in WGS-84 co-ordinates, high res
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

                    PositioninWGS_HRcoordinates = String.Concat(string1, string2, string3, string4, string5, string6, string7, string8);

                    data_position = data_position + 8;

                    CalculatePositionWGS84_HRcoordinates(PositioninWGS_HRcoordinates);

                    //coordinatesWGS84 = new CoordinatesWGS84(latWGS84_HR * GeoUtils.DEGS2RADS, lonWGS84_HR * GeoUtils.DEGS2RADS);
                    //CoordinatesXYZ coordGeocentric = GeoUtils1.change_geodesic2geocentric(coordinatesWGS84);
                    //CoordinatesXYZ coordSystemCartesian = GeoUtils1.change_geocentric2system_cartesian(coordGeocentric);
                    //coordinatesUVH = GeoUtils1.change_system_cartesian2stereographic(coordSystemCartesian);

                } // 7 I021/131 Position in WGS-84 co-ordinates, high res

                if (Char.ToString(FSPEC_fake[7]) == "1") //FX
                {
                }// FX

                if (FSPEC.Length > 8)
                {
                    if (Char.ToString(FSPEC_fake[8]) == "1") // 8 I021/072 Time of Applicability for Velocity
                    {
                        i = 0;
                        while (i < 3)
                        {
                            string string1 = Convert.ToString(paquete[data_position + i]);
                            string string2 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                            string2 = AddZeros(string2);
                            TimeofApplicability_Velocity = String.Concat(TimeofApplicability_Velocity, string2);
                            i = i + 1;
                        }

                        data_position = data_position + 3;

                        TimeofApplicability_Velocity_seconds = Convert.ToInt32(TimeofApplicability_Velocity, 2) / 128;

                    }

                    if (Char.ToString(FSPEC_fake[9]) == "1") // 9 I021/150 Air Speed 
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

                    }

                    if (Char.ToString(FSPEC_fake[10]) == "1") // 10 I021/151 True Air Speed
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

                    }

                    if (Char.ToString(FSPEC_fake[11]) == "1") // 11 I021/080 Target Address
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
                    }

                    if (Char.ToString(FSPEC_fake[12]) == "1") // 12 I021/073 Time of Message Reception of Position
                    {

                        i = 0;
                        while (i < 3)
                        {
                            string string1 = Convert.ToString(paquete[data_position + i]);
                            string string2 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                            string2 = AddZeros(string2);
                            TimeofMessageReception_Position = String.Concat(TimeofMessageReception_Position, string2);
                            i = i + 1;
                        }

                        data_position = data_position + 3;

                        TimeofMessageReception_Position_seconds = Convert.ToInt32(TimeofMessageReception_Position, 2);
                        TimeofMessageReception_Position_seconds = TimeofMessageReception_Position_seconds / 128;

                    }

                    if (Char.ToString(FSPEC_fake[13]) == "1") // 13 I021 / 074 Time of Message Reception of Position-High
                    {

                        i = 0;
                        while (i < 4)
                        {
                            string string1 = Convert.ToString(paquete[data_position + i]);
                            string string2 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                            string2 = AddZeros(string2);
                            TimeofMessageReception_HRPosition = String.Concat(TimeofMessageReception_HRPosition, string2);
                            i = i + 1;
                        }

                        data_position = data_position + 4;

                        Calculate_TimeofMessageReception_HRPosition(TimeofMessageReception_HRPosition);
                    }

                    if (Char.ToString(FSPEC_fake[14]) == "1") // 14 I021/075 Time of Message Reception of Velocity 
                    {
                        i = 0;
                        while (i < 3)
                        {
                            string string1 = Convert.ToString(paquete[data_position + i]);
                            string string2 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                            string2 = AddZeros(string2);
                            TimeofMessageReception_Velocity = String.Concat(TimeofMessageReception_Velocity, string2);
                            i = i + 1;
                        }

                        data_position = data_position + 3;

                        int time = Convert.ToInt32(TimeofMessageReception_Velocity, 2);
                        TimeofMessageReception_Velocity_seconds = time * 1 / 128;

                    }

                    if (Char.ToString(FSPEC_fake[15]) == "1") // FX - Field extension indicator 
                    {
                    }// FX

                    if (FSPEC.Length > 16)
                    {
                        if (Char.ToString(FSPEC_fake[16]) == "1") // 15 I021 / 076 Time of Message Reception of Velocity-High Precision
                        {
                            i = 0;
                            while (i < 4)
                            {
                                string string1 = Convert.ToString(paquete[data_position + i]);
                                string string2 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                                string2 = AddZeros(string2);
                                TimeofMessageReception_HRVelocity = String.Concat(TimeofMessageReception_HRVelocity, string2);
                                i = i + 1;
                            }

                            data_position = data_position + 4;

                            Calculate_TimeofMessageReception_HRVelocity(TimeofMessageReception_HRVelocity);
                            var a = TimeofMessageReception_HRVelocity_seconds;
                        }

                        if (Char.ToString(FSPEC_fake[17]) == "1") // 16 I021/140 Geometric Height
                        {
                            i = 0;
                            while (i < 2)
                            {
                                string string1 = Convert.ToString(paquete[data_position + i]);
                                string string2 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                                string2 = AddZeros(string2);
                                GeometricHeight = String.Concat(GeometricHeight, string2);
                                i = i + 1;
                            }

                            data_position = data_position + 2;

                            if (GeometricHeight[0] == Convert.ToChar("0"))
                            {
                                string str1 = GeometricHeight.Substring(1, 15);
                                GeometricHeight_feet = Calculate_ComplementoA2(str1);
                                GeometricHeight_feet = GeometricHeight_feet * 6.25;
                            }
                            else
                            {
                                string str1 = GeometricHeight.Substring(1, 15);
                                GeometricHeight_feet = Convert.ToInt32(str1, 2);
                                GeometricHeight_feet = GeometricHeight_feet * 6.25;
                            }

                        }

                        if (Char.ToString(FSPEC_fake[18]) == "1")// 17 I021/090 Quality Indicators
                        {
                            // primero leemos el primer paquete y lo pasamos a binario

                            string string_packet = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                            string_packet = AddZeros(string_packet);

                            if ((Convert.ToString(string_packet[7])) == "0") // si ultima posicion es un 0 guardamos el octeto y pasamos al siguiente
                            {
                                QualityIndicators = string_packet;
                                data_position = data_position + 1;
                            }
                            if ((Convert.ToString(string_packet[7])) == "1") // si ultimo valor =1 hacemos un bucle que vaya concatenando todos los octetos acabados en 1
                            {
                                i = 0;
                                data_position = data_position + 1;
                                while ((Convert.ToString(string_packet[string_packet.Length - 1])) == "1" && string_packet.Length < 32)
                                {
                                    string string_packet2 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                                    string_packet2 = AddZeros(string_packet2);
                                    string_packet = string.Concat(string_packet, string_packet2);
                                    data_position = data_position + 1;
                                    i = i + 1;
                                }
                            }

                            QualityIndicators = string_packet;

                            Calculate_QualityIndicators(QualityIndicators);

                        }

                        if (Char.ToString(FSPEC_fake[19]) == "1")// 18 I021/210 MOPS Version
                        {
                            i = 0;
                            while (i < 1)
                            {
                                string string1 = Convert.ToString(paquete[data_position + i]);
                                string string2 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                                string2 = AddZeros(string2);
                                MOPSVersion = String.Concat(MOPSVersion, string2);
                                i = i + 1;
                            }

                            data_position = data_position + 1;

                            Calculate_MOPSVersion(MOPSVersion);
                        }

                        if (Char.ToString(FSPEC_fake[20]) == "1") // 19 I021/070 Mode 3/A Code
                        {
                            bool bool1 = false;

                            i = 0;
                            while (i < 2 && data_position < paquete.Length)
                            {
                                string string1 = Convert.ToString(paquete[data_position + i]);
                                string string2 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                                string2 = AddZeros(string2);
                                Mode3ACode_bin = String.Concat(Mode3ACode_bin, string2);
                                i = i + 1;
                                bool1 = true;
                            }

                            data_position = data_position + 2;

                            if (bool1 == true)
                            {
                                int int1 = Convert.ToInt32(Mode3ACode_bin, 2); // pasamos de binario a decimal
                                Mode3ACode_oct = Convert.ToString(int1, 8); // pasamos de decimal a octal
                                while (Mode3ACode_oct.Length < 4) { Mode3ACode_oct = String.Concat("0", Mode3ACode_oct); }
                            }

                        }

                        if (Char.ToString(FSPEC_fake[21]) == "1") // 20 I021/230 Roll Angle
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

                            int int1 = Convert.ToInt32(RollAngle, 2);
                            RollAngle_degrees = int1 * 0.01;
                        }

                        if (Char.ToString(FSPEC_fake[22]) == "1") // 21 I021/145 Flight Level
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

                            FlightLevel_FL = Math.Round((Calculate_ComplementoA2(FlightLevel)) / 4);

                        }

                        if (Char.ToString(FSPEC_fake[23]) == "1") // FX - Field extension indicator
                        {
                        } // FX

                        if (FSPEC.Length > 24)
                        {
                            if (Char.ToString(FSPEC_fake[24]) == "1") // 22 I021/152 Magnetic Heading
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
                            }

                            if (Char.ToString(FSPEC_fake[25]) == "1") // 23 I021/200 Target Status 
                            {
                                i = 0;
                                while (i < 1)
                                {
                                    string string1 = Convert.ToString(paquete[data_position + i]);
                                    string string2 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                                    string2 = AddZeros(string2);
                                    TargetStatus = String.Concat(TargetStatus, string2);
                                    i = i + 1;
                                }

                                data_position = data_position + 1;

                                Calculate_TargetStatus(TargetStatus);
                            }

                            if (Char.ToString(FSPEC_fake[26]) == "1") // 24 I021/155 Barometric Vertical Rate
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


                            }

                            if (Char.ToString(FSPEC_fake[27]) == "1") // 25 I021/157 Geometric Vertical Rate
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

                                Calculate_GeometricVerticalRate(GeometricVerticalRate);

                            }

                            if (Char.ToString(FSPEC_fake[28]) == "1") // 26 I021/160 Airborne Ground Vector 
                            {
                                i = 0;
                                while (i < 4)
                                {
                                    string string1 = Convert.ToString(paquete[data_position + i]);
                                    string string2 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                                    string2 = AddZeros(string2);
                                    AirborneGoundVector = String.Concat(AirborneGoundVector, string2);
                                    i = i + 1;
                                }

                                data_position = data_position + 4;

                                Calculate_AirborneGroundVector(AirborneGoundVector);
                            }

                            if (Char.ToString(FSPEC_fake[29]) == "1") // 27 I021/165 Track Angle Rate
                            {
                                i = 0;
                                while (i < 2)
                                {
                                    string string1 = Convert.ToString(paquete[data_position + i]);
                                    string string2 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                                    string2 = AddZeros(string2);
                                    TrackAngleRate = String.Concat(TrackAngleRate, string2);
                                    i = i + 1;
                                }

                                data_position = data_position + 2;

                                string str1 = TrackAngleRate.Substring(6, 10);
                                TrackAngleRate_degs = (Calculate_ComplementoA2(str1)) / 32;
                            }

                            if (Char.ToString(FSPEC_fake[30]) == "1") // 28 I021/077 Time of Report Transmission 
                            {
                                i = 0;
                                while (i < 3)
                                {
                                    string string1 = Convert.ToString(paquete[data_position + i]);
                                    string string2 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                                    string2 = AddZeros(string2);
                                    TimeofASTERIXReportTransmission = String.Concat(TimeofASTERIXReportTransmission, string2);
                                    i = i + 1;
                                }

                                data_position = data_position + 3;

                                double int1 = Convert.ToInt32(TimeofASTERIXReportTransmission, 2);
                                TimeofASTERIXReportTransmission_seconds = int1 / 128;
                            }

                            if (Char.ToString(FSPEC_fake[31]) == "1") // FX - Field extension indicator 
                            {

                            }// FX

                            if (FSPEC.Length > 32)
                            {
                                if (Char.ToString(FSPEC_fake[32]) == "1") // 29 I021/170 Target Identification
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
                                }

                                if (Char.ToString(FSPEC_fake[33]) == "1") // 30 I021/020 Emitter Category 
                                {

                                    i = 0;
                                    while (i < 1)
                                    {
                                        string string1 = Convert.ToString(paquete[data_position + i]);
                                        string string2 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                                        string2 = AddZeros(string2);
                                        EmitterCategory = String.Concat(EmitterCategory, string2);
                                        i = i + 1;
                                    }

                                    data_position = data_position + 1;

                                    Calculate_EmitterCategory(EmitterCategory);

                                }

                                if (Char.ToString(FSPEC_fake[34]) == "1") // 31 I021/220 Met Information
                                {
                                    // primero leemos el primer paquete y lo pasamos a binario

                                    string string_packet = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                                    string_packet = AddZeros(string_packet);

                                    if ((Convert.ToString(string_packet[7])) == "0") // si ultima posicion es un 0 guardamos el octeto y pasamos al siguiente
                                    {
                                        MetInformation = string_packet;
                                        data_position = data_position + 1;
                                    }
                                    if ((Convert.ToString(string_packet[7])) == "1") // si ultimo valor =1 hacemos un bucle que vaya concatenando todos los octetos acabados en 1
                                    {
                                        i = 0;
                                        data_position = data_position + 1;
                                        while ((Convert.ToString(string_packet[string_packet.Length - 1])) == "1" && string_packet.Length < 64)
                                        {
                                            string string_packet2 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                                            string_packet2 = AddZeros(string_packet2);
                                            string_packet = string.Concat(string_packet, string_packet2);
                                            data_position = data_position + 1;
                                            i = i + 1;
                                        }
                                    }

                                    MetInformation = string_packet;

                                    Calculate_MetInformation(MetInformation);

                                }

                                if (Char.ToString(FSPEC_fake[35]) == "1") // 32 I021/146 Selected Altitude  
                                {
                                    i = 0;
                                    while (i < 2)
                                    {
                                        string string1 = Convert.ToString(paquete[data_position + i]);
                                        string string2 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                                        string2 = AddZeros(string2);
                                        SelectedAltitude = String.Concat(SelectedAltitude, string2);
                                        i = i + 1;
                                    }

                                    data_position = data_position + 2;

                                    Calculate_SelectedAltitude(SelectedAltitude);
                                }

                                if (Char.ToString(FSPEC_fake[36]) == "1") // 33 I021/148 Final State Selected Altitude 
                                {
                                    i = 0;
                                    while (i < 2)
                                    {
                                        string string1 = Convert.ToString(paquete[data_position + i]);
                                        string string2 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                                        string2 = AddZeros(string2);
                                        FinalStateSelectedAltitude = String.Concat(FinalStateSelectedAltitude, string2);
                                        i = i + 1;
                                    }

                                    data_position = data_position + 2;

                                    Calculate_FinalStateSelectedAltitude(FinalStateSelectedAltitude);

                                }

                                if (Char.ToString(FSPEC_fake[37]) == "1") // 34 I021/110 Trajectory Intent
                                {


                                    string string_packet = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                                    string_packet = AddZeros(string_packet);

                                    if ((Convert.ToString(string_packet[7])) == "0") // si ultima posicion es un 0 guardamos el octeto y pasamos al siguiente
                                    {
                                        TrajectoryIntent = string_packet;
                                        data_position = data_position + 1;

                                    }

                                    if ((Convert.ToString(string_packet[7])) == "1") // si ultimo valor =1 hacemos un bucle que vaya concatenando todos los octetos acabados en 1
                                    {
                                        while (Convert.ToString(string_packet[string_packet.Length - 1]) == "1" && string_packet.Length < (18 * 8))
                                        {
                                            string string_packet2 = Convert.ToString(Convert.ToInt32(paquete[data_position + 1], 16), 2);
                                            string_packet2 = AddZeros(string_packet2);
                                            string_packet = string.Concat(string_packet, string_packet2);
                                            data_position = data_position + 1;
                                        }

                                    }

                                    TrajectoryIntent = string_packet;
                                    Calculate_TrajectoryIntent(TrajectoryIntent);

                                }

                                if (Char.ToString(FSPEC_fake[38]) == "1") // 35 I021/016 Service Management
                                {
                                    i = 0;
                                    while (i < 1)
                                    {
                                        string string1 = Convert.ToString(paquete[data_position + i]);
                                        string string2 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                                        string2 = AddZeros(string2);
                                        ServiceManagement = String.Concat(ServiceManagement, string2);
                                        i = i + 1;
                                    }

                                    data_position = data_position + 1;

                                    RP = Convert.ToInt32(ServiceManagement, 2) * 0.5;

                                }

                                if (Char.ToString(FSPEC_fake[39]) == "1") // FX - Field extension indicator 
                                {

                                }// FX

                                if (FSPEC.Length > 40)
                                {
                                    if (Char.ToString(FSPEC_fake[40]) == "1") // 36 I021/008 Aircraft Operational Status
                                    {
                                        i = 0;
                                        while (i < 1)
                                        {
                                            string string1 = Convert.ToString(paquete[data_position + i]);
                                            string string2 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                                            string2 = AddZeros(string2);
                                            AircraftOperationalStatus = String.Concat(AircraftOperationalStatus, string2);
                                            i = i + 1;
                                        }

                                        data_position = data_position + 1;

                                        Calculate_AircraftOperationalStatus(AircraftOperationalStatus);
                                    }

                                    if (Char.ToString(FSPEC_fake[41]) == "1") // 37 I021/271 Surface Capabilities and Characteristics
                                    {

                                        // primero leemos el primer paquete y lo pasamos a binario

                                        string string_packet = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                                        string_packet = AddZeros(string_packet);

                                        if ((Convert.ToString(string_packet[7])) == "0") // si ultima posicion es un 0 guardamos el octeto y pasamos al siguiente
                                        {
                                            SurfaceCapabilitiesandCharacteristicas = string_packet;
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

                                        SurfaceCapabilitiesandCharacteristicas = string_packet;

                                        Calculate_SurfaceCapabilitiesandCharacterístics(SurfaceCapabilitiesandCharacteristicas);

                                    }

                                    if (Char.ToString(FSPEC_fake[42]) == "1") // 38 I021/132 Message Amplitude 
                                    {
                                        i = 0;
                                        while (i < 1)
                                        {
                                            string string1 = Convert.ToString(paquete[data_position + i]);
                                            string string2 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                                            string2 = AddZeros(string2);
                                            MessageAmplitude = String.Concat(MessageAmplitude, string2);
                                            i = i + 1;
                                        }

                                        data_position = data_position + 1;

                                        MessageAmplitude_dBm = Calculate_ComplementoA2(MessageAmplitude);
                                    }

                                    if (Char.ToString(FSPEC_fake[43]) == "1") // 39 I021/250 Mode S MB Data
                                    {

                                        data_position = data_position + Calculate_ModeSMBData(paquete, data_position);

                                    }

                                    if (Char.ToString(FSPEC_fake[44]) == "1") // 40 I021/260 ACAS Resolution Advisory Report 
                                    {
                                        i = 0;
                                        while (i < 7)
                                        {
                                            string string1 = Convert.ToString(paquete[data_position + i]);
                                            string string2 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                                            string2 = AddZeros(string2);
                                            ACASResolutionAdvisoryReport = String.Concat(ACASResolutionAdvisoryReport, string2);
                                            i = i + 1;
                                        }

                                        data_position = data_position + 7;

                                        Calculate_ACASResolutionAdvisoryReport(ACASResolutionAdvisoryReport);
                                    }

                                    if (Char.ToString(FSPEC_fake[45]) == "1") // 41 I021/400 Receiver ID
                                    {
                                        i = 0;
                                        while (i < 1)
                                        {
                                            string string1 = Convert.ToString(paquete[data_position + i]);
                                            string string2 = Convert.ToString(Convert.ToInt32(string1, 16), 2);
                                            string2 = AddZeros(string2);
                                            ReceiverID = String.Concat(ReceiverID, string2);
                                            i = i + 1;
                                        }

                                        data_position = data_position + 1;

                                        Calculate_ReceiverID(ReceiverID);
                                    }

                                    if (Char.ToString(FSPEC_fake[46]) == "1") // 42 I021/295 Data Ages 
                                    {

                                        // primero leemos el primer paquete y lo pasamos a binario

                                        string string_packet = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                                        string_packet = AddZeros(string_packet);

                                        if ((Convert.ToString(string_packet[7])) == "0") // si ultima posicion es un 0 guardamos el octeto y pasamos al siguiente
                                        {
                                            DataAges = string_packet;
                                            data_position = data_position + 1;
                                        }
                                        if ((Convert.ToString(string_packet[7])) == "1") // si ultimo valor =1 hacemos un bucle que vaya concatenando todos los octetos acabados en 1
                                        {
                                            i = 0;
                                            data_position = data_position + 1;
                                            while (((Convert.ToString(string_packet[string_packet.Length - 1])) == "1") && (string_packet.Length < 32))
                                            {
                                                string string_packet2 = Convert.ToString(Convert.ToInt32(paquete[data_position], 16), 2);
                                                string_packet2 = AddZeros(string_packet2);
                                                string_packet = string.Concat(string_packet, string_packet2);
                                                data_position = data_position + 1;
                                                i = i + 1;
                                            }
                                        }


                                        DataAges = string_packet;
                                        data_position = Calculate_Data_Ages(DataAges, paquete, data_position);
                                    }

                                    if (Char.ToString(FSPEC_fake[47]) == "1") // FX - Field extension indicator 
                                    {

                                    }// FX
                                }
                            }
                        }
                    }
                }
            }

            // Pasamos a Geodesic
            if (this.PositioninWGS_HRcoordinates.Length > 0)
            {
                coordGeodesic = new CoordinatesWGS84(this.latWGS84_HR * GeoUtils.DEGS2RADS, this.lonWGS84_HR * GeoUtils.DEGS2RADS, this.GeometricHeight_feet * GeoUtils.FEET2METERS);
            }
            else
            {
                coordGeodesic = new CoordinatesWGS84(this.latWGS84 * GeoUtils.DEGS2RADS, this.lonWGS84 * GeoUtils.DEGS2RADS, this.GeometricHeight_feet * GeoUtils.FEET2METERS);
            }

            // Calculamos coordenadas System Cartesian
            CoordinatesXYZ coordGeocentric = GeoUtils1.change_geodesic2geocentric(coordGeodesic);
            coordSystemCartesian = GeoUtils1.change_geocentric2system_cartesian(coordGeocentric);

            // Calculamos coordenadas Stereograficas
            coordStereographic = GeoUtils1.change_system_cartesian2stereographic(coordSystemCartesian);
        }

        ParsedMessage IMessage.parseData(IMessage message)
        {
            return new ParsedMessage(this);
        }
    }
}