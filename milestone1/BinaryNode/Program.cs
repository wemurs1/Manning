// See https://aka.ms/new-console-template for more information
using BinaryNode;

Console.WriteLine("Build Test Tree");
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

Console.WriteLine("Tree Output");
Console.WriteLine(root.ToString());
Console.WriteLine(a.ToString());
Console.WriteLine(b.ToString());
Console.WriteLine(c.ToString());
Console.WriteLine(d.ToString());
Console.WriteLine(e.ToString());
Console.WriteLine(f.ToString());