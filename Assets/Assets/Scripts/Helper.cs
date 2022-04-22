using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{


    public static void DoFaceLeft(GameObject obj, bool left)
    {
        if (left == true)
        {
            obj.transform.localRotation = Quaternion.Euler(0, 180, 0);

        }
        else
        {
            obj.transform.localRotation = Quaternion.Euler(0, 0, 0);

        }
    }

    public static bool GetDirection(GameObject obj)
    {


        if (obj.transform.eulerAngles.y == 180)
        {
            return true;
        }
        else
        {
            return false;
        }

    }


    public static void EnemyDirection(GameObject player, GameObject enemy)
    {
        Rigidbody2D rb;

        float ex = enemy.transform.position.x;
        float px = player.transform.position.x;

        rb = enemy.GetComponent<Rigidbody2D>();




        float playerdistance = ex - px;

        if (playerdistance > 3)
        {

            rb.velocity = new Vector3(-2, 0, 0);
            DoFaceLeft(enemy, true);
        }
        else if (playerdistance < -3)
        {

            rb.velocity = new Vector3(2, 0, 0);
            DoFaceLeft(enemy, false);
        }
        else
        {

            rb.velocity = new Vector3(0, 0, 0);

        }
    }


    public static void SpearDirection(GameObject player, GameObject spear)
    {
        Rigidbody2D rb;

        float sx = spear.transform.position.x;
        float px = player.transform.position.x;

        rb = spear.GetComponent<Rigidbody2D>();




        float playerdistance = sx - px;

        if (playerdistance > 1)
        {

            rb.velocity = new Vector3(-2, 0, 0);
            DoFaceLeft(spear, true);
        }
        else if (playerdistance < -1)
        {

            rb.velocity = new Vector3(2, 0, 0);
            DoFaceLeft(spear, false);
        }
        else
        {

            rb.velocity = new Vector3(0, 0, 0);

        }
    }



    /*
    public static class Globals
    {
        public const int Left = -1;
        public const int Right = 1;
    }

    */
}

