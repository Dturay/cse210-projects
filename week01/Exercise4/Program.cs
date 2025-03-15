using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");
        Console.Write("Enter number: ");
        string input = Console.ReadLine();
        int number = int.Parse(input);


        while (number != 0)
        {
            numbers.Add(number);
            Console.Write("Enter number: ");
            input = Console.ReadLine();
            number = int.Parse(input);
        }
        int sum = numbers.Sum();
        double average = (double)sum / numbers.Count;
        int largest = numbers.Max();

        Console.WriteLine($"The sum is {sum}.");
        Console.WriteLine($"The average is {average}.");
        Console.WriteLine($"The largest number is {largest}.");
    }
}