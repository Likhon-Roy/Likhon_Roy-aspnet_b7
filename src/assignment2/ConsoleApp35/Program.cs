
var x = Console.ReadLine().Split(' ');

ulong n = ulong.Parse(x[0]);
ulong k = ulong.Parse(x[1]);

var a = (n + 1) / 2;

if (k <= a)
{
    var m = k * 2;
    Console.WriteLine(m - 1);
}
else
{
    var o = k - (n + 1) / 2;
    Console.WriteLine(o * 2);
}