using DichotomicSearch.Application.Classes;
using DichotomicSearch.Application.Classes.Implementations;

var morseCodeNodes = NodeTreeBuilder.LoadFileFromRelativePath();
Console.WriteLine("Loaded morse code tree!");

var _searchService = new SearchTraversalService(morseCodeNodes);

FormatMorseOutput(".-.");
FormatMorseOutput(".");
FormatMorseOutput("--");
FormatMorseOutput("---");

FormatMorseSentenceOutput(".-. . -- ---");
Console.WriteLine("Right, your turn ya arsepiece\n");

var userInput = Console.ReadLine();

while(!string.IsNullOrEmpty(userInput))
{
    if (userInput[0] == '.' || userInput[0] == '-')
    {
        if (userInput.Contains(' '))
            FormatMorseSentenceOutput(userInput);
        else
            FormatMorseOutput(userInput);
    }
    else
    {
        var morseOutput = userInput.Select(_searchService.TransformNodeToSymbol).ToList();

        Console.WriteLine($"[{userInput}] {string.Join(" ", morseOutput)}");
    }

    userInput = Console.ReadLine();
}

Console.WriteLine("\nCheerio then");

void FormatMorseSentenceOutput(string input)
{
    var output = input
        .Split(' ')
        .Select(FormatSentenceSymbols)
        .ToList()
        .Aggregate((a, b) => $"{a}{b}");

    Console.WriteLine(output);
}

string FormatSentenceSymbols(string input)
{
    if (input == "")
        return " ";

    var text = _searchService.TransformSymbolsToNodes(input);

    return text.Length == 1 ?
        text[0] :
        $"[{string.Join(',', text)}]";
}

void FormatMorseOutput(string input) =>
    Console.WriteLine("[{0}] {1}",
        input,
        string.Join(' ', TransformSymbolsToText(input))
    );

string TransformSymbolsToText(string input)
{
    var textOutput = _searchService.TransformSymbolsToNodes(input);

    return textOutput.Any() ?
        string.Join(' ', _searchService.TransformSymbolsToNodes(input)) :
        $"Invalid morse code";
}
