// class Program
// {
//     const double PI = 3.14159;
//     readonly double e = 2.71828;

//     static void Main()
//     {
//         Program p = new Program();
//         Console.WriteLine("The value of PI is: " + PI);
//         Console.WriteLine("The value of e is: " + p.e);
//     }
// }

int a = 5, b = 8;
Console.WriteLine(a > b);    // False
Console.WriteLine(a == b);   // False
Console.WriteLine(a != b);   // True

bool ok = a < b && b > 0;    // && 与
bool any = a > 100 || b > 0; // || 或
bool not = !ok;              // ! 非
Console.WriteLine($"{ok} {any} {not}");


string[] names = { "Tom", "Jerry", "Spike" };
foreach (var name in names)
    Console.Write($"{name} ");   // Tom Jerry Spike
Console.WriteLine();