int n = int.Parse(Console.ReadLine());
long x, y, z;


for (int i = 0; i < n; i++)
{
    var u = Console.ReadLine().Split(' ');

    x = long.Parse(u[0]);
    y = long.Parse(u[1]);
    z = long.Parse(u[2]);

    long ans = 0;

    ans = z - z % x + y;

    if (ans <= z)
    {
        Console.WriteLine(ans);
    }
    else
    {
        Console.WriteLine((z - z % x - (x - y)));
    }
}