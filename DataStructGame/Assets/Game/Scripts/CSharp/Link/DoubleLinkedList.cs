using System;
using System.Collections.Generic;
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

    public DoubleLinkedNode<T> First => m_first;
    public DoubleLinkedNode<T> Last => m_last;
    public int                Count => m_count;

    public void Prepend(DoubleLinkedNode<T> newNode){
      if(_CheckParamIsNull(newNode))
        return;;

      if(_IsEmpty()){
        _FirstAdd(newNode);
      } else {
        var currentNode = m_first;
        m_first = newNode;
        newNode.Next = currentNode;
        currentNode.Previous = newNode;
      }

      m_count++;
    }

    public void Append(DoubleLinkedNode<T> newNode){
      if(_CheckParamIsNull(newNode))
        return;;
      
      if(_IsEmpty()){
        _FirstAdd(newNode);
      } else {
        var currentNode = m_last;
        m_last = newNode;
        currentNode.Next = m_last;
        newNode.Previous = currentNode;
      }

      m_count++;
    }
    
    /// <summary>
    /// 向指定的链表元素添加
    /// </summary>
    /// <param name="direction">方向，previous表示向上添加，next表示向下添加</param>
    /// <param name="newNode">添加元素</param>
    /// <param name="targetNode">目标元素,默认是null，默认时根据direction判断是first还是last</param>
    public void Add(Common.CSharp.Common.NodeDirection direction,DoubleLinkedNode<T> newNode, DoubleLinkedNode<T> targetNode = null) {
      if(_CheckParamIsNull(newNode))
        return;

      if (_IsEmpty())
        _FirstAdd(newNode);
      else if (direction == Common.CSharp.Common.NodeDirection.Previous) {
        if (targetNode == null) {
          Prepend(newNode);
          return;
        }
        _InsertNodeByNodePoint(direction, targetNode, newNode);
      }
      else {
        if (targetNode == null) {
          Append(newNode);
          return;
        }
        _InsertNodeByNodePoint(direction,targetNode,newNode);
      }

      m_count++;
    }

    /// <summary>
    /// 删除方法，如果targetNode为null或则该链表不存在该节点，都会执行默认删除方法，
    /// </summary>
    /// <param name="direction">方向，previous表示向上添加，next表示向下添加</param>
    /// <param name="targetNode">目标节点，默认为null，如果为null，则会根据方面判定frist或则last</param>
    public void Delete(Common.CSharp.Common.NodeDirection direction,DoubleLinkedNode<T> targetNode = null) {
      if (_IsEmpty())
        return;
      
      if (m_count == 1) {
        _FirstAdd(null);
      }
      else {
        if (targetNode == null) {
          if (direction == Common.CSharp.Common.NodeDirection.Next) {
            m_first = m_first.Next;
            m_first.Previous = null;
          }
          else {
            m_last = m_first.Next;
            m_last.Next = null;
          }
        }
        else {
          targetNode = _GetElement(direction, m_first, targetNode);
          _DoubleNodeLink(targetNode.Previous,targetNode.Next);
        }
      }
      
      m_count--;
    }

    public void Clear(){
      m_count = 0;
      m_first = m_last = null;
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
    
    //首次添加，也可以用于删除唯一的元素
    private void _FirstAdd(DoubleLinkedNode<T> newNode) {
      if(_CheckParamIsNull(newNode))
        return;
      
      m_first = m_last = newNode;
    }

    /// <summary>
    /// 主要用于插入时的替换操作
    /// </summary>
    /// <param name="direction">方向</param>
    /// <param name="originNode">原始元素</param>
    /// <param name="newNode">新元素</param>
    private void _InsertNodeByNodePoint(Common.CSharp.Common.NodeDirection direction,DoubleLinkedNode<T> originNode, DoubleLinkedNode<T> newNode) {
      if(_CheckParamIsNull(originNode) || _CheckParamIsNull(newNode))
        return;
      
      var currentNode = originNode;
      if (direction == Common.CSharp.Common.NodeDirection.Previous) {
        newNode.Next         = currentNode;
        if (currentNode.Previous != null) {
          newNode.Previous     = currentNode.Previous;
          currentNode.Previous.Next = newNode; 
        }
        currentNode.Previous      = newNode;
      }
      else {
        newNode.Previous = currentNode;
        if (currentNode.Next != null) {
          newNode.Next = currentNode.Next;
          currentNode.Next.Previous = newNode;
        }
        currentNode.Next = newNode;
      }
    }

    /// <summary>
    /// 用于获取指定的元素
    /// </summary>
    /// <param name="direction">方向，用于是从next进行查询还是previous进行查询</param>
    /// <param name="startNode">可以自行设置从哪个节点开始</param>
    /// <param name="targetNode">指定目标元素，如果只知道data，可以自行构建一个Node，然后放入查询</param>
    /// <returns></returns>
    private DoubleLinkedNode<T> _GetElement(Common.CSharp.Common.NodeDirection direction,DoubleLinkedNode<T> startNode, DoubleLinkedNode<T> targetNode) {
      if(_CheckParamIsNull(startNode) || _CheckParamIsNull(targetNode))
        return null;
      
      var current = startNode;

      //该判定如果写在while循环中，可以减少代码，但是执行效率会变低，每次while都会作一次判断
      if (direction == Common.CSharp.Common.NodeDirection.Next) {
        while (current != null) {
          if (current.Data.Equals(targetNode.Data))
            return current;
          current = current.Next;
        }
      }
      else {
        while (current != null) {
          if (current.Data.Equals(targetNode.Data))
            return current;
          current = current.Previous;
        }
      }

      return null;
    }

    //用于两个节点的相互指向，用于知道某个节点的前后两个指向，进行该节点的删除操作
    private void _DoubleNodeLink(DoubleLinkedNode<T> previousNode, DoubleLinkedNode<T> nextNode) {
      if(_CheckParamIsNull(previousNode) || _CheckParamIsNull(nextNode))
        return;
      
      previousNode.Next = nextNode;
      nextNode.Previous = previousNode;
    }
    
    private bool _CheckParamIsNull(DoubleLinkedNode<T> node) {
      if (node == null) {
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


  public class DoubleLinkedNode<T>{
    private DoubleLinkedNode<T> m_next      = null;
    private DoubleLinkedNode<T> m_previous  = null;
    private T                   m_data      = default(T);
    
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
  }
}