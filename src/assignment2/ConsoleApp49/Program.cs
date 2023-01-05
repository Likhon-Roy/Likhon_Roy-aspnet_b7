int n = int.Parse(Console.ReadLine());
int c = 0;
int s = 0;
for (int i = 0; i < n; i++)
{
    int a, b;
    var x = Console.ReadLine().Split(' ');
    a = int.Parse(x[0]);
    b = int.Parse(x[1]);

    s = s - a;
    s = s + b;
    c = Math.Max(c, s);
}
Console.Write(c);