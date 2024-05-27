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

        return previousNodePossibilities;
    }

    public string TransformNodeToSymbol(char node)
    {
        if (node == ' ')
            return " ";

        var searchNode = node.ToString().ToUpper();
        string result = "";

        while (!string.IsNullOrEmpty(searchNode))
        {
            Node? currentNode = null;

            currentNode = _morseCodeNodes.FirstOrDefault(x => x.Left == searchNode || x.Right == searchNode);
            if (currentNode != null)
            {
                result += currentNode.Left == searchNode ? 
                    MorseCode.Symbols.DOT_SYMBOL :
                    MorseCode.Symbols.DASH_SYMBOL;

                searchNode = currentNode.Parent; 
                continue;
            }

            searchNode = string.Empty;
        }

        return result;
    }

    private string BuildNodeSymbolsRecursive(string result, string? searchNode)
    {
        if (string.IsNullOrEmpty(searchNode))
            return result;

        var foundNode = _morseCodeNodes.FirstOrDefault(x => x.Left == searchNode || x.Right == searchNode);

        if (foundNode == null)
            return result;

        result += foundNode.Left == searchNode ?
            MorseCode.Symbols.DOT_SYMBOL :
            MorseCode.Symbols.DASH_SYMBOL;

        return BuildNodeSymbolsRecursive(result, foundNode.Parent);
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
