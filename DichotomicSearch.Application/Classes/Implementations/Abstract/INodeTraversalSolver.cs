namespace DichotomicSearch.Application.Classes.Implementations.Abstract;
public interface INodeTraversalSolver
{
    string[] TransformSymbolsToNodes(string signals);
    string TransformNodeToSymbol(char node);
}
