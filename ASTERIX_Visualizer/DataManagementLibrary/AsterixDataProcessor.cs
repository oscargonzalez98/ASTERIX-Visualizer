using DataModelLibrary;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interop;

namespace DataManagementLibrary
{
    public class AsterixDataProcessor
    {
        List<ParsedMessage> listParsedMessages = new List<ParsedMessage>();
        ConcurrentDictionary<string, List<ParsedMessage>> icaoGroups = new ConcurrentDictionary<string, List<ParsedMessage>>();
        ConcurrentDictionary<double, List<ParsedMessage>> smrGroups = new ConcurrentDictionary<double, List<ParsedMessage>>();
        ConcurrentBag<ParsedMessage> allSMR = new ConcurrentBag<ParsedMessage>();
        ConcurrentBag<ParsedMessage> allADSB_MLAT = new ConcurrentBag<ParsedMessage>();
        ConcurrentDictionary<string, List<ParsedMessage>> correlatedTracks;
        ConcurrentDictionary<string, List<ParsedMessage>> fusedAircraftTracks;

        public AsterixDataProcessor() { }

        public ProcessedAsterixData processData(List<string[]> asterixMessages)
        {

            // 1. PROCESS AND PARSE ASTERIX MESSAGES, GROUP MESSAGES BY ICAO24 AND SMR TRACK NUMBER

            processAndParseASTERIXMessages(asterixMessages);

            // 2. CORRELATE ADS-B & MLAT MESSAGES WITH SMR TRACKS

            CorrelateTrajectories(icaoGroups, smrGroups, 50.0);

            int i = 0;

            /*


Parallel.ForEach(allSMR, smr =>
{
    Parallel.ForEach(allADSB_MLAT, adsb =>
    {
        if (Math.Abs((smr.getTimestamp() - adsb.getTimestamp())) < 1.5)
        {
            double distance = Haversine(smr.getLat(), smr.getLon(), adsb.getLat(), adsb.getLon());
            if (distance < 50) // meters
            {
                correlatedTracks.TryAdd(smr.getTrackNumber(), adsb.getICAOAddress());
            }
        }
    });
});



fusedAircraftTracks = new ConcurrentDictionary<string, List<ParsedMessage>>();

// Step 1: Copy over ADS-B and MLAT groups
foreach (var kvp in icaoGroups)
{
    fusedAircraftTracks[kvp.Key] = new List<ParsedMessage>(kvp.Value);
}

// Step 2: Add correlated SMR messages to the correct aircraft
foreach (var smr in allSMR)
{
    if (smr.getTrackNumber() != null &&
        correlatedTracks.TryGetValue(smr.getTrackNumber(), out string icao))
    {
        var list = fusedAircraftTracks.GetOrAdd(icao, _ => new List<ParsedMessage>());
        list.Add(smr);
    }
}

// Step 3: Sort each aircraft's messages by timestamp
foreach (var kvp in fusedAircraftTracks)
{
    kvp.Value.Sort((a, b) => a.getTimestamp().CompareTo(b.getTimestamp()));
}
*/
            return new ProcessedAsterixData(listParsedMessages, icaoGroups, smrGroups, allSMR, allADSB_MLAT, correlatedTracks, fusedAircraftTracks);
        }



        private void processAndParseASTERIXMessages(List<string[]> asterixMessages)
        {
            Parallel.ForEach(asterixMessages, message =>
            {
                IMessage processedMessage = processMessage(message);

                if (processedMessage != null)
                {
                    ParsedMessage parsedMessage = processedMessage.parseData(processedMessage);
                    listParsedMessages.Add(parsedMessage);

                    if (parsedMessage.getSource() == "ADS-B" || parsedMessage.getSource() == "MLAT")
                    {
                        if (!string.IsNullOrWhiteSpace(parsedMessage.getICAOAddress()))
                        {
                            var list = icaoGroups.GetOrAdd(parsedMessage.getICAOAddress(), _ => new List<ParsedMessage>());
                            lock (list) { list.Add(parsedMessage); }
                            allADSB_MLAT.Add(parsedMessage);
                        }
                    }
                    else if (parsedMessage.getSource() == "SMR" && parsedMessage.getTrackNumber() != null)
                    {
                        var list = smrGroups.GetOrAdd(parsedMessage.getTrackNumber(), _ => new List<ParsedMessage>());
                        lock (list) { list.Add(parsedMessage); }
                        allSMR.Add(parsedMessage);
                    }
                }
            });
        }

