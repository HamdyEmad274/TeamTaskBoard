using System;
// Team Task Board -- v0 (this is the "before" version)
// Everything lives in Main. No classes for a task. No encapsulation. No exception handling.
// This is deliberately messy -- tonight we refactor it live.

public abstract class TaskItem
{
    public string Id { get; }
    public string Title { get; private set; }
    public bool IsDone { get; private set; }

    protected TaskItem(string id, string title)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("Task id cannot be empty", nameof(id));
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Task title cannot be empty", nameof(title));
        Id = id;
        Title = title;
    }

    public abstract string Label();

    public void MarkDone()
    {
        IsDone = true;
    }
    public void MarkOpen()
    {
        IsDone = false;
    }

    public override string ToString()
    {
        return $"{Label()} {Id} - {Title} {(IsDone ? "✓" : "")}";
    }


}
