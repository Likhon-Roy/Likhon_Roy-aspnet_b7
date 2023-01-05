int n = int.Parse(Console.ReadLine());

int a = 0;
int b = 0;

for (int i = 0; i < n; i++)
{
    var inp = Console.ReadLine().Split(' ');

    foreach (string s in inp)
    {
        int x = int.Parse(s);

        if (x > 0)
        {
            b += x;
        }
        else
        {
            if (b < 1)
            {
                ++a;
            }
            else
            {
                --b;
            }
        }
    }
    Console.WriteLine(a);
}

