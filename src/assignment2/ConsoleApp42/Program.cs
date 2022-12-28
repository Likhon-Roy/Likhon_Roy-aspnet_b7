var x = Console.ReadLine().Split(' ');

int n = int.Parse(x[0]);
int k = int.Parse(x[1]);

int t = 240, d = 0, ans = 0;

t -= k;

for (int i = 1; i <= n; i++)
{
    d += i * 5;
    if (d <= t)
    {
        ans++;
    }
    else
    {
        break;
    }
}

Console.WriteLine(ans);