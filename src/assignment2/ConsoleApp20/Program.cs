int k, l, m, n, d, c;

k = int.Parse(Console.ReadLine());
l = int.Parse(Console.ReadLine());
m = int.Parse(Console.ReadLine());
n = int.Parse(Console.ReadLine());
d = int.Parse(Console.ReadLine());

c = d;

if (k == 1)
{
    Console.WriteLine(d);
}
else if (l == 1)
{
    Console.WriteLine(d);
}
else if(m == 1)
{
    Console.WriteLine(d);
}
else if(n == 1)
{
    Console.WriteLine(d);
}


else
{
    for (int i = 1; i <= d; i++)
    {
        if ((i % k != 0) && (i % l != 0) && (i % m != 0) && (i % n != 0))
        {
            c--;
        }  
    }
    Console.WriteLine(c);
}