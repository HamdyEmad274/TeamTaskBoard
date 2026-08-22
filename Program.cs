using System;
using System.Collections.Generic;

// Team Task Board -- v0 (this is the "before" version)
// Everything lives in Main. No classes for a task. No encapsulation. No exception handling.
// This is deliberately messy -- tonight we refactor it live.

class Program
{
    // Public, mutable, global state -- anyone anywhere could change these directly.
    public static List<string> taskIds = new List<string>();
    public static List<string> taskTitles = new List<string>();
    public static List<string> taskTypes = new List<string>(); // "bug", "feature", "chore"
    public static List<bool> taskDone = new List<bool>();

    static void Main(string[] args)
    {
        // Add a few tasks -- notice the four parallel lists have to be kept in sync by hand.
        AddTask("T1", "Login page throws on empty password", "bug");
        AddTask("T2", "Add dark mode toggle", "feature");
        AddTask("T3", "Update NuGet packages", "chore");
        AddTask("T4", "Null reference on checkout", "bug");

        ShowBoard();

        Console.WriteLine();
        Console.WriteLine("Completing T2...");
        CompleteTask("T2");

        Console.WriteLine();
        Console.WriteLine("Completing T5 (doesn't exist)...");
        CompleteTask("T5"); // <-- this will crash the whole program. That's the point.

        Console.WriteLine("You will never see this line print.");
    }

    static void AddTask(string id, string title, string type)
    {
        taskIds.Add(id);
        taskTitles.Add(title);
        taskTypes.Add(type);
        taskDone.Add(false);
    }

    static void CompleteTask(string id)
    {
        int index = -1;
        for (int i = 0; i < taskIds.Count; i++)
        {
            if (taskIds[i] == id)
            {
                index = i;
                break;
            }
        }
        // No check for index == -1 here -- if the id doesn't exist, the next line
        // throws ArgumentOutOfRangeException and the whole program dies.
        taskDone[index] = true;
        Console.WriteLine("Marked " + taskIds[index] + " as done.");
    }

    static void ShowBoard()
    {
        Console.WriteLine("=== Team Task Board ===");
        for (int i = 0; i < taskIds.Count; i++)
        {
            string prefix;
            // Same three-way "what type is this" branch, copy-pasted everywhere this
            // curriculum needs to print a task -- a smell for inheritance/polymorphism.
            if (taskTypes[i] == "bug")
            {
                prefix = "[BUG]";
            }
            else if (taskTypes[i] == "feature")
            {
                prefix = "[FEATURE]";
            }
            else
            {
                prefix = "[CHORE]";
            }
            string status = taskDone[i] ? "(done)" : "(open)";
            Console.WriteLine(prefix + " " + taskIds[i] + " - " + taskTitles[i] + " " + status);
        }
    }
}
