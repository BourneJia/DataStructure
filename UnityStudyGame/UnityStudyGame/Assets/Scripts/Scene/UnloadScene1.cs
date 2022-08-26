using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UnloadScene1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Find("UnLoadButton").GetComponent<Button>().onClick.AddListener(() => {
            Debug.Log("卸载场景");
            SceneManager.UnloadSceneAsync("New Scene1");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
