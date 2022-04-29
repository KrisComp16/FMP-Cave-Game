using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ChangeScene();
        }
    }

    void ChangeScene()
    {

        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Level_1")
        {
            SceneManager.LoadScene("Level_2");
        }
        if (scene.name == "Level_2")
        {
            SceneManager.LoadScene("Level_1");
        }
    }
}



