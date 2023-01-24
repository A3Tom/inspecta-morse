namespace DichotomicSearch.Application.Models;

public class MorseCode
{
    public class Symbols
    {
        public const char DOT_SYMBOL = '.';
        public const char DASH_SYMBOL = '-';
        public const char UNKNOWN_SYMBOL = '?';
    }

    public static HashSet<Node> Tree =>
        new()
        {
                new Node(string.Empty, "E", "T"),
                new Node("E", "I", "A"),
                new Node("T", "N", "M"),
                new Node("I", "S", "U"),
                new Node("A", "R", "W"),
                new Node("N", "D", "K"),
                new Node("M", "G", "O")
        };
}
