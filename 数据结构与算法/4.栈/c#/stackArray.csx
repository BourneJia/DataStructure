using System;

public class Stack<T>{
  private T[] mData;
  private int mCount;
  public Stack(){
    mData = new T[10];
  }
  public Stack(int cap){
    if(cap < 0)
      throw new IndexOutOfRangeException();

    mData = new T[cap];
  }

  public bool IsEmpty(){
    return mData.Length == 0;
  }

  public T Top{
    get {
      if(IsEmpty()) 
        throw new IndexOutOfRangeException();
      return mData[mCount-1];
    }
  }

  public void Push(T data){
    if(mCount == mData.Length)
     throw new IndexOutOfRangeException();
    mData[mCount] = data;
    mCount++;
  }

  public T Pop(){
    if(mCount > 0){
      var top = Top;
      //释放Pop 出的内容
      mData[mCount] = default(T);
      mCount--;
      return top;
    }

    throw new IndexOutOfRangeException();
  }
}