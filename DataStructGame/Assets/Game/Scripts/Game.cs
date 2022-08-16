using System.Collections;
using System.Collections.Generic;
using Game.Scripts.CSharp.Array;
using Game.Scripts.CSharp.Link;
using UnityEngine;

namespace Game.Scripts {
    public class Game : MonoBehaviour {
        // Start is called before the first frame update
        void Start() {
            // var a = new Array<int>();
            // a[0] = 1;
            // a[1] = 2;
            // a[2] = 3;
            // a[3] = 4;
            // a[4] = 5;
            // a[5] = 6;
            // foreach (var value in a.data) {
            //     Debug.Log(value);
            // }
            var double_list = new DoubleLinkedList<int>();
            double_list.Append(23);
            double_list.Append(324);
            double_list.Append(564);
            double_list.Append(12);
            double_list.Append(6);
            // double_list.Prepend(7);
            // double_list.Prepend(76);
            // double_list.Prepend(45);
            // double_list.Prepend(75);
            // double_list.Prepend(345);
            // double_list.InsertAtTargetDataByNext(453);
            // double_list.DeleteAtDeleteDataByNext(564);
            foreach (var value in double_list) {
                Debug.Log(value);
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }   
}
