using UnityEngine;

namespace Game.Scripts.Common.CSharp {
    public static class Common {
        public static bool CheckIndexOutForEqual(int a, int b) {
            bool result = false;
            
            if(a < 0 || a >= b){
                Debug.LogAssertion("数组下标超出数组范围");
                result = true;
            }

            return result;
        }

        public static bool CheckIndexOutForNotEqual(int a, int b) {
            bool result = false;
            
            if(a < 0 || a > b){
                Debug.LogAssertion("数组下标超出数组范围");
                result = true;
            }

            return result;
        }
    }
}


/**
 DoubleNodeArrayPool:deleteCacheList
 
 using System.Collections;
using Game.Scripts.CSharp.Link;

namespace Game.Scripts.Common.CSharp {
    public class DoubleNodeArrayPool<T> {
        private const int DEFAULT_CAPACITY = 10000;
        
        private int m_capacity;
        private int m_count;
        private int m_tailIndex;
        
        private DoubleLinkedNode<T>[] m_poolArray;
        //private Dictionary<T, int> m_poolDic;
        private ArrayList             m_deleteCache;

        public DoubleNodeArrayPool():this(DEFAULT_CAPACITY) {
            
        }
        
        public DoubleNodeArrayPool(int capacity) {
            m_capacity = capacity;
            m_count = 0;
            m_tailIndex = 0;
            m_poolArray = new DoubleLinkedNode<T>[m_capacity];
            m_deleteCache = new ArrayList();
        }

        public DoubleLinkedNode<T> GetNodeInstance(T data) {
            if (data == null)
                return null;

            var resultNode = GetNodeByData(data);

            if (resultNode != null) {
                return resultNode;
            }

            if (IsIndexMax()) {
                var delIndex = IsFull();
                if (delIndex >= 0) {
                    resultNode = _GetRecoverInstance(delIndex,data);
                }
                else {
                    _Resize(2*DEFAULT_CAPACITY);
                    resultNode = _Add(data);
                }
            }
            else {
                resultNode = _Add(data);
            }

            return resultNode;
        }

        public DoubleLinkedNode<T> GetNodeByData(T data) {
            if (data == null)
                return null;

            if (IsEmpty()) {
                return null;
            }

            for (int i = 0; i < m_tailIndex; i++) {
                for (int j = 0; j < m_deleteCache.Count; j++) {
                    if (i != (int)m_deleteCache[j]) {
                        if (m_poolArray[i].Data.Equals(data)) {
                            return m_poolArray[i];
                        }  
                    }
                }
            }

            //var index = m_poolDic[data];
            return null;
        }

        public DoubleLinkedNode<T> DeleteNode(T data) {
            if (data == null)
                return null;
            
            //var delIndex = m_poolDic[data];
            DoubleLinkedNode<T> resultNode = null;
            for (int i = 0; i <= m_tailIndex; i++) {
                if (m_poolArray[i].Data.Equals(data)) {
                    resultNode = m_poolArray[i];
                    m_deleteCache.Add(i);
                    //m_poolArray[i].Clear();
                    break;
                }
            }
            
            // m_poolArray[delIndex].Clear();
            //m_poolDic.Remove(data);
            m_count--;
            
            return resultNode;
        }

        private DoubleLinkedNode<T> _Add(T data) {
            m_poolArray[m_tailIndex] = new DoubleLinkedNode<T>(data);
            //m_poolDic.Add(m_poolArray[m_tailIndex].Data, m_tailIndex);
            var currentNode = m_poolArray[m_tailIndex];
            m_count++;
            m_tailIndex++;
            return currentNode;
        }

        private DoubleLinkedNode<T> _GetRecoverInstance(int delIndex,T data) {
            if (delIndex < 0 || data == null) {
                return null;
            }

            m_poolArray[delIndex].Data = data;
            m_poolArray[delIndex].Next = null;
            m_poolArray[delIndex].Previous = null;
            //m_poolDic.Add(m_poolArray[delIndex].Data,delIndex);
            m_deleteCache.Remove(delIndex);
            m_count++;
            
            return m_poolArray[delIndex];
        }

        private void _Resize(int size) {
            m_capacity = size;
            DoubleLinkedNode<T>[] new_array = new DoubleLinkedNode<T>[m_capacity];
            //Dictionary<T, int> new_dic = new Dictionary<T, int>();

            for (int i = 0; i < m_count; i++) {
                new_array[i] = m_poolArray[i];
                //new_dic.Add(new_array[i].Data, i);
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
        public int IsFull() {
            if(m_deleteCache.Count == 0 && IsIndexMax())
                return -1;
            
            return (int)m_deleteCache[m_deleteCache.Count-1];
        }

        public void _Clear() {
            m_capacity = 0;
            m_count = 0;
            m_deleteCache = null;
            m_poolArray = null;
            //m_poolDic = null;
        }
    }
}
 
 
 */
 
 /********************DoubleNodeListPool 
  using Game.Scripts.CSharp.Link;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Common.CSharp {
    public class DoubleNodeListPool<T> {
        //private DoublePoolNode<T> m_first = new DoublePoolNode<T>();
        private SingleLinkedNode<T> m_firstNode = new SingleLinkedNode<T>();
        
        private int m_count = 0;

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

        public bool DeleteNode(SingleLinkedNode<T> node) {
            if (IsEmpty() || node == null) {
                Debug.LogWarning("node is null,don`t delete");
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
  * 
  */
  
  /**DoubleLinkedList
using System;
using System.Collections.Generic;
using Game.Scripts.Common.CSharp;
using UnityEngine;

namespace Game.Scripts.CSharp.Link {
  /// <summary>
  /// 双向链表，提供链表特性
  /// 1. 具有节点操作性质，头部与尾部拼接
  /// 2. 具有头部与尾部删除特性
  /// 3. 具有双向获取特性
  /// 4. 建议根据特性，不实用index等方式遍历，尽量采用链表特性
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class DoubleLinkedList<T> : IEnumerable<T> {
    private DoubleLinkedNode<T> m_first = null;
    private DoubleLinkedNode<T> m_last  = null;
    private int                 m_count = 0;
    private DoubleNodeArrayPool<T> m_doubleNodeArrayPool = new DoubleNodeArrayPool<T>();
    //private SingleNodeListPool<T>  m_singleNodeListPool = new SingleNodeListPool<T>();

    public DoubleLinkedNode<T> First => m_first;
    public DoubleLinkedNode<T> Last  => m_last;
    public int                Count  => m_count;
    
    /// <summary>
    /// 链表首位进行插入操作,插入在首节点之前，插入的节点可以通过First获取
    /// </summary>
    /// <param name="new_Data"></param>
    public void Prepend(T new_Data){
      if(_CheckNodeDataIsNull(new_Data))
        return;

      var new_Node = m_doubleNodeArrayPool.GetNodeInstance(new_Data);
      
      if(_IsEmpty()){
        m_first = m_last = new_Node;
      } else {
        var currentNode = m_first;
        m_first = new_Node;
        new_Node.Next = currentNode;
        currentNode.Previous = new_Node;
        new_Node.Previous = null;
      }

      m_count++;
    }

    /// <summary>
    /// 链表末位添加的，插入在末尾节点，插入节点可以通过Last获取
    /// </summary>
    /// <param name="new_Data"></param>
    public void Append(T new_Data){
      if(_CheckNodeDataIsNull(new_Data))
        return;

      //var new_Node = m_doubleNodeListPool.GetNodeInstance(new_Data);//new DoubleLinkedNode<T>(new_Data);
      //var new_Node = new DoubleLinkedNode<T>(new_Data);
      //var new_Node = m_doubleNodeArrayPool.GetNodeInstance(new_Data);
      var new_Node = m_doubleNodeArrayPool.GetNodeInstance(new_Data);

      if(_IsEmpty()){
        m_first = m_last = new_Node;
      } else {
        var currentNode = m_last;
        m_last = new_Node;
        currentNode.Next = m_last;
        new_Node.Previous = currentNode;
        new_Node.Next = null;
      }

      m_count++;
    }

    /// <summary>
    /// 插入已知目标节点的next位置
    /// </summary>
    /// <param name="new_Data"></param>
    /// <param name="targetNode"></param>
    /// <returns></returns>
    public DoubleLinkedNode<T> InsertAtTargetDataByNext(T new_Data, DoubleLinkedNode<T> targetNode = null) {
      if(_CheckNodeDataIsNull(new_Data))
        return null;
    
      var new_Node = m_doubleNodeArrayPool.GetNodeInstance(new_Data);
    
      if (_IsEmpty())
        m_first = m_last = new_Node;
      else if (targetNode == null) {
        new_Node = m_last;
        Append(new_Data);
      }else {
        new_Node = _InsertByNext(targetNode, new_Data);
      }
    
      return new_Node;
    }
    
    /// <summary>
    /// 插入已知目标节点的pre位置
    /// </summary>
    /// <param name="new_Data"></param>
    /// <param name="targetNode"></param>
    /// <returns></returns>
    public DoubleLinkedNode<T> InsertAtTargetDataByPre(T new_Data, DoubleLinkedNode<T> targetNode = null) {
      if(_CheckNodeDataIsNull(new_Data))
        return null;
    
      var new_Node = m_doubleNodeArrayPool.GetNodeInstance(new_Data);
    
      if (_IsEmpty())
        m_first = m_last = new_Node;
      else if (targetNode == null) {
        new_Node = m_first;
        Prepend(new_Data);
      }else {
        new_Node = _InsertByPre(targetNode, new_Data);
      }
    
      return new_Node;
    }

    /// <summary>
    /// 删除节点，从next进行
    /// </summary>
    /// <param name="deleteData"></param>
    /// <returns></returns>
    public DoubleLinkedNode<T> DeleteAtDeleteData(T deleteData) {
      if (_IsEmpty())
        return null;

      DoubleLinkedNode<T> deleteNode = m_doubleNodeArrayPool.DeleteNode(deleteData);

      if (deleteData != null) {
        _DoubleNodeLink(deleteNode.Previous,deleteNode.Next);
        m_count--;
      }

      return deleteNode;
    }

    public void Clear(){
      m_count = 0;
      m_first = m_last = null;
      m_doubleNodeArrayPool = null;
    }
    
    public void PrintAll() {
      var current = m_first;
      var i = 0;
      
      while (current != null) {
        Debug.Log("List["+i+"]:"+ current.Data.ToString());
        current = current.Next;
        i++;
      }
      
    }
    
    private bool _IsEmpty(){
      return (Count == 0);
    }

    /// <summary>
    /// 主要用于插入时的替换操作,根据节点的previous进行插入
    /// </summary>
    /// <param name="originNode"></param>
    /// <param name="new_Data"></param>
    /// <returns>返回插入的节点</returns>
    private DoubleLinkedNode<T> _InsertByPre(DoubleLinkedNode<T> originNode, T new_Data) {
      if(_CheckNodeIsNull(originNode) || _CheckNodeDataIsNull(new_Data))
        return null;

      var new_Node = m_doubleNodeArrayPool.GetNodeInstance(new_Data);
      
      new_Node.Next         = originNode;
      if (originNode.Previous != null) {
        new_Node.Previous     = originNode.Previous;
        originNode.Previous.Next = new_Node; 
      }
      originNode.Previous      = new_Node;

      return new_Node;
    }

    /// <summary>
    /// 主要用于插入时的替换操作,根据节点的next进行插入
    /// </summary>
    /// <param name="originNode"></param>
    /// <param name="new_Data"></param>
    /// <returns>返回插入的节点</returns>
    private DoubleLinkedNode<T> _InsertByNext(DoubleLinkedNode<T> originNode, T new_Data) {
      if(_CheckNodeIsNull(originNode) || _CheckNodeDataIsNull(new_Data))
        return null;

      var new_Node = m_doubleNodeArrayPool.GetNodeInstance(new_Data);
      
      new_Node.Previous = originNode;
      if (originNode.Next != null) {
        new_Node.Next = originNode.Next;
        originNode.Next.Previous = new_Node;
      }
      originNode.Next = new_Node;

      return new_Node;
    }

    /// <summary>
    /// 通过指定节点的next指向进行遍历查询
    /// </summary>
    /// <param name="startNode">开始节点</param>
    /// <param name="targetData">目标节点数据</param>
    /// <returns>返回删除节点</returns>
    private DoubleLinkedNode<T> _GetNodeByNext(DoubleLinkedNode<T> startNode, T targetData) {
      if(_CheckNodeIsNull(startNode) || _CheckNodeDataIsNull(targetData))
        return null;

      while (startNode != null) {
        if (startNode.Data.Equals(targetData))
          return startNode;
        startNode = startNode.Next;
      }

      return null;
    }

    /// <summary>
    /// 通过指定节点的previous指向进行遍历查询
    /// </summary>
    /// <param name="startNode">开始节点</param>
    /// <param name="targetData">目标节点数据</param>
    /// <returns>返回删除节点</returns>
    private DoubleLinkedNode<T> _GetNodeByPre(DoubleLinkedNode<T> startNode, T targetData) {
      if(_CheckNodeIsNull(startNode) || _CheckNodeDataIsNull(targetData))
        return null;

      while (startNode != null) {
        if (startNode.Data.Equals(targetData))
          return startNode;
        startNode = startNode.Previous;
      }

      return null;
    }

    //用于两个节点的相互指向，用于知道某个节点的前后两个指向，进行该节点的删除操作
    private void _DoubleNodeLink(DoubleLinkedNode<T> previousNode, DoubleLinkedNode<T> nextNode) {
      //该节点为首节点
      if (previousNode == null && nextNode != null) {
        m_first = nextNode;
        m_first.Previous = null;
      }
      
      //该节点为尾节点
      if(previousNode != null && nextNode == null) {
        m_last = previousNode;
        m_last.Next = null;
      }

      //节点在中间范围
      if (previousNode != null && nextNode != null) {
        previousNode.Next = nextNode;
        nextNode.Previous = previousNode;
      }

      //只有一个节点
      if (previousNode == null && nextNode == null) {
        m_first = m_last = null;
      }

    }
    
    private bool _CheckNodeIsNull(DoubleLinkedNode<T> node) {
      if (node == null) {
        Debug.LogAssertion("The node is null");
        return true;
      }

      return false;
    }

    private bool _CheckNodeDataIsNull(T nodeData) {
      if (nodeData == null) {
        Debug.LogAssertion("The node is null");
        return true;
      }

      return false;
    }
    
迭代器实现
    internal class DoubleLinkedListEnumerator : IEnumerator<T> {
      private DoubleLinkedNode<T> m_currentNode;
      private DoubleLinkedList<T> m_linkedList;

      public DoubleLinkedListEnumerator(DoubleLinkedList<T> list) {
        m_currentNode = list.First;
        m_linkedList  = list;
      }

      public T Current => m_currentNode.Data;

      object System.Collections.IEnumerator.Current => Current;

      public bool MoveNext() {
        m_currentNode = m_currentNode.Next;

        return (m_currentNode != null);
      }

      public bool MovePrevious() {
        m_currentNode = m_currentNode.Previous;

        return (m_currentNode != null);
      }

      public void Reset() {
        m_currentNode = m_linkedList.First;
      }

      public void Dispose() {
        m_currentNode = null;
        m_linkedList = null;
      }
    }

    public IEnumerator<T> GetEnumerator() {
      var node = First;
      while (node != null) {
        yield return node.Data;
        node = node.Next;
      }
      //return new DoubleLinkedListEnumerator(this);
    }
    
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
      return this.GetEnumerator();
    }
  }
  
  public class DoubleLinkedNode<T> {
    private DoubleLinkedNode<T> m_next      = null;
    private DoubleLinkedNode<T> m_previous  = null;
    private T                   m_data      = default(T);
    private bool                m_isDelete  = false;

    public DoubleLinkedNode() {
      
    }

    public DoubleLinkedNode(T dataItem){
      m_data = dataItem;
    }

    public DoubleLinkedNode(T dataItem, DoubleLinkedNode<T> next, DoubleLinkedNode<T> previous){
      m_data = dataItem;
      m_next = next;
      m_previous = previous;
    }

    public T Data {
      get => m_data;
      set => m_data = value;
    }

    public DoubleLinkedNode<T> Next {
      get => m_next;
      set => m_next = value;
    }

    public DoubleLinkedNode<T> Previous {
      get => m_previous;
      set => m_previous = value;
    }

    public bool IsDelete {
      get => m_isDelete;
      set => m_isDelete = value;
    }

    public void Clear() {
      m_data = default;
      m_previous = null;
      m_next = null;
      m_isDelete = false;
    }
  }
}
   */