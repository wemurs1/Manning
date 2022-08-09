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

string result;
result = "Preorder:      ";
foreach (NaryNode<string> node in root.TraversePreorder())
{
    result += string.Format("{0} ", node.Value);
}
Console.WriteLine(result);

result = "Postorder:     ";
foreach (NaryNode<string> node in root.TraversePostorder())
{
    result += string.Format("{0} ", node.Value);
}
Console.WriteLine(result);

result = "BreadthFirst:  ";
foreach (NaryNode<string> node in root.TraverseBreadthFirst())
{
    result += string.Format("{0} ", node.Value);
}
Console.WriteLine(result);