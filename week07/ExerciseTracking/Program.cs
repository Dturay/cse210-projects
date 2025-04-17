using System;
using System.Collections.Generic;

// Base class representing a generic Activity
public abstract class Activity
{
    // Shared attributes
    private DateTime _date;
    private int _minutes;

    // Constructor
    public Activity(DateTime date, int minutes)
    {
        _date = date;
        _minutes = minutes;
    }

    // Public property to access the Minutes
    public int Minutes
    {
        get { return _minutes; }
    }

    // Abstract methods for distance, speed, and pace calculations
    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    // Summary method for displaying activity details
    public virtual string GetSummary()
    {
        return $"{_date:dd MMM yyyy} {GetType().Name} ({Minutes} min)";
    }
}





// Derived class for Running activity
public class Running : Activity
{
    private double _distance; // Distance in miles

    // Constructor
    public Running(DateTime date, int minutes, double distance)
        : base(date, minutes)
    {
        _distance = distance;
    }

    // Get distance
    public override double GetDistance()
    {
        return _distance;
    }

    // Calculate speed in mph
    public override double GetSpeed()
    {
        return (GetDistance() / Minutes) * 60.0; // Speed in mph
    }

    // Calculate pace in minutes per mile
    public override double GetPace()
    {
        return Minutes / GetDistance(); // Pace in min per mile
    }

    // Summary override to include running specifics
    public override string GetSummary()
    {
        return base.GetSummary() +
               $" - Distance: {GetDistance()} miles, Speed: {GetSpeed()} mph, Pace: {GetPace()} min per mile";
    }
}





// Derived class for Cycling activity
public class Cycling : Activity
{
    private double _speed; // Speed in mph

    // Constructor
    public Cycling(DateTime date, int minutes, double speed)
        : base(date, minutes)
    {
        _speed = speed;
    }

    // Calculate distance based on speed
    public override double GetDistance()
    {
        return (GetSpeed() / 60.0) * Minutes; // Distance in miles
    }

    // Return speed
    public override double GetSpeed()
    {
        return _speed; // Speed already set
    }

    // Calculate pace in minutes per mile
    public override double GetPace()
    {
        return 60.0 / GetSpeed(); // Pace in min per mile
    }

    // Summary override to include cycling specifics
    public override string GetSummary()
    {
        return base.GetSummary() +
               $" - Distance: {GetDistance()} miles, Speed: {GetSpeed()} mph, Pace: {GetPace()} min per mile";
    }
}





// Derived class for Swimming activity
public class Swimming : Activity
{
    private int _laps; // Number of laps

    // Constructor
    public Swimming(DateTime date, int minutes, int laps)
        : base(date, minutes)
    {
        _laps = laps;
    }

    // Calculate distance in kilometers
    public override double GetDistance()
    {
        return (_laps * 50.0) / 1000.0; // Distance in kilometers
    }

    // Calculate speed in kph
    public override double GetSpeed()
    {
        return (GetDistance() / Minutes) * 60.0; // Speed in kph
    }

    // Calculate pace in minutes per kilometer
    public override double GetPace()
    {
        return Minutes / GetDistance(); // Pace in min per km
    }

    // Summary override to include swimming specifics
    public override string GetSummary()
    {
        return base.GetSummary() +
               $" - Distance: {GetDistance()} km, Speed: {GetSpeed()} kph, Pace: {GetPace()} min per km";
    }
}





// Main program class
class Program
{
    static void Main(string[] args)
    {
        // List to hold activities
        List<Activity> activities = new List<Activity>();

        // Create activities
        activities.Add(new Running(new DateTime(2022, 11, 3), 30, 3.0));
        activities.Add(new Cycling(new DateTime(2022, 11, 4), 45, 15.0));
        activities.Add(new Swimming(new DateTime(2022, 11, 5), 60, 20));

        // Display summaries of each activity
        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}