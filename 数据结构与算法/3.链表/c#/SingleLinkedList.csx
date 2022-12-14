using System;


SingleLinkedList<int> a = new SingleLinkedList<int>();

a.Insert(0,0);
a.Insert(1,1);
a.Insert(2,2);
a.Insert(3,3);
a.Insert(4,4);
a.Insert(5,5);
a.Insert(6,6);
a.Insert(7,7);
a.Insert(8,8);

// Print("第五位数为：");
Print(a.Find(7));
Print("删除前的长度：");
Print(a.Length);

// Print("删除第五位数");
a.Delete(7);
Print("删除后的长度");
Print(a.Length);
// Print("删除后的第五位数为：");
Print(a.Find(7));

/***
描述：单链表结构
    1. 提供增删改查功能
*/
public class SingleLinkedList<T>{
  private SingleLinkedNode<T> mFirst;
  private SingleLinkedNode<T> mLast;
  private int mLenght;

  public int Length{
    get {return mLenght;}
  }

  public SingleLinkedNode<T> Head {
    get { return mFirst;}
  }

  public SingleLinkedList(){
    mFirst  = null;
    mLast   = null;
    mLenght = 0;
  }

  private bool isEmpty(){
    return (mLenght == 0);
  }

  //插入单链表第一个位，替换mFirst
  public void Prepend(T dataItem){

    SingleLinkedNode<T> newNode = new SingleLinkedNode<T>(dataItem);

    if (isEmpty()){
      mFirst = mLast = newNode;
    } else {
      var currentNode = mFirst;
      newNode.Next = currentNode;
      mFirst = newNode;
    }

    mLenght++;

    //return true;
  }

  public void Append(T dataItem){
    SingleLinkedNode<T> newNode = new SingleLinkedNode<T>(dataItem);

    if (isEmpty()){
      mFirst = mLast = newNode;
    } else {
      var currentNode = mLast;
      currentNode.Next = newNode;
      mLast = newNode;
    }

    mLenght++;

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

      mLenght++;
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

      mFirst = mFirst.Next;
      mLenght--;
    
    } else if (index == Length-1){
      var currentNode = mFirst;
      
      while(currentNode.Next != null && currentNode.Next != mLast)
        currentNode = currentNode.Next;
      
      currentNode.Next = null;
      mLast = currentNode;
      mLenght--;
    
    } else {
      int i = 0;
      var currentNode = mFirst;

      //这样的好处是防止中间断层，其余方面暂时没有想到相比下方的foreach语法特性有其它优势
      while (currentNode.Next != null){
        if (i + 1 == index){
          currentNode.Next = currentNode.Next.Next;
          mLenght--;
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
      return mFirst.Data;
    }
    if(index == (Length - 1)){
      return mLast.Data;
    }
    if(index > 0 && index < (Length-1)){
      var currentNode = mFirst;
      for(int i = 0; i < index; i++)
        currentNode = currentNode.Next;
      return currentNode.Data;
    }

    throw new IndexOutOfRangeException();
  }

  public void Clear(){
    mFirst  = null;
    mLast   = null;
    mLenght = 0;
  }
}

/**
描述：链表结点类
mData：自身数据
mNext：指向下个数据的引用
*/
public class SingleLinkedNode<T>{
  private T mData;
  private SingleLinkedNode<T> mNext;

  //提供默认构造函数
  public SingleLinkedNode(){
    mNext = null;
    mData = default(T);
  }

  //提供赋值构造
  public SingleLinkedNode(T tData){
    mNext = null;
    mData = tData;
  }

  public T Data {
    set{ mData = value; }
    get{ return mData; }
  }

  public SingleLinkedNode<T> Next {
    get { return mNext; }
    set { mNext = value; }
  }
}