int n, k, i;
int sum = 0;

int[] a = new int[100];

var y = Console.ReadLine().Split(' ');

n = int.Parse(y[0]);
k = int.Parse(y[1]);

var x = Console.ReadLine().Split(' ');

for (i = 0; i < x.Length; i++)
{
    a[i] = int.Parse(x[i]);
}


for (i = 0; i < n; i++)
{
    if (a[i] >= a[k - 1] && a[i] > 0)
        sum++;
}

Console.WriteLine(sum);