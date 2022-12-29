int n = int.Parse(Console.ReadLine());
double x;
ulong a;

for (int i = 0; i < n; i++)
{
    a = 0;

    x = double.Parse(Console.ReadLine());

    var z = ((x / 2) - 1);

    a = (ulong)Math.Ceiling(z);

    Console.WriteLine(a);

}
