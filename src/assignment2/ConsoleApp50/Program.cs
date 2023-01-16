int n, s, r, p = 0, z = 0;

n = int.Parse(Console.ReadLine());

for (int i = 0; i < n; i++)
{
    var x = Console.ReadLine().Split(' ');

    s = int.Parse(x[0]);
    r = int.Parse(x[1]);

    if (s > r)
    {
        p++;
    }
    else if (s < r)
    {
        z++;
    }
}
if (p > z)
{
    Console.WriteLine("Mishka");
}
else if (z > p)
{
    Console.WriteLine("Chris");
}
else
{
    Console.WriteLine("Friendship is magic!^^");
}