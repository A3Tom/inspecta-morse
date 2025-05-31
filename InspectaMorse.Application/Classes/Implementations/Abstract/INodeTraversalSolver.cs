namespace InspectaMorse.Application.Classes.Implementations.Abstract;
public interface INodeTraversalSolver
{
    string TransformNodeToSymbol(string node);
    string[] TransformSymbolsToNodes(string signals);
}
