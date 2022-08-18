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

        private Array<DoubleLinkedNode<int>> array = new Array<DoubleLinkedNode<int>>();
        //CachePool<int> cachePool = new CachePool<int>();
        int i = 0;
        
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            while (i < 500) {
                double_list.Append(i+i);
                
                i++;
            }
            double_list.PrintAll();
        }
        
    }   
}
