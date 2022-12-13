int a = 0;
int b = 0;

int n = int.Parse(Console.ReadLine());

string s = Console.ReadLine();

for (int i = 0; i < n; i++)
{
    if (s[i] == 'A')
        a++;
    else if (s[i] == 'D')
        b++;
}
if (a == b)
{
    Console.Write("Friendship");
}
else if (a > b)
{
    Console.Write("Anton");
}
else
    Console.Write("Danik");
