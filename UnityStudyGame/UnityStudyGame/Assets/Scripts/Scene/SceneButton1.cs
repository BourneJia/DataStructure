using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneButton1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        transform.Find("SwitchButton").GetComponent<Button>().onClick.AddListener(() => {
            Debug.Log("This is Scene1,Begin switch Scene2");
            SceneManager.LoadScene(1,LoadSceneMode.Additive);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
