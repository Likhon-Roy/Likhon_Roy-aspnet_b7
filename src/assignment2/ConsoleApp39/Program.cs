using System.Text;

int n = int.Parse(Console.ReadLine());
string s;

for (int i = 0; i < n; i++)
{
    s = Console.ReadLine();
    StringBuilder? stringBuilder = new StringBuilder(s);


    if (s.Length > 10)
    {
        Console.Write(stringBuilder[0]);
        Console.Write(stringBuilder.Length - 2);
        Console.WriteLine(stringBuilder[stringBuilder.Length - 1]);
    }
    else
    {
        Console.WriteLine(s);
    }
}