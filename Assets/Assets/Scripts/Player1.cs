
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
    public Font Minecraft;
    public static int Health;
    public Animator Hearts;
    Weapon playerWeapon;
    public bool pickaxeActive;
    public Animator Inventory;
    public AudioManager am;
    public Animator Death;
    public bool isPlaying1 = true;
    public bool isPlaying2 = false;
    public PointsManager pm;




    // player weapons
    enum Weapon
    {
        Coal,
        Pickaxe,
    }

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
    public static Player1 instance;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        jumping = false;

        playerState = State.Idle;
        playerWeapon = Weapon.Coal;

        //gameObject.GetComponent<ParticleSystem>().emission.enabled = false;
        //MyText.text = "";

        //ps.Pause();

        print("player state = " + playerState);

        Health = 5;

        pickaxeActive = false;

        //MusicChecker();


        //makes it a singleton
        /*
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
        */

        playerscore = PlayerPrefs.GetInt("playerscore");
    }

    // Update is called once per frame
    void Update()
    { 
        DoJump();
        DoMove();
        //DoShoot();
        DoLand();
        //MyText.text = "" + playerscore;
        //DoSpecialEffectsAnim();
        //ParticleSystem.Stop();
        //DoSpecialEffects();
        ShootingAnimation();
        //Highscore();
        AttackChanger();

        //print(Health);

        //MusicChecker();

        SetScore();
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

        //print("jumping=" + jumping);

        if( (jumping == true) && (isGrounded == true) && (rb.velocity.y<0))
        {
            jumping = false;
            anim.SetBool("Jump", false);
            //print("landed");
            
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
            am.Play("Jump");
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






    void ShootingAnimation()
    {
        Vector2 velocity = rb.velocity;
        if (Input.GetButton("Fire2"))
        {
            velocity.x = 0;

            if( playerWeapon == Weapon.Coal )
            {
                anim.SetBool("Shooting", true);
                anim.SetBool("Attack", false);
            }
            else if( playerWeapon == Weapon.Pickaxe )
            {
                anim.SetBool("Attack", true);
                anim.SetBool("Shooting", false);
            }
            

            if (velocity.x != 0)
            {
                anim.SetBool("Walking", false);
            }
        }
        if (Input.GetButton("Fire1"))
        {
            velocity.x = 0;

            if (playerWeapon == Weapon.Coal)
            {
                anim.SetBool("Shooting", true);
                anim.SetBool("Attack", false);
            }
            else if (playerWeapon == Weapon.Pickaxe)
            {
                anim.SetBool("Attack", true);
                anim.SetBool("Shooting", false);
            }


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
        anim.SetBool("Attack", false);
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
                pm.playerscore = pm.playerscore + 100;
                //print(playerscore);
                am.Play("Collectible");
            }
            if (other.gameObject.tag == "Heart")
            {
                Health = Health + 1;
                pm.playerscore = pm.playerscore + 100;
                Hearts.SetInteger("Health", Health);
                am.Play("Heart");
            }


        }

        if (other.gameObject.tag == "Lava")
        {
            //print("I've fallen into lava!");
            velocity.y = 10f;
            velocity.x = 0f;
            am.Play("Death");
            am.Play("Death2");
            am.Play("Fire");
            am.Pause("SkeletonWalk1");
            am.Pause("SkeletonWalk2");
            am.Pause("SkeletonWalk3");
            am.Pause("SkeletonWalk4");
            Death.SetTrigger("DEath");
            am.PauseMusic("Level1");
            am.Play("Level1");
            anim.SetBool("LavaDeath", true);
            anim.SetBool("Jump", false);
            playerState = State.Death;

            
        }

        if (other.gameObject.tag == "Bone")
        {
            if (Health > 0)
            {
                Health -= 1;
            }
            else if (Health > 5)
            {
                Health = 5;
            }
            else
            {
                playerState = State.Death;
                am.Play("Death");
                am.Play("Death2");
                am.Play("Fire");
                am.Pause("SkeletonWalk1");
                am.Pause("SkeletonWalk2");
                am.Pause("SkeletonWalk3");
                am.Pause("SkeletonWalk4");
                Death.SetTrigger("DEath");
                am.PauseMusic("Level1");
                am.Play("Level1");
                anim.SetBool("Death", true);
                //DoDeath();
            }

            Hearts.SetInteger("Health", Health);
        }

        rb.velocity = velocity;
    }



    void DoDeath()
    {
        SceneManager.LoadScene("Death");
        //(this.gameObject);
        //Destroy(gameObject);
        pm.playerscore = 0;
    }
    
    /*
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

        GUI.skin.font = Minecraft;

        GUILayout.Label($"<color='white'><size=20>Score = {playerscore}\nHighscore = {highscore}</size></color>\n");
    }


    void SetHighscore(string name, int Value)
    {
        PlayerPrefs.SetInt(name, Value);
    }
    */

    public void PickaxeInactive()
    {
        pickaxeActive= false;
    }

    public void PickaxeActive()
    {
        pickaxeActive = true;
    }

    void AttackChanger()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && (playerWeapon == Weapon.Coal))
        {
            playerWeapon = Weapon.Pickaxe;
            Inventory.SetBool("Pickaxe", true);
            Inventory.SetBool("Coal", false);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && (playerWeapon == Weapon.Pickaxe))
        {
            playerWeapon = Weapon.Coal;
            Inventory.SetBool("Pickaxe", false);
            Inventory.SetBool("Coal", true);
        }
    }

    /*
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
        }
    }
    */

    public void WalkSound()
    {
        int num = 0;

        num = Random.Range(1, 5);

        if (num == 1)
        {
            am.Play("Footstep1");
        }
        if (num == 2)
        {
            am.Play("Footstep2");
        }
        if (num == 3)
        {
            am.Play("Footstep3");
        }
        if (num == 4)
        {
            am.Play("Footstep4");
        }
    }

    void SetScore()
    {
        //
        PlayerPrefs.SetInt("playerscore",  pm.playerscore);

    }

}

