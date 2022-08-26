using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneTemplate;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateScene1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Find("CreateButton").GetComponent<Button>().onClick.AddListener(() => {
            //Debug.Log("创建场景");
            var name = SceneManager.GetActiveScene().name;
            Debug.Log(name);
            SceneManager.CreateScene("New Scene1");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
