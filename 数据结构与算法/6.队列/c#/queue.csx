using System;

public class Queue<T>{
  private int mSize;
  private int head;
  private int tail;
  private T[] mData;
  private const int defaultCap = 10;

  public int Size{
    get { return mSize; }
  }

  public Queue() : this(defaultCap) {
    // mSize = 0;
    // head  = 0;
    // tail  = 0;
    // mData = new T[defaultCap];
  }

  public Queue(int size){
    if(size < 0)
      throw new IndexOutOfRangeException();
        mSize = 0;
    head  = 0;
    tail  = 0;
    mData = new T[size];
  }

  public bool IsEmpty{
    get {return (mSize == 0);}
  }

  public T Top {
    get {
      if (IsEmpty)
        throw new Exception("Queue is empty");
        
      return mData[head];
    }
  }

  public void Enqueue(T dataItem){
    if(mSize == mData.Length)
      throw new Exception("Queue No Space");
    
    mData[tail++] = dataItem;

    //用于循环添加到array中
    if(tail == mData.Length)
      tail = 0;

    mSize++;
  }

  public T Dequeue(){
    if(IsEmpty)
      throw new Exception("Queue is empty");

    var topItem = mData[head];
    mData[head] = default(T);

    mSize--;

    head++; 

    //用于循环添加到array中
    if(head == mData.Length)
      head = 0;

    return topItem;
  }
}