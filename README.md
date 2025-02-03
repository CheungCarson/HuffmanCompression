# HuffmanCompression
This program implements a Huffman Compression and Decompression for ASCII text files. This data compression algorithm assigns binary codes to characters based on their frequency. The most frequent characters are given the shortest codes.

## Compression
1. Reads ASCII text file and finds the frequency of each character.
2. Generates Huffman Tree based on character frequencies.
3. Generates Huffman codes using the Huffman Tree.
4. Converts the binary codes to decimal values.
5. Write to a new compressed file.

## Decompression
1. Reads compressed file.
2. Converts decimal values back to their binary codes.
3. It uses an identical Huffman Tree to read the binary codes and return the file to its original state.
