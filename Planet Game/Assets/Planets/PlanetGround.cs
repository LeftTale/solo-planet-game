using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class PlanetGround : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rbPlayer;
    private CharacterController2D controller;

    private void Awake()
    {
        player = GameObject.Find("MainPlayer").gameObject;
        rbPlayer = player.GetComponent<Rigidbody2D>();
        controller = player.GetComponent<CharacterController2D>();
       
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            controller.PlanGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            controller.PlanGrounded = false;
        }
    }
}
