namespace SanAndreasMail.Infra.Helpers
{
    interface IShortestPathFinder
    {
        Node[] FindShortestPath(Node from, Node to);
    }
}
