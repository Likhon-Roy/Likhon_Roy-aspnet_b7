string h = "hello";
string s = Console.ReadLine();

int a = 0;
int count = 0;

for (int i = 0; i < s.Length; i++)
{
    if (s[i] == h[a])
    {
        a++;
        count++;
    }
    if (count == 5)
    {
        break;
    }

if(count == 6)
    {

    }
}
