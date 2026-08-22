public class BugTask : TaskItem
{
    public BugTask(string id, string title) : base(id, title)
    {
    }

    public override string Label()
    {
        return "[BUG]";
    }
}
