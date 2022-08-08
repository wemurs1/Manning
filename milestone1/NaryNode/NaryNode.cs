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
            StringBuilder naryString = new StringBuilder();
            naryString.Append(Value!.ToString() + ":");
            foreach (var child in Children)
            {
                naryString.Append(" " + child.Value!.ToString());
            }

            return naryString.ToString();
        }
    }
}