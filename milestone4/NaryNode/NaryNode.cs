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
    }
}