using NaryNode;

var root = new NaryNode<string>("Root");
var a = new NaryNode<string>("A");
var b = new NaryNode<string>("B");
var c = new NaryNode<string>("C");
var d = new NaryNode<string>("D");
var e = new NaryNode<string>("E");
var f = new NaryNode<string>("F");
var g = new NaryNode<string>("G");
var h = new NaryNode<string>("H");
var i = new NaryNode<string>("I");

root.AddChild(a);
root.AddChild(b);
root.AddChild(c);
a.AddChild(d);
a.AddChild(e);
d.AddChild(g);
c.AddChild(f);
f.AddChild(h);
f.AddChild(i);

// Find some values.
FindValue(root, "Root");
FindValue(root, "E");
FindValue(root, "F");
FindValue(root, "Q");

// Find F in the C subtree.
FindValue(c, "F");

void FindValue(NaryNode<string> node, string value)
{
    var result = node.FindNode(value);
    if (result != null)
    {
        Console.WriteLine($"Found {value}");
    }
    else
    {
        Console.WriteLine($"Value {value} not found");
    }
}