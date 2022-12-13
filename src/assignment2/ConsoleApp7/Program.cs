int n = int.Parse(Console.ReadLine());

var y = Console.ReadLine().Split(' ');

var li = new List<int>();
var mas = new int[105];

for (int i = 0; i < y.Length; i++)
{
    li.Add(int.Parse(y[i]));
}


for (int i = 1; i <= n; i++)
{
    mas[li[i - 1]] = i;
}
for (int i = 1; i <= n; i++)
{
    Console.Write($"{mas[i]} ");
}