using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Space(10)]
    [Header("Components")]
    public CharacterController2D controller;
    public Animator animator;
    private Camera playerCam;
    private Vector3 camPosition;
    private Vector3 playerPosition;
    

    [Space(10)]
    [Header("Player")]
    [SerializeField] float runSpeed = 40f;
    private float horizontalMove;
    private bool jump;
    private bool camReset;


    private void Awake()
    {
        playerCam = GetComponentInChildren<Camera>();

    }

    void Update()
    {
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

            if (camReset)
                playerCam.transform.position = Vector3.Lerp(camPosition, playerPosition, 1f);

            if (playerCam.transform.localPosition == new Vector3(0, 0, -10))
                camReset = false;
        }
    }

    void FixedUpdate()
    {
        //Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;

        camPosition = playerCam.transform.position;
        playerPosition = new Vector3(transform.position.x,transform.position.y,-1f);
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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Grav"))
        {
            Vector3 planPosition = new Vector3(other.transform.position.x,other.transform.position.y,-1f);
            playerCam.transform.position = Vector3.Lerp(camPosition,planPosition, 1);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Grav"))
        {
            camReset = true;
           
        }
    }
}
