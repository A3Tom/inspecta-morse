using DichotomicSearch.Application.Models;

namespace DichotomicSearch.Application.Classes;
public class SearchService
{
    public SearchService(ICollection<Node> morseCodeNodes)
    {
        _morseCodeNodes = morseCodeNodes;
    }

    private readonly ICollection<Node> _morseCodeNodes;

    public string[] TransformSymbolsToNodes(string signals)
    {
        var previousNodePossibilities = new string[] { string.Empty };

        for (int i = 0; i < signals.Length; i++)
        {
            var validNodes = GetValidNodes(_morseCodeNodes, previousNodePossibilities);
            var currentNodePossibilities = new List<string>();

            foreach (var node in validNodes)
            {
                if (DestinationNodeIsNotEmpty(node.Left) && IsDotOrUnkown(signals[i]))
                    currentNodePossibilities.Add(node.Left!);

                if (DestinationNodeIsNotEmpty(node.Right) && IsDashOrUnkown(signals[i]))
                    currentNodePossibilities.Add(node.Right!);
            }
            previousNodePossibilities = currentNodePossibilities.ToArray();
        }

        return previousNodePossibilities.ToArray();
    }

    private static bool IsDotOrUnkown(char signal) =>
        signal == MorseCode.Symbols.DOT_SYMBOL ||
        signal == MorseCode.Symbols.UNKNOWN_SYMBOL;

    private static bool IsDashOrUnkown(char signal) =>
        signal == MorseCode.Symbols.DASH_SYMBOL ||
        signal == MorseCode.Symbols.UNKNOWN_SYMBOL;

    private static bool DestinationNodeIsNotEmpty(string? destination) => 
        !string.IsNullOrEmpty(destination);

    private static IEnumerable<Node> GetValidNodes(ICollection<Node> morseCodeTree, string[] parents) => 
        morseCodeTree.Where(x => parents.Contains(x.Parent));
}
