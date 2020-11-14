using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Space(10)]
    [Header("Components")]
    public CharacterController2D controller;
    public Animator animator;

    [Space(10)]
    [Header("Player")]
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    // Update is called once per frame
    void Update()
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
}
