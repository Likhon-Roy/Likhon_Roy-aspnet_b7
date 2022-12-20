int a, b, c = 0;

int n = int.Parse(Console.ReadLine());

for(int i = 0; i < n; i++)
{
    var x = Console.ReadLine().Split(' ');

    a = int.Parse(x[0]);
    b = int.Parse(x[1]);

    if (b - a <= 2)
        c++;
}

Console.WriteLine(c);
