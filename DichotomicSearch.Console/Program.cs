// See https://aka.ms/new-console-template for more information
using DichotomicSearch.Application.Models;
using System.Reflection;
using System.Text;
using System.Text.Json;

const string ROOT_DIR_NAME = "dichotomic-search";
const string MORSE_CODE_TREE_RELATIVE_PATH = @"resources/morse-coded-tree.json";

Console.WriteLine("Hello, World!");

var fileName = GetFilePath();

string jsonString = File.ReadAllText(fileName, Encoding.UTF8);
var morseCodeTree = JsonSerializer.Deserialize<HashSet<Node>>(jsonString)!;

foreach (var node in morseCodeTree)
    Console.WriteLine($"[{node.Parent}] : {node.Left} | {node.Right}");

Console.WriteLine("Loaded morse code tree!");



static string GetFilePath()
{
    var runLocation = Assembly.GetExecutingAssembly().Location;
    var folderLocationIdx = runLocation.IndexOf(ROOT_DIR_NAME);
    return $"{runLocation[..(folderLocationIdx + ROOT_DIR_NAME.Length)]}/{MORSE_CODE_TREE_RELATIVE_PATH}";
}