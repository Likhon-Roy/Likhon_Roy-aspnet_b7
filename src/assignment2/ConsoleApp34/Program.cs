int n = int.Parse(Console.ReadLine());

var ar = new int[n];

int m = 0;

long s = 0;

var x = Console.ReadLine().Split(' ');

for (int i = 0; i < n; i++)
{
    ar[i] = int.Parse(x[i]);

    m = Math.Max(ar[i], m);
}
for (int i = 0; i < n; i++)
{
    var a = m - ar[i];
    s = s + a;
}
Console.WriteLine(s);