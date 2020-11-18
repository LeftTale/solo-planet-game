using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlanetGround : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Rigidbody2D rbPlayer;
    private PlayerMovement moveScript;

    private void Awake()
    {
       rbPlayer = player.GetComponent<Rigidbody2D>();
       moveScript = player.GetComponent<PlayerMovement>();
    }
    
    

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetButton("Jump"))
        {
            rbPlayer.AddRelativeForce(new Vector2(0, 10), ForceMode2D.Impulse);
            
        }
        else if (other.gameObject.CompareTag("Player") && moveScript.HorizontalMove == 0)
            rbPlayer.velocity = Vector2.zero;

        
    }
}
