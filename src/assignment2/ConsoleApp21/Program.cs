int a = int.Parse(Console.ReadLine());
var m = new List<int>();

var x = Console.ReadLine().Split(' ');

for (int i = 0; i < x.Length; i++)
{
    m.Add(int.Parse(x[i]));
}

m.Sort();

for (int i = 0; i < x.Length; i++)
{
    Console.Write($"{m[i]} ");
}