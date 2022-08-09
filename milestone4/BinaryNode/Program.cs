// See https://aka.ms/new-console-template for more information
using BinaryNode;

var root = new BinaryNode<string>("Root");
var a = new BinaryNode<string>("A");
var b = new BinaryNode<string>("B");
var c = new BinaryNode<string>("C");
var d = new BinaryNode<string>("D");
var e = new BinaryNode<string>("E");
var f = new BinaryNode<string>("F");

root.AddLeft(a);
root.AddRight(b);
a.AddLeft(c);
a.AddRight(d);
b.AddRight(e);
e.AddLeft(f);

// Find some values.
FindValue(root, "Root");
FindValue(root, "E");
FindValue(root, "F");
FindValue(root, "Q");

// Find F in the B subtree.
FindValue(b, "F");

void FindValue(BinaryNode<string> node, string value)
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