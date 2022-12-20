int n = int.Parse(Console.ReadLine());

for (int i = 0; i < n; i++)
{
    var temp = Console.ReadLine().Split(' ');
    int x = int.Parse(temp[0]);
    int y = int.Parse(temp[1]);

    if (x % y == 0)
    {
        Console.WriteLine("0");
    }
    else
    {
        int w = x % y;

        Console.WriteLine(y - w);
    }
}