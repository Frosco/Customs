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

            string input;

            do
            {
                input = Console.ReadLine();
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

            Console.Clear();
            Console.WriteLine("Bra. Hur tung är den i kg?");
            wrongInput = true;

            int weight = 0;

            do
            {
                try
                {
                    weight = Convert.ToInt32(Console.ReadLine());
                    if (weight > 0)
                        wrongInput = false;
                    else
                        Console.WriteLine("Du kan inte få den så lätt. Hur tung är den på riktigt? ");
                }
                catch (Exception)
                {
                    Console.WriteLine("På nåt sätt förstår jag dig - inte. Säg det igen.");
                }
            } while (wrongInput);

            Console.Clear();
            Console.WriteLine("Bra. Är det ett miljöfordon? [j/n] ");

            wrongInput = true;
            bool environmental = false;

            do
            {
                input = Console.ReadLine();

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

            Console.Clear();

            Console.WriteLine($"Datum och tid är {DateTime.Now} just nu. Stämmer det?");
            Console.WriteLine("[1] Det är rätt.");
            Console.WriteLine("[2] Jag vill ange ett annat datum och tid.");

            double fee = 0;
            wrongInput = true;

            do
            {
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        fee = CustomsGuard.GetFee(new Vehicle(weight, type, environmental));
                        wrongInput = false;
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Din tid kommer. Om du säger mig vad den är.");
                        Console.WriteLine("Vilket datum har vi? [Skriv i formatet ÅÅÅÅ-MM-DD]");

                        var moment = new DateTime(0);

                        wrongInput = true;
                        do
                        {
                            try
                            {
                                input = Console.ReadLine();
                                moment = Convert.ToDateTime(input);
                                wrongInput = false;
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Det var inte ett rätt datum. Försök igen.");
                            }
                        } while (wrongInput);

                        wrongInput = true;

                        Console.WriteLine("Ange tiden. [Skriv i formatet TT:MM]");

                        do
                        {
                            try
                            {
                                input = Console.ReadLine();
                                var timeString = input.Split(':');
                                var hours = Convert.ToInt32(timeString[0]);
                                var minutes = Convert.ToInt32(timeString[1]);

                                if (hours > 23 || hours < 0 || minutes > 59 || minutes < 0)
                                {
                                    throw new Exception();
                                }

                                moment.AddHours(hours);
                                moment.AddMinutes(minutes);
                                wrongInput = false;
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Det var inte en rätt tid. Försök det igen."); ;
                            }

                        } while (wrongInput);

                        fee = CustomsGuard.GetFee(new Vehicle(weight, type, environmental), moment);

                        break;
                    default:
                        Console.WriteLine("Det var inte vad jag frågade. Säg det igen.");
                        break;
                }
            } while (wrongInput);

            Console.Clear();
            Console.WriteLine($"Det blir då {fee} SEK.");
            Console.WriteLine("Ha en bra dag, jag stannar här, men känn ingen sorg för mig.");
            Console.WriteLine();
            Console.WriteLine("Tryck ENTER för att säga hejdå.");
            Console.ReadLine();
        }
    }
}
