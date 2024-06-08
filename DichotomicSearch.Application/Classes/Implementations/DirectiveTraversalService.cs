using DichotomicSearch.Application.Classes.Implementations.Abstract;
using DichotomicSearch.Application.Models;

namespace DichotomicSearch.Application.Classes.Implementations;
public class DirectiveTraversalService(ICollection<Node> treeNodes) : NodeTraversalSolver(treeNodes), INodeTraversalSolver
{
    public string[] TransformSymbolsToNodes(string signals)
    {
        var currentNode = _treeNodes.First();

        foreach (var directive in signals)
        {
            currentNode = directive switch
            {
                '0' => GetLeftNode(currentNode),
                '1' => GetRightNode(currentNode),
                _ => throw new ArgumentException($"{directive} is not a valid directive - found in {signals}"),
            };
        }

        return [ currentNode.Key! ];
    }

    public string TransformNodeToSymbol(char node) => BuildNodeSymbols(string.Empty, node.ToString().ToUpper());

    private string BuildNodeSymbols(string result, string? searchNode)
    {
        if (string.IsNullOrEmpty(searchNode))
            return result;

        var foundNode = _treeNodes.FirstOrDefault(x => x.Left == searchNode || x.Right == searchNode);

        if (foundNode == null)
            return result;

        var huffmanCode = foundNode.Left == searchNode ? '0' : '1';

        result = result.Insert(0, huffmanCode.ToString());

        return BuildNodeSymbols(result, foundNode.Key);
    }

    private Node GetLeftNode(Node parentNode) => _treeNodes.First(x => x.Key == parentNode.Left);
    private Node GetRightNode(Node parentNode) => _treeNodes.First(x => x.Key == parentNode.Right);
}
