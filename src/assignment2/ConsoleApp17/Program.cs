int x = 0;

for (int i = 1; i <= 5; ++i)
{
    string[] s = Console.ReadLine().Split(' ');

    for (int j = 1; j <= 5; ++j)
    {
        x = int.Parse(s[j - 1]);
        if (x == 1)
        {
            Console.WriteLine(Math.Abs(i - 3) + Math.Abs(j - 3));
        }
    }
}