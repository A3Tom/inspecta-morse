using DichotomicSearch.Application.Classes;

var morseCodeNodes = MorseCodeTreeBuilder.LoadFileFromRelativePath();
Console.WriteLine("Loaded morse code tree!");

var _searchService = new SearchService(morseCodeNodes);

FormatOutput(".-.");
FormatOutput(".");
FormatOutput("--");
FormatOutput("---");

void FormatOutput(string input) =>
    Console.WriteLine("[{0}] {1}",
        input,
        string.Join(' ', _searchService.TransformSymbolsToNodes(input))
    );
