using System.Text;

int t = int.Parse(Console.ReadLine());

while (t-- != 0)
{
    string s = Console.ReadLine();

    int ssize = s.Length;

    StringBuilder an = new StringBuilder();

    an.Append(s[0]);

    for (int i = 1; i < ssize - 1; i += 2)
    {
        an.Append(s[i]);
    }
    an.Append(s[ssize - 1]);

    Console.WriteLine(an.ToString());
}