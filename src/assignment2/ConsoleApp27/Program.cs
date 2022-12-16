using System.Text;

var x = Console.ReadLine();

var y = Console.ReadLine();

StringBuilder reverse = new StringBuilder("");

for (int i = x.Length - 1; i >= 0; i--)
{
    reverse.Append(x[i]);
}

x = reverse.ToString();

if (x == y)
{
    Console.WriteLine("YES");
}
else
{
    Console.WriteLine("NO");
}