using System;

DoubleLinkedList<int> a = new DoubleLinkedList<int>(); 

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
Print(a.GetElement(7));
Print("删除前的长度：");
Print(a.Count);

// Print("删除第五位数");
a.Delete(7);
Print("删除后的长度");
Print(a.Count);
// Print("删除后的第五位数为：");
Print(a.GetElement(7));

public class DoubleLinkedList<T>{
  private int mCount;
  private DoubleLinkedNode<T> mFirst;
  private DoubleLinkedNode<T> mLast;

  public int Count{
    get {return mCount;}
  }

  public DoubleLinkedNode<T> Head{
    get {return mFirst;}
  }
  public DoubleLinkedList(){
    mFirst = null;
    mLast = null;
    mCount = 0;
  }

  private bool isEmpty(){
    return (Count == 0);
  }

  public T GetElement(int index){
    if(isEmpty() || index < 0 || index >= Count){
      Console.WriteLine("Index 不正确，且链表可能为空");
      throw new IndexOutOfRangeException();
    }

    if(index == 0){
      return mFirst.Data;
    } else if (index == (Count - 1)){
      return mLast.Data;
    } 

    DoubleLinkedNode<T> currentNode = null;

    if(index > Count/2){
      currentNode = mLast;
      for (int i = (Count - 1); i > index; i--){
        currentNode = currentNode.Previous;
      }
    } else {
      currentNode = mFirst;
      for(int i = 0; i < index; i++){
        currentNode = currentNode.Next;
      }
    }

    return currentNode.Data;
  }

  public bool SetElementByIndex(int index, T newEle){
    bool isSuccess = true;

    if(isEmpty() || index < 0 || index > Count){
      isSuccess = false;
      throw new IndexOutOfRangeException("Index 不正确，且链表可能为空");
    }

    if(index == 0){
      mFirst.Data = newEle;
    } else if(index == (Count - 1)){
      mLast.Data = newEle;
    } else {
      DoubleLinkedNode<T> currentNode = null;

      if(index > (Count / 2)){
        currentNode = mLast;
        for(int i = (Count - 1); i > index; i--){
          currentNode = currentNode.Previous;
        }
      } else {
        currentNode = mFirst;
        for(int i = 0; i < index; i++){
          currentNode = currentNode.Next;
        }
      }

      currentNode.Data = newEle;
    }

    return isSuccess;
  }

  public void Prepend(T dataItem){
    DoubleLinkedNode<T> newNode = new DoubleLinkedNode<T>(dataItem);

    if(isEmpty()){
      mFirst = mLast = newNode;
    } else {
      var currentNode = mFirst;
      mFirst = newNode;
      newNode.Next = currentNode;
      currentNode.Previous = mFirst;
    }

    mCount++;
  }

  public void Append(T dataItem){
    DoubleLinkedNode<T> newNode = new DoubleLinkedNode<T>(dataItem);

    if(isEmpty()){
      mFirst = mLast = newNode;
    } else {
      var currentNode = mLast;
      mLast = newNode;
      currentNode.Next = mLast;
      mLast.Previous = currentNode;
    }

    mCount++;
  }

  //需要画图梳理下内容，尤其是插入内容
  public void Insert(int index, T newEle){
    if (index < 0 || index > Count){
      throw new IndexOutOfRangeException("index 超出范围");
    }

    if(index == 0){
      Prepend(newEle);
    } else if (index == Count){
      Append(newEle);
    } else {
      DoubleLinkedNode<T> currentNode = null;
      DoubleLinkedNode<T> newNode = new DoubleLinkedNode<T>(newEle);

      if(index > (Count/2)){
        currentNode = mLast;

        for(int i = mCount - 1 ; i > index; i--){
          currentNode = currentNode.Previous;
        }

        var oldNode = currentNode;

        if(oldNode != null){
          currentNode.Next.Previous = newNode;
        }

        newNode.Next = oldNode;
        currentNode.Next = newNode;
        newNode.Previous = currentNode;

        mCount++;
      } else {
        currentNode = mFirst;

        for(int i = 0; i < index; i++){
          currentNode = mFirst;
        }

        var oldNode = currentNode;

        if(oldNode != null){
          currentNode.Next.Previous = newNode;
        }

        newNode.Next = oldNode;
        currentNode.Next = newNode;
        newNode.Previous = currentNode;

        mCount++;
      }
    }

  }

  //删除部分逻辑可以画图处理下
  public void Delete(int index){
    if (isEmpty() || index < 0 || index > Count){
      throw new IndexOutOfRangeException("index 超出范围");
    }

    if(index == 0){
      mFirst = mFirst.Next;
      if(mFirst != null)
        mFirst.Previous = null;
    } else if (index == Count - 1){
      mLast = mLast.Previous;
      if(mLast != null)
        mLast.Next = null;
    } else {
      int i = 0;
      var currentNode = mFirst;

      while(i < index){
        currentNode = currentNode.Next;
        i++;
      }

      var newPrevious = currentNode.Previous;
      var newNext = currentNode.Next;
      newPrevious.Next = newNext;

      if(newNext != null)
        newNext.Previous = newPrevious;

      currentNode = newPrevious;
    } 

    mCount--;
  }

  public void Clear(){
    mCount = 0;
    mFirst = mLast = null;
  }
}


public class DoubleLinkedNode<T>{
  private T mData;
  private DoubleLinkedNode<T> mNext;
  private DoubleLinkedNode<T> mPrevious;

  public DoubleLinkedNode(){
    mData = default(T);
    mNext = null;
    mPrevious = null;
  }

  public DoubleLinkedNode(T dataItem){
    mData = dataItem;
    mNext = null;
    mPrevious = null;
  }

  public DoubleLinkedNode(T dataItem, DoubleLinkedNode<T> next, DoubleLinkedNode<T> previous){
    mData = dataItem;
    mNext = next;
    mPrevious = previous;
  }

  public T Data{
    get {return mData;}
    set {mData = value;}
  }

  public DoubleLinkedNode<T> Next{
    get {return mNext;}
    set {mNext = value;}
  }

  public DoubleLinkedNode<T> Previous{
    get {return mPrevious;}
    set {mPrevious = value;}
  }
}