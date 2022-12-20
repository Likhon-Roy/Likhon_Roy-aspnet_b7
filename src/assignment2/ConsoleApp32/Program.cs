using System.Text;

string s = Console.ReadLine();

StringBuilder sb = new StringBuilder("");

var li = new List<int>();

bool x = false;

for (int i = 0; i < s.Length; i++)
{
    if (s[i] >= 'a' && s[i] <= 'z')
    {
        li.Add((int)(s[i]));

        //for (int j = 0; j < sb.Length; j++)
        //{

        //    //if (sb[j] == s[i])
        //    //{
        //    //    break;
        //    //}
        //    //sb.Append(s[i]);
        //}
        //sb.Append(s[i]);
    }
}

var y = li.Distinct().ToList();

Console.WriteLine(y.Count);