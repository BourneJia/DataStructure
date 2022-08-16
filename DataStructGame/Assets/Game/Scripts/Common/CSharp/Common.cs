using System;
using System.Collections;
using UnityEngine;

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

    // public class SingleNodeCache<T> where T : new(){
    //     private readonly object OBJ = new object();
    //     
    //     private T m_instance = default(T);
    //
    //     public T Instance {
    //         get {
    //             lock (OBJ) {
    //                 if (m_instance == null) {
    //                     m_instance = new T();
    //                 }
    //
    //                 return m_instance;
    //             }
    //         }
    //     }
    // }

    // public class ChachePool<T> {
    //     private const int DEFAULT_CAPACITY = 10;
    //
    //     private int m_count      = 0;
    //     private T[] m_cacheList  = new T[DEFAULT_CAPACITY];
    //     private Hashtable m_hash = new Hashtable(DEFAULT_CAPACITY);
    //
    //     
    // }

}