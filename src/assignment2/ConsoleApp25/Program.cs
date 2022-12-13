ulong n = ulong.Parse(Console.ReadLine());

int count = 0;
while (n != 0)
{
    if (n % 10 == 4 || n % 10 == 7)
    {
        count += 1;
    }
    n = n / 10;
}
if (count == 4 || count == 7)
{
    Console.WriteLine("YES");
}
else
{
    Console.WriteLine("NO");
}