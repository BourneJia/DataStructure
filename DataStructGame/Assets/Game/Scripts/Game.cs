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
        CachePool<int> cachePool = new CachePool<int>();
        
        void Start() {
            // var i = 0;
            // while (i < 500) {
            //     double_list.Append(i+i);
            //     i++;
            // }
            //
            // var j = 0;
            // while (j < 500) {
            //     double_list.DeleteFirstNode();
            //     j++;
            // }
        }

        // Update is called once per frame
        void Update() {
            var i = 0;
            while (i < 500) {
                double_list.Append(i+i);//add
                //var a = double_list.Last;
                //array.Insert(array.length, double_list.Last);
                //double_list.DeleteFirstNode();
                //cachePool.Put(double_list.Last);
                i++;
            }
            
            // var j = 0;
            // while (j < 50) {
            //     var a = double_list.DeleteFirstNode();
            //     j++;
            // }
            //double_list.Clear();
        }
        
    }   
}
