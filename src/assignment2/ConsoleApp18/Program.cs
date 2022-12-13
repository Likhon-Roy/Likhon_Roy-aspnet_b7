int x = 0;

for (int i = 1; i <= 5; ++i)
{
    string[] s = Console.ReadLine().Split(' ');

    for (int j = 1; j <= 5; ++j)
    {
        x = int.Parse(s[j - 1]);
        if (x == 1)
        {
            var a = Math.Abs(i - 3);
            var b = Math.Abs(j - 3);
            var c = a + b;
            Console.WriteLine(c);
        }
    }
}