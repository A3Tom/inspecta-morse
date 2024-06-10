using DichotomicSearch.Application.Classes.Implementations;
using DichotomicSearch.Application.Models;

namespace dichotomic_search;
public class HuffmanEncodingProgram
{
    public static void Run()
    {
        var exampleText = "eee11eeh51h524uu88toooiocca~-11f1f";
        var nodeTree = BuildLeafNodes(exampleText);

        var hoffmanTree = BuildHuffmanTree(nodeTree);
        //var nodeTree = NodeTreeBuilder.LoadFileFromRelativePath(Settings.TEST_HUFFMAN_TREE_RELATIVE_PATH, FileType.YAML);
        var traversalService = new DirectiveTraversalService(hoffmanTree);

        var leafNodes = BuildLeafNodes(exampleText);

        var encodingDict = leafNodes
            .ToDictionary(
                k => k.Key!,
                v => traversalService.TransformNodeToSymbol(v.Key!)
            );

        foreach (var encodingKey in encodingDict.OrderBy(x => x.Value.Length))
            Console.WriteLine($"[{encodingKey.Key}] {encodingKey.Value}");

        var encoding = exampleText.Select(x => encodingDict[x.ToString()]);
        var encodedString = string.Join("", encoding);

        Console.WriteLine($"Original string: {exampleText}");
        Console.WriteLine($"Encoded string: {encodedString}");

        Console.WriteLine("Stats:\n");
        Console.WriteLine($"Example text bits : {exampleText.Length * 8}");
        Console.WriteLine($"Huffman Encoded text bits : {encodedString.Length}");

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
