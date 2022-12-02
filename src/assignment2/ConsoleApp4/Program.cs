
int n = int.Parse(Console.ReadLine());


for (int i = 1; i <= n; i++)
{
    if (i % 2 == 1)
        Console.Write("I hate ");
    else
        Console.Write("I love ");
    if (i == n)
        Console.WriteLine("it");
    else
        Console.Write("that ");
}
