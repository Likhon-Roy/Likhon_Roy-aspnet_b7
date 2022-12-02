int n = int.Parse(Console.ReadLine());

int sum = 0;

var y = Console.ReadLine().Split(' ');

var li = new List<int>();

for (int i = 0; i < y.Length; i++)
{
	li.Add(int.Parse(y[i]));
}

for (int i = 0; i < n; i++)
{
	int x = li[i];
	sum += x;
}

if (sum != 0)
{
	Console.WriteLine("HARD");
}
else
{
	Console.WriteLine("EASY");
}