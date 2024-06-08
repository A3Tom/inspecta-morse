using DichotomicSearch.Application.Classes;
using DichotomicSearch.Application;
using DichotomicSearch.Application.Models;

namespace dichotomic_search;
public class HuffmanEncodingProgram
{
    public static void Run()
    {
        var nodeTree = NodeTreeBuilder.LoadFileFromRelativePath(Settings.TEST_HUFFMAN_TREE_RELATIVE_PATH, FileType.YAML);


    }
}
