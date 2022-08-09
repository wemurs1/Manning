using System.Runtime.CompilerServices;

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
            var rootString = Value!.ToString();
            var leftString = "null";
            var rightString = "null";
            if (LeftChild != null && LeftChild.Value != null) leftString = LeftChild.Value.ToString();
            if (RightChild != null && RightChild.Value != null) rightString = RightChild.Value.ToString();
            return $"{rootString}: {leftString} {rightString}";
        }
    }
}