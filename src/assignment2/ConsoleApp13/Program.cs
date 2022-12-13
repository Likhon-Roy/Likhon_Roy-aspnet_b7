int n, m;

var z = new List<int>();

var x = Console.ReadLine().Split(' ');

n = int.Parse(x[0]);
m = int.Parse(x[1]);

var y = Console.ReadLine().Split(' ');

for (int i = 0; i < m; ++i)
{
    z.Add(int.Parse(y[i]));
}

z.Sort();

int l = z[n - 1] - z[0];

for (int i = 1; i <= m - n; ++i)
{
    if (z[i + n - 1] - z[i] < l)
    {
        l = z[i + n - 1] - z[i];
    }
}

Console.WriteLine(l);