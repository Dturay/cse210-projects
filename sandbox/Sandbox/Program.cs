using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World!!!");

        int age = 24;
        Console.WriteLine($"You are {age} years old.");

        Console.WriteLine("What is your color? ");
        string color = Console.ReadLine();

        Console.WriteLine($"Your favorite color is {color}");
    }
}