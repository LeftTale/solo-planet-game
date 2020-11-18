using System;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Animator animator;
    private ParticleSystem particle;
    private Animator playerAnimator;
    private GameObject player;
    private Camera playerCam;
    private Camera rocketCam;
    private SpriteRenderer playerSprite;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        particle = GetComponentInChildren<ParticleSystem>();
        player = GameObject.Find("MainPlayer");
        playerAnimator = player.GetComponent<Animator>();
        rocketCam = GetComponentInChildren<Camera>();
        playerCam = player.GetComponentInChildren<Camera>();
        playerSprite = player.GetComponent<SpriteRenderer>();

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetButton("Crouch"))
        {
            playerAnimator.SetBool("Climb",true);
            particle.Play();
            player.transform.Translate(Vector2.up * Time.deltaTime*10);
            playerCam.enabled = false;
            rocketCam.enabled = true;
            animator.SetBool("TakeOff",true);
            playerSprite.enabled = false;

        }
    }
}
