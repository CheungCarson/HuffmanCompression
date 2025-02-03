using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanCompression
{
    public class Node
    {
        public char Character {  get; set; }
        public int Frequency { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(char character, int frequency)
        {
            Character = character;
            Frequency = frequency;
        }

        public int CompareTo(Node other)
        {
            return Frequency.CompareTo(other.Frequency);
        }
    }
}
