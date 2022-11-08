using System.Collections.Generic;

namespace Huffman
{
    internal class Knoten : HuffmanBaum
    {
        public char Symbol { get; set; }
        public int Frequenz { get; set; }
        public Knoten Right { get; set; }
        public Knoten Left { get; set; }
        private bool IstBlatt()
        {
            return (this.Left == null && this.Right == null);
        }

        public List<bool> Traverse(char symbol, List<bool> data)
        {
            // Leaf if (Right == null && Left == null)
            if (IstBlatt())
            {
                if (symbol.Equals(this.Symbol))
                {
                    return data;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                List<bool> left = null;
                List<bool> right = null;

                if (Left != null)
                {
                    List<bool> leftPath = new List<bool>();
                    leftPath.AddRange(data);
                    leftPath.Add(false);

                    left = Left.Traverse(symbol, leftPath);
                }

                if (Right != null)
                {
                    List<bool> rightPath = new List<bool>();
                    rightPath.AddRange(data);
                    rightPath.Add(true);
                    right = Right.Traverse(symbol, rightPath);
                }

                if (left != null)
                {
                    return left;
                }
                else
                {
                    return right;
                }
            }
        }
    }
}
