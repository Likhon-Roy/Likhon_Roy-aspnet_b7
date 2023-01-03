using System.Text;

int n = int.Parse(Console.ReadLine());
string s = Console.ReadLine();


HashSet<string> myhash1 = new HashSet<string>();

for (int i = 0; i < s.Length; ++i)
{
    string x = s[i].ToString();
    var b = x.ToLower();
    myhash1.Add(b);
}
if (myhash1.Count == 26)
{
    Console.WriteLine("YES");
}
else
    Console.WriteLine("NO");