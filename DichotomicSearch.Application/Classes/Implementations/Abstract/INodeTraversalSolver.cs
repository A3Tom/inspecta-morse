namespace DichotomicSearch.Application.Classes.Implementations.Abstract;
public interface INodeTraversalSolver
{
    string TransformNodeToSymbol(char node);
    string[] TransformSymbolsToNodes(string signals);
}
