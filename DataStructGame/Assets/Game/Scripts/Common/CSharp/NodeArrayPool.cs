using System.Collections;
using Game.Scripts.CSharp.Link;

namespace Game.Scripts.Common.CSharp {
    public class NodeArrayPool<T> {
        private const int DEFAULT_CAPACITY = 5;
        
        private int m_capacity;
        private int m_count;
        private int m_tailIndex;
        
        private DoubleLinkedNode<T>[] m_poolArray;

        public NodeArrayPool():this(DEFAULT_CAPACITY) {
            
        }
        
        public NodeArrayPool(int capacity) {
            m_capacity = capacity;
            m_count = 0;
            m_tailIndex = 0;
            m_poolArray = new DoubleLinkedNode<T>[m_capacity];
        }

        public DoubleLinkedNode<T> Get(T data) {
            if (data == null) return null;

            DoubleLinkedNode<T> resultNode = null;

            if (IsIndexMax()) {
                resultNode = _GetDelNode();
                if (resultNode == null) {
                    _Resize(2*m_capacity);
                    resultNode = _Add(data);
                }
            }
            else {
                resultNode = _Add(data);
            }

            return resultNode;
        }

        public DoubleLinkedNode<T> Delete(DoubleLinkedNode<T> delNode) {
            if (IsEmpty() || delNode == null)
                return null;

            for (int i = 0; i <= m_tailIndex; i++) {
                if (m_poolArray[i].Equals(delNode)) {
                    m_poolArray[i].IsDelete = true;
                    m_count--;
                    return m_poolArray[i];
                }
            }

            return null;
        }

        private DoubleLinkedNode<T> _GetDelNode() {
            if (IsEmpty()) {
                return null;
            }

            for (int i = 0; i < m_tailIndex; i++) {
                if (m_poolArray[i].IsDelete) {
                    m_poolArray[i].Clear();
                    return m_poolArray[i];
                }
            }

            return null;
        }
        
        private DoubleLinkedNode<T> _Add(T data) {
            m_poolArray[m_tailIndex] = new DoubleLinkedNode<T>(data);
            //m_poolDic.Add(m_poolArray[m_tailIndex].Data, m_tailIndex);
            var currentNode = m_poolArray[m_tailIndex];
            m_count++;
            m_tailIndex++;
            return currentNode;
        }

        private void _Resize(int size) {
            m_capacity = size;
            DoubleLinkedNode<T>[] new_array = new DoubleLinkedNode<T>[m_capacity];

            for (int i = 0; i < m_tailIndex; i++) {
                new_array[i] = m_poolArray[i];
            }

            m_poolArray = new_array;
        }

        /// <summary>
        /// 判断array是否到达最大值下标
        /// </summary>
        /// <returns></returns>
        public bool IsIndexMax() {
            return (m_tailIndex >= m_capacity);
        }

        public bool IsEmpty() {
            return (m_count == 0);
        }

        /// <summary>
        /// 判断数组是否已经满了
        /// </summary>
        /// <returns>-1表示已经满了</returns>
        public bool IsFull() {

            return (m_count >= m_capacity);
        }

        public void _Clear() {
            m_capacity = 0;
            m_count = 0;
            m_poolArray = null;
            //m_poolDic = null;
        }
    }
}