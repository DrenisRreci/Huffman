using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Huffman
{
    internal class HuffmanBaum
    {
        public List<Knoten> nodes;
        public Knoten Wurzel { get; set; }
        private Dictionary<char, int> Frequenzen;
        public List<Knoten> GeordneteKnoten;

        public void printTree(Knoten current)
        {
            if(current.Left != null)
            {
                printTree(current.Left);
            }
            if (current.Right != null)
            {
                printTree(current.Right);
            }
            if(IstBlatt(current))
            {
                Console.WriteLine("Reached leaf: " + current.Symbol);
            }
        }

        public void ErstelleBaum(string source)
        {
            Frequenzen = new Dictionary<char, int>();
            nodes = new List<Knoten>();

            for (int i = 0; i < source.Length; i++)
            {
                if (!Frequenzen.ContainsKey(source[i]))
                {
                    Frequenzen.Add(source[i], 0);
                }

                // Den Frequenz des jeweiligen chars inkrementieren
                Frequenzen[source[i]]++;
            }

            foreach (KeyValuePair<char, int> symbol in Frequenzen)
            {
                nodes.Add(new Knoten() { Symbol = symbol.Key, Frequenz = symbol.Value });
            }

            GeordneteKnoten = nodes.OrderBy(node => node.Frequenz).ToList<Knoten>();

            while (nodes.Count > 1)
            {
                List<Knoten> orderedNodes = nodes.OrderBy(node => node.Frequenz).ToList<Knoten>();

                if (orderedNodes.Count >= 2)
                {
                    // Take first two items
                    List<Knoten> taken = orderedNodes.Take(2).ToList<Knoten>();

                    // Create a parent node by combining the frequencies
                    Knoten parent = new Knoten()
                    {
                        Symbol = '*',
                        Frequenz = taken[0].Frequenz + taken[1].Frequenz,
                        Left = taken[0],
                        Right = taken[1]
                    };

                    nodes.Remove(taken[0]);
                    nodes.Remove(taken[1]);
                    nodes.Add(parent);
                }

                Wurzel = nodes.FirstOrDefault();

            }

        }

        public BitArray Encode(string source)
        {
            List<bool> encodedSource = new List<bool>();

            for (int i = 0; i < source.Length; i++)
            {
                List<bool> encodedSymbol = Wurzel.Traverse(source[i], new List<bool>());
                encodedSource.AddRange(encodedSymbol);
            }

            BitArray bits = new BitArray(encodedSource.ToArray());

            return bits;
        }

        public string Decode(BitArray bits)
        {
            Knoten current = Wurzel;
            string decoded = "";

            foreach (bool bit in bits)
            {
                if (bit)
                {
                    if (current.Right != null)
                    {
                        current = current.Right;
                    }
                }
                else
                {
                    if (current.Left != null)
                    {
                        current = current.Left;
                    }
                }

                if (IstBlatt(current))
                {
                    decoded += current.Symbol;
                    current = Wurzel;
                }
            }

            return decoded;
        }

        private bool IstBlatt(Knoten node)
        {
            return (node.Left == null && node.Right == null);
        }
    }
}
