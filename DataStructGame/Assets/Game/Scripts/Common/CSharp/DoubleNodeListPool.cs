using Game.Scripts.CSharp.Link;

namespace Game.Scripts.Common.CSharp {
    public class DoubleNodeListPool<T> {
        private DoublePoolNode<T> m_first = new DoublePoolNode<T>();
        private int m_count = 0;

        public DoubleLinkedNode<T> GetNodeInstance(T data) {
            if (IsEmpty()) {
                m_first = new DoublePoolNode<T>(new DoubleLinkedNode<T>(data));

                m_count++;
                return m_first.data;
            }

            var currentNode = m_first;
            while (currentNode.next != null) {
                currentNode = currentNode.next;
            }

            currentNode.next = new DoublePoolNode<T>(new DoubleLinkedNode<T>(data));
            m_count++;
            return currentNode.next.data;
        }

        public DoubleLinkedNode<T> GetNodeByData(T data) {
            if (IsEmpty())
                return null;

            var currentNode = _GetPoolNodeByData(data);

            return currentNode.data;
        }

        public DoubleLinkedNode<T> DeleteNodeByData(T data) {
            if (IsEmpty()) {
                return null;
            }

            var currentNode = _GetPoolNodeByData(data);
            var preCurrentNode = _GetPrePoolNodeByData(data);
            preCurrentNode.next = currentNode.next;

            m_count--;

            return currentNode.data;
        }

        private DoublePoolNode<T> _GetPoolNodeByData(T data) {
            
            var currentNode = m_first;
            while (currentNode.next != null) {
                if (currentNode.data.Data.Equals(data)) {
                    return currentNode;
                }

                currentNode = currentNode.next;
            }

            return null;
        }

        private DoublePoolNode<T> _GetPrePoolNodeByData(T data) {
            var currentNode = m_first;
            while (currentNode != null) {
                if (currentNode.next.data.Data.Equals(data)) {
                    return currentNode;
                }

                currentNode = currentNode.next;
            }

            return null;
        }


        public bool IsEmpty() {
            return (m_count == 0);
        }

        public void Clear() {
            m_first = null;
            m_count = 0;
        }
    }

    public class DoublePoolNode<T> {
        public DoubleLinkedNode<T> data { get; set; }

        public DoublePoolNode<T> next { get; set; }

        public DoublePoolNode() {
            
        }

        public DoublePoolNode(DoubleLinkedNode<T> _data) {
            data = _data;
        }
    }
}