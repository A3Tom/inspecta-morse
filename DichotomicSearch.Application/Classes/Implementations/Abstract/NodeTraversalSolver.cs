using InspectaMorse.Application.Models;

namespace InspectaMorse.Application.Classes.Implementations.Abstract;

public abstract class NodeTraversalSolver(ICollection<Node> treeNodes)
{
    internal readonly ICollection<Node> _treeNodes = treeNodes;
}
