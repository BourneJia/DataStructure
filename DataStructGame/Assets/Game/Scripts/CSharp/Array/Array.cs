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
      //提供一个默认值进行初始化
      private T[] m_data     = new T[10];
      private int m_capacity = 10;
      private int m_length   = 0;
      
      public T[] Data   => m_data;
      public int Length => m_length;

      public Array() {
        
      }

      public Array(int capacity){
        m_data     = new T[capacity];
        m_capacity = capacity;
      }

      public bool Insert(int index, T newElem){
        //判断是否有容量
        if(m_length >= m_capacity){
          _Resize();
        }
        //判断下标是否正确，防止出现不连续下标以及负数下标
        Common.CSharp.Common.S_CheckIndexOutForNotEqual(index,m_length);

        //此处把替换位置后面的值进行平移挪动，然后进行赋值
        for(int i = m_length; i > index; i--){
          m_data[i] = m_data[i-1];
        }

        m_data[index] = newElem;
        m_length++;

        return true;
      }
      
      public bool Delete(int index){
        //判断下标是否正确，防止出现不连续下标以及负数下标
        Common.CSharp.Common.S_CheckIndexOutForEqual(index,m_length);
        
        //此处把替换位置前面的值进行平移挪动，然后进行赋值
        for(int i = index; i < m_length-1; i++){
          m_data[i] = m_data[i+1];
        }

        m_length--;

        return true;
      }

      public void Clear(){
        m_data   = null;
        m_length = 0;
      }

      public void PrintAll(){
        for(int i = 0; i < m_length; i++){
          Debug.Log("mData["+i+"]:"+m_data[i].ToString());
        }
      }
      
      //双倍扩容
      private void _Resize(){
        m_capacity = m_capacity*2;
        T[] newData = new T[m_capacity];

        for(int i = 0; i < m_length; i++){
          newData[i] = m_data[i];
        }

        m_data = newData;
      }

   } 
}