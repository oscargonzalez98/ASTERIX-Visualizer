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
        ConcurrentDictionary<double, string> correlatedTracks = new ConcurrentDictionary<double, string>();

        public AsterixDataProcessor() { }

        public ProcessedAsterixData processData(List<string[]> asterixMessages)
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

            return new ProcessedAsterixData(listParsedMessages, icaoGroups, smrGroups, allSMR, allADSB_MLAT, correlatedTracks);
        }



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
