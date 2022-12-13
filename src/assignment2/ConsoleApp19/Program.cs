string s = Console.ReadLine();

int ct = 0;

int length = s.Length;

foreach (char c in s)
{
    if (65 <= (int)c && (int)c <= 90)
    {
        ct++;
    }
}
if ((s.Length / 2) < ct)
{
    Console.WriteLine(s.ToUpper());
}
else
    Console.WriteLine(s.ToLower());