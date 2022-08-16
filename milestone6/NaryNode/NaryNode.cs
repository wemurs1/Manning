using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;

namespace NaryNode
{
    public class NaryNode<T>
    {
        public T Value { get; set; }
        public List<NaryNode<T>> Children;

        // New constants and properties go here...
        private const double BOX_WIDTH = 80;
        private const double BOX_HEIGHT = 40;
        private const double BOX_HALF_WIDTH = BOX_WIDTH / 2;
        private const double BOX_HALF_HEIGHT = BOX_HEIGHT / 2;
        private const int X_SPACING = 20;
        private const int Y_SPACING = 20;
        private const int LEAF_OFFSET = 40;
        private const int LINK_OFFSET = 10;

        private Point center;
        private Rect subtreeBounds;

        public Point Center { get { return center; } }
        public Rect SubtreeBounds { get { return subtreeBounds; } }

        public NaryNode(T value)
        {
            Value = value;
            Children = new List<NaryNode<T>>();
        }

        public void AddChild(NaryNode<T> child)
        {
            Children.Add(child);
        }

        // Return an indented string representation of the node and its children.
        public override string ToString()
        {
            return ToString("");
        }

        // Recursively create a string representation of this node's subtree.
        // Display this value indented, followed its children indented one more level.
        // End in a newline.
        public string ToString(string spaces)
        {
            // Create a string named result that initially holds the
            // current node's value followed by a new line.
            string result = string.Format("{0}{1}:\n", spaces, Value);

            // Add the children representations
            // indented by one more level.
            foreach (NaryNode<T> child in Children)
                result += child.ToString(spaces + "  ");
            return result;
        }

        // Recursively search this node's subtree looking for the target value.
        // Return the node that contains the value or null.
        public NaryNode<T>? FindNode(T target)
        {
            // See if this node contains the value.
            if (Value!.Equals(target!)) return this;

            // Search the child subtrees.
            foreach (NaryNode<T> child in Children)
            {
                NaryNode<T>? result = child.FindNode(target);
                if (result != null) return result;
            }

            // We did not find the value. Return null.
            return null;
        }

        public List<NaryNode<T>> TraversePreorder()
        {
            List<NaryNode<T>> result = new List<NaryNode<T>>();

            // Add this node to the traversal.
            result.Add(this);

            // Add the children.
            foreach (NaryNode<T> child in Children)
            {
                result.AddRange(child.TraversePreorder());
            }

            return result;
        }

        public List<NaryNode<T>> TraversePostorder()
        {
            List<NaryNode<T>> result = new List<NaryNode<T>>();

            // Add the children.
            foreach (NaryNode<T> child in Children)
            {
                result.AddRange(child.TraversePreorder());
            }

            // Add this node to the traversal.
            result.Add(this);
            return result;
        }

        public List<NaryNode<T>> TraverseBreadthFirst()
        {
            List<NaryNode<T>> result = new List<NaryNode<T>>();
            Queue<NaryNode<T>> queue = new Queue<NaryNode<T>>();

            // Start with the top node in the queue.
            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                // Remove the top node from the queue and
                // add it to the result list.
                NaryNode<T> node = queue.Dequeue();
                result.Add(node);

                // Add the node's children to the queue.
                foreach (NaryNode<T> child in node.Children)
                    queue.Enqueue(child);
            }

            return result;
        }

