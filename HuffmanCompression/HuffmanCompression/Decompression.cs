using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanCompression
{
    public class Decompression
    {

        public string DecodeText(string encodedText, Node root)
        {
            StringBuilder decodedText = new StringBuilder();
            Node currentNode = root;

            // Traverse the tree based on the bits in the encoded text
            foreach (char bit in encodedText)
            {
                //Checks wether to traverse left of right down the tree.
                //If the currentNode is a leaf node, append it.
                currentNode = bit == '0' ? currentNode.Left : currentNode.Right;
                if (currentNode.Character != '\0')
                {
                    // Append the decoded character
                    decodedText.Append(currentNode.Character);
                    currentNode = root; 
                }
            }
            return decodedText.ToString();
        }
        public void DecompressFile(Dictionary<char, int> frequencyTable)
        {
            HuffmanTree tree = new HuffmanTree();

            using (var fileStream = new FileStream("Compressed.txt", FileMode.Open))
            using (var reader = new BinaryReader(fileStream))
            {
                Node huffmanTree = tree.BuildHuffmanTree(frequencyTable);
                int paddingLength = reader.ReadByte();


                var bitStringBuilder = new StringBuilder();
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    byte value = reader.ReadByte();

                    // Convert decimal to binary
                    bitStringBuilder.Append(Convert.ToString(value, 2).PadLeft(8, '0'));  
                }

                //Remove Padding
                string bitString = bitStringBuilder.ToString();
                if(paddingLength > 0)
                {
                    bitString = bitString.Substring(0, bitString.Length - paddingLength);
                }

                // Decode the binary string using the Huffman tree
                string decodedText = DecodeText(bitString, huffmanTree);

                File.WriteAllText("Decompressed.txt", decodedText);
            }
        }
    }
}
