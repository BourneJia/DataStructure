using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Game.Scripts.CSharp.Array;
using Game.Scripts.CSharp.Link;
using UnityEngine;
using Random = System.Random;

namespace Game.Scripts.Common.CSharp {
    public static class Common {
        public static bool CheckIndexOutForEqual(int a, int b) {
            bool result = false;
            
            if(a < 0 || a >= b){
                Debug.LogAssertion("数组下标超出数组范围");
                result = true;
            }

            return result;
        }

        public static bool CheckIndexOutForNotEqual(int a, int b) {
            bool result = false;
            
            if(a < 0 || a > b){
                Debug.LogAssertion("数组下标超出数组范围");
                result = true;
            }

            return result;
        }
    }

    // public class DoubleNodeArrayPool<T> {
    //     private const int DEFAULT_CAPACITY = 10000;
    //     private int m_capacity;
    //     private int m_count;
    //     private int m_tailIndex;
    //     private DoubleLinkedNode<T>[] m_poolArray;
    //     private Dictionary<T, int> m_poolDic;
    //     private ArrayList m_deleteCache;
    //
    //     public DoubleNodeArrayPool():this(DEFAULT_CAPACITY) {
    //         
    //     }
    //     
    //     public DoubleNodeArrayPool(int capacity) {
    //         m_capacity = capacity;
    //         m_count = 0;
    //         m_tailIndex = 0;
    //         m_poolArray = new DoubleLinkedNode<T>[m_capacity];
    //         m_poolDic = new Dictionary<T, int>();
    //         m_deleteCache = new ArrayList();
    //     }
    //
    //     public DoubleLinkedNode<T> GetNodeInstance(T data) {
    //         DoubleLinkedNode<T> resultNode = null;
    //         if (data == null)
    //             return resultNode;
    //
    //         if (IsIndexMax()) {
    //             var delIndex = IsFull();
    //             if (delIndex > 0) {
    //                 m_poolArray[delIndex].Data = data;
    //                 m_poolDic.Add(m_poolArray[delIndex].Data,delIndex);
    //                 m_count++;
    //                 resultNode = m_poolArray[delIndex];
    //             }
    //             else {
    //                 _Resize(2*DEFAULT_CAPACITY);
    //                 resultNode = _Add(data);
    //             }
    //         }
    //         else {
    //             resultNode = _Add(data);
    //         }
    //
    //         return resultNode;
    //     }
    //
    //     public DoubleLinkedNode<T> GetNodeByData(T data) {
    //         if (data == null)
    //             return null;
    //         
    //         var index = m_poolDic[data];
    //         return m_poolArray[index];
    //     }
    //
    //     public DoubleLinkedNode<T> DeleteNode(T data) {
    //         if (data == null)
    //             return null;
    //         
    //         var delIndex = m_poolDic[data];
    //         var resultNode = m_poolArray[delIndex];
    //         m_poolArray[delIndex].Clear();
    //         m_poolDic.Remove(data);
    //
    //         return resultNode;
    //     }
    //
    //     private DoubleLinkedNode<T> _Add(T data) {
    //         m_poolArray[m_tailIndex] = new DoubleLinkedNode<T>(data);
    //         m_poolDic.Add(m_poolArray[m_tailIndex].Data, m_tailIndex);
    //         var currentNode = m_poolArray[m_tailIndex];
    //         m_count++;
    //         m_tailIndex++;
    //         return currentNode;
    //     }
    //
    //     private void _Resize(int size) {
    //         m_capacity = size;
    //         DoubleLinkedNode<T>[] new_array = new DoubleLinkedNode<T>[m_capacity];
    //         //Dictionary<T, int> new_dic = new Dictionary<T, int>();
    //
    //         for (int i = 0; i < m_count; i++) {
    //             new_array[i] = m_poolArray[i];
    //             //new_dic.Add(new_array[i].Data, i);
    //         }
    //
    //         m_poolArray = new_array;
    //     }
    //
    //     /// <summary>
    //     /// 判断array是否到达最大值下标
    //     /// </summary>
    //     /// <returns></returns>
    //     public bool IsIndexMax() {
    //         return (m_tailIndex >= m_capacity);
    //     }
    //
    //     public bool IsEmpty() {
    //         return (m_count == 0);
    //     }
    //
    //     /// <summary>
    //     /// 判断数组是否已经满了
    //     /// </summary>
    //     /// <returns>-1表示已经满了</returns>
    //     public int IsFull() {
    //         for (int i = 0; i <= m_count; i++) {
    //             if (m_poolArray[i].Data == null)
    //                 return i;
    //         }
    //
    //         return -1;
    //     }
    //
    //     public void _Clear() {
    //         m_capacity = 0;
    //         m_count = 0;
    //         m_deleteCache = null;
    //         m_poolArray = null;
    //         m_poolDic = null;
    //     }
    // }

}