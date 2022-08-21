using Game.Scripts.CSharp.Link;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Common.CSharp {
    public class SingleNodeListPool<T> {
        private SingleLinkedNode<T> m_firstNode = new SingleLinkedNode<T>();
        
        private int m_count = 0;

        public SingleLinkedNode<T> First => m_firstNode;

        public SingleLinkedNode<T> GetNode(T data) {
            if (IsEmpty()) {
                m_firstNode = new SingleLinkedNode<T>(data);

                m_count++;
                return m_firstNode;
            }

            var currentNode = m_firstNode;
            while (currentNode.Next != null) {
                currentNode = currentNode.Next;
            }

            currentNode.Next = new SingleLinkedNode<T>(data);
            m_count++;
            return currentNode.Next;
        }

        public bool DeleteNode(SingleLinkedNode<T> delNode,SingleLinkedNode<T> delPreNode) {
            if (IsEmpty() || delNode == null) {
                Debug.LogWarning("node is null,don`t delete");
                return false;
            }
            
            if (delPreNode == null) {
                m_firstNode = delNode.Next;
            }
            else {
                delPreNode.Next = delNode.Next; 
            }
            
            delNode.Clear();
            m_count--;
            return true;
        }

        public bool IsEmpty() {
            return (m_count == 0);
        }

        public void Clear() {
            m_firstNode = null;
            m_count = 0;
        }
    }
}