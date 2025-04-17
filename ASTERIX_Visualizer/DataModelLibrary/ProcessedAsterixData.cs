using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelLibrary
{
    public class ProcessedAsterixData
    {
        List<ParsedMessage> listParsedMessages;
        ConcurrentDictionary<string, List<ParsedMessage>> icaoGroups;
        ConcurrentDictionary<double, List<ParsedMessage>> smrGroups;
        ConcurrentBag<ParsedMessage> allSMR;
        ConcurrentBag<ParsedMessage> allADSB_MLAT;
        ConcurrentDictionary<string, List<ParsedMessage>> correlatedTracks;
        ConcurrentDictionary<string, List<ParsedMessage>> fusedAircraftTracks;

        public ProcessedAsterixData(List<ParsedMessage> listParsedMessages, ConcurrentDictionary<string, List<ParsedMessage>> icaoGroups, ConcurrentDictionary<double, List<ParsedMessage>> smrGroups, ConcurrentBag<ParsedMessage> allSMR, ConcurrentBag<ParsedMessage> allADSB_MLAT, ConcurrentDictionary<string, List<ParsedMessage>> correlatedTracks, ConcurrentDictionary<string, List<ParsedMessage>> fusedAircraftTracks) {
            this.listParsedMessages = listParsedMessages;
            this.icaoGroups = icaoGroups;
            this.smrGroups = smrGroups;
            this.allSMR = allSMR;
            this.allADSB_MLAT = allADSB_MLAT;
            this.correlatedTracks = correlatedTracks;
            this.fusedAircraftTracks = fusedAircraftTracks;
        }

        public ConcurrentBag<ParsedMessage> getMessagesBySecond(double s)
        {
            ConcurrentBag<ParsedMessage> messagesBySecond = new ConcurrentBag<ParsedMessage>();
            Parallel.ForEach(listParsedMessages, (message) =>
            {
                if (message.getTimestamp() >= s && message.getTimestamp() < s + 1)
                {
                    messagesBySecond.Add(message);
                }
            });

            return messagesBySecond;
        }

        public ConcurrentBag<ParsedMessage> getMessagesByRangeSecond(double lower_second, double higher_second)
        {
            ConcurrentBag<ParsedMessage> messagesByRangeSeconds = new ConcurrentBag<ParsedMessage>();
            Parallel.ForEach(listParsedMessages, (message) =>
            {
                if (message.getTimestamp() >= lower_second && message.getTimestamp() < higher_second + 1)
                {
                    messagesByRangeSeconds.Add(message);
                }
            });

            return messagesByRangeSeconds;
        }

        public double[] getStartAndFinishSeconds()
        {
            double start = listParsedMessages.Min(m => m.getTimestamp());
            double finish = listParsedMessages.Max(m => m.getTimestamp());
            return new double[] { start, finish }; // Fixed the invalid syntax
        }
    }
}
