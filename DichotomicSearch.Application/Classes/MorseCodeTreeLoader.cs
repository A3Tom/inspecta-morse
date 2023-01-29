using DichotomicSearch.Application.Models;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace DichotomicSearch.Application.Classes;
public class MorseCodeTreeBuilder
{
    // Well ain't this specific ...
    public static HashSet<Node> LoadFileFromRelativePath()
    {
        var fileName = GetFilePath();

        string jsonString = File.ReadAllText(fileName, Encoding.UTF8);
        return JsonSerializer.Deserialize<HashSet<Node>>(jsonString)!;
    }

    private static string GetFilePath()
    {
        var runLocation = Assembly.GetExecutingAssembly().Location;
        var folderLocationIdx = runLocation.IndexOf(Settings.ROOT_DIR_NAME);
        return $"{runLocation[..(folderLocationIdx + Settings.ROOT_DIR_NAME.Length)]}/{Settings.MORSE_CODE_TREE_RELATIVE_PATH}";
    }
}
