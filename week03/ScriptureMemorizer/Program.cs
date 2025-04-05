// For creativity and exceeding requirement, I modified the program to work with a library of scriptures
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorizer
{
    // Class to represent a scripture reference
    public class Reference
    {
        public string Book { get; }
        public int StartChapter { get; }
        public int StartVerse { get; }
        public int? EndVerse { get; }

        // Constructor for single verse reference
        public Reference(string book, int chapter, int verse)
        {
            Book = book;
            StartChapter = chapter;
            StartVerse = verse;
            EndVerse = null;
        }

        // Constructor for range of verses
        public Reference(string book, int chapter, int startVerse, int endVerse)
        {
            Book = book;
            StartChapter = chapter;
            StartVerse = startVerse;
            EndVerse = endVerse;
        }

        public override string ToString()
        {
            return EndVerse.HasValue
                ? $"{Book} {StartChapter}:{StartVerse}-{EndVerse}"
                : $"{Book} {StartChapter}:{StartVerse}";
        }
    }

    // Class to represent a word in the scripture
    public class Word
    {
        public string Text { get; private set; }
        public bool IsHidden { get; private set; }

        public Word(string text)
        {
            Text = text;
            IsHidden = false;
        }

        public string GetDisplayText()
        {
            return IsHidden ? new string('_', Text.Length) : Text;
        }

        public void Hide()
        {
            IsHidden = true;
        }
    }

    // Class to represent the scripture itself
    public class Scripture
    {
        public Reference Reference { get; }
        private List<Word> Words { get; }

        public Scripture(Reference reference, string text)
        {
            Reference = reference;
            Words = text.Split(' ').Select(word => new Word(word)).ToList();
        }

        public void Display()
        {
            Console.WriteLine($"{Reference}\n{string.Join(" ", Words.Select(word => word.GetDisplayText()))}");
        }

        public bool HideRandomWord(Random random)
        {
            // Check if all words are already hidden
            if (Words.All(w => w.IsHidden))
                return false;

            // Select a random word index
            int index;
            do
            {
                index = random.Next(Words.Count);
            } while (Words[index].IsHidden);

            // Hide the selected word
            Words[index].Hide();
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create a library of scriptures
            List<Scripture> scriptureLibrary = new List<Scripture>
            {
                new Scripture(new Reference("John", 3, 16), "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life."),
                new Scripture(new Reference("Romans", 8, 28), "And we know that in all things God works for the good of those who love him, who have been called according to his purpose."),
                new Scripture(new Reference("Philippians", 4, 13), "I can do all this through him who gives me strength."),
                new Scripture(new Reference("Proverbs", 3, 5, 6), "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight."),
                new Scripture(new Reference("Psalm", 119, 105), "Your word is a lamp for my feet, a light on my path.")
            };

            Random random = new Random();
            bool continueStruggling = true;

            while (continueStruggling)
            {
                // Select a random scripture from the library
                Scripture scripture = scriptureLibrary[random.Next(scriptureLibrary.Count)];

                do
                {
                    Console.Clear();
                    scripture.Display();
                    Console.WriteLine("\nPress Enter to hide a word or type '0' to exit.");
                    string input = Console.ReadLine()?.Trim();

                    if (input?.ToLower() == "0")
                    {
                        continueStruggling = false; // Exit the loop if the user types '0'
                        break; 
                    }

                } while (scripture.HideRandomWord(random)); // Keep hiding words until all are hidden

                // Final display of the selected scripture after all words are hidden
                Console.Clear();
                scripture.Display();
                Console.WriteLine("\nAll words are hidden. Press any key to continue to another scripture or '0' to quit.");
                Console.ReadKey();
            }
        }
    }
}