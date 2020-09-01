namespace SanAndreasMail.Infra.Helpers
{
    public interface IShortestPathFinder
    {
        Node[] FindShortestPath(Node from, Node to);
    }
}
