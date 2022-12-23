int a, s = 0;

var x = Console.ReadLine().Split(' ');
int n = int.Parse(x[0]);
int h = int.Parse(x[1]);

var y = Console.ReadLine().Split(' ');

for (int i = 0; i < n; i++)
{

    if (int.Parse(y[i]) > h)
    {
        s++;
    }
    s++;
}

Console.WriteLine(s);
