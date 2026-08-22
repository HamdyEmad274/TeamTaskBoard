public class FeatureTask : TaskItem
{
    public FeatureTask(string id, string title) : base(id, title)
    {
    }

    public override string Label()
    {
        return "[FEATURE]";
    }
}
