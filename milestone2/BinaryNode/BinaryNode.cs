using System.Runtime.CompilerServices;
using System.Text;

namespace BinaryNode
{
    public class BinaryNode<T>
    {
        public T Value { get; set; }
        public BinaryNode<T>? LeftChild { get; set; }
        public BinaryNode<T>? RightChild { get; set; }

        public BinaryNode(T value)
        {
            Value = value;
            LeftChild = null;
            RightChild = null;
        }

        public void AddLeft(BinaryNode<T> leftChild)
        {
            LeftChild = leftChild;
        }

        public void AddRight(BinaryNode<T> rightChild)
        {
            RightChild = rightChild;
        }

        public override string ToString()
        {
            return ToString("");
        }

        public string ToString(string spacing)
        {
            var rootString = new StringBuilder(spacing + Value!.ToString() + ":");
            spacing = spacing + "  ";
            if (LeftChild != null && LeftChild.Value != null)
            {
                if (RightChild != null && RightChild.Value != null)
                {
                    rootString.Append(Environment.NewLine);
                    rootString.Append(LeftChild.ToString(spacing));
                    rootString.Append(Environment.NewLine);
                    rootString.Append(RightChild.ToString(spacing));
                }
                else
                {
                    rootString.Append(Environment.NewLine);
                    rootString.Append(LeftChild.ToString(spacing));
                    rootString.Append(Environment.NewLine);
                    rootString.Append(spacing + "None");
                }
            }
            else
            {
                if (RightChild != null && RightChild.Value != null)
                {
                    rootString.Append(Environment.NewLine);
                    rootString.Append(spacing + "None");
                    rootString.Append(Environment.NewLine);
                    rootString.Append(RightChild.ToString(spacing));
                }
            }
            return rootString.ToString();
        }
    }
}