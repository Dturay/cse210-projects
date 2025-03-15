using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Numer Guessing Game.");
        Console.WriteLine();

        Console.WriteLine("Choose 1 to start a new game");
        Console.WriteLine("Choose 2 to exit game");
        Console.Write("Enter your choice here: ");
        
        Console.WriteLine();

        string Choice = Console.ReadLine();
        int choice = int.Parse(Choice);

        while (choice != 2)
        {
            if (choice == 1)
            {
                Random random = new Random();
                int magicNo = random.Next(1, 101);
                Console.WriteLine("You have to guess the magic number from 1 to 100.");
                Console.Write("What is your guess? ");
                string guess = Console.ReadLine();
                int guessNo = int.Parse(guess);
                Console.WriteLine();
                int guessCount = 0;

                while (guessNo != magicNo)
                {
                    guessCount++;
                    if (guessNo > magicNo)
                    {
                        Console.WriteLine("Wrong, guess lower");
                    }
                    else if (guessNo < magicNo)
                    {
                        Console.WriteLine("Wrong, guess higer");

                    }
                    Console.Write("What is your guess? ");
                    guess = Console.ReadLine();
                    guessNo = int.Parse(guess);
                }
                Console.WriteLine("You guessed it!");
                Console.WriteLine($"You used {guessCount} guesses.");
                Console.WriteLine();
            }
            else if (choice < 1 || choice > 2)
            {
                Console.WriteLine("Invalid Entry! Enter either 1 or 2.");
            }
            Console.WriteLine();
            Console.WriteLine("Choose 1 to start a new game");
            Console.WriteLine("Choose 2 to exit game");
            Console.Write("Enter your choice here: ");
            Choice = Console.ReadLine();
            choice = int.Parse(Choice);
            Console.WriteLine();


        }
        Console.WriteLine("Thank you for playing, until next time.");
    }    
}