using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneButton2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Find("Button").GetComponent<Button>().onClick.AddListener(() => {
            Debug.Log("This is Scene2,Begin switch Scene1");
            SceneManager.LoadScene(0);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