        // New code goes here...
        private void ArrangeSubtree(double xmin, double ymin)
        {
            // Calculate cy, the Y coordinate for this node.
            // This doesn't depend on the children.
            subtreeBounds.Y = ymin;
            center.Y = ymin + BOX_HALF_HEIGHT;

            // If the node has no children, just place it here and return.
            if (Children.Count == 0)
            {
                subtreeBounds.X = xmin;
                center.X = xmin + BOX_HALF_WIDTH;
                subtreeBounds.Width = BOX_WIDTH;
                subtreeBounds.Height = BOX_HEIGHT;
                return;
            }

            // Set child_xmin and child_ymin to the
            // start position for child subtrees.
            double childXmin = xmin;
            double childYmin = ymin + BOX_HEIGHT + Y_SPACING;
            Point leafStart = new Point();

            // Position the child subtrees.
            leafStart.Y = childYmin;
            leafStart.X = childXmin + LEAF_OFFSET;
            int widthOffset = 0;
            foreach (var node in Children)
            {
                if (node.IsLeaf())
                {
                    node.ArrangeSubtree(leafStart.X, leafStart.Y);
                    leafStart.Y += BOX_HEIGHT + Y_SPACING;
                    widthOffset = LEAF_OFFSET;
                }
                else
                {
                    // Arrange the child subtrees and update
                    // child_xmin to allow room for its subtree.
                    node.ArrangeSubtree(childXmin, childYmin);
                    childXmin = childXmin + node.SubtreeBounds.Width + X_SPACING;
                }
            }

            // Arrange this node depending on the number of children.
            // Two children. Center this node over the child nodes.
            // Use the subtree bounds to set our subtree bounds.
            subtreeBounds.X = xmin;
            subtreeBounds.Width = 
                Children[Children.Count - 1].SubtreeBounds.X - 
                Children[0].SubtreeBounds.X + 
                Children[Children.Count - 1].SubtreeBounds.Width + 
                widthOffset;
            center.X = xmin + (subtreeBounds.Width / 2);
            subtreeBounds.Height = Children[0].SubtreeBounds.Height;
            foreach (var node in Children)
            {
                if (node.SubtreeBounds.Height > subtreeBounds.Height) subtreeBounds.Height = node.SubtreeBounds.Height;
            }
            subtreeBounds.Height += BOX_HEIGHT + Y_SPACING;
        }

        private void DrawSubtreeLinks(Canvas canvas)
        {
            // Draw the subtree's links.
            foreach (var node in Children)
            {
                if (IsTwig())
                {
                    var linkX = Center.X - BOX_HALF_WIDTH + LINK_OFFSET;
                    Point linkPositionParent = new Point(linkX, Center.Y + LINK_OFFSET);
                    Point linkPositionChild = new Point(linkX, node.Center.Y);
                    Point childEdge = new Point(node.Center.X - BOX_HALF_WIDTH, node.Center.Y);
                    canvas.DrawLine(linkPositionParent, linkPositionChild, Brushes.Black, 1);
                    canvas.DrawLine(linkPositionChild, childEdge, Brushes.Black, 1);
                }
                else
                {
                    var halfMark = Center.Y + ((node.Center.Y - Center.Y) / 2);
                    Point halfwayPointParent = new Point(Center.X, halfMark);
                    canvas.DrawLine(Center, halfwayPointParent, Brushes.Black, 1);
                    Point halfwayPointChild = new Point(node.Center.X, halfMark);
                    canvas.DrawLine(node.Center, halfwayPointChild, Brushes.Black, 1);
                    canvas.DrawLine(halfwayPointParent, halfwayPointChild, Brushes.Black, 1);
                    node.DrawSubtreeLinks(canvas);
                }
            }

            // Outline the subtree for debugging.
            // canvas.DrawRectangle(SubtreeBounds, null, Brushes.Red, 1);
        }

        private void DrawSubtreeNodes(Canvas canvas)
        {
            // Draw the node.
            Rect nodeBounds = new Rect();
            nodeBounds.X = Center.X - BOX_HALF_WIDTH;
            nodeBounds.Y = Center.Y - BOX_HALF_HEIGHT;
            nodeBounds.Width = BOX_WIDTH;
            nodeBounds.Height = BOX_HEIGHT;
            var backgroundBrush = IsLeaf() ? Brushes.White : Brushes.Pink;
            canvas.DrawRectangle(nodeBounds, backgroundBrush, Brushes.Black, 1);
            canvas.DrawLabel(
                nodeBounds,
                Value != null ? Value.ToString()! : "",
                null,
                Brushes.Black,
                HorizontalAlignment.Center,
                VerticalAlignment.Center,
                12,
                0);


            // Draw the descendants' nodes.
            foreach (var node in Children)
            {
                node.DrawSubtreeNodes(canvas);
            }
        }

        public void ArrangeAndDrawSubtree(Canvas canvas, double xmin, double ymin)
        {
            ArrangeSubtree(xmin, ymin);
            DrawSubtreeLinks(canvas);
            DrawSubtreeNodes(canvas);
        }

        public bool IsLeaf()
        {
            return Children.Count == 0;
        }

        public bool IsTwig()
        {
            foreach (var node in Children)
            {
                if (!node.IsLeaf()) return false;
            };

            return !IsLeaf();
        }
    }
}
