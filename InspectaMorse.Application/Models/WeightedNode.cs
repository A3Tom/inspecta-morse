namespace InspectaMorse.Application.Models;
public class WeightedNode : Node
{
    public int Weighting { get; init; }

    public WeightedNode(string key, int weighting)
    {
        Key = key;
        Weighting = weighting;
    }

    public WeightedNode(WeightedNode leftNode, WeightedNode rightNode)
    {
        Key = leftNode.Key + rightNode.Key;
        Weighting = leftNode.Weighting + rightNode.Weighting;

        Left = leftNode.Key;
        Right = rightNode.Key;
    }
}
