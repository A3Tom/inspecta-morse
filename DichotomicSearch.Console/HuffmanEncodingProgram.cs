using DichotomicSearch.Application.Classes;
using DichotomicSearch.Application;
using DichotomicSearch.Application.Models;
using DichotomicSearch.Application.Classes.Implementations;

namespace dichotomic_search;
public class HuffmanEncodingProgram
{
    public static void Run()
    {
        var exampleText = "eerrtawdggg0987 awdawd 55123 ffa asdwd 3r3 e44444 12 af afafwaww awdawd ";
        var nodeTree = BuildLeafNodes(exampleText);

        var hoffmanTree = BuildHuffmanTree(nodeTree);
        //var nodeTree = NodeTreeBuilder.LoadFileFromRelativePath(Settings.TEST_HUFFMAN_TREE_RELATIVE_PATH, FileType.YAML);
        var traversalService = new DirectiveTraversalService(hoffmanTree);

        var encoding = hoffmanTree.Select(x => traversalService.TransformNodeToSymbol(x.Key!)).ToList();

        var encodedString = string.Join("", encoding);
        Console.WriteLine($"Example text bytes : {exampleText.Length * 8}");
        Console.WriteLine($"Huffman Encoded text bytes : {encodedString.Length}");

        decimal compressionRatio = GetCompressionRatio(exampleText, encodedString);
        decimal spaceSavedPct = GetSpaceSavedPercentage(exampleText, encodedString);
        Console.WriteLine($"Compression Ratio: {compressionRatio:N2} | Space Saved: {spaceSavedPct:N2}%");
    }

    private static decimal GetCompressionRatio(string input, string encodedOutput)
    {
        decimal inputBits = input.Length * 8;
        decimal encodedBits = encodedOutput.Length;

        return inputBits / encodedBits;
    }

    private static decimal GetSpaceSavedPercentage(string input, string encodedOutput)
    {
        decimal inputBits = input.Length * 8;
        decimal encodedBits = encodedOutput.Length;

        return (1 - (encodedBits / inputBits)) * 100;
    }

    private static HashSet<Node> BuildHuffmanTree(List<WeightedNode> nodeTree)
    {
        var systemEntropy = nodeTree.Sum(x => x.Weighting);

        var result = new HashSet<Node>();

        while (nodeTree.Count > 1)
        {
            var newNode = new WeightedNode(nodeTree[1], nodeTree[0]);
            result.Add(newNode);
            
            nodeTree.RemoveAt(0);
            nodeTree.RemoveAt(0);

            if (newNode.Weighting == systemEntropy)
                break;

            var insertionIndex = GetWeightedNodeInsertIndex(nodeTree, newNode.Weighting);
            nodeTree.Insert(insertionIndex, newNode);
        }

        return result;
    }

    private static int GetWeightedNodeInsertIndex(List<WeightedNode> treeNodes, int weighting)
    {
        if (!treeNodes.Any() || treeNodes.Last().Weighting <= weighting)
            return treeNodes.Count - 1;

        for (int i = 0; i < treeNodes.Count; i++)
            if (treeNodes[i].Weighting > weighting)
                return i;

        return 0;
    }


    private static List<WeightedNode> BuildLeafNodes(string text)
        => text.GroupBy(g => g)
            .Select(x => new WeightedNode(x.Key.ToString(), x.Count()))
            .OrderBy(x => x.Weighting)
            .ToList();
}
