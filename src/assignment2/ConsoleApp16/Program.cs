int n = int.Parse(Console.ReadLine());

var a = new int[110];

double an = 0.0, s = 0.0;

var x = Console.ReadLine().Split(' ');

for (int i = 0; i < n; i++)
{

    s += int.Parse(x[i]);
}
an = s / n;

Console.WriteLine(an);