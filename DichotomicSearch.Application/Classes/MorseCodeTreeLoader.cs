using DichotomicSearch.Application.Models;
using System.Reflection;
using System.Text;
using System.Text.Json;
using YamlDotNet.Serialization;

namespace DichotomicSearch.Application.Classes;
public class NodeTreeBuilder
{
    public static HashSet<Node> LoadYamlFileFromRelativePath(string relativePath)
    {
        var fullFilePath = $"{GetBaseFilePath()}/{relativePath}";
        
        string yamlString = File.ReadAllText(fullFilePath, Encoding.UTF8);
        var yamlDeserializer = new DeserializerBuilder().Build();

        return yamlDeserializer.Deserialize<HashSet<Node>>(yamlString);
    }

    public static void SaveNodeTreeAsYamlFile(HashSet<Node> nodes, string filename)
    {
        var yamlSerializer = new SerializerBuilder().Build();
        var outputYaml = yamlSerializer.Serialize(nodes);

        var filepath = $"{GetBaseFilePath()}/{filename}.yaml";
        File.AppendAllText(filepath, outputYaml);
    }

    public static HashSet<Node> LoadJsonFileFromRelativePath(string relativePath)
    {
        var fileName = $"{GetBaseFilePath()}/{relativePath}";

        string jsonString = File.ReadAllText(fileName, Encoding.UTF8);
        return JsonSerializer.Deserialize<HashSet<Node>>(jsonString)!;
    }

    private static string GetBaseFilePath()
    {
        var runLocation = Assembly.GetExecutingAssembly().Location;
        var folderLocationIdx = runLocation.IndexOf(Settings.ROOT_DIR_NAME);
        return $"{runLocation[..(folderLocationIdx + Settings.ROOT_DIR_NAME.Length)]}";
    }
}
