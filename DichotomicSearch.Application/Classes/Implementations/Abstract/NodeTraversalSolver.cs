using DichotomicSearch.Application.Models;

namespace DichotomicSearch.Application.Classes.Implementations.Abstract;

public abstract class NodeTraversalSolver(ICollection<Node> treeNodes)
{
    internal readonly ICollection<Node> _treeNodes = treeNodes;
}
