using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Globals;

public class Enemy : MonoBehaviour
{

    private Rigidbody2D rb;
    public Transform player;
    public GameObject spear;
    public Transform firepoint;
    public GameObject collectible;
    private Animator anim;
    public GameObject bones;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //player.gameObject = GameObject.Find("Coal Miner");
        anim = GetComponent<Animator>();

        anim.SetBool("Death", false);
        anim.SetBool("Throwing", false);
    }

    // Update is called once per frame
    void Update()
    {
        DoMove();
        Helper.EnemyDirection(player.gameObject, gameObject);
        // DoJump();


    }

    

    void DoMove()
    {
        Vector2 velocity = rb.velocity;

        float ex = transform.position.x;

        float px = player.position.x;

        float distance = ex - px;


        if (ex > px)
        {
            Helper.DoFaceLeft(gameObject, true);
            velocity.x = -2;
            
        }
        
        else if (ex == px)
        {
            Helper.DoFaceLeft(gameObject, false);
            velocity.x = 0;

        }
        
        
        else
        {
            Helper.DoFaceLeft(gameObject, false);
            velocity.x = -2;
            
        }

        if (velocity.x != 0)
        {
            anim.SetBool("Walking", true);
        }

        anim.SetBool("Throwing", false);

        if (anim.GetBool("Death") == false)
        {
            if (distance < 5)
            {
                velocity.x = 0;
                anim.SetBool("Walking", false);
                anim.SetBool("Throwing", true);
            }
            else if (distance < -5)
            {
                velocity.x = 0;
                anim.SetBool("Walking", false);
                anim.SetBool("Throwing", true);
            }
        }
        else
        {
            return;
        }


        

        rb.velocity = velocity;
    }
    





    void DoJump()
    {
        Vector2 velocity = rb.velocity;

        float ey = transform.position.y;

        float py = player.transform.position.y;

        if (ey < py)
        {

            if (velocity.y < 0.01f)
            {
                velocity.y = 8f;    // give the player a velocity of 5 in the y axis

            }
        }

        rb.velocity = velocity;
    }




    void DoThrow()
    {
        print("Throw");


        if (Helper.GetDirection(gameObject) == true)
        {
            MakeBullet(spear, firepoint.position.x, firepoint.position.y, -6, 0);
        }
        else
        {
            MakeBullet(spear, firepoint.position.x, firepoint.position.y, 6, 0);
        }
    }

    public void MakeBullet(GameObject prefab, float xpos, float ypos, float xvel, float yvel)
    {
        GameObject instance = Instantiate(prefab, new Vector3(xpos, ypos, 0), Quaternion.identity);

        Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(xvel, yvel, 0);
        if (xvel < 0)
        {
            Helper.DoFaceLeft(gameObject, true);
        }
        else
        {
            Helper.DoFaceLeft(gameObject, false);
        }


    }

    void DropCollectible(GameObject prefab, float xpos, float ypos)
    {
        GameObject instance = Instantiate(prefab, new Vector3(xpos, ypos, 0), Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        print("tag=" + gameObject.tag);

        if (other.gameObject.tag == "Coal")
        {
            print("I've been hit by coal!");
            anim.SetBool("Death", true);
            anim.SetBool("Walking", false);
            anim.SetBool("Throwing", false);


        }
    }

    void OnTriggerStay2D(Collider2D other)
    { 
        if( other.gameObject.tag == "Player")
        {
            Player1 script = other.gameObject.GetComponent<Player1>();
            if( script.pickaxeActive )
            {
                print("I've been hit by a pickaxe!");
                anim.SetBool("Death", true);
                anim.SetBool("Walking", false);
                anim.SetBool("Throwing", false);


            }
        }

    }

    void DoDeath()
    {
        Destroy(this.gameObject);
        DropCollectible(collectible, firepoint.position.x, firepoint.position.y);
        DropBones(bones, firepoint.position.x + 0.5f, firepoint.position.y + 1);
    }

    void DropBones(GameObject prefab, float xpos, float ypos)
    {
        GameObject instance = Instantiate(prefab, new Vector3(xpos, ypos, 0), Quaternion.identity);
    }


}