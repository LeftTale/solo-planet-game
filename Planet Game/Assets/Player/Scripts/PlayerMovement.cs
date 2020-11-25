using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.UI;

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

    [Space(10)] 
    [Header("Planet Specifics")]
    private bool overHeating;





    private void Start()
    {
        //Save the name of the level as a string
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        audioSource = GetComponentInChildren<AudioSource>();
        playerCam = GameObject.Find("PlayerCam").GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetKeyDown((KeyCode.C)))
        {
            cutScene.time = 24f;
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


            if (dead && locked)
            { 
                transform.position = new Vector3(xDeath,yDeath,0);
                deathTimer -= Time.deltaTime;
                if (deathTimer < 0)
                {
                    SceneManager.LoadScene(sceneName);
                }
            }
            else if (dead)
            {
                deathTimer -= Time.deltaTime;
                if (deathTimer < 0)
                {
                    SceneManager.LoadScene(sceneName);
                }
            }
        }
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

    private void OnCollisionStay2D(Collision2D other)
    {
        ///Fire Planet Code
        if (other.gameObject.CompareTag("FirePlanet"))
        {

        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Grav"))
        {
            Vector3 planPosition = new Vector3(other.transform.position.x,other.transform.position.y,-1f);
            //playerCam.transform.position = Vector3.Lerp(camPosition,planPosition, 1);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Grav"))
        {
            // camReset = true;
           
        }
        else if (other.gameObject.CompareTag("LevelBoundary"))
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

}
