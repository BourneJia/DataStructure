using System;
using System.Collections;
using System.Collections.Generic;
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
        
        public static int GetRandomNumber(){
            int lvl = 0;

            //如果随机数大于等于0.5，且在规定范围内，就进行返回，否则就提升级别
            while ((new Random()).NextDouble() < 0.5 && lvl <= 1 && lvl < 32)
                lvl++;

            return lvl;
        }
    }

    public class ChachePool<T> where T : new(){
        public const int DEFAULT_CAPACITY = 1000;
    
        private int m_count                        = 0;
        private int m_tail                         = -1;
        private int m_head                         = 0;
        private DoubleLinkedNode<T>[] m_cacheArray = new DoubleLinkedNode<T>[DEFAULT_CAPACITY];
        //private Dictionary<T, int> m_cacheDic = new Dictionary<T, int>(DEFAULT_CAPACITY);

        public int Count => m_count;
        public int Tail => m_tail;
        public int Head => m_head;
        
        public DoubleLinkedNode<T> HeadData {
            get { 
                if (IsEmpty()) 
                    return default;
                return m_cacheArray[m_head];
            }
        }

        public DoubleLinkedNode<T> TailData {
            get {
                if (IsEmpty()) 
                    return default;
                return m_cacheArray[m_tail];
            }
        }

        public DoubleLinkedNode<T> GetNodeInstance() {
            if (!IsFull()) {
                for (int i = 0; i < DEFAULT_CAPACITY; i++) {
                    if (m_cacheArray[i] == null) {
                        m_cacheArray[i] = new DoubleLinkedNode<T>();
                        return m_cacheArray[i];
                    }
                }
            }
            return m_cacheArray[0];
        }
        
        //public 

        // public void Add() {
        //     
        // }

        // //获取最新添加的内容
        // public T Get() {
        //     if (IsEmpty()) {
        //         Debug.LogAssertion("容器为空");
        //         return default;
        //     }
        //
        //     // if (m_cacheArray[0] == null) {
        //     //     this.Put(new T());
        //     // }
        //
        //     return m_cacheArray[0];
        // }
        //
        // public void Put(T data) {
        //     if (data == null) {
        //         Debug.LogAssertion("缓存容器不支持null！");
        //         return;
        //     }
        //     // var index = m_cacheDic[data];
        //     
        //     if (m_cacheDic.ContainsKey(data)) {
        //         var index = m_cacheDic[data];
        //         Update(index);
        //     }
        //     else {
        //         if(IsFull())
        //             RemoveAndCache(data);
        //         else {
        //             Cache(data,m_count);
        //         }
        //     }
        //
        //     //int index = holder.(data);
        // }
        //
        // public void Update(int end) {
        //     T target = m_cacheArray[end];
        //     RightShift(end);
        //     m_cacheArray[0] = target;
        //     m_cacheDic.Add(target,0);
        // }
        //
        // public void Cache(T data, int end) {
        //     RightShift(end);
        //     m_cacheArray[0] = data;
        //     m_cacheDic.Add(data,0);
        //     m_count++;
        //     //m_tail++;
        // }
        //
        // public void RemoveAndCache(T data) {
        //     T key = m_cacheArray[--m_count];
        //     m_cacheDic.Remove(key);
        //     Cache(data,m_count);
        //     // if (m_tail >= m_count) {
        //     //     m_tail = 0;
        //     // }
        //     //
        //     // if (m_head >= m_count) {
        //     //     m_head = 0;
        //     // }
        //     // else {
        //     //     m_head++;
        //     // }
        // }
        //
        // public void RightShift(int end) {
        //     for (int i = end - 1;  i >= 0; i--) {
        //         m_cacheArray[i + 1] = m_cacheArray[i];
        //         m_cacheDic.Remove(m_cacheArray[i]);
        //         m_cacheDic.Add(m_cacheArray[i],i+1);
        //     }
        // }

        // public bool IsContain(T data) {
        //     return m_cacheDic.ContainsKey(data);
        // }

        public bool IsEmpty() {
            return m_count == 0;
        }

        public bool IsFull() {
            return m_count == DEFAULT_CAPACITY;
        }

        // public void Clear() {
        //     m_count = 0;
        //     m_cacheArray = null;
        //     m_cacheDic = null;
        // }
        //
        // public void PrintAll() {
        //     for(int i = 0; i < m_count; i++) {
        //         Debug.Log("m_cacheArray value:"+ m_cacheArray[i]);
        //         Debug.Log("m_cacheArray index:"+ i);
        //     }
        //
        //     foreach (var value in m_cacheDic) {
        //         Debug.Log("m_cacheDic value:"+value.Key.ToString());
        //         Debug.Log("m_cacheDic index:"+value.Value.ToString());
        //     }
        // }
    }

}