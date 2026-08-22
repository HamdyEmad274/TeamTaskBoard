using System;
using System.Collections.Generic;


public class TaskBoard
{
    private readonly List<TaskItem> _tasks = new();
    private readonly Dictionary<string, TaskItem> _byId = new();
    private readonly Queue<TaskItem> _pending = new();
    private readonly Stack<string> _recentlyCompleted = new();

    public void CompleteTask(string id)
    {
        if (!_byId.TryGetValue(id, out var task))
            throw new TaskNotFoundException(id);
        task.MarkDone();
        _recentlyCompleted.Push(id);
        Console.WriteLine($"Marked {id} as done.");
    }

    public void UndoLastCompletion()
    {
        if (_recentlyCompleted.Count == 0)
        {
            Console.WriteLine("Nothing to undo");
            return;
        }

        var id = _recentlyCompleted.Pop();
        _byId[id].MarkOpen();
        Console.WriteLine($"Undid completion of {id} - it's now open again");
    }

    public void AddTask(TaskItem task)
    {
        _tasks.Add(task);
        _byId.Add(task.Id, task);
        _pending.Enqueue(task);
        Console.WriteLine($"Added task {task.Id} - {task.Title}");
    }

    public void ShowBoard()
    {
        Console.WriteLine("====== TEAM TASK BOARD======");
        foreach (var task in _tasks)
        {
            Console.WriteLine(task);
        }
    }

    public TaskItem? PullNextPending()
    {
        while (_pending.Count > 0)
        {
            var next = _pending.Dequeue();
            if (!next.IsDone)
                return next;
        }
        return null;
    }

}

class Program
{
    // Public, mutable, global state -- anyone anywhere could change these directly.
    //public static List<string> taskIds = new List<string>();
    //public static List<string> taskTitles = new List<string>();
    //public static List<string> taskTypes = new List<string>(); // "bug", "feature", "chore"
    //public static List<bool> taskDone = new List<bool>();

    static void Main(string[] args)
    {
        //Add a few tasks --notice the four parallel lists have to be kept in sync by hand.

        TaskBoard board = new();

        board.AddTask(new BugTask("T1", "Login page throws on empty password"));
        board.AddTask(new FeatureTask("T2", "Add dark mode toggle"));
        board.AddTask(new ChoreTask("T3", "Update NuGet packages"));
        board.AddTask(new BugTask("T4", "Null reference on checkout"));

        board.ShowBoard();

        Console.WriteLine();
        Console.WriteLine("Completing T2...");
        board.CompleteTask("T2");

        Console.WriteLine();
        Console.WriteLine("Undo last completion");
        board.UndoLastCompletion();

        Console.WriteLine();
        Console.WriteLine("Completing T5 (doesn't exist)...");
        try
        {
            board.CompleteTask("T5"); // <-- this will crash the whole program. That's the point.

        }
        catch (TaskNotFoundException ex)
        {
            Console.WriteLine("Couldn't complete the " + ex.Message);
        }
        Console.WriteLine("You will never see this line print.");

        Console.WriteLine();
        var next = board.PullNextPending();
        Console.WriteLine("Next pending task" + (next != null ? next.ToString() : "none"));

        if (next != null)
        {
            board.CompleteTask(next.Id);
        }

        Console.WriteLine();
        board.ShowBoard();

        Console.WriteLine("Added New Line Different");
        Console.WriteLine("Added New Line with extra");

    }
}
