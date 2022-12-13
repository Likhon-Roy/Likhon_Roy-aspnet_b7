using System.Diagnostics.Metrics;

string s = Console.ReadLine();
int condition = 1;
int x = 0;
for (int i = 1; i < s.Length; i++)
{
    if (s[i] == s[i - 1])
    {
        condition++;
        if (condition == 7)
        {
            Console.WriteLine("YES");
            x = 1;
            break;
        }
    }
    else
    {
        condition = 1;
    }
}
if (x != 1)
{
    Console.WriteLine("NO");
}