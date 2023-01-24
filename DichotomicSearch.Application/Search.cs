using DichotomicSearch.Application.Models;

namespace DichotomicSearch.Application;
public static class Search
{
    public static string[] TransformSymbolsToNodes(ICollection<Node> morseCodeTree, string signals)
    {
        var previousNodePossibilities = new string[] { string.Empty };

        for (int i = 0; i < signals.Length; i++)
        {
            var nodes = GetValidNodes(morseCodeTree, previousNodePossibilities);
            var currentNodePossibilities = new List<string>();

            foreach (var node in nodes)
            {
                if (signals[i] == MorseCode.Symbols.DOT_SYMBOL || signals[i] == MorseCode.Symbols.UNKNOWN_SYMBOL)
                    currentNodePossibilities.Add(node.Left);

                if (signals[i] == MorseCode.Symbols.DASH_SYMBOL || signals[i] == MorseCode.Symbols.UNKNOWN_SYMBOL)
                    currentNodePossibilities.Add(node.Right);
            }
            previousNodePossibilities = currentNodePossibilities.ToArray();
        }

        return previousNodePossibilities.ToArray();
    }

    private static IEnumerable<Node> GetValidNodes(ICollection<Node> morseCodeTree, string[] parents)
            => morseCodeTree.Where(x => parents.Contains(x.Parent));
}
