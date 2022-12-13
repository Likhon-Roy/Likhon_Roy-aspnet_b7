using System.Text;

int n, a;

var y = Console.ReadLine().Split(' ');

n = int.Parse(y[0]);

string s = Console.ReadLine();

StringBuilder stringBuilder = new StringBuilder(s);


for(a= int.Parse(y[1]) ; a != 0; a--)
{ 
    for (int i = 1; i < n; ++i)
    {
        if (stringBuilder[i] == 'G' && stringBuilder[i - 1] == 'B')
        {
            stringBuilder[i] = 'B';
            stringBuilder[i - 1] = 'G';
            ++i;
        }
    }
}
Console.WriteLine(stringBuilder.ToString());