using Etap0;

namespace Program
{
    public class Program
    {
        static void Main(string[] args)
        {
            Kalkulator kalkulator = new Kalkulator(); ;
            Console.WriteLine(kalkulator.suma(1,2));
            Console.WriteLine(kalkulator.roznica(1, 2));
        }
    }
}