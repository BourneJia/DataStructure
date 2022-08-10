using System;

Array<int> a = new Array<int>(10);
a.Insert(0,1);
a.Insert(1,2);
a.Insert(2,3);
a.Insert(3,4);
a.Insert(4,5);
a.Insert(5,6);

int b = a.Find(2);
Print(a.Length);
var d = a.Delete(0);
Print(a.Length);
a.PrintAll();

/**
描述：c#自定义数组
功能点：
     1. 可以动态扩容
     2. 可以插入，删除，以及查询
*/
public class Array<T> where T : IComparable<T> {
    //基础数组结构，用于数据存储
    private T[] mData;
    //数组容量
    private int mCapacity;
    //数组内部长度
    private int mLength;

    public int Length => mLength;

    public T[] Data => mData;

    public Array(int capacity){
      mData     = new T[capacity];
      mCapacity = capacity;
      mLength   = 0;
    }

    public bool Insert(int index, T newElem){
      //判断是否有容量
      if(mLength == mCapacity){
        //Console.WriteLine("error: 数组没有多余空间进行添加");
        //throw new Exception("error: 数组下标超出数组范围");
        //return false;
        resize();
      }
      //判断下标是否正确，防止出现不连续下标以及负数下标
      if(index < 0 || index > mLength){
        //Console.WriteLine("error: 数组下标超出数组范围");
        throw new Exception("error: 数组下标超出数组范围");
        //return false;
      }

      //此处把替换位置后面的值进行平移挪动，然后进行赋值
      for(int i = mLength; i > index; i--){
        mData[i] = mData[i-1];
      }

      mData[index] = newElem;

      mLength++;

      return true;
    }

    //根据下标查找对应数组值
    public T Find(int index){
      if(index < 0 || index > mLength-1){
        //Console.WriteLine("error: 数组下标超出数组范围");
        throw new IndexOutOfRangeException("Index was outside the bounds of the list");
      }

      return mData[index];
    }

    public bool Delete(int index){
      if(index < 0 || index > mLength-1){
        //Console.WriteLine("error: 数组下标超出数组范围");
        throw new Exception("error: 数组下标超出数组范围");
        //return false;
      }
      
      //此处把替换位置前面的值进行平移挪动，然后进行赋值
      for(int i = index; i < mLength-1; i++){
        mData[i] = mData[i+1];
      }

      mLength--;

      return true;
    }

    public void Clear(){
      mData   = new T[mCapacity];
      mLength = 0;
    }

    public void PrintAll(){
      for(int i = 0; i < mLength; i++){
        Console.WriteLine("mData["+i+"]");
        Console.WriteLine(mData[i].ToString());
      }
    }
    
    //双倍扩容
    private void resize(){
      mCapacity = mCapacity*2;
      T[] newData = new T[mCapacity];

      for(int i = 0; i < mLength; i++){
        newData[i] = Data[i];
      }

      mData = newData;
    }
}