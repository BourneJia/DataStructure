using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Common.CSharp;
using Game.Scripts.CSharp.Array;
using Game.Scripts.CSharp.Link;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts {
    public class Game : MonoBehaviour {
        // Start is called before the first frame update
        DoubleLinkedList<int> double_list = new DoubleLinkedList<int>();
        ChachePool<DoubleLinkedNode<int>> chachePool = new ChachePool<DoubleLinkedNode<int>>();
        
        void Start() {

            for (int i = 0; i < 1000; i++) {
                DoubleLinkedNode<int> a = new DoubleLinkedNode<int>();
                chachePool.Put(a);
            }

            double_list.CachePool = chachePool;

            if (chachePool.Get() != null) {
                
            }
            else {
                Debug.Log("is null");
            }

            // double_list.Append(5);
            // double_list.Append(6);
            // double_list.PrintAll();
            
            
        }

        // Update is called once per frame
        void Update() {
            
        }

        public class Student<T> {

            // private ChachePool<Student<T>> chachePool = null;
            //
            // public Student(ChachePool<Student<T>> _chachePool) {
            //     
            // }

            public string Name {
                get;
                set;
            }

            public int Age {
                get;
                set;
            }

            public string Address {
                get;
                set;
            }

            public T value {
                get;
                set;
            }
        }
    }   
}
