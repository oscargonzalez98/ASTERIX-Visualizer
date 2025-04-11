using DataModel;
using DataModelLibrary;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DataManagementLibrary
{
    public class AsterixDataLoader : IDataLoader<List<string[]>>
    {

        public AsterixDataLoader()
        { 
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
