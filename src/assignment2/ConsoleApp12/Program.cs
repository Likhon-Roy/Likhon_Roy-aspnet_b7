string s = Console.ReadLine();

var x = char.ToUpper(s[0]).ToString();

Console.Write(x);

for (int i = 1; i < s.Length; i++)
{
    Console.Write(s[i]);
}
