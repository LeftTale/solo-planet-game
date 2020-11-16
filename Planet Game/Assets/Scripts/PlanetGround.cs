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
        if (!moveScript.Jump)
            if (other.gameObject.CompareTag("Player") && moveScript.HorizontalMove == 0)
                rbPlayer.velocity = Vector2.zero;
    }
}
