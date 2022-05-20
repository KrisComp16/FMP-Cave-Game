using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour
{
    void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("playerscore", 0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Tutorial()
    {
        

        SceneManager.LoadScene("Level_1");
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level_2");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Level_3");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
