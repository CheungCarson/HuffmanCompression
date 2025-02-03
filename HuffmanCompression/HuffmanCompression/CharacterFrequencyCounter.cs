using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanCompression
{
    public class CharacterFrequencyCounter
    {
        public Dictionary<char, int> Counter()
        {
            var frequencyTable = new Dictionary<char, int>();

            try
            {
                using (FileStream reader = new FileStream("wap.txt", FileMode.Open))
                {
                    int byteRead;

                    while ((byteRead = reader.ReadByte()) != -1)
                    {
                        if (frequencyTable.ContainsKey((char)byteRead))
                        {
                            frequencyTable[(char)byteRead]++;
                        }
                        else
                        {
                            frequencyTable.Add((char)byteRead, 1);
                        }
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }

            //Sorts frequencyTable in descending order
            frequencyTable = frequencyTable.OrderByDescending(c => c.Value).ToDictionary();

            return frequencyTable;
        }
    }
}
