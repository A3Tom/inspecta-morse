using DichotomicSearch.Application.Models;

namespace DichotomicSearch.Application;
public class Search
{
    //public static string[] TransformSymbolsToNodes(string signals)
    //{
    //    var previousNodePossibilities = new string[] { string.Empty };

    //    for (int i = 0; i < signals.Length; i++)
    //    {
    //        var nodes = GetValidNodes(previousNodePossibilities);
    //        var currentNodePossibilities = new List<string>();

    //        foreach (var node in nodes)
    //        {
    //            if (signals[i] == DOT_SIGNAL || signals[i] == UNKNOWN_SIGNAL)
    //                currentNodePossibilities.Add(node.Left);

    //            if (signals[i] == DASH_SIGNAL || signals[i] == UNKNOWN_SIGNAL)
    //                currentNodePossibilities.Add(node.Right);
    //        }
    //        previousNodePossibilities = currentNodePossibilities.ToArray();
    //    }

    //    return previousNodePossibilities.ToArray();
    //}

    //private static IEnumerable<Node> GetValidNodes(string[] parents)
    //        => MorseCode.Tree.Where(x => parents.Contains(x.Parent));
}
