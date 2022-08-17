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
        //ChachePool<int> chachePool = new ChachePool<int>();
        
        void Start() {
            var i = 0;
            while (i < 500) {
                double_list.Append(i+i);
                i++;
            }

            var j = 0;
            while (j < 200) {
                var deleteNode = double_list.DeleteFirstNode();
                //hachePool.Put(deleteNode);
                j++;
            }

            double_list.PrintAll();
            
            Debug.Log("*******************************************************");
            
            double_list.CachePool.PrintAll();
        }

        // Update is called once per frame
        void Update() {
            
        }
        
    }   
}
