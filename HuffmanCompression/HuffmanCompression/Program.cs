namespace HuffmanCompression
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CharacterFrequencyCounter counter = new CharacterFrequencyCounter();
            Decompression decompression = new Decompression();
            HuffmanTree huffmanTree = new HuffmanTree();

            Dictionary<char, int> frequencyTable = counter.Counter();
            var rootNode = huffmanTree.BuildHuffmanTree(frequencyTable);
            var huffmanCodes = huffmanTree.GenerateHuffmanCodes(rootNode);
            string encodedText = huffmanTree.CompressedText(huffmanCodes);
            huffmanTree.WriteCompressedFile(encodedText, frequencyTable);
            decompression.DecompressFile(frequencyTable);
        }
    }
}
