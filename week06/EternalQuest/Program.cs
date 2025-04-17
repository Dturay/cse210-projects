// For exceeding requirement, this program implements user accounts, allowing multiple users to manage their goals separately within the framework.

using System;
using System.Collections.Generic;
using System.IO;

abstract class Goal
{
    protected string name;
    protected int points;
    protected bool isCompleted;

    public Goal(string name, int points)
    {
        this.name = name;
        this.points = points;
        this.isCompleted = false;
    }

    public abstract int RecordAchievement();
    public abstract string GetStatus();

    public string GetName() => name;
    
    public bool IsCompleted() => isCompleted;

    protected void SetCompleted() => isCompleted = true;
}

class SimpleGoal : Goal
{
    public SimpleGoal(string name, int points) : base(name, points) { }

    public override int RecordAchievement()
    {
        SetCompleted();
        return points; // Award points on completion
    }

    public override string GetStatus()
    {
        return isCompleted ? $"[X] {name}" : $"[ ] {name}";
    }
}

class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points) { }

    public override int RecordAchievement() => points; // Award points for every achievement
    
    public override string GetStatus() => $"[ ] {name} (Eternal Goal)";
}

class ChecklistGoal : Goal
{
    private int timesToComplete;
    private int currentCount;

    public ChecklistGoal(string name, int points, int timesToComplete) : base(name, points)
    {
        this.timesToComplete = timesToComplete;
        this.currentCount = 0;
    }

    public override int RecordAchievement()
    {
        currentCount++;
        if (currentCount >= timesToComplete)
        {
            SetCompleted();
            return points + 500; // Bonus points on completion
        }
        return points; // Regular points for each achievement
    }

    public override string GetStatus()
    {
        return currentCount >= timesToComplete 
            ? $"[X] {name} (Completed {currentCount}/{timesToComplete})" 
            : $"[ ] {name} (Completed {currentCount}/{timesToComplete})";
    }
}

class User
{
    public string Username { get; }
    public List<Goal> Goals { get; }
    public int TotalPoints { get; private set; }

    public User(string username)
    {
        Username = username;
        Goals = new List<Goal>();
        TotalPoints = 0;
    }

    public void AddGoal(Goal goal) => Goals.Add(goal);

    public void RecordGoal(string goalName)
    {
        foreach (var goal in Goals)
        {
            if (goal.GetName() == goalName)
            {
                TotalPoints += goal.RecordAchievement();
                Console.WriteLine($"Recorded achievement for {goalName}. Total Points: {TotalPoints}");
                return;
            }
        }
        Console.WriteLine("Goal not found.");
    }

    public void DisplayGoals()
    {
        Console.WriteLine($"{Username}'s Goals:");
        foreach (var goal in Goals)
        {
            Console.WriteLine(goal.GetStatus());
        }
        Console.WriteLine($"Total Points: {TotalPoints}");
    }

    // Method to update total points
    public void UpdateTotalPoints(int points)
    {
        TotalPoints = points;
    }
}

class EternalQuest
{
    private Dictionary<string, User> users;
    private User currentUser;

    public EternalQuest()
    {
        users = new Dictionary<string, User>();
    }

    public void CreateUser(string username)
    {
        if (!users.ContainsKey(username))
        {
            users[username] = new User(username);
            Console.WriteLine($"User {username} created successfully.");
        }
        else
        {
            Console.WriteLine($"User {username} already exists.");
        }
    }

    public void LoginUser(string username)
    {
        if (users.ContainsKey(username))
        {
            currentUser = users[username];
            Console.WriteLine($"Welcome back, {username}!");
        }
        else
        {
            Console.WriteLine($"User {username} does not exist. Please create an account.");
        }
    }

    public void AddGoal(Goal goal)
    {
        currentUser?.AddGoal(goal);
    }

    public void DisplayGoals() => currentUser?.DisplayGoals();

    public void RecordGoal(string goalName)
    {
        currentUser?.RecordGoal(goalName);
    }

    public void SaveGoals(string filename)
    {
        using (StreamWriter sw = new StreamWriter(filename))
        {
            foreach (var user in users)
            {
                sw.WriteLine($"{user.Key},{user.Value.TotalPoints}");
                foreach (var goal in user.Value.Goals)
                {
                    sw.WriteLine($"{user.Value.Username},{goal.GetName()},{goal.IsCompleted()}");
                }
            }
        }
        Console.WriteLine("Goals saved.");
    }

    public void LoadGoals(string filename)
    {
        if (File.Exists(filename))
        {
            using (StreamReader sr = new StreamReader(filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        // User information
                        string username = parts[0];
                        int points = int.Parse(parts[1]);
                        CreateUser(username); // Ensure the user is created
                        users[username].UpdateTotalPoints(points); // Load points
                    }
                    else if (parts.Length == 3)
                    {
                        // Goal information
                        string username = parts[0];
                        string goalName = parts[1];
                        bool completed = bool.Parse(parts[2]);

                        if (users.ContainsKey(username))
                        {
                            // Using SimpleGoal for loading as a placeholder; you can customize this further as necessary
                            users[username].AddGoal(new SimpleGoal(goalName, 0)); 
                            if (completed)
                            {
                                users[username].RecordGoal(goalName);
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Goals loaded.");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
}

class Program
{
    static void Main()
    {
        EternalQuest quest = new EternalQuest();
        
        // User creation and login
        Console.Write("Enter a username to create or login: ");
        string username = Console.ReadLine();
        quest.CreateUser(username);
        quest.LoginUser(username);

        // Add goals for the logged in user
        quest.AddGoal(new SimpleGoal("Run a marathon", 1000));
        quest.AddGoal(new EternalGoal("Read Scriptures", 100));
        quest.AddGoal(new ChecklistGoal("Attend the temple", 50, 10));

        while (true)
        {
            Console.WriteLine("\n1. Display Goals\n2. Record Goal Achievement\n3. Save Goals\n4. Load Goals\n5. Exit");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    quest.DisplayGoals();
                    break;
                case "2":
                    Console.Write("Enter goal name to record achievement: ");
                    string goalName = Console.ReadLine();
                    quest.RecordGoal(goalName);
                    break;
                case "3":
                    quest.SaveGoals("goals.txt");
                    break;
                case "4":
                    quest.LoadGoals("goals.txt");
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}