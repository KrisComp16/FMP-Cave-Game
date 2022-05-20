using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    private int score;
    public int playerscore;
    public int highscore;
    public Font Minecraft;
    public static PointsManager instance = null;

    // Start is called before the first frame update
    void Start()
    {
        playerscore = PlayerPrefs.GetInt("playerscore");
    }

    // Update is called once per frame
    void Update()
    {
        Highscore();

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
        //DontDestroyOnLoad(gameObject);
    }

    void Highscore()
    {


        if (playerscore > highscore)
        {
            highscore = playerscore;

            SetScore("highscore", highscore);
        }
        else
        {
            return;
        }

    }

    void OnGUI()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);

        GUI.skin.font = Minecraft;

        GUILayout.Label($"<color='white'><size=20>Score = {playerscore}\nHighscore = {highscore}</size></color>\n");
    }


    void SetScore(string name, int Value)
    {
        PlayerPrefs.SetInt(name, Value);
    }



}
