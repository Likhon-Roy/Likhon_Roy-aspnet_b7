
int n = int.Parse(Console.ReadLine());

int a;
for (int k = 0; k < n; k++)
{
    a = int.Parse(Console.ReadLine());
    int ans = 0;
    int i = 0, j = 1;
    while (i != a)
    {
        if (j % 3 != 0 && j % 10 != 3)
        {
            ans = j;
            j++;
        }
        else
        {
            j++;
            continue;
        }
        i++;
    }
    Console.WriteLine(ans);
}
