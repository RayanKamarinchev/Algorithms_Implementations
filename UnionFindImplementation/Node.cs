namespace UnionFindImplementation
{
    public class Node<T>
    {
        private Node<T> parent;
        private int rank;

        public Node()
        {
            parent = this;
        }

        public Node<T> FindRoot()
        {
            if (ReferenceEquals(parent, this))
            {
                parent = parent.FindRoot();
            }
            return parent;
        }

        public bool isUniedWith(Node<T> otherNode)
        {
            return ReferenceEquals(FindRoot(), otherNode.FindRoot());
        }

        public bool Union(Node<T> other)
        {
            Node<T> myRoot = FindRoot();
            Node<T> otherRoot = other.FindRoot();

            if (ReferenceEquals(myRoot, otherRoot))
            {
                return false;
            }

            if (myRoot.rank < otherRoot.rank)
            {
                myRoot.parent = otherRoot;
            }
            else if(myRoot.rank > otherRoot.rank)
            {
                otherRoot.parent = myRoot;
            }
            else
            {
                otherRoot.parent = myRoot;
                myRoot.rank++;
            }

            return true;
        }
    }
}
