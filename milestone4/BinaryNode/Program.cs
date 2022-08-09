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

string result;
result = "Preorder:      ";
foreach (BinaryNode<string> node in root.TraversePreorder())
{
    result += string.Format("{0} ", node.Value);
}
Console.WriteLine(result);

result = "Inorder:       ";
foreach (BinaryNode<string> node in root.TraverseInorder())
{
    result += string.Format("{0} ", node.Value);
}
Console.WriteLine(result);

result = "Postorder:     ";
foreach (BinaryNode<string> node in root.TraversePostorder())
{
    result += string.Format("{0} ", node.Value);
}
Console.WriteLine(result);

result = "BreadthFirst:  ";
foreach (BinaryNode<string> node in root.TraverseBreadthFirst())
{
    result += string.Format("{0} ", node.Value);
}
Console.WriteLine(result);