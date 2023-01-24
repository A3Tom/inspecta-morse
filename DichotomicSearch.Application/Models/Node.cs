namespace DichotomicSearch.Application.Models;

public class Node
{
    public Node(string parent, string left, string right)
    {
        Parent = parent;
        Left = left;
        Right = right;
    }

    public readonly string Parent;
    public readonly string Left;
    public readonly string Right;
}
