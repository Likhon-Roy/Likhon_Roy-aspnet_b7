int n, m, i, j, s, c, d;

n = int.Parse(Console.ReadLine());

var a = new List<int>();

s = 0;

var x = Console.ReadLine().Split(' ');
for (i = 0; i < n; i++)
{
    a.Add(int.Parse(x[i]));
    s += int.Parse(x[i]);
}

s = s / 2;
a.Sort();
c = 0;
d = 0;

for (i = n - 1; i >= 0; i--)
{
    d += a[i];
    c++;
    if (d > s)
        break;
}

Console.WriteLine(c);