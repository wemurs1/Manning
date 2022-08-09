using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        public BinaryNode<T>? FindNode(T value)
        {
            if (Value!.Equals(value)) return this;
            var leftChildResult = LeftChild != null ? LeftChild.FindNode(value) : null;
            if (leftChildResult != null) return leftChildResult;
            var rightChildResult = RightChild != null ? RightChild.FindNode(value) : null;
            if (rightChildResult != null) return rightChildResult;
            return null;
        }

        public List<BinaryNode<T>> TraversePreorder()
        {
            List<BinaryNode<T>> list = new List<BinaryNode<T>>();

            list.Add(this);
            if (LeftChild != null) list.AddRange(LeftChild.TraversePreorder());
            if (RightChild != null) list.AddRange(RightChild.TraversePreorder());

            return list;
        }

        public List<BinaryNode<T>> TraverseInorder()
        {
            List<BinaryNode<T>> list = new List<BinaryNode<T>>();

            if (LeftChild != null) list.AddRange(LeftChild.TraversePreorder());
            list.Add(this);
            if (RightChild != null) list.AddRange(RightChild.TraversePreorder());

            return list;
        }

        public List<BinaryNode<T>> TraversePostorder()
        {
            List<BinaryNode<T>> list = new List<BinaryNode<T>>();

            if (LeftChild != null) list.AddRange(LeftChild.TraversePostorder());
            if (RightChild != null) list.AddRange(RightChild.TraversePostorder());
            list.Add(this);

            return list;
        }

        public List<BinaryNode<T>> TraverseBreadthFirst()
        {
            List<BinaryNode<T>> list = new List<BinaryNode<T>>();
            Queue<BinaryNode<T>> workQueue = new Queue<BinaryNode<T>>();

            workQueue.Enqueue(this);

            while (workQueue.Count() != 0)
            {
                var node = workQueue.Dequeue();
                list.Add(node);
                if (node.LeftChild != null) workQueue.Enqueue(node.LeftChild);
                if (node.RightChild != null) workQueue.Enqueue(node.RightChild);
            }

            return list;
        }
    }
}