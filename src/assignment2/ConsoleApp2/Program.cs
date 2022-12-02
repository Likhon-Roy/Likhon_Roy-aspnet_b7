using System.Text;

string x = Console.ReadLine();
string y = Console.ReadLine();

StringBuilder s1 = new StringBuilder(x);
StringBuilder s2 = new StringBuilder(y);

for (int i = 0; i < s1.Length; ++i)
{
    if (s1[i] == s2[i])
    {
        s1[i] = '0';
    }
    else
    {
        s1[i] = '1';
    }
}

Console.WriteLine(s1);