        public void CorrelateTrajectories(
                        ConcurrentDictionary<string, List<ParsedMessage>> adsbTrajectories,
                        ConcurrentDictionary<double, List<ParsedMessage>> smrTrajectories,
                        double thresholdMeters = 50.0)
        {
            correlatedTracks = new ConcurrentDictionary<string, List<ParsedMessage>>();

            Parallel.ForEach(adsbTrajectories, adsbTrajectory =>
            {
                var localMatches = new List<ParsedMessage>();

                foreach (var smrTrajectory in smrTrajectories)
                {
                    if (!HasTimeOverlap(adsbTrajectory.Value, smrTrajectory.Value))
                    {
                        double minAvgDistance = double.MaxValue;
                        int adsbCount = adsbTrajectory.Value.Count;
                        int smrCount = smrTrajectory.Value.Count;

                        for (int shift = 0; shift < adsbCount; shift++)
                        {
                            var adsbSub = adsbTrajectory.Value.Skip(shift).ToList();
                            int compareCount = Math.Min(adsbSub.Count, smrCount);
                            if (compareCount < 5) break;

                            double totalDistance = 0;
                            bool earlyExit = false;

                            for (int i = 0; i < compareCount; i++)
                            {
                                List<ParsedMessage> positions = smrTrajectory.Value;
                                double dist = HaversineDistance(adsbSub[i].getLat(), adsbSub[i].getLon(), positions[i].getLat(), positions[i].getLon());
                                totalDistance += dist;

                                // Early exit if getting too far
                                if ((totalDistance / (i + 1)) > thresholdMeters * 2)
                                {
                                    earlyExit = true;
                                    break;
                                }
                            }

                            if (earlyExit) continue;

                            double avgDistance = totalDistance / compareCount;
                            if (avgDistance < minAvgDistance)
                                minAvgDistance = avgDistance;
                        }

                        if (minAvgDistance < thresholdMeters)
                        {
                            int trackNumber = (int)smrTrajectory.Key;
                            localMatches.AddRange(smrTrajectory.Value);
                        }
                    }
                }

                if (localMatches.Any()) {
                    correlatedTracks[adsbTrajectory.Key] = localMatches;
                }
            });
        }

        private static bool HasTimeOverlap(List<ParsedMessage> a, List<ParsedMessage> b)
        {
            var aStart = a.First().getTimestamp();
            var aEnd = a.Last().getTimestamp();
            var bStart = b.First().getTimestamp();
            var bEnd = b.Last().getTimestamp();

            return aStart <= bEnd && bStart <= aEnd;
        }

        private static double HaversineDistance(double lat1, double lon1, double lat2, double lon2)
        {
            double R = 6371000; // Earth radius in meters
            double dLat = ToRad(lat2 - lat1);
            double dLon = ToRad(lon2 - lon1);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ToRad(lat1)) * Math.Cos(ToRad(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        private static double ToRad(double angle) => angle * Math.PI / 180;



        private IMessage processMessage(string[] message)
        {
            int CAT = int.Parse(message[0], System.Globalization.NumberStyles.HexNumber);

            if (CAT == 10)
            {
                CAT10 newcat10 = new CAT10(message);
                newcat10.Calculate_FSPEC(newcat10.paquete);
                return newcat10;
            }


            else if (CAT == 21)
            {
                CAT21 newcat21 = new CAT21(message);
                newcat21.Calculate_FSPEC(newcat21.paquete);
                return newcat21;

                /*
                if (this.comboBox1.Text == "CAT 21 v2.1")
                {
                    CAT21 newcat21 = new CAT21(message);
                    newcat21.Calculate_FSPEC(newcat21.paquete);
                    listaCAT21.Add(newcat21);
                }


                else if (comboBox1.Text == "CAT 21 v0.23")
                {
                    CAT21v23 newcat21v23 = new CAT21v23(message);
                    newcat21v23.Calculate_FSPEC(newcat21v23.paquete);
                    listaCAT21v23.Add(newcat21v23);
                }
                */
            }
            return null;
        }

        public List<string[]> loadData(string filePath)
        {

            byte[] fileBytes = File.ReadAllBytes(filePath);
            List<byte[]> listabyte = new List<byte[]>();
            int i = 0;
            int contador = fileBytes[2];

            while (i < fileBytes.Length)
            {
                byte[] array = new byte[contador];
                for (int j = 0; j < array.Length; j++)
                {
                    array[j] = fileBytes[i];
                    i++;
                }
                listabyte.Add(array);
                if (i + 2 < fileBytes.Length)
                {
                    contador = fileBytes[i + 2];
                }
            }


            List<string[]> listahex = new List<string[]>();
            for (int x = 0; x < listabyte.Count; x++)
            {
                byte[] buffer = listabyte[x];
                string[] arrayhex = new string[buffer.Length];
                for (int y = 0; y < buffer.Length; y++)
                {
                    arrayhex[y] = buffer[y].ToString("X");
                    if (arrayhex[y].Length != 2)
                    {
                        arrayhex[y] = String.Concat("0", arrayhex[y]);
                    }
                }
                listahex.Add(arrayhex);
            }

            return listahex;
        }
    }
}
