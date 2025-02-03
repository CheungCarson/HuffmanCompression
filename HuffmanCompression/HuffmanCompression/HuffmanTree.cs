using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanCompression
{
    public class HuffmanTree
    {
        CharacterFrequencyCounter frequency = new CharacterFrequencyCounter();
        

        public Node BuildHuffmanTree(Dictionary<char, int> frequcnyTable)
        {
            var priorityQueue = new List<Node>();

            foreach (var set in frequcnyTable)
            {
                priorityQueue.Add(new Node(set.Key, set.Value));
            }

            while (priorityQueue.Count > 1)
            {
                var left = priorityQueue[priorityQueue.Count - 1];
                var right = priorityQueue[priorityQueue.Count - 2];
                priorityQueue.RemoveAt(priorityQueue.Count - 1);
                priorityQueue.RemoveAt(priorityQueue.Count - 1);

                var parent = new Node('\0',left.Frequency + right.Frequency)
                {
                    Left = left,
                    Right = right,
                };

                priorityQueue.Add(parent);
                priorityQueue.Sort((Node a, Node b) => b.Frequency.CompareTo(a.Frequency));
            }

            return priorityQueue[0];
        }

        public Dictionary<char, string> GenerateHuffmanCodes(Node root)
        {
            var huffmancodes = new Dictionary<char, string>();
            GenerateHuffmanCodesRecursive(root, "", huffmancodes);
            return huffmancodes;
        }

        public void GenerateHuffmanCodesRecursive(Node node, string code, Dictionary<char, string> huffmancodes)
        {
            if (node == null) return;

            if (node.Character != '\0')
            {
                huffmancodes[node.Character] = code;
            }

            GenerateHuffmanCodesRecursive(node.Left, code + "0", huffmancodes);
            GenerateHuffmanCodesRecursive(node.Right, code + "1", huffmancodes);
        }

        public string CompressedText(Dictionary<char, string> huffmancodes)
        {
            //Choose file to compress and decompress
            string file = "wap.txt";

            using (FileStream reader = new FileStream(file, FileMode.Open))
            {
                var encodedText = new StringBuilder();

                int byteRead;
                while ((byteRead = reader.ReadByte()) != -1)
                {
                    char c = (char)byteRead;
                    encodedText.Append(huffmancodes[c]);
                }

                return encodedText.ToString();

            }
        }

        public void WriteCompressedFile(string encodedText, Dictionary<char, int> frequencyTable)
        {
            using (var fileStream = new FileStream("Compressed.txt", FileMode.Create))
            using (var writer = new BinaryWriter(fileStream))
            {
                int paddingLength = (8 - (encodedText.Length % 8)) % 8;
                string paddedEncodedText = encodedText + new string('0', paddingLength);


                //convert from binary code to decimal
                var decimalValues = new List<byte>();
                for (int i = 0; i < encodedText.Length; i += 8)
                {
                    string chunk = paddedEncodedText.Substring(i, 8);

                    // Convert binary chunk to decimal
                    decimalValues.Add(Convert.ToByte(chunk, 2)); 
                }

                writer.Write((byte)paddingLength);

                // Write decimal values
                foreach (byte value in decimalValues)
                {
                    writer.Write(value);
                }

            }
        }
    }
}
