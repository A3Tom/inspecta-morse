using DichotomicSearch.Application.Classes;
using DichotomicSearch.Application;

namespace dichotomic_search;
public class HuffmanEncodingProgram
{
    public static void Run()
    {
        var nodeTree = NodeTreeBuilder.LoadYamlFileFromRelativePath(Settings.TEST_HUFFMAN_TREE_RELATIVE_PATH);


    }
}
