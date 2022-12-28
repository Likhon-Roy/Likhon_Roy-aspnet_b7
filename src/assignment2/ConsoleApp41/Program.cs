
int n = int.Parse(Console.ReadLine());

int ans = 0;

for (int i = 0; i < n; i++)
{
    var x = Console.ReadLine().Split(' ');

    var a = int.Parse(x[0]);
    var b = int.Parse(x[1]);
    var c = int.Parse(x[2]);

    if (a + b + c >= 2)
    {
        ans += 1;
    }
}

Console.WriteLine(ans);