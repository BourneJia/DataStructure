using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.CSharp.Link {
  
    /// <summary>
    /// 单链表结构：
    /// 1. 快速插入首节点
    /// 2. 快速插入尾节点
    /// 3. 提供迭代器
    /// 4. 由于链表本身对随机访问不友好，就不强行进行遍历查询功能提供
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingleLinkedList<T> : IEnumerable<T> {
      private SingleLinkedNode<T> m_first = null;
      private SingleLinkedNode<T> m_last  = null;
      private int m_length                = 0;
      
      public SingleLinkedNode<T> First => m_first;
      public SingleLinkedNode<T> Last  => m_last;
      public int Length                => m_length;

      public SingleLinkedList() {
        
      }

      //插入单链表首位，替换m_first
      public void Prepend(SingleLinkedNode<T> newNode){
        if (_CheckParamIsNull(newNode)) 
          return;

        if (_IsEmpty()){
          _FirstAdd(newNode);
        } else {
          var replaceNode = m_first;
          m_first         = newNode;
          newNode.Next    = replaceNode;
        }

        m_length++;
      }

      //插入单链表末位，替换m_last
      public void Append(SingleLinkedNode<T> newNode){
        if (_CheckParamIsNull(newNode)) 
          return;

        if (_IsEmpty()){
          _FirstAdd(newNode);
        } else {
          var replaceNode  = m_last;
          m_last           = newNode;
          replaceNode.Next = newNode;
        }

        m_length++;
      }

      //去除单链表首部内容
      public void DeleteFirstNode() {
        if (!_IsEmpty()) {
          if (m_length == 1)
            m_first = m_last = null;
          else {
            m_first = m_first.Next;
            m_length--;
          }
        }
      }

      /// <summary>
      /// 直接插入某个已知节点的next区域
      /// </summary>
      /// <param name="originNode">默认获取的是由data创建的初始节点信息，由于本身不想创建额外CG，因此让使用者直接在外部创建</param>
      /// <param name="newNode">新的节点内容</param>
      public void InsertByNode(SingleLinkedNode<T> originNode,SingleLinkedNode<T> newNode) {
        if (_CheckParamIsNull(newNode) || _CheckParamIsNull(originNode))
          return;

        //根据节点的data值，获取完整节点信息
        originNode = _GetElement(m_first,originNode);

        if (originNode != null) {
          if(originNode.Equals(m_last))
            Append(newNode);
          else {
            _InsertNode(originNode,newNode);
          }
        }
      }

      /// <summary>
      /// 删除某个节点
      /// </summary>
      /// <param name="deleteNode">删除节点数据</param>
      public void DeleteByNode(SingleLinkedNode<T> deleteNode) {
        if (_CheckParamIsNull(deleteNode)) 
          return;

        deleteNode = _GetElement(m_first, deleteNode);
        
        if (deleteNode != null) {
          var deleteNodePre = _GetPreElement(m_first,deleteNode);
          deleteNodePre.Next = deleteNode.Next;
        }
      }

      /// <summary>
      /// 获取前置的节点内容
      /// </summary>
      /// <param name="node"></param>
      /// <returns></returns>
      public SingleLinkedNode<T> GetPreNode(SingleLinkedNode<T> node) {
        if (_CheckParamIsNull(node))
          return null;

        var resultNode = _GetPreElement(m_first,node);

        if (_CheckParamIsNull(resultNode))
          return null;
        return resultNode;
      }

      public void Clear(){
        m_first  = null;
        m_last   = null;
        m_length = 0;
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
      
      
      //首次添加，也可以用于删除唯一的元素
      private void _FirstAdd(SingleLinkedNode<T> newNode) {
        m_first = m_last = newNode;
      }

      /// <summary>
      /// 插入替换方法
      /// </summary>
      /// <param name="originNode"></param>
      /// <param name="newNode"></param>
      private void _InsertNode(SingleLinkedNode<T> originNode, SingleLinkedNode<T> newNode) {
        if (_CheckParamIsNull(originNode) || _CheckParamIsNull(newNode))
          return;

        var replaceNode = originNode;
        originNode.Next = newNode;
        newNode.Next    = replaceNode.Next.Next;
      }

      /// <summary>
      /// 通过一个节点数据获取该节点整体信息方法
      /// </summary>
      /// <param name="startNode">开始遍历查询的节点</param>
      /// <param name="targetNode">目标节点</param>
      /// <returns></returns>
      private SingleLinkedNode<T> _GetElement(SingleLinkedNode<T> startNode, SingleLinkedNode<T> targetNode) {
        if (_CheckParamIsNull(startNode) || _CheckParamIsNull(targetNode))
          return null;
        var current = startNode;

        while (current != null) {
          if (current.Data.Equals(targetNode.Data))
            return current;
          current = current.Next;
        }

        return null;
      }

      /// <summary>
      /// 获取该节点的前面节点
      /// </summary>
      /// <param name="startNode">开始遍历查询的节点</param>
      /// <param name="targetNode">目标节点</param>
      /// <returns></returns>
      private SingleLinkedNode<T> _GetPreElement(SingleLinkedNode<T> startNode, SingleLinkedNode<T> targetNode) {
        if (_CheckParamIsNull(startNode) || _CheckParamIsNull(targetNode))
          return null;
        
        var current = startNode;

        while (current != null) {
          if (current.Next.Data.Equals(targetNode.Data))
            return current;
          current = current.Next;
        }

        return null;
      }

      private bool _CheckParamIsNull(SingleLinkedNode<T> node) {
        if (node == null) {
          Debug.LogAssertion("The node is null");
          return false;
        }

        return true;
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
    }
}