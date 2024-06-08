using DichotomicSearch.Application.Classes.Implementations.Abstract;
using DichotomicSearch.Application.Models;

namespace DichotomicSearch.Application.Classes.Implementations;

public class SearchTraversalService(ICollection<Node> treeNodes) : NodeTraversalSolver(treeNodes), INodeTraversalSolver
{
    public string[] TransformSymbolsToNodes(string signals)
    {
        var previousNodePossibilities = new string[] { string.Empty };

        for (int i = 0; i < signals.Length; i++)
        {
            var validNodes = GetValidNodes(_treeNodes, previousNodePossibilities);
            var currentNodePossibilities = new List<string>();

            foreach (var node in validNodes)
            {
                if (DestinationNodeIsNotEmpty(node.Left) && IsDotOrUnkown(signals[i]))
                    currentNodePossibilities.Add(node.Left!);

                if (DestinationNodeIsNotEmpty(node.Right) && IsDashOrUnkown(signals[i]))
                    currentNodePossibilities.Add(node.Right!);
            }
            previousNodePossibilities = [.. currentNodePossibilities];
        }

        return previousNodePossibilities;
    }

    public string TransformNodeToSymbol(char node) => node == ' ' ? " " : BuildNodeSymbolsRecursive("", node.ToString().ToUpper());

    private string BuildNodeSymbolsRecursive(string result, string? searchNode)
    {
        if (string.IsNullOrEmpty(searchNode))
            return result;

        var foundNode = _treeNodes.FirstOrDefault(x => x.Left == searchNode || x.Right == searchNode);

        if (foundNode == null)
            return result;

        var morseSymbol = foundNode.Left == searchNode ?
            MorseCodeSymbols.DOT_SYMBOL :
            MorseCodeSymbols.DASH_SYMBOL;

        result = result.Insert(0, morseSymbol.ToString());

        return BuildNodeSymbolsRecursive(result, foundNode.Key);
    }

    private static bool IsDotOrUnkown(char signal) =>
        signal == MorseCodeSymbols.DOT_SYMBOL ||
        signal == MorseCodeSymbols.UNKNOWN_SYMBOL;

    private static bool IsDashOrUnkown(char signal) =>
        signal == MorseCodeSymbols.DASH_SYMBOL ||
        signal == MorseCodeSymbols.UNKNOWN_SYMBOL;

    private static bool DestinationNodeIsNotEmpty(string? destination) =>
        !string.IsNullOrEmpty(destination);

    private static IEnumerable<Node> GetValidNodes(ICollection<Node> morseCodeTree, string[] parents) =>
        morseCodeTree.Where(x => parents.Contains(x.Key));
}
