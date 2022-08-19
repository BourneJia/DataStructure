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
        DoubleLinkedList<int> double_list = new DoubleLinkedList<int>();
        // int i = 0;
        
        void Start() {
            // int i = 0;
            // while (i < 10) {
            //     double_list.Append(i+i);
            //     
            //     i++;
            // }
            //
            // double_list.PrintAll();
            //
            // int j = 0;
            // while (j < 5) {
            //     double_list.DeleteAtDeleteData(double_list.First.Data);
            //
            //     j++;
            // }
            //
            // double_list.PrintAll();
            //
            // double_list.Append(0);
            //
            // double_list.PrintAll();
            // int z = 0;
            // while (z < 10) {
            //     double_list.Append(z+z);
            //     
            //     z++;
            // }
            //
            // double_list.PrintAll();
            
            
            
            // int x = 0;
            // while (x < 5) {
            //     double_list.DeleteAtDeleteData(double_list.First.Data);
            //
            //     x++;
            // }
            //
            // double_list.PrintAll();
        }

        // Update is called once per frame
        void Update() {
            int i = 0;
            while (i < 100) {
                double_list.Append(i+i);
                
                i++;
            }
            
            double_list.PrintAll();
            
            int j = 0;
            while (j < 50) {
                double_list.DeleteAtDeleteData(double_list.First.Data);
            
                j++;
            }
            
            double_list.PrintAll();
        }
        
    }   
}
