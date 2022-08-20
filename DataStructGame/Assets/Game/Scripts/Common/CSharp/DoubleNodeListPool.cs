using Game.Scripts.CSharp.Link;
using TMPro;

namespace Game.Scripts.Common.CSharp {
    public class DoubleNodeListPool<T> {
        //private DoublePoolNode<T> m_first = new DoublePoolNode<T>();
        private DoubleLinkedNode<T> m_firstNode = new DoubleLinkedNode<T>();
        
        private int m_count = 0;

        public DoubleLinkedNode<T> GetNode(T data) {
            if (IsEmpty()) {
                m_firstNode = new DoubleLinkedNode<T>(data);

                m_count++;
                return m_firstNode;
            }

            var currentNode = m_firstNode;
            while (currentNode.Next != null) {
                currentNode = currentNode.Next;
            }

            currentNode.Next = new DoubleLinkedNode<T>(data);
            m_count++;
            return currentNode.Next;
        }

        public bool DeleteNode(DoubleLinkedNode<T> node) {
            if (IsEmpty() || node == null) {
                return false;
            }

            var currentNode = m_firstNode;
            while (!currentNode.Next.Equals(node)) {
                currentNode = currentNode.Next;
            }

            currentNode.Next = node.Next;
            node.Clear();
            m_count--;
            return true;
        }

        // public DoubleLinkedNode<T> GetNodeInstance(T data) {
        //     if (IsEmpty()) {
        //         m_first = new DoublePoolNode<T>(new DoubleLinkedNode<T>(data));
        //
        //         m_count++;
        //         return m_first.data;
        //     }
        //
        //     var currentNode = m_first;
        //     while (currentNode.next != null) {
        //         currentNode = currentNode.next;
        //     }
        //
        //     currentNode.next = new DoublePoolNode<T>(new DoubleLinkedNode<T>(data));
        //     m_count++;
        //     return currentNode.next.data;
        // }
        //
        // public DoubleLinkedNode<T> GetNodeByData(T data) {
        //     if (IsEmpty())
        //         return null;
        //
        //     var currentNode = _GetPoolNodeByData(data);
        //
        //     return currentNode.data;
        // }
        //
        // public DoubleLinkedNode<T> DeleteNodeByData(T data) {
        //     if (IsEmpty()) {
        //         return null;
        //     }
        //
        //     var currentNode = _GetPoolNodeByData(data);
        //     var preCurrentNode = _GetPrePoolNodeByData(data);
        //     preCurrentNode.next = currentNode.next;
        //
        //     m_count--;
        //
        //     return currentNode.data;
        // }
        //
        // private DoublePoolNode<T> _GetPoolNodeByData(T data) {
        //     
        //     var currentNode = m_first;
        //     while (currentNode.next != null) {
        //         if (currentNode.data.Data.Equals(data)) {
        //             return currentNode;
        //         }
        //
        //         currentNode = currentNode.next;
        //     }
        //
        //     return null;
        // }
        //
        // private DoublePoolNode<T> _GetPrePoolNodeByData(T data) {
        //     var currentNode = m_first;
        //     while (currentNode != null) {
        //         if (currentNode.next.data.Data.Equals(data)) {
        //             return currentNode;
        //         }
        //
        //         currentNode = currentNode.next;
        //     }
        //
        //     return null;
        // }
        
        public bool IsEmpty() {
            return (m_count == 0);
        }

        public void Clear() {
            m_firstNode = null;
            m_count = 0;
        }
    }

    // public class DoublePoolNode<T> {
    //     public DoubleLinkedNode<T> data { get; set; }
    //
    //     public DoublePoolNode<T> next { get; set; }
    //
    //     public DoublePoolNode() {
    //         
    //     }
    //
    //     public DoublePoolNode(DoubleLinkedNode<T> _data) {
    //         data = _data;
    //     }
    // }
}