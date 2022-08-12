using System;

namespace Game.Scripts.CSharp.Link {
    public class SingleLinkedList<T> {
      private SingleLinkedNode<T> m_first;
      private SingleLinkedNode<T> m_last;
      private int m_length;

      public int Length{
        get {
          return m_length;
        }
      }

      public SingleLinkedNode<T> Head {
        get {
          return m_first;
        }
      }

      public SingleLinkedList(){
        m_first  = null;
        m_last   = null;
        m_length = 0;
      }

      private bool isEmpty(){
        return (m_length == 0);
      }

      //插入单链表第一个位，替换mFirst
      public void Prepend(T dataItem){

        SingleLinkedNode<T> newNode = new SingleLinkedNode<T>(dataItem);

        if (isEmpty()){
          m_first = m_last = newNode;
        } else {
          var currentNode = m_first;
          newNode.Next = currentNode;
          m_first = newNode;
        }

        m_length++;

        //return true;
      }

      public void Append(T dataItem){
        SingleLinkedNode<T> newNode = new SingleLinkedNode<T>(dataItem);

        if (isEmpty()){
          m_first = m_last = newNode;
        } else {
          var currentNode = m_last;
          currentNode.Next = newNode;
          m_last = newNode;
        }

        m_length++;

        //return true;
      }

      public bool Insert(int index, T dataItem){
        bool isSuccess = true;

        if(index == 0){
          Prepend(dataItem);
        } else if (index == Length){
          Append(dataItem);
        } else if (index > 0 && index < Length ) {
          var currentNode = Head;
          var newNode = new SingleLinkedNode<T>(dataItem);
          for(int i = 0; i < index; i++){
            currentNode = currentNode.Next;
          }
          newNode.Next = currentNode.Next;
          currentNode.Next = newNode;

          m_length++;
        } else {
          Console.WriteLine("index 不在规定范围内");
          isSuccess = false;
        }
        return isSuccess;
      }

      public bool Delete(int index){
        bool isSuccess = true;

        if(isEmpty() || index < 0 || index >= Length){
          Console.WriteLine("index 不在规定范围内");
          isSuccess = false;
        }

        if(index == 0){

          m_first = m_first.Next;
          m_length--;
        
        } else if (index == Length-1){
          var currentNode = m_first;
          
          while(currentNode.Next != null && currentNode.Next != m_last)
            currentNode = currentNode.Next;
          
          currentNode.Next = null;
          m_last = currentNode;
          m_length--;
        
        } else {
          int i = 0;
          var currentNode = m_first;

          //这样的好处是防止中间断层，其余方面暂时没有想到相比下方的foreach语法特性有其它优势
          while (currentNode.Next != null){
            if (i + 1 == index){
              currentNode.Next = currentNode.Next.Next;
              m_length--;
              break;
            }
            i++;
            currentNode = currentNode.Next;
          }

          // for(int j = 0; j < index; j++){
          //   if (j == (index-1)){
          //     currentNode.Next = currentNode.Next.Next;
          //     mLenght--;
          //     break;
          //   }

          //   currentNode = currentNode.Next;
          // }
        } 

        return isSuccess;
      }

      public T Find(int index){
        if(isEmpty() || index < 0 || index >= Length){
          Console.WriteLine("index 不在规定范围内");
          throw new IndexOutOfRangeException();
        }

        if(index == 0){
          return m_first.Data;
        }
        if(index == (Length - 1)){
          return m_last.Data;
        }
        if(index > 0 && index < (Length-1)){
          var currentNode = m_first;
          for(int i = 0; i < index; i++)
            currentNode = currentNode.Next;
          return currentNode.Data;
        }

        throw new IndexOutOfRangeException();
      }

      public void Clear(){
        m_first  = null;
        m_last   = null;
        m_length = 0;
      }
    }
    
    /**
    描述：链表结点类
    mData：自身数据
    mNext：指向下个数据的引用
    */
    public class SingleLinkedNode<T>{
        private T m_data;
        private SingleLinkedNode<T> m_next;

        //提供默认构造函数
        public SingleLinkedNode(){
            m_next = null;
            m_data = default(T);
        }

        //提供赋值构造
        public SingleLinkedNode(T tData){
            m_next = null;
            m_data = tData;
        }

        public T Data {
            set{ m_data = value; }
            get{ return m_data; }
        }

        public SingleLinkedNode<T> Next {
            get { return m_next; }
            set { m_next = value; }
        }
    }
}