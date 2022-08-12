using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;
using System.Runtime.CompilerServices;
using System.Threading;

namespace BinaryNode
{
    public class BinaryNode<T>
    {
        public T Value { get; set; }
        public BinaryNode<T> LeftChild, RightChild;

        // New constants and properties go here...
        private const double NODE_RADIUS = 10;
        private const int X_SPACING = 20;
        private const int Y_SPACING = 20;
        public Point Center { get; private set; }
        public Rect SubtreeBounds { get; private set; }

        public BinaryNode(T value)
        {
            Value = value;
            LeftChild = null;
            RightChild = null;
        }

        public void AddLeft(BinaryNode<T> child)
        {
            LeftChild = child;
        }

        public void AddRight(BinaryNode<T> child)
        {
            RightChild = child;
        }

        // Return an indented string representation of the node and its children.
        public override string ToString()
        {
            return ToString("");
        }

        // Recursively create a string representation of this node's subtree.
        // Display this value indented, followed by the left and right
        // values indented one more level.
        // End in a newline.
        public string ToString(string spaces)
        {
            // Create a string named result that initially holds the
            // current node's value followed by a new line.
            string result = string.Format("{0}{1}:\n", spaces, Value);

            // See if the node has any children.
            if ((LeftChild != null) || (RightChild != null))
            {
                // Add null or the child's value.
                if (LeftChild == null)
                    result += string.Format("{0}{1}null\n", spaces, "  ");
                else
                    result += LeftChild.ToString(spaces + "  ");

                // Add null or the child's value.
                if (RightChild == null)
                    result += string.Format("{0}{1}null\n", spaces, "  ");
                else
                    result += RightChild.ToString(spaces + "  ");
            }
            return result;
        }

        // Recursively search this node's subtree looking for the target value.
        // Return the node that contains the value or null.
        public BinaryNode<T> FindNode(T target)
        {
            // See if this node contains the value.
            if (Value.Equals(target)) return this;

            // Search the left child subtree.
            BinaryNode<T> result = null;
            if (LeftChild != null)
                result = LeftChild.FindNode(target);
            if (result != null) return result;

            // Search the right child subtree.
            if (RightChild != null)
                result = RightChild.FindNode(target);
            if (result != null) return result;

            // We did not find the value. Return null.
            return null;
        }

        public List<BinaryNode<T>> TraversePreorder()
        {
            List<BinaryNode<T>> result = new List<BinaryNode<T>>();

            // Add this node to the traversal.
            result.Add(this);

            // Add the child subtrees.
            if (LeftChild != null) result.AddRange(LeftChild.TraversePreorder());
            if (RightChild != null) result.AddRange(RightChild.TraversePreorder());
            return result;
        }

        public List<BinaryNode<T>> TraverseInorder()
        {
            List<BinaryNode<T>> result = new List<BinaryNode<T>>();

            // Add the left subtree.
            if (LeftChild != null) result.AddRange(LeftChild.TraverseInorder());

            // Add this node.
            result.Add(this);

            // Add the right subtree.
            if (RightChild != null) result.AddRange(RightChild.TraverseInorder());
            return result;
        }

        public List<BinaryNode<T>> TraversePostorder()
        {
            List<BinaryNode<T>> result = new List<BinaryNode<T>>();

            // Add the child subtrees.
            if (LeftChild != null) result.AddRange(LeftChild.TraversePostorder());
            if (RightChild != null) result.AddRange(RightChild.TraversePostorder());

            // Add this node.
            result.Add(this);
            return result;
        }

        public List<BinaryNode<T>> TraverseBreadthFirst()
        {
            List<BinaryNode<T>> result = new List<BinaryNode<T>>();
            Queue<BinaryNode<T>> queue = new Queue<BinaryNode<T>>();

            // Start with the top node in the queue.
            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                // Remove the top node from the queue and
                // add it to the result list.
                BinaryNode<T> node = queue.Dequeue();
                result.Add(node);

                // Add the node's children to the queue.
                if (node.LeftChild != null) queue.Enqueue(node.LeftChild);
                if (node.RightChild != null) queue.Enqueue(node.RightChild);
            }

            return result;
        }

        // New code goes here...
        private void ArrangeSubtree(double xmin, double ymin)
        {
            // Calculate cy, the Y coordinate for this node.
            // This doesn't depend on the children.
            SubtreeBounds.Y = ymin;
            Center.Y = ymin + NODE_RADIUS;

            // If the node has no children, just place it here and return.
            if ((LeftChild == null) && (RightChild == null))
            {
                SubtreeBounds.X = xmin;
                SubtreeBounds.Width = 2 * NODE_RADIUS;
                SubtreeBounds.Height = 2 * NODE_RADIUS;
                Center.X = xmin + NODE_RADIUS;
                return;
            }

            // Set child_xmin and child_ymin to the
            // start position for child subtrees.
            double childXmin = xmin;
            double childYmin = ymin + 2 * NODE_RADIUS + Y_SPACING;

            // Position the child subtrees.
            if (LeftChild != null)
            {
                // Arrange the left child subtree and update
                // child_xmin to allow room for its subtree.
                LeftChild.ArrangeSubtree(childXmin, childYmin);
                childXmin = childXmin + LeftChild.SubtreeBounds.X;


                // If we also have a right child,
                // add space between their subtrees.
                if (RightChild != null) childXmin += X_SPACING;
            }

            if (RightChild != null)
            {
                // Arrange the right child subtree.
                RightChild.ArrangeSubtree(childXmin, childYmin);

            }

            // Arrange this node depending on the number of children.
            if ((LeftChild != null) && (RightChild != null))
            {
                // Two children. Center this node over the child nodes.
                // Use the subtree bounds to set our subtree bounds.
                Center.X = LeftChild.SubtreeBounds.X + LeftChild.SubtreeBounds.Width - xmin;
                SubtreeBounds.X = xmin;
                SubtreeBounds.Width = RightChild.SubtreeBounds.Y + RightChild.SubtreeBounds.Width - xmin;
                SubtreeBounds.Height =
                    max(SubtreeBounds.LeftChild.Height, SubtreeBounds.RightChild.Height) +
                    2 * NODE_RADIUS + X_SPACING;
            }
            else if (LeftChild != null)
            {
                // We have only a left child.
                Center.X = LeftChild.SubtreeBounds.X + LeftChild.SubtreeBounds.Width - xmin;
                SubtreeBounds.X = xmin;
                SubtreeBounds.Width = RightChild.SubtreeBounds.Y + RightChild.SubtreeBounds.Width - xmin;
                SubtreeBounds.Height =
                    max(SubtreeBounds.LeftChild.Height, SubtreeBounds.RightChild.Height) +
                    2 * NODE_RADIUS + X_SPACING;
            }
            else
            {
                // We have only a right child.
                // We have only a left child.
                Center.X = LeftChild.SubtreeBounds.X + LeftChild.SubtreeBounds.Width - xmin;
                SubtreeBounds.X = xmin;
                SubtreeBounds.Width = RightChild.SubtreeBounds.Y + RightChild.SubtreeBounds.Width - xmin;
                SubtreeBounds.Height =
                    max(SubtreeBounds.LeftChild.Height, SubtreeBounds.RightChild.Height) +
                    2 * NODE_RADIUS + X_SPACING;
            }

        }

        private void DrawSubtreeLinks(Canvas canvas)
        {
            // Draw the subtree's links.
            if (LeftChild != null)
            {
                canvas.DrawLine(Center, LeftChild.Center, Brushes.Black, 1);
                LeftChild.DrawSubtreeLinks(canvas);
            }

            if (RightChild != null)
            {
                canvas.DrawLine(Center, RightChild.Center, Brushes.Black, 1);
                RightChild.DrawSubtreeLinks(canvas);
            }

            // Outline the subtree for debugging.
            canvas.DrawRectangle(SubtreeBounds, null, Brushes.Red, 1);
        }

        private void DrawSubtreeNodes(Canvas canvas)
        {
            // Draw the node.
            Rect nodeBounds = new Rect();
            nodeBounds.X = Center.X;
            nodeBounds.Y = Center.Y;
            nodeBounds.Width = 2 * NODE_RADIUS;
            nodeBounds.Height = 2 * NODE_RADIUS;
            canvas.DrawEllipse(nodeBounds, null, Brushes.Black, 1);
            canvas.DrawLabel(
                nodeBounds,
                Value.ToString(),
                null,
                Brushes.Black,
                HorizontalAlignment.Center,
                VerticalAlignment.Center,
                12,
                0);


            // Draw the descendants' nodes.
            if (LeftChild != null) LeftChild.DrawSubtreeNodes(canvas);
            if (RightChild != null) RightChild.DrawSubtreeNodes(canvas);
        }

        public void ArrangeAndDrawSubtree(Canvas canvas, double xmin, double ymin)
        {
            ArrangeSubtree(xmin, ymin);
            DrawSubtreeLinks(canvas);
            DrawSubtreeNodes(canvas);
        }
    }
}
