using System.Collections;
using System.Collections.Generic;
using Game.Scripts.CSharp.Array;
using Game.Scripts.CSharp.Link;
using UnityEngine;

namespace Game.Scripts {
    public class Game : MonoBehaviour {
        // Start is called before the first frame update
        void Start() {
            Debug.Log("Game Start 1");
            DoubleLinkedList<int> d_link = new DoubleLinkedList<int>();
            DoubleLinkedNode<int> a = new DoubleLinkedNode<int>(1);
            DoubleLinkedNode<int> b = new DoubleLinkedNode<int>(2);
            DoubleLinkedNode<int> c = new DoubleLinkedNode<int>(3);
            DoubleLinkedNode<int> d = new DoubleLinkedNode<int>(4);
            DoubleLinkedNode<int> e = new DoubleLinkedNode<int>(5);
            DoubleLinkedNode<int> f = new DoubleLinkedNode<int>(6);
            DoubleLinkedNode<int> g = new DoubleLinkedNode<int>(7);
            DoubleLinkedNode<int> h = new DoubleLinkedNode<int>(8);
            DoubleLinkedNode<int> i = new DoubleLinkedNode<int>(9);
            DoubleLinkedNode<int> k = new DoubleLinkedNode<int>(10);
            
            d_link.Add(Common.CSharp.Common.NodeDirection.Next,a);
            d_link.Add(Common.CSharp.Common.NodeDirection.Next,b);
            d_link.Add(Common.CSharp.Common.NodeDirection.Next,c);
            d_link.Add(Common.CSharp.Common.NodeDirection.Next,d);
            d_link.Add(Common.CSharp.Common.NodeDirection.Next,e);
            d_link.Add(Common.CSharp.Common.NodeDirection.Next,f);
            d_link.Add(Common.CSharp.Common.NodeDirection.Next,g);
            d_link.Add(Common.CSharp.Common.NodeDirection.Next,h);
            d_link.Add(Common.CSharp.Common.NodeDirection.Next,i,c);
            
            d_link.Delete(Common.CSharp.Common.NodeDirection.Next,c);
            // d_link.Append(b);
            // d_link.Append(c);
            // d_link.Append(d);
            // d_link.Append(e);
            // d_link.Append(f);
            // d_link.Append(g);
            // d_link.DeleteByNode(e);
            // d_link.DeleteFirstNode();
            // d_link.Prepend(k);
            // d_link.InsertByNode(f,i);
            d_link.PrintAll();
            
            Debug.Log("Game Start");
            //Debug.Log(d_link.First.Data.ToString());
            
            foreach (var node in d_link) {
                Debug.Log(node);
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }   
}
