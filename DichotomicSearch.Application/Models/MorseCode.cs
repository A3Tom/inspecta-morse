namespace DichotomicSearch.Application.Models;

public class MorseCode
{
    public class Symbols
    {
        public const string DOT_SYMBOL = ".";
        public const string DASH_SYMBOL = "-";
        public const string UNKNOWN_SYMBOL = "?";
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
