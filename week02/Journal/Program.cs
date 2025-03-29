// For creativity and exceeding requirement, I added a Custom prompt, Journaling reminder and Mood options.

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        journal.Run();
    }
}

// Class to represent individual journal entries
public class JournalEntry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }
    public string Mood { get; set; } 

    public JournalEntry(string prompt, string response, string mood)
    {
        Prompt = prompt;
        Response = response;
        Mood = mood;
        Date = DateTime.Now.ToShortDateString(); // Store date as a string
    }
}

// Class to manage a collection of journal entries
public class Journal
{
    private List<JournalEntry> entries = new List<JournalEntry>();
    private List<string> customPrompts = new List<string>();
    private static readonly string[] prompts = new string[]
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    public void Run()
    {
        // Starting a reminder thread
        Thread reminderThread = new Thread(Reminder);
        reminderThread.Start();

        bool running = true;
        while (running)
        {
            Console.WriteLine("\nJournal Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display journal");
            Console.WriteLine("3. Save journal to file");
            Console.WriteLine("4. Load journal from file");
            Console.WriteLine("5. Add custom prompt");
            Console.WriteLine("6. Exit");
            Console.Write("\nChoose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    WriteEntry();
                    break;
                case "2":
                    DisplayEntries();
                    break;
                case "3":
                    SaveJournal();
                    break;
                case "4":
                    LoadJournal();
                    break;
                case "5":
                    AddCustomPrompt();
                    break;
                case "6":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private void Reminder()
    {
        while (true)
        {
            Console.WriteLine("Reminder: Don't forget to write in your journal today!");
            Thread.Sleep(86400000); // Reminder every 24 hours
        }
    }

    private void WriteEntry()
    {
        List<string> allPrompts = new List<string>(prompts);
        allPrompts.AddRange(customPrompts); // Include custom prompts

        Random random = new Random();
        string prompt = allPrompts[random.Next(allPrompts.Count)];
        Console.WriteLine($"\nPrompt: {prompt}");
        Console.Write("Your response: ");
        string response = Console.ReadLine();

        Console.WriteLine("How do you feel about this? ");
        Console.WriteLine("1. Happy");
        Console.WriteLine("2. Sad");
        Console.WriteLine("3. Excited");
        Console.WriteLine("4. Anxious");
        Console.Write("Your mood (1-4): ");
        string moodSelection = Console.ReadLine();
        string mood = moodSelection switch
        {
            "1" => "Happy",
            "2" => "Sad",
            "3" => "Excited",
            "4" => "Anxious",
            _ => "Unknown"
        };

        JournalEntry entry = new JournalEntry(prompt, response, mood);
        entries.Add(entry);
        Console.WriteLine("Entry added.");
    }

    private void DisplayEntries()
    {
        Console.WriteLine("\nJournal Entries:");
        foreach (var entry in entries)
        {
            Console.WriteLine($"Date: {entry.Date}");
            Console.WriteLine($"Prompt: {entry.Prompt}");
            Console.WriteLine($"Response: {entry.Response}");
            Console.WriteLine($"Mood: {entry.Mood}"); // Display mood
            Console.WriteLine();
        }
    }

    private void SaveJournal()
    {
        Console.Write("Enter filename to save journal: ");
        string filename = Console.ReadLine();

        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (var entry in entries)
            {
                outputFile.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}|{entry.Mood}");
            }
        }

        Console.WriteLine("Journal saved.");
    }

    private void LoadJournal()
    {
        Console.Write("Enter filename to load journal: ");
        string filename = Console.ReadLine();

        entries.Clear(); // Clear existing entries

        string[] lines = File.ReadAllLines(filename);
        foreach (string line in lines)
        {
            string[] parts = line.Split('|');
            if (parts.Length >= 4) // Ensuring we have enough data
            {
                entries.Add(new JournalEntry(parts[1], parts[2], parts[3]) { Date = parts[0] });
            }
        }

        Console.WriteLine("Journal loaded.");
    }

    private void AddCustomPrompt()
    {
        Console.Write("Enter your custom prompt: ");
        string customPrompt = Console.ReadLine();
        customPrompts.Add(customPrompt);
        Console.WriteLine("Custom prompt added!");
    }
}