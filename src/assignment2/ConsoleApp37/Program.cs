var li = new List<int>();

int z = 3;

var x = Console.ReadLine().Split(' ');

for (int i = 0; i < x.Length; i++)
{
    li.Add(int.Parse(x[i]));

}

li.Sort();

for (int i = 0; i < 3; i++)
{
    if (li[i] != li[i + 1])
    {
        z--;
    }
}

Console.Write(z);
