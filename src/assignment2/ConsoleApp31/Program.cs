int i, n;

int a = int.Parse(Console.ReadLine());
int b = int.Parse(Console.ReadLine());
int c = int.Parse(Console.ReadLine());

n = a + b + c;
n = Math.Max(n, (a * b * c));
n = Math.Max(n, (a + b) * c);
n = Math.Max(n, a * (b + c));
n = Math.Max(n, a + (b * c));
n = Math.Max(n, (a * b) + c);

Console.WriteLine(n);