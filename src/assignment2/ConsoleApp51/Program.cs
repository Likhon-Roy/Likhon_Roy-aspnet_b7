// 155 problem
int count = 0;
int a = int.Parse(Console.ReadLine());

var arr = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();

int max = arr[0];
int min = arr[0];

for (int i = 0; i < a; i++)
{
    if (arr[i] > max)
    {
        max = arr[i];
        count++;
    }
    if (arr[i] < min)
    {
        min = arr[i];
        count++;
    }
}
Console.WriteLine(count);