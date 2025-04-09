using DataModel;
using DataModelLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagementLibrary
{
    public class AsterixDataLoader : IDataLoader<List<string[]>>
    {
        string filePath = "";
        List<CAT10> listaCAT10 = new List<CAT10>();
        List<CAT20> listaCAT20 = new List<CAT20>();
        List<CAT21> listaCAT21 = new List<CAT21>();
        List<CAT21v23> listaCAT21v23 = new List<CAT21v23>();

        int SAC = 0;
        int SIC = 0;

        public AsterixDataLoader(string filePath)
        {
            this.filePath = filePath;   
        }

        public List<CAT10> getListCAT10()
        {
            return listaCAT10;
        }

        public List<CAT20> GetListCAT20()
        {
            return listaCAT20;
        }

        public List<CAT21> GetListCAT21()
        {
            return listaCAT21;
        }

        public List<CAT21v23> GetListCAT21v23()
        {
            return listaCAT21v23;
        }

        public string AddZeros(string octeto)
        {
            while (octeto.Length < 8)
            {
                octeto = octeto.Insert(0, "0");
            }
            return octeto;
        }

        public void Calculate_DataSourceIdentification(string paquete)
        {
            string string1 = paquete.Substring(0, 8);
            string string2 = paquete.Substring(8, 8);

            SAC = Convert.ToInt32(string1, 2);
            SIC = Convert.ToInt32(string2, 2);
        }
        
        public List<string[]> loadData(string path)
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
