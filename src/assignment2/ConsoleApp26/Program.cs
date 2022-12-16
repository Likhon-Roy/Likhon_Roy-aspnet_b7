int n = int.Parse(Console.ReadLine());

int a = 0, b = 0, c = 0;

for (int j = 0; j < n; j++)

{
    string s = Console.ReadLine();
    string[] v = s.Split(' ');

    int x = int.Parse(v[0]);
    int y = int.Parse(v[1]);
    int z = int.Parse(v[2]);

    a += x;
    b += y;
    c += z;
}

if (a == 0 && b == 0 && c == 0)
{
    Console.WriteLine("YES");
}

else
{
    Console.WriteLine("NO");
}