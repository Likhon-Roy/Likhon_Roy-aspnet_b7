int ans = 0;

var a = new int[] { 100, 20, 10, 5, 1 };

int n = int.Parse(Console.ReadLine());

for (int i = 0; i < 5; i++)
{
    ans += n / a[i];

    n = n % a[i];
}

Console.WriteLine(ans);