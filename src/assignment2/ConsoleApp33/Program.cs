int n;

var a = new int[100];

int num = int.Parse(Console.ReadLine());

for (int i = 0; i < num; i++)
{
    n = int.Parse(Console.ReadLine());

    var x = Console.ReadLine().Split(' ');

    for (int k = 0; k < n; k++)
    {
        a[k] = int.Parse(x[k]);
    }

    int g = 0, f = 0;

    for (int j = 0; j < n; j++)
    {
        if (j % 2 != a[j] % 2)
        {
            if (a[j] % 2 == 1)
            {
                g++;
            }
            else
            {
                f++;
            }
        }
    }
    if (g != f)
    {
        Console.WriteLine("-1");
    }
    else
    {
        Console.WriteLine(f);
    }
}