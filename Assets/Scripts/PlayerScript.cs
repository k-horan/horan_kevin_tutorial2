using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    private AudioSource sound;
    private SpriteRenderer sprite;

    public AudioClip otherClip;

    public float speed;
    public float jumpforce;

    public Text score;
    public Text lives;
    public Text win;

    private int level;
    private int scoreValue = 0;
    private int livesValue = 3;
    private int flipCount = 0;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();

        sound = GetComponent<AudioSource>();
        sound.loop = true;

        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        level = 1;
        win.text = "";
        score.text = scoreValue.ToString();
        lives.text = livesValue.ToString();
    }

    void Update ()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
        


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            livesValue--;
            lives.text = livesValue.ToString();
            setGameStatus();
            Destroy(collision.collider.gameObject); 

        }

        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            setGameStatus();
            Destroy(collision.collider.gameObject);
        }

    }

    private void setGameStatus()
    {
        // new level, move player and reset lives
        if (scoreValue == 4 && level == 1)
        {
            transform.position = new Vector3(-14.3f, 32.4f, 0.0f);
            livesValue = 3;
            scoreValue = 0;
            lives.text = livesValue.ToString();
            score.text = scoreValue.ToString();
            level = 2;   
        }

        // win game, display text and disable player
        if (scoreValue == 4 && level == 2)
        {
            win.text = "You Win! Game Created By Kevin Horan";
            speed = 0;
            jumpforce = 0;
            sound.clip = otherClip;
            sound.loop = false;
            sound.Play();
            Destroy(sprite);
        }
        
        // lose game, display text and disable player
        if (livesValue == 0)
        {
            win.text = "You Lose! Game Created By Kevin Horan";
            speed = 0;
            jumpforce = 0;
            Destroy(sprite);
        }        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
                anim.SetInteger("State", 2);
            }

            // running left
            if (Input.GetKey(KeyCode.A))
            {
                if (flipCount % 2 == 0)
                {
                    FlipSprite();
                }

                anim.SetInteger("State", 1);
            }

            // running right
            if (Input.GetKey(KeyCode.D))
            {
                if (flipCount % 2 == 1)
                {
                    FlipSprite();
                }

                anim.SetInteger("State", 1);
            }

            // idle
            if(!Input.anyKey)
            {
                anim.SetInteger("State", 0);
            }        
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            anim.SetInteger("State", 2);
        }
    }
    private void FlipSprite()
    {
        if (sprite.flipX == true)
        {
            sprite.flipX = false;
        }

        else
        {
            sprite.flipX = true;
        }

        flipCount++;
    }
}