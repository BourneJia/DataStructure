using System.Collections.Generic;
using Game.Scripts.CSharp.Link;
using UnityEngine;

namespace Game.Scripts.Common.CSharp {
public class CachePool<T>{
        public const int DEFAULT_CAPACITY = 10;
    
        private int m_count                                     = 0;
        private DoubleLinkedNode<T>[] m_cacheArray              = new DoubleLinkedNode<T>[DEFAULT_CAPACITY];
        private Dictionary<DoubleLinkedNode<T>, int> m_cacheDic = new Dictionary<DoubleLinkedNode<T>, int>(DEFAULT_CAPACITY);
        
        public DoubleLinkedNode<T> Get(int index = 0) {
            if (!IsEmpty())
                return m_cacheArray[index];

            return null;
        }

        public void Put(DoubleLinkedNode<T> data) {
            if (data == null) {
                Debug.LogAssertion("缓存容器不支持null！");
                //return default;
            }

            if (m_cacheDic.ContainsKey(data)) {
                var index = m_cacheDic[data];
                Update(index);
            }
            else {
                if(IsFull())
                    RemoveAndCache(data);
                else {
                    Cache(data,m_count);
                }
            }
            
        }
        
        public void Update(int end) {
            DoubleLinkedNode<T> target = m_cacheArray[end];
            //清除之前重复的内容。
            m_cacheArray[end] = default;
            m_cacheDic.Remove(target);
            
            RightShift(end - 1);
            m_cacheArray[0] = target;
            m_cacheDic.Add(target,0);
            
        }
        
        public void Cache(DoubleLinkedNode<T> data, int end) {
            RightShift(end);
            m_cacheArray[0] = data;
            m_cacheDic.Add(data,0);
            m_count++;
        }
        
        public void RemoveAndCache(DoubleLinkedNode<T> data) {
            DoubleLinkedNode<T> key = m_cacheArray[--m_count];
            m_cacheDic.Remove(key);
            key.Clear();
            Cache(data,m_count);
        }
        
        public void RightShift(int end) {
            for (int i = end - 1;  i >= 0; i--) {
                m_cacheArray[i + 1] = m_cacheArray[i];
                m_cacheDic.Remove(m_cacheArray[i]);
                m_cacheDic.Add(m_cacheArray[i],i+1);
            }
        }

        public bool IsContain(DoubleLinkedNode<T> data) {
            return m_cacheDic.ContainsKey(data);
        }

        public bool IsEmpty() {
            return m_count == 0;
        }

        public bool IsFull() {
            return m_count == DEFAULT_CAPACITY;
        }

        public void Clear() {
            if (!IsEmpty()) {
                for (int i = 0; i < m_count; i ++) {
                    m_cacheArray[i].Clear();
                }
            }
            
            m_count = default;
            m_cacheArray = null;
            m_cacheDic = null;
        }
        
        public void PrintAll() {
            for(int i = 0; i < m_count; i++) {
                Debug.Log("m_cacheArray value:"+ m_cacheArray[i]);
                Debug.Log("m_cacheArray index:"+ i);
            }
        
            foreach (var value in m_cacheDic) {
                Debug.Log("m_cacheDic value:"+value.Key.Data.ToString());
                Debug.Log("m_cacheDic index:"+value.Value.ToString());
            }
        }
    }
}