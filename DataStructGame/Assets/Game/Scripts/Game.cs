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
        private DlinkedList<int> double_list = new DlinkedList<int>();
        private SingleLinkedList<int> single_list = new SingleLinkedList<int>();

        void Start() {

            var i = 0;
            while (i<10) {
                double_list.Add(i + 1);
                i++;
            }
            
            double_list.PrintAll();

            var j = 0;
            while (j<5) {
                double_list.Delete(j+1);
                j++;
            }
            
            double_list.PrintAll();
        }

        // Update is called once per frame
        void Update() {
            // var i = 0;
            // while (i < 200) {
            //     double_list.Add(i + 1);
            //     i++;
            // }
            //
            // double_list.PrintAll();
        }
        
    }

    public class Student {
        public string Name {
            get;
            set;
        }

        public int Age {
            get;
            set;
        }

        public Student(string name,int age) {
            Name = name;
            Age = age;
        }

        public void Clear() {
            Name = null;
            Age = default;
        }
    }
}
