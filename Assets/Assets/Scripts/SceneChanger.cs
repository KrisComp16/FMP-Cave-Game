using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public AudioManager am;
    public bool isPlaying1 = true;
    public bool isPlaying2 = false;
    public bool isPlaying3 = false;

    //public static SceneChanger instance = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ButtonPress();
        MusicChecker();
    }

    /*
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
    */


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


    public void MusicChecker()
    {
        Scene scene = SceneManager.GetActiveScene();



        print("scene = " + scene.name);

        if (scene.name == "Level_1" && isPlaying1 == true)
        {
            am.PlayMusic("Level1");
            isPlaying1 = false;
            isPlaying2 = true;
        }
        if (scene.name == "Level_2" && isPlaying2 == true)
        {
            print("Level 2!");
            am.PauseMusic("Level1");
            am.PlayMusic("Level2");
            isPlaying2 = false;
            isPlaying3 = true;
        }
        if (scene.name == "Level_3" && isPlaying3 == true)
        {
            am.PauseMusic("Level1");
            am.PauseMusic("Level2");
            am.PlayMusic("Level3");
            isPlaying3 = false;
        }
    }

}