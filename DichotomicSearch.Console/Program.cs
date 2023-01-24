// See https://aka.ms/new-console-template for more information
using DichotomicSearch.Application.Models;

Console.WriteLine("Hello, World!");


var hing = MorseCode.Tree.ToDictionary(k => k.Parent, v => new { v.Left, v.Right });
File.WriteAllText("whatever.json", System.Text.Json.JsonSerializer.Serialize(hing));


Console.WriteLine("Done!");
