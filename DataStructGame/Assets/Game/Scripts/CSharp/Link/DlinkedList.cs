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
  public class DlinkedList<T> : IEnumerable<T> {
    private DoubleLinkedNode<T> m_first = null;
    private DoubleLinkedNode<T> m_last  = null;
    private int                 m_count = 0;
    private NodeArrayPool<T> m_doubleNodeArrayPool = new NodeArrayPool<T>();

    public DoubleLinkedNode<T> First => m_first;
    public DoubleLinkedNode<T> Last  => m_last;
    public int                Count  => m_count;

    public DoubleLinkedNode<T> Add(T new_Data) {
      if(_CheckNodeDataIsNull(new_Data))
        return null;
      
      var new_Node = m_doubleNodeArrayPool.Get(new_Data);

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

      return new_Node;
    }

    public DoubleLinkedNode<T> Delete(T deleteData) {
      if (_IsEmpty())
        return null;

      DoubleLinkedNode<T> deleteNode = _GetNodeByData(deleteData);

      if (deleteData != null) {
        _DoubleNodeLink(deleteNode.Previous,deleteNode.Next);
        m_doubleNodeArrayPool.Delete(deleteNode);
        m_count--;
      }

      return deleteNode;
    }

    public DoubleLinkedNode<T> Get(T data) {
      if (_IsEmpty() || data == null)
        return null;

      return _GetNodeByData(data);
    }

    public DoubleLinkedNode<T> _GetNodeByData(T data) {
      if (_IsEmpty() || data == null)
        return null;

      var currentNode = m_first;
      while (!currentNode.Data.Equals(data)) {
        currentNode = currentNode.Next;
      }

      return currentNode;
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
    
    /*****************************迭代器实现*************************************/
    internal class DoubleLinkedListEnumerator : IEnumerator<T> {
      private DoubleLinkedNode<T> m_currentNode;
      private DlinkedList<T> m_linkedList;

      public DoubleLinkedListEnumerator(DlinkedList<T> list) {
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