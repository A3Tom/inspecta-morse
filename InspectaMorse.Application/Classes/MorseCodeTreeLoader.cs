using InspectaMorse.Application;
using InspectaMorse.Application.Models;
using System.Reflection;
using System.Text;
using System.Text.Json;
using YamlDotNet.Serialization;

namespace InspectaMorse.Application.Classes;
public class NodeTreeBuilder
{
    public static HashSet<Node> LoadFileFromRelativePath(string relativePath, FileType fileType)
        => fileType == FileType.JSON ?
            LoadJsonFileFromRelativePath(relativePath) :
            LoadYamlFileFromRelativePath(relativePath);

    private static HashSet<Node> LoadYamlFileFromRelativePath(string relativePath)
    {
        var fullFilePath = $"{GetBaseFilePath()}/{relativePath}.yaml";

        string yamlString = File.ReadAllText(fullFilePath, Encoding.UTF8);
        var yamlDeserializer = new DeserializerBuilder().Build();

        return yamlDeserializer.Deserialize<HashSet<Node>>(yamlString);
    }

    public static void SaveNodeTreeAsYamlFile(HashSet<Node> nodes, string filename)
    {
        var yamlSerializer = new SerializerBuilder().Build();
        var outputYaml = yamlSerializer.Serialize(nodes);

        var filepath = $"{GetBaseFilePath()}/{filename}.yaml";
        File.Delete(filepath);
        File.AppendAllText(filepath, outputYaml);
    }

    private static HashSet<Node> LoadJsonFileFromRelativePath(string relativePath)
    {
        var fileName = $"{GetBaseFilePath()}/{relativePath}.json";

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
