using System;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Space(10)]
    [Header("Components")]
    public CharacterController2D controller;
    public Animator animator;
    private Camera playerCam;
    public TextDirector textDirector;
    private AudioSource audioSource;
    public AudioClip typingAudioClip;
    public AudioClip ambientMusic;
    public PlayableDirector cutScene;
    private Rigidbody2D rbPlayer;
    private GameManager gameManager;


    [Space(10)]
    [Header("Player")]
    [SerializeField] float runSpeed = 40f;
    private float horizontalMove;
    private bool jump;
    private string sceneName;
    private bool dead;
    float deathTimer = 6f;
    private bool locked;
    private float xDeath, yDeath;
    private bool deathBlock;
    private Vector3 startingPos;
    

    [Space(10)] 
    [Header("Planet Specifics")]
    private bool overHeating;





    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        startingPos = transform.position;

        audioSource = GetComponentInChildren<AudioSource>();

        playerCam = GameObject.Find("PlayerCam").GetComponent<Camera>();

        GameManager.isInputEnabled = true;

        rbPlayer = GetComponent<Rigidbody2D>();
        
        if (sceneName != "IntroLevel")
        {
            GameManager.isBoostEnabled = true;
            textDirector = GameObject.Find("AveryUI").transform.Find("TextDirector").gameObject
                .GetComponent<TextDirector>();
        }
        else
        {
            GameManager.isBoostEnabled = false;
            textDirector = GameObject.Find("Canvas").transform.Find("UIDirector").gameObject
                .GetComponent<TextDirector>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown((KeyCode.C)))
        {
            cutScene.time = 24f;
        }

        //Script that controls the death logic
        if (dead && locked)
        { 
            transform.position = new Vector3(xDeath,yDeath,0);
            deathTimer -= Time.deltaTime;
            if (deathTimer < 0)
            {
                Death();
            }
        }
        else if (dead)
        { 
            if(!deathBlock)
                DeathAnimBlock();
                
            GameManager.isInputEnabled = false;

            deathTimer -= Time.deltaTime;

            if (deathTimer < 0)
            {
                Death();
            }
        }

        if (GameManager.isInputEnabled)
        {
            //Sets the run speed
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            //if the movement is positive or negative animate
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            //When an input corresponding to jump is pressed it plays the animation and makes the character jump
            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetBool("Jump", true);
            }

            //When on the planet, speed up the character
            runSpeed = controller.Attracted ? 120f : 40f;

            //Alternative Death Logic
            if (dead && locked)
            { 
                transform.position = new Vector3(xDeath,yDeath,0);
                deathTimer -= Time.deltaTime;
                if (deathTimer < 0)
                {
                    Death();
                }
            }
            else if (dead)
            { 
                if(!deathBlock)
                    DeathAnimBlock();
                
                GameManager.isInputEnabled = false;

                deathTimer -= Time.deltaTime;

                if (deathTimer < 0)
                {
                    Death();
                }
            }
        }
    }

    void Death()
    {
        transform.position = startingPos;
        dead = false;
        GameManager.isInputEnabled = true;
        rbPlayer.velocity = Vector2.zero;
        textDirector.SendDeathText(5);
        deathTimer = 6f;
    }

    //Triggers death animation
    void DeathAnimBlock()
    { 
        animator.SetTrigger("Death");
        deathBlock = true;
        animator.SetBool("isDead", true);
    }

    void FixedUpdate()
    {
        //Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }

    //Disables the jump animation
    public void OnLanding()
    {
        animator.SetBool("Jump", false);
    }

    public float HorizontalMove
    {
        get => horizontalMove;
        set => horizontalMove = value;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Deadly"))
        {
            Dead = true;
            textDirector.SendDeathText(4);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Dictates what happens when the player dies or loses
        if (other.CompareTag("Finish"))
        {
            audioSource.Pause();
            audioSource.clip = typingAudioClip;
            audioSource.Play();
            playerCam.transform.parent = null;
            
            if (sceneName == "IntroLevel")
            {
                textDirector.SendDeathText(0);
                dead = true;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Kills the player if they leave the level
        if (other.gameObject.CompareTag("LevelBoundary"))
        {
            xDeath = transform.position.x;
            yDeath = transform.position.y;
            textDirector.SendDeathText(1);
            dead = true;
            locked = true;
        }
    }

    public void EnableCam()
    {
        playerCam.enabled = true;
    }

    public bool Dead
    {
        get => dead;
        set => dead = value;
    }
}
