using System.Collections.Generic;
using Game.Scripts.Common.CSharp;
using UnityEngine;

namespace Game.Scripts.CSharp.Link {
  
    /// <summary>
    /// 单链表结构：
    /// 1. 快速插入首节点
    /// 2. 快速插入尾节点
    /// 3. 提供迭代器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingleLinkedList<T> : IEnumerable<T> {
      private SingleLinkedNode<T> m_first = null;
      private int m_length                = 0;
      private SingleNodeListPool<T> m_singlePool = new SingleNodeListPool<T>();


      public SingleLinkedNode<T> First => m_first;
      public int                Length => m_length;

      public SingleLinkedNode<T> Add(T data) {
        if (_CheckNodeDataIsNull(data))
          return null;

        var new_Node = m_singlePool.GetNode(data);
        
        if (_IsEmpty()) {
          m_first = new_Node;
        }

        m_length++;
        
        return new_Node;
      }

      public bool Delete(T data) {
        var result = false;
        if (_IsEmpty() || _CheckNodeDataIsNull(data))
          return false;

        var delNode = GetNodeByData(data);
        var delPreNode = GetPreNodeByData(data);
        
        if (m_singlePool.DeleteNode(delNode, delPreNode)) {
          if (delPreNode == null) {
            m_first = m_singlePool.First;
          }
          m_length--;
          result = true;
        }

        return result;
      }

      public SingleLinkedNode<T> GetNodeByData(T data) {
        if (_IsEmpty() || _CheckNodeDataIsNull(data))
          return null;

        var currentNode = m_first;
        while (currentNode.Next != null) {
          if (currentNode.Data.Equals(data)) {
            return currentNode;
          }
          currentNode = currentNode.Next;
        }

        return null;
      }

      public SingleLinkedNode<T> GetPreNodeByData(T data) {
        if (_IsEmpty() || _CheckNodeDataIsNull(data)) {
          Debug.LogAssertion("The data is firstData, can`t get preData");
          return null;
        }

        if (m_first.Data.Equals(data))
          return null;

        var currentNode = m_first;
        while (currentNode.Next != null) {
          if (currentNode.Next.Data.Equals(data))
            return currentNode;
          currentNode = currentNode.Next;
        }

        return null;
      }

      public void Clear(){
        m_first  = null;
        m_length = 0;
        m_singlePool = null;
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
        return (m_length == 0);
      }

      private bool _CheckNodeDataIsNull(T nodeData) {
        if (nodeData == null) {
          Debug.LogAssertion("The nodeData is null");
          return true;
        }

        return false;
      }
      
      private bool _CheckNodeIsNull(SingleLinkedNode<T> node) {
        if (node == null) {
          Debug.LogAssertion("The node is null");
          return true;
        }

        return false;
      }

      /*******************************迭代器实现****************************************/
      internal class SingleLinkedListEnumerator : IEnumerator<T> {
        private SingleLinkedNode<T> m_currentNode = null;
        private SingleLinkedList<T> m_linkedList  = null;

        public SingleLinkedListEnumerator(SingleLinkedList<T> list) {
          m_currentNode = list.First;
          m_linkedList  = list;
        }

        public T Current => m_currentNode.Data;

        object System.Collections.IEnumerator.Current => Current;

        public bool MoveNext() {
          m_currentNode = m_currentNode.Next;

          return (m_currentNode != null);
        }

        public void Reset() {
          m_currentNode = m_linkedList.First;
        }

        public void Dispose() {
          m_currentNode = null;
          m_linkedList  = null;
        }
      }

      public IEnumerator<T> GetEnumerator() {
        return new SingleLinkedListEnumerator(this);
      }

      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
        return this.GetEnumerator();
      }
    }
    
    /**
    描述：链表结点类
    mData：自身数据
    mNext：指向下个数据的引用
    */
    public class SingleLinkedNode<T>{
        private T                   m_data = default(T);
        private SingleLinkedNode<T> m_next = null;

        public SingleLinkedNode() {
        }

        //提供赋值构造
        public SingleLinkedNode(T tData){
          m_data = tData;
        }

        public T Data {
          get => m_data;
          set => m_data = value;
        }

        public SingleLinkedNode<T> Next {
          get => m_next;
          set => m_next = value;
        }

        public void Clear() {
          m_data = default;
          m_next = null;
        }
    }
    
}