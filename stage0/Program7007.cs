
namespace stage0;
partial  class Program
{
    private static void Main(string[] args)
    {
        welcome7007();
        welcome1885();
        Console.ReadKey();
    }
    static partial void welcome1885();
    private static void welcome7007()
    {
        Console.WriteLine("Enter your name: ");
        string name = Console.ReadLine();
        Console.WriteLine($"{name}, welcome to my first console application");
    }
}