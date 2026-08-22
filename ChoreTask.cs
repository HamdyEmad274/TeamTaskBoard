public class ChoreTask : TaskItem
{
    public ChoreTask(string id, string title) : base(id, title)
    {
    }

    public override string Label()
    {
        return "[CHORE]";
    }
}
