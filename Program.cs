using System;

// Emil Ejderklev (NET23)

namespace NumbersGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.Clear();

                PlayGame();
                Console.Write("Vill du spela igen? (Ja / nej) : ");
            }
            while (Console.ReadLine().ToLower() == "ja");

            Console.WriteLine("Tack för att du spelade!");
        }

        static void PlayGame()
        {
            // Declare and initialize variables
            int min, max, chances, userNumber, i = 0;
            string[] lowNumber  = { "Tyvärr, du gissade för lågt!", "Nästan, men det var lite för lågt!", "Inte riktigt där, du behöver gissa högre.", "Lite mer än så, försök igen med ett högre tal.", "Din gissning var nästan korrekt, men den var för låg." };
            string[] highNumber = { "Tyvärr, du gissade för högt!", "Nästan, men det var lite för högt!", "Inte riktigt där, du behöver gissa lägre.", "Lite mer än så, försök igen med ett lägre tal.", "Din gissning var nästan korrekt, men den var för hög." };
            string[] oneGreater = { "För högt, du är nästan där!", "Högre än målet, men nästan rätt!", "Du är bränande het, bara en liten bit till!", "Nästan, men en aning för högt!", "Lite mer, och du träffar målet!" };
            string[] oneLower   = { "För lågt, du är nästan där!", "Lite för lågt, försök igen!", "Nästan, men en smula för lågt!", "Lite mer, och du är i mål!", "Du är på rätt väg, bara ett snäpp högre!" };

            // Get difficulty level
            var difficulty = GetDifficulty();

            min = difficulty.Item1;
            max = difficulty.Item2;
            chances = difficulty.Item3;

            // Generate a random number
            int randomNumber = RandomValue(min, max);

            // Display game instructions
            Console.Write($"Välkommen! Jag tänker på ett nummer mellan {min} till {max}. Kan du gissa vilket? Du får {chances} försök:\n");

            while (i < chances)
            {
                // User's guess
                string input = Console.ReadLine();

                if (int.TryParse(input, out userNumber) && userNumber >= min && userNumber <= max)
                {
                    i++;

                    // Check user's guess
                    string result = CheckGuess(userNumber, randomNumber, lowNumber, highNumber, oneGreater, oneLower);

                    Console.WriteLine(result);

                    if (result == $"Wohoo! Du klarade det!")
                    {
                        Console.WriteLine($"Rätt svar var {randomNumber} och det tog dig {i} försök! ");
                        break;
                    }
                    else if (i == chances)
                    {
                        Console.WriteLine($"\nTyvärr, du lyckades inte gissa talet på {chances} försök! Numret var {randomNumber}!");
                    }
                }
                else
                {
                    Console.WriteLine($"Felaktig inmatning. Vänligen skriv in en siffra mellan {min} och {max}.");
                }
            }
        }

        static (int, int, int) GetDifficulty()
        {
            int min, max, chances;

            // Display difficulty selection options
            Console.WriteLine("Hej välj svårighetsgrad till NumbersGame:");
            Console.WriteLine("1. Lätt  (1-100,   10 försök)");
            Console.WriteLine("2. Medel (1-1000,  15 försök)");
            Console.WriteLine("3. Svår  (1-10000, 20 försök)");
            Console.WriteLine("4. Anpassad");
            string input = Console.ReadLine();
            Console.Clear();

            switch (input)
            {
                case "1":
                    min = 1;
                    max = 100;
                    chances = 10;
                    break;
                case "2":
                    min = 1;
                    max = 1000;
                    chances = 15;
                    break
                case "3":
                    min = 1;
                    max = 10000;
                    chances = 20;
                    break;
                case "4":
                    // Custom difficulty settings
                    Console.Write("Ange minsta värdet: ");
                    min = int.Parse(Console.ReadLine());

                    Console.Write("Ange högsta värdet: ");
                    max = int.Parse(Console.ReadLine());

                    Console.Write("Ange antal försök: ");
                    chances = int.Parse(Console.ReadLine());
                    break;
                default:
                    Console.WriteLine("Ogiltigt val. Välj en siffra mellan 1 och 4.");
                    return GetDifficulty(); // Recursively call Difficulty to retry input
            }

            return (min, max, chances);
        }

        static int RandomValue(int min, int max)
        {
            // Generate a random number within the specified range
            Random random = new Random();
            int randomNumber = random.Next(min, max);
            return randomNumber;
        }

        static string RandomRespons(string[] responses)
        {
            // Get a random response from an array
            Random random = new Random();
            int randomIndex = random.Next(0, responses.Length);
            return responses[randomIndex];
        }

        static string CheckGuess(int userNumber, int targetNumber, string[] lowNumber, string[] highNumber, string[] oneGreater, string[] oneLower)
        {
            if (userNumber == targetNumber + 1)
            {
                Console.WriteLine("En lägre");
                return RandomRespons(oneGreater);
            }
            else if (userNumber == targetNumber - 1)
            {
                Console.WriteLine("En högre");
                return RandomRespons(oneLower);
            }
            else if (userNumber < targetNumber)
            {
                Console.WriteLine("Högre");
                return RandomRespons(lowNumber);
            }
            else if (userNumber > targetNumber)
            {
                Console.WriteLine("Lägre");
                return RandomRespons(highNumber);
            }
            else
            {
                return $"Wohoo! Du klarade det!";
            }
        }
    }
}