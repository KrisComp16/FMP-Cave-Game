
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
    public GameObject coal;
    public Transform firepoint;
    //public Text MyText;
    //public ParticleSystem ps;
    private int score;
    public int playerscore;
    public int highscore;
    bool jumping;


    // 0 = idle
    // 1 = walk
    // 2 = throw
    // 3 = death 1
    // 4 = death 2

    enum State
    {
        Idle,
        Walk,
        Jumping,
        Death,

    }


    State playerState;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        jumping = false;

        playerState = State.Idle;

        //gameObject.GetComponent<ParticleSystem>().emission.enabled = false;
        //MyText.text = "";

        //ps.Pause();

        print("player state = " + playerState);

    }

    // Update is called once per frame
    void Update()
    { 
        DoJump();
        DoMove();
        DoAttack();
        //DoShoot();
        DoLand();
        //MyText.text = "" + playerscore;
        //DoSpecialEffectsAnim();
        //ParticleSystem.Stop();
        //DoSpecialEffects();
        ShootingAnimation();
        Highscore();
    }


    void FixedUpdate()
    {
        DoRayCollisionCheck();
    }



    void DoLand()
    {
        if (playerState == State.Death)
        {
            return;
        }

        print("jumping=" + jumping);

        if( (jumping == true) && (isGrounded == true) && (rb.velocity.y<0))
        {
            jumping = false;
            anim.SetBool("Jump", false);
            print("landed");
            
        }

    }


    void DoJump()
    {
        if (playerState == State.Death)
        {
            return;
        }


        Vector2 velocity = rb.velocity;

        // check for jump
        if (  ((Input.GetKey("w")==true) || (Input.GetKey(KeyCode.Space)==true) || (Input.GetKey(KeyCode.UpArrow)==true)) && (isGrounded == true) )
        { 
            velocity.y = 8f;    // give the player a velocity of 5 in the y axis
            anim.SetBool("Jump", true);
            jumping = true; 
        }
        

        rb.velocity = velocity;

    }

    void DoMove()
    {
        if( playerState == State.Death )
        {
            return;
        }

        Vector2 velocity = rb.velocity;

        // stop player sliding when not pressing left or right
        velocity.x = 0;

        // check for moving left
        if (Input.GetKey("a"))
        {
            velocity.x = -5;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            velocity.x = -5;
        }

        // check for moving right
        if (Input.GetKey("d"))
        { 
            velocity.x = 5;
        }
        if (Input.GetKey(KeyCode.RightArrow))
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

    void ShootingAnimation()
    {
        Vector2 velocity = rb.velocity;
        if (Input.GetButton("Fire2"))
        {
            velocity.x = 0;
            anim.SetBool("Shooting", true);

            if (velocity.x != 0)
            {
                anim.SetBool("Walking", false);
            }
        }

        
        rb.velocity = velocity;
    }
    void StopShootingAnimaton()
    {
        anim.SetBool("Shooting", false);
    }

    void DoShoot()
    {
        if (Helper.GetDirection(gameObject) == true)
        {
            MakeBullet(coal, firepoint.position.x, firepoint.position.y, -6, 0);
        }
        else
        {
            MakeBullet(coal, firepoint.position.x, firepoint.position.y, 6, 0);
        }
    }
    void MakeBullet(GameObject prefab, float xpos, float ypos, float xvel, float yvel)
    {
        GameObject instance = Instantiate(prefab, new Vector3(xpos, ypos, 0), Quaternion.identity);

        Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(xvel, yvel, 0);



    }
    /*
    void DoShoot()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Instantiate(coal, transform.position, transform.rotation);
        }

    }
    
    */


    void DoRayCollisionCheck()
    {
        float rayLength = 1.2f;

        //cast a ray downward of length 1
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, rayLength);

        Color hitColor = Color.white;

        isGrounded = false;

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
        // draw a debug ray to show ray position
        // You need to enable gizmos in the editor to see these
        Debug.DrawRay(transform.position, -Vector2.up * rayLength, hitColor);

    }


    void OnTriggerEnter2D(Collider2D other)
    {

        Vector2 velocity = rb.velocity;


        if (playerState != State.Death)
        {
            if (other.gameObject.tag == "Gem")
            {
                playerscore = playerscore + 100;
                print(playerscore);
            }


        }

        if (other.gameObject.tag == "Lava")
        {
            print("I've fallen into lava!");
            velocity.y = 8f;
            velocity.x = 0f;
            anim.SetBool("LavaDeath", true);
            anim.SetBool("Jump", false);
            playerState = State.Death;
            
        }

        rb.velocity = velocity;
    }

 
/*
    void DoSpecialEffects()
    {
        ps.Play();
    }

    void StopSpecialEffects()
    {
        ps.Stop();
    }
*/
    void DoDeath()
    {
        Destroy(this.gameObject);
        Destroy(gameObject);
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        playerscore = 0;
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

    void OnGUI()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);

        GUILayout.Label($"<color='white'><size=20>Score = {playerscore}\nHighscore = {highscore}</size></color>\n");
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

