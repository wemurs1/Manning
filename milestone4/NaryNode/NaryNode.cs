using System.Text;

namespace NaryNode
{
    public class NaryNode<T>
    {
        public T? Value { get; set; }
        public List<NaryNode<T>> Children { get; set; }

        public NaryNode(T value)
        {
            Value = value;
            Children = new List<NaryNode<T>>();
        }

        public void AddChild(NaryNode<T> child)
        {
            Children.Add(child);
        }

        public override string ToString()
        {
            return ToString("");
        }

        public string ToString(string spacing)
        {
            var naryString = new StringBuilder(spacing + Value!.ToString() + ":");
            spacing = spacing + "  ";
            foreach (var child in Children)
            {
                naryString.Append(Environment.NewLine);
                naryString.Append(spacing + child.ToString(spacing));
            }

            return naryString.ToString();
        }

        public NaryNode<T>? FindNode(T value)
        {
            if (Value!.Equals(value)) return this;
            foreach (var child in Children)
            {
                var result = child.FindNode(value);
                if (result != null) return result;
            }
            return null;
        }

        public List<NaryNode<T>> TraversePreorder()
        {
            List<NaryNode<T>> list = new List<NaryNode<T>>();

            list.Add(this);
            foreach (var child in Children)
            {
                list.AddRange(child.TraversePreorder());
            }

            return list;
        }

        public List<NaryNode<T>> TraversePostorder()
        {
            List<NaryNode<T>> list = new List<NaryNode<T>>();

            foreach (var child in Children)
            {
                list.AddRange(child.TraversePostorder());
            }
            list.Add(this);

            return list;
        }

        public List<NaryNode<T>> TraverseBreadthFirst()
        {
            List<NaryNode<T>> list = new List<NaryNode<T>>();
            Queue<NaryNode<T>> workQueue = new Queue<NaryNode<T>>();

            workQueue.Enqueue(this);

            while (workQueue.Count() != 0)
            {
                var node = workQueue.Dequeue();
                list.Add(node);
                foreach (var child in node.Children)
                {
                    workQueue.Enqueue(child);
                }
            }

            return list;
        }
    }
}