using NaryNode;

Console.WriteLine("Build Test Tree");
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

Console.WriteLine("Tree Output");
Console.WriteLine(root.ToString());
Console.WriteLine(a.ToString());
Console.WriteLine(b.ToString());
Console.WriteLine(c.ToString());
Console.WriteLine(d.ToString());
Console.WriteLine(e.ToString());
Console.WriteLine(f.ToString());
Console.WriteLine(g.ToString());
Console.WriteLine(h.ToString());
Console.WriteLine(i.ToString());