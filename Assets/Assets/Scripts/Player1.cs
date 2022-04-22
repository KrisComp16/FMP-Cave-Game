
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Globals;

public class Player1 : MonoBehaviour
{
    private Rigidbody2D rb;
    bool isGrounded;
    private Animator anim;
    //public GameObject fireball;
    //public Transform firepoint;
    //public Text MyText;
    public ParticleSystem ps;
    private int score;
    public int playerscore;
    public int highscore;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //gameObject.GetComponent<ParticleSystem>().emission.enabled = false;
        //MyText.text = "";
        ps = GetComponent<ParticleSystem>();
        ps.Pause();

    }

    // Update is called once per frame
    void Update()
    {


        DoJump();
        DoMove();
        DoAttack();
        //DoShoot();
        DoRayCollisionCheck();
        //MyText.text = "" + playerscore;
        DoSpecialEffectsAnim();
        //ParticleSystem.Stop();
        //DoSpecialEffects();
    }






    void DoJump()
    {
        Vector2 velocity = rb.velocity;

        // check for jump
        if (Input.GetKey("w") && isGrounded == true)
        {
            if (velocity.y < 0.01f)
            {
                velocity.y = 8f;    // give the player a velocity of 5 in the y axis
                anim.SetBool("Jump", true);
            }
        }
        if (isGrounded == false)
        {
            anim.SetBool("Jump", false);
        }

        rb.velocity = velocity;

    }

    void DoMove()
    {
        Vector2 velocity = rb.velocity;

        // stop player sliding when not pressing left or right
        velocity.x = 0;

        // check for moving left
        if (Input.GetKey("a"))
        {
            velocity.x = -5;
        }

        // check for moving right
        if (Input.GetKey("d"))
        {
            velocity.x = 5;
        }

        anim.SetBool("Walking", false);


        if (velocity.x != 0)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }

        if (velocity.x < -0.5f)
        {
            Helper.DoFaceLeft(gameObject, true);


        }
        if (velocity.x > 0.5f)
        {
            Helper.DoFaceLeft(gameObject, false);
        }


        rb.velocity = velocity;

    }


    /*
    private void OnCollisionStay2D(Collision2D collosion)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
    */




    void DoAttack()
    {

        if (Input.GetKey("v"))
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
        }


    }
    /*
    void DoShoot()
    {
        if (Input.GetButtonDown("Fire2"))
        {





            if (Helper.GetDirection(gameObject) == true)
            {
                MakeBullet(fireball, firepoint.position.x, firepoint.position.y, -6, 0);
            }
            else
            {
                MakeBullet(fireball, firepoint.position.x, firepoint.position.y, 6, 0);
            }

        }

    }
    void MakeBullet(GameObject prefab, float xpos, float ypos, float xvel, float yvel)
    {
        GameObject instance = Instantiate(prefab, new Vector3(xpos, ypos, 0), Quaternion.identity);

        Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(xvel, yvel, 0);



    }
    
    void DoShoot()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Instantiate(fireball, transform.position, transform.rotation);
        }

    }
    */



    void DoRayCollisionCheck()
    {
        float rayLength = 1.2f;

        //cast a ray downward of length 1
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, rayLength);

        Color hitColor = Color.white;


        if (hit.collider != null)
        {

            if (hit.collider.tag == "Enemy")
            {
                isGrounded = true;
                hitColor = Color.green;
            }

            if (hit.collider.tag == "Ground")
            {
                isGrounded = true;
                hitColor = Color.green;
            }

        }
        else
        {
            isGrounded = false;

        }
        // draw a debug ray to show ray position
        // You need to enable gizmos in the editor to see these
        Debug.DrawRay(transform.position, -Vector2.up * rayLength, hitColor);

    }


    void OnTriggerEnter2D(Collider2D other)
    {

        Vector2 velocity = rb.velocity;

        if (other.gameObject.tag == "Gem")
        {
            playerscore = playerscore + 100;
            print(playerscore);
        }
        if (other.gameObject.tag == "Lava")
        {
            print("I've fallen into lava!");
            velocity.y = 8f;
            anim.SetBool("LavaDeath", true);
            
        }

        rb.velocity = velocity;
    }

    void DoSpecialEffectsAnim()
    {

        anim.SetBool("SpecEff", false);

        if (Input.GetKey("f"))
        {
            anim.SetBool("SpecEff", true);
        }

    }


    void DoSpecialEffects()
    {
        ps.Play();
    }

    void StopSpecialEffects()
    {
        ps.Stop();
    }

    void DoDeath()
    {
        Destroy(this.gameObject);
        Destroy(gameObject);
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        playerscore = 0;
    }

    void OnGUI()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);

        GUILayout.Label($"<color='white'><size=20>Score = {playerscore}\nHighscore = {highscore}</size></color>\n");
    }

    void Highscore()
    {


        if (playerscore > highscore)
        {
            highscore = playerscore;

            SetHighscore("highscore", highscore);
        }
        else
        {
            return;
        }

    }

    void Score()
    {
        if (Input.GetKey("="))
        {
            playerscore += 1;
        }
        if (Input.GetKey("-"))
        {
            playerscore -= 1;
        }
    }

    void SetHighscore(string name, int Value)
    {
        PlayerPrefs.SetInt(name, Value);
    }
}

