using System;

public class TaskNotFoundException : Exception
{
    public TaskNotFoundException(string id) : base("Task " + id + " not found.")
    {
    }
}
