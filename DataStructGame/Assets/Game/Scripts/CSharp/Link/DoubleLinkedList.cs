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
    //private CachePool<T> m_cachePool = new CachePool<T>();
    private DoubleNodeListPool<T> m_doubleNodeListPool = new DoubleNodeListPool<T>();

    private DoubleNodeArrayPool<T> m_doubleNodeArrayPool = new DoubleNodeArrayPool<T>();
    //private DoubleNodeArrayPool<T> m_doubleNodeArrayPool = new DoubleNodeArrayPool<T>();

    //public CachePool<T> CachePool    => m_cachePool;
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
      //var new_Node = m_doubleNodeArrayPool.GetNodeInstance(new_Data);
      var new_Node = m_doubleNodeArrayPool.GetNodeInstance(new_Data);

      if(_IsEmpty()){
        m_first = m_last = new_Node;
      } else {
        var currentNode = m_last;
        m_last = new_Node;
        currentNode.Next = m_last;
        new_Node.Previous = currentNode;
      }

      m_count++;
    }

    /// <summary>
    /// 插入已知目标节点的next位置
    /// </summary>
    /// <param name="new_Data"></param>
    /// <param name="targetNode"></param>
    /// <returns></returns>
    // public DoubleLinkedNode<T> InsertAtTargetDataByNext(T new_Data, DoubleLinkedNode<T> targetNode = null) {
    //   if(_CheckNodeDataIsNull(new_Data))
    //     return null;
    //
    //   var new_Node = m_doubleNodeArrayPool.GetNodeInstance(new_Data);
    //
    //   if (_IsEmpty())
    //     m_first = m_last = new_Node;
    //   else if (targetNode == null) {
    //     new_Node = m_last;
    //     Append(new_Data);
    //   }else {
    //     new_Node = _InsertByNext(targetNode, new_Data);
    //   }
    //
    //   return new_Node;
    // }
    
    /// <summary>
    /// 插入已知目标节点的pre位置
    /// </summary>
    /// <param name="new_Data"></param>
    /// <param name="targetNode"></param>
    /// <returns></returns>
    // public DoubleLinkedNode<T> InsertAtTargetDataByPre(T new_Data, DoubleLinkedNode<T> targetNode = null) {
    //   if(_CheckNodeDataIsNull(new_Data))
    //     return null;
    //
    //   var new_Node = m_doubleNodeArrayPool.GetNodeInstance(new_Data);
    //
    //   if (_IsEmpty())
    //     m_first = m_last = new_Node;
    //   else if (targetNode == null) {
    //     new_Node = m_first;
    //     Prepend(new_Data);
    //   }else {
    //     new_Node = _InsertByPre(targetNode, new_Data);
    //   }
    //
    //   return new_Node;
    // }

    /// <summary>
    /// 删除节点，从next进行
    /// </summary>
    /// <param name="deleteData"></param>
    /// <returns></returns>
    public DoubleLinkedNode<T> DeleteAtDeleteData(T deleteData) {
      if (_IsEmpty())
        return null;

      DoubleLinkedNode<T> deleteNode = m_doubleNodeArrayPool.DeleteNode(deleteData);
      
      _DoubleNodeLink(deleteNode.Previous,deleteNode.Next);

      m_count--;
      
      return deleteNode;
    }

    public void Clear(){
      m_count = 0;
      m_first = m_last = null;
      //m_replaceCache = null;
      //m_cachePool.Clear();
      m_doubleNodeListPool = null;
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

      var new_Node = new DoubleLinkedNode<T>(new_Data);
      
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
      
      var new_Node = new DoubleLinkedNode<T>(new_Data);
      
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
      if(_CheckNodeIsNull(previousNode) || _CheckNodeIsNull(nextNode))
        return;
      
      previousNode.Next = nextNode;
      nextNode.Previous = previousNode;
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

    public void Clear() {
      m_data = default(T);
      m_previous = null;
      m_next = null;
    }
  }
}