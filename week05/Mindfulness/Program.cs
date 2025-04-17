// For extra requirement, the program allow users to save and review their reflections and lists in a file

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        MindfulnessProgram mindfulnessProgram = new MindfulnessProgram();
        mindfulnessProgram.Start();
    }
}

class MindfulnessProgram
{
    private Dictionary<string, Activity> activities;

    public MindfulnessProgram()
    {
        activities = new Dictionary<string, Activity>
        {
            { "1", new BreathingActivity() },
            { "2", new ReflectionActivity() },
            { "3", new ListingActivity() }
        };
    }

    public void Start()
    {
        while (true)
        {
            Console.WriteLine("Choose an activity:");
            Console.WriteLine("1: Breathing Activity");
            Console.WriteLine("2: Reflection Activity");
            Console.WriteLine("3: Listing Activity");
            Console.WriteLine("4: Review Saved Reflections");
            Console.WriteLine("5: Exit");

            string choice = Console.ReadLine();

            if (activities.ContainsKey(choice))
            {
                activities[choice].Start();
            }
            else if (choice == "4")
            {
                ReviewSavedReflections();
            }
            else if (choice == "5")
            {
                Console.WriteLine("Goodbye!");
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }
    }

    private void ReviewSavedReflections()
    {
        const string filename = "reflections.txt";

        if (File.Exists(filename))
        {
            Console.WriteLine("Saved Reflections and Lists:");
            Console.WriteLine("==============================");
            string[] reflections = File.ReadAllLines(filename);
            foreach (string line in reflections)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("==============================");
        }
        else
        {
            Console.WriteLine("No reflections found.");
        }

        Console.WriteLine("Press any key to return to the main menu...");
        Console.ReadKey();
    }
}

abstract class Activity
{
    protected int duration;

    public void Start()
    {
        SetDuration();
        Console.WriteLine("Prepare to begin...");
        Pause(3);
        PerformActivity();
        EndMessage();
    }

    protected void SetDuration()
    {
        Console.Write("Enter the duration of the activity in seconds: ");
        duration = int.Parse(Console.ReadLine());
    }

    protected void EndMessage()
    {
        Console.WriteLine("Good job! You have completed the activity.");
        Pause(3);
        Console.WriteLine($"You spent {duration} seconds on this activity.");
        Pause(3);
    }

    protected void Pause(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"\r... {i} seconds remaining");
            Thread.Sleep(1000);
        }
        Console.WriteLine("\r" + new string(' ', 30)); // Clear the line
    }

    protected abstract void PerformActivity();
}

class BreathingActivity : Activity
{
    protected override void PerformActivity()
    {
        Console.WriteLine("This activity will help you relax by walking you through breathing in and out slowly.");
        Thread.Sleep(2000);

        DateTime startTime = DateTime.Now;
        while ((DateTime.Now - startTime).TotalSeconds < duration)
        {
            Console.WriteLine("Breathe in...");
            CountdownPause(4);
            Console.WriteLine("Breathe out...");
            CountdownPause(4);
        }
    }

    private void CountdownPause(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"\r{i}");
            Thread.Sleep(1000);
        }
        Console.WriteLine("\r" + new string(' ', 5)); // Clear the line
    }
}

class ReflectionActivity : Activity
{
    private static readonly List<string> prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private static readonly List<string> questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    protected override void PerformActivity()
    {
        Console.WriteLine("This activity will help you reflect on times in your life when you have shown strength and resilience.");
        Thread.Sleep(2000);

        Random rand = new Random();
        string prompt = prompts[rand.Next(prompts.Count)];
        Console.WriteLine(prompt);
        Pause(5);

        List<string> reflections = new List<string>();
        DateTime startTime = DateTime.Now;
        while ((DateTime.Now - startTime).TotalSeconds < duration)
        {
            string question = questions[rand.Next(questions.Count)];
            Console.WriteLine(question);
            Pause(5);

            Console.Write("Your reflection: ");
            string response = Console.ReadLine();
            reflections.Add($"Question: {question} - Response: {response}");
        }

        SaveReflections(reflections);
    }

    private void SaveReflections(List<string> reflections)
    {
        const string filename = "reflections.txt";
        using (StreamWriter sw = new StreamWriter(filename, true)) // Append to file
        {
            foreach (string reflection in reflections)
            {
                sw.WriteLine(reflection);
            }
        }
        Console.WriteLine("Your reflections have been saved.");
    }
}

class ListingActivity : Activity
{
    private static readonly List<string> prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    protected override void PerformActivity()
    {
        Console.WriteLine("This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");
        Thread.Sleep(2000);

        Random rand = new Random();
        string prompt = prompts[rand.Next(prompts.Count)];
        Console.WriteLine(prompt);
        Pause(3);

        List<string> items = new List<string>();
        DateTime startTime = DateTime.Now;
        while ((DateTime.Now - startTime).TotalSeconds < duration)
        {
            Console.Write("List an item (or type 'done' to finish): ");
            string item = Console.ReadLine();
            if (item.ToLower() == "done")
            {
                break;
            }
            items.Add(item);
        }

        SaveItemsToFile(items);
        Console.WriteLine($"You listed {items.Count} items.");
    }

    private void SaveItemsToFile(List<string> items)
    {
        const string filename = "reflections.txt";
        using (StreamWriter sw = new StreamWriter(filename, true)) // Append to file
        {
            sw.WriteLine("Listed Items: " + string.Join(", ", items));
        }
        Console.WriteLine("Your listed items have been saved.");
    }
}