using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public static SceneChanger instance = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ButtonPress();
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    void ButtonPress()
    {
        if (Input.GetKey("p"))
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