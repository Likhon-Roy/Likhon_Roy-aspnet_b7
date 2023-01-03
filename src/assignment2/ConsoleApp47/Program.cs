int c = 0;

int n = int.Parse(Console.ReadLine());

var a = new int[1000];

for (int i = 0; i < n; i++)
{
    a[i] = int.Parse(Console.ReadLine());
}

for (int i = 0; i < n; i++)
{
    if (a.Length != i)
    {
        if (a[i] != a[i + 1])
            c++;
    }
}

Console.WriteLine(c);