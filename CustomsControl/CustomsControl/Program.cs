using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomsControl
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hej! Jag är tullvakten.");
            Console.WriteLine("Är du en vän med en bil? Eller med en motorcykel? Vad har du där för bil?");
            Console.WriteLine("[1] Personbil");
            Console.WriteLine("[2] Lastbil");
            Console.WriteLine("[3] Motorcykel");

            var wrongInput = true;
            vehicleType type = 0;

            do
            {
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        type = vehicleType.car;
                        wrongInput = false;
                        break;
                    case "2":
                        type = vehicleType.truck;
                        wrongInput = false;

                        break;
                    case "3":
                        type = vehicleType.motorcycle;
                        wrongInput = false;
                        break;
                    default:
                        Console.WriteLine("På nåt sätt förstår jag dig - inte. Säg det igen.");
                        break;
                }

            } while (wrongInput);

            Console.WriteLine();
            Console.WriteLine("Bra. Hur tung är den i kg?");
            wrongInput = true;

            int weight;

            do
            {
                weight = Convert.ToInt32(Console.ReadLine());
                if (weight > 0)
                    wrongInput = false;
                else
                    Console.WriteLine("Du kan inte få bilen så lätt. Hur tung är den på riktigt? ");

            } while (wrongInput);

            Console.WriteLine();
            Console.WriteLine("Bra. Är det en miljöbil? [j/n] ");

            wrongInput = true;
            bool environmental = false;

            do
            {
                var input = Console.ReadLine();

                switch (input.ToLower())
                {
                    case "j":
                        environmental = true;
                        wrongInput = false;
                        break;
                    case "n":
                        environmental = false;
                        wrongInput = false;
                        break;
                    default:
                        Console.WriteLine("På nåt sätt förstår jag dig - inte. Säg det igen.");
                        break;
                }

            } while (wrongInput);

            var fee = CustomsGuard.GetFee(new Vehicle(weight, type, environmental));

            Console.WriteLine($"Det blir då {fee} SEK.");
            Console.WriteLine("Ha en bra dag, jag stannar här, men känn ingen sorg för mig.");
        }
    }
}
