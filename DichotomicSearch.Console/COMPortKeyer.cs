using DichotomicSearch.Application.Classes.Implementations;
using DichotomicSearch.Application.Classes;
using DichotomicSearch.Application.Models;
using System.IO.Ports;
using DichotomicSearch.Application;

namespace dichotomic_search;
public class COMPortKeyer
{
    private static readonly HashSet<Node> _morseCodeNodes = NodeTreeBuilder.LoadFileFromRelativePath(Settings.MORSE_CODE_TREE_RELATIVE_PATH, FileType.YAML);

    private static readonly SearchTraversalService _morseSearchService = new(_morseCodeNodes);
    private static readonly SerialPort _morseKeyerSerialPort = new(Settings.MORSE_KEYER_COM_PORT);

    public static void Run()
    {
        if (!_morseKeyerSerialPort.IsOpen)
            _morseKeyerSerialPort.Open();

        while (true)
        {
            while (_morseKeyerSerialPort.BytesToRead > 0)
            {
                var incoming = _morseKeyerSerialPort.ReadLine();
                if (incoming.StartsWith("Morse Message : "))
                {
                    var colonidx = incoming.IndexOf(":") + 1;
                    var morseCode = incoming.Replace("\r", "")[colonidx..].Trim().Replace("_", " ");
                    var translation = TranslateSentence(morseCode);

                    Console.WriteLine($"Input: \n{morseCode} \nTranslated : \n{string.Join(" ", translation)}\n");
                }
            }
        }
    }

    private static string TranslateSentence(string morseCode) => 
        string.Join(
            " ",
            morseCode.Split('/')
                .Select(TranslateWord)
                .ToList()
        );

    private static string TranslateWord(string word) => 
        string.Join(
            "", 
            word.Split(" ")
                .Select(x => _morseSearchService.TransformSymbolsToNodes(x).FirstOrDefault() ?? "")
                .Where(x => !string.IsNullOrEmpty(x))
                .ToList()
        );
}
