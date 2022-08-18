using System;
using Game.Scripts.Common.CSharp;
using UnityEngine;

namespace Game.Scripts.CSharp.Array
{
   /**描述：c#自定义数组
  功能点：
       1. 可以动态扩容
       2. 可以插入，删除
       3. 本身自带快速随机访问特性，因此不特地提供查询
  */
  public class Array<T> {
      private const int DEFAULT_CAPACITY = 10;
      
      private T[] m_data;
      private int m_capacity;
      private int m_length               = 0;
      public T[] data   => m_data;
      public int capacity => m_capacity;
      public int length => m_length;

      public T this[int index] {
        get {
          //index 不可以与 m_length 相等，否则超出下标
          if (Common.CSharp.Common.CheckIndexOutForEqual(index, m_length))
            throw new Exception("数组出现越界");
          return m_data[index];
        }
        set {
          //设定只能重新赋值，不能进行添加，否则需要加入扩容操作
          if (Common.CSharp.Common.CheckIndexOutForEqual(index, m_length))
            return;
          m_data[index] = value;
        }
      }
      
      public Array() : this(DEFAULT_CAPACITY){
        
      }

      public Array(int size) {
        _Resize(size);
      }

      public bool Insert(int index, T new_Elem){
        //判断下标是否正确，防止出现不连续下标以及负数下标
        if (Common.CSharp.Common.CheckIndexOutForNotEqual(index, m_length))
          return false;
        
        //判断是否有容量
        if(m_length >= m_capacity){
          _Resize(m_capacity*2);
        }

        //此处把替换位置后面的值进行平移挪动，然后进行赋值
        for(int i = m_length; i > index; i--){
          m_data[i] = m_data[i-1];
        }

        m_data[index] = new_Elem;
        m_length++;

        return true;
      }
      
      public bool Delete(int index){
        //判断下标是否正确，防止出现不连续下标以及负数下标
        if(Common.CSharp.Common.CheckIndexOutForEqual(index,m_length))
          return false;
        
        //此处把替换位置前面的值进行平移挪动，然后进行赋值
        for(int i = index; i < m_length-1; i++){
          m_data[i] = m_data[i+1];
        }

        m_length--;

        return true;
      }

      public void Clear(){
        m_data     = null;
        m_length   = 0;
        m_capacity = 0;
      }

      public void PrintAll(){
        for(int i = 0; i < m_length; i++){
          Debug.Log("mData["+i+"]:"+m_data[i].ToString());
        }
      }
      
      //双倍扩容
      private void _Resize(int size){
        m_capacity = size;
        T[] newData = new T[m_capacity];

        for(int i = 0; i < m_length; i++){
          newData[i] = m_data[i];
        }

        m_data = newData;
      }

   } 
}