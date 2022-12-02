long n = long.Parse(Console.ReadLine());

if (n % 2 == 0)
{
    Console.WriteLine(n / 2);
}

else
{
    var x = (-(n + 1) / 2);
    Console.WriteLine(x);
}