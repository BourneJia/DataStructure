using System.Collections.Generic;
using Game.Scripts.Common.CSharp;
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

      //private SingleNodeCache<SingleLinkedNode<T>> m_rplaceCache = new SingleNodeCache<SingleLinkedNode<T>>();


      public SingleLinkedNode<T> First => m_first;
      public SingleLinkedNode<T> Last  => m_last;
      public int                Length => m_length;

      /// <summary>
      /// 链表首位进行插入操作,插入在首节点之前，插入的节点可以通过First获取
      /// </summary>
      /// <param name="nodeData">插入的数据内容</param>
      public void Prepend(T nodeData){
        if (_CheckNodeDataIsNull(nodeData)) 
          return;

        var new_Node = new SingleLinkedNode<T>(nodeData);

        if (_IsEmpty()){
          m_first = m_last = new_Node;
        } else {
          var replaceNode = m_first;
          m_first         = new_Node;
          new_Node.Next   = replaceNode;
        }

        m_length++;
      }

      /// <summary>
      /// 链表末位添加的，插入在末尾节点，插入节点可以通过Last获取
      /// </summary>
      /// <param name="nodeData">插入的数据内容</param>
      public void Append(T nodeData){
        if (_CheckNodeDataIsNull(nodeData)) 
          return;

        var new_Node = new SingleLinkedNode<T>(nodeData);

        if (_IsEmpty()) {
          m_first = m_last = new_Node;
        } else {
          var replaceNode  = m_last;
          m_last           = new_Node;
          replaceNode.Next = new_Node;
        }

        m_length++;
      }

      /// <summary>
      /// 插入已知目标节点的next位置
      /// </summary>
      /// <param name="new_Data">新数据</param>
      /// <param name="targetNode">目标节点</param>
      /// <returns>返回插入的节点信息</returns>
      public SingleLinkedNode<T> InsertAtTargetNodeNext(T new_Data, SingleLinkedNode<T> targetNode) {
        if (_CheckNodeDataIsNull(new_Data) && _CheckNodeIsNull(targetNode))
          return null;
        
        var new_Node = _InsertAtNodeNext(targetNode,new_Data);
        
        return new_Node;
      }

      /// <summary>
      /// 插入对应数据节点的next位置
      /// </summary>
      /// <param name="new_Data">新数据</param>
      /// <param name="targetData">目标节点数据</param>
      /// <returns></returns>
      public SingleLinkedNode<T> InsertAtTargetDataNext(T new_Data, T targetData) {
        if (_CheckNodeDataIsNull(new_Data) && _CheckNodeDataIsNull(targetData)) 
          return null;
        
        var targetNode = _GetNode(m_first,targetData);
        
        var new_Node = _InsertAtNodeNext(targetNode,new_Data);

        return new_Node;
      }

      /// <summary>
      /// 插入已知目标节点的pre位置
      /// </summary>
      /// <param name="new_Data">新数据</param>
      /// <param name="targetNode">目标节点</param>
      /// <returns></returns>
      public SingleLinkedNode<T> InsertAtTargetNodePre(T new_Data, SingleLinkedNode<T> targetNode) {
        if (_CheckNodeDataIsNull(new_Data) && _CheckNodeIsNull(targetNode))
          return null;

        var new_Node = _InsertAtNodePre(targetNode, new_Data);

        return new_Node;
      }

      /// <summary>
      /// 插入已知目标节点的pre位置
      /// </summary>
      /// <param name="new_Data">新数据</param>
      /// <param name="targetData">目标节点信息</param>
      /// <returns></returns>
      public SingleLinkedNode<T> InsertAtTargetDataPre(T new_Data, T targetData) {
        if (_CheckNodeDataIsNull(new_Data) && _CheckNodeDataIsNull(targetData)) 
          return null;
        
        var targetNode = _GetNode(m_first,targetData);
        
        var new_Node = _InsertAtNodePre(targetNode,new_Data);

        return new_Node;
      }

      /// <summary>
      /// 去除单链表首部内容
      /// </summary>
      /// <returns>返回删除前的First</returns>
      public SingleLinkedNode<T> DeleteFirstNode() {
        SingleLinkedNode<T> resultNode = m_first;

        if (!_IsEmpty()) {
          if (m_length == 1) {
            m_first = m_last = null;
            resultNode = null;
          }
          else {
            m_first = m_first.Next;
          }
          m_length--;
        }
        else {
          resultNode = null;
        }

        return resultNode;
      }

      /// <summary>
      /// 删除单链表尾部内容
      /// </summary>
      /// <returns>返回删除前的last</returns>
      public SingleLinkedNode<T> DeleteLastNode() {
        SingleLinkedNode<T> resultNode = m_last;
        if (_IsEmpty())
          return null;
        
        var preNodeFromLastNode = _GetPreNode(m_first, m_last.Data);
        
        if (preNodeFromLastNode == null) {
          m_first = m_last = null;
          resultNode = null;
        }
        else {
          m_last = preNodeFromLastNode;
        }

        m_length--;
        return resultNode;
      }

      /// <summary>
      /// 根据已知的节点进行删除
      /// </summary>
      /// <param name="targetNode">需要删除的节点</param>
      /// <returns>返回删除的节点内容</returns>
      public SingleLinkedNode<T> DeleteAtTargetNode(SingleLinkedNode<T> targetNode) {
        if (_CheckNodeIsNull(targetNode))
          return null;
        
        var preNodeFromDeleteNode = _GetPreNode(m_first, targetNode.Data);
        preNodeFromDeleteNode.Next = targetNode.Next;

        return targetNode;
      }

      /// <summary>
      /// 根据已知的节点数据进行删除
      /// </summary>
      /// <param name="targetData">目标节点数据</param>
      /// <returns>返回删除的节点信息</returns>
      public SingleLinkedNode<T> DeleteAtTargetData(T targetData) {
        if (_CheckNodeDataIsNull(targetData))
          return null;

        var deleteNode = _GetNode(m_first, targetData);

        if (deleteNode != null) {
          var preNodeFromDeleteNode = _GetPreNode(m_first, targetData);
          preNodeFromDeleteNode.Next = deleteNode.Next;
        }

        return deleteNode;
      }
      

      /// <summary>
      /// 获取前置的节点内容
      /// </summary>
      /// <param name="node"></param>
      /// <returns></returns>
      public SingleLinkedNode<T> GetPreNode(SingleLinkedNode<T> node) {
        if (_CheckNodeIsNull(node))
          return null;

        var preNode = _GetPreNode(m_first,node.Data);

        if (_CheckNodeIsNull(preNode))
          return null;
        return preNode;
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

      /// <summary>
      /// 插入替换方法,插入在目标节点的next位置
      /// </summary>
      /// <param name="targetNode">目标节点</param>
      /// <param name="new_Data"></param>
      private SingleLinkedNode<T> _InsertAtNodeNext(SingleLinkedNode<T> targetNode, T new_Data) {
        if (_CheckNodeIsNull(targetNode) || _CheckNodeDataIsNull(new_Data))
          return null;

        var new_Node = new SingleLinkedNode<T>(new_Data);
        
        if (targetNode.Equals(m_last)) {
          Append(new_Node.Data);
          new_Node = m_last;
        }
        else {
          var replaceNode = targetNode;
          targetNode.Next = new_Node;
          new_Node.Next   = replaceNode.Next.Next;
          //清空replacenode
          replaceNode     = null;
        }

        return new_Node;
      }

      /// <summary>
      /// 插入替换方法,插入在目标节点的pre位置
      /// </summary>
      /// <param name="targetNode">目标节点</param>
      /// <param name="new_Data">新数据</param>
      /// <returns></returns>
      private SingleLinkedNode<T> _InsertAtNodePre(SingleLinkedNode<T> targetNode, T new_Data) {
        if (_CheckNodeIsNull(targetNode) || _CheckNodeDataIsNull(new_Data))
          return null;
        var preNodeFromTargetNode = _GetPreNode(m_first, targetNode.Data);

        return _InsertAtNodeNext(preNodeFromTargetNode, new_Data);
      }

      /// <summary>
      /// 通过一个节点数据获取该节点整体信息方法
      /// </summary>
      /// <param name="startNode">开始遍历查询的节点</param>
      /// <param name="targetData">目标节点数据</param>
      /// <returns></returns>
      private SingleLinkedNode<T> _GetNode(SingleLinkedNode<T> startNode, T targetData) {
        if (_CheckNodeDataIsNull(startNode.Data) || _CheckNodeDataIsNull(targetData))
          return null;
        var current = startNode;

        while (current != null) {
          if (current.Data.Equals(targetData))
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
      private SingleLinkedNode<T> _GetPreNode(SingleLinkedNode<T> startNode, T targetData) {
        if (_CheckNodeDataIsNull(startNode.Data) || _CheckNodeDataIsNull(targetData))
          return null;
        
        var current = startNode;

        while (current != null) {
          if (current.Next.Data.Equals(targetData))
            return current;
          current = current.Next;
        }

        return null;
      }

      private bool _CheckNodeDataIsNull(T nodeData) {
        if (nodeData == null) {
          Debug.LogAssertion("The node is null");
          return false;
        }

        return true;
      }
      
      private bool _CheckNodeIsNull(SingleLinkedNode<T> node) {
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