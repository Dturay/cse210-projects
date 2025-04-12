using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create a list of videos
        List<Video> videos = new List<Video>();

        // Create video instances and add comments
        Video video1 = new Video("Learn C# in 10 Minutes", "TechGuru", 600);
        video1.AddComment("Alice", "Great tutorial, very clear!");
        video1.AddComment("Bob", "Super helpful, thanks!");
        video1.AddComment("Charlie", "I love the pacing of this video.");

        Video video2 = new Video("Top 10 Coding Tips", "CodeMaster", 900);
        video2.AddComment("Dave", "These tips are awesome!");
        video2.AddComment("Eve", "I learned so much from this.");
        video2.AddComment("Frank", "Thanks for sharing!");

        Video video3 = new Video("Understanding Design Patterns", "DevPro", 1200);
        video3.AddComment("Grace", "Amazing explanation!");
        video3.AddComment("Hank", "Can you do one on Singleton pattern?");
        video3.AddComment("Ivy", "This is gold!");

        // Add videos to the list
        videos.Add(video1);
        videos.Add(video2);
        videos.Add(video3);

        // Display information about each video
        foreach (Video video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.Length} seconds");
            Console.WriteLine($"Number of Comments: {video.GetCommentCount()}");
            Console.WriteLine("Comments:");
            foreach (var comment in video.GetComments())
            {
                Console.WriteLine($"- {comment.Name}: {comment.Text}");
            }
            Console.WriteLine();
        }
    }
}

class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; }
    private List<Comment> comments;

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        comments = new List<Comment>();
    }

    public void AddComment(string name, string text)
    {
        comments.Add(new Comment(name, text));
    }

    public int GetCommentCount()
    {
        return comments.Count;
    }

    public List<Comment> GetComments()
    {
        return comments;
    }
}

class Comment
{
    public string Name { get; set; }
    public string Text { get; set; }

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }
}