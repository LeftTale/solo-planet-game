using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wildAlienMovement : MonoBehaviour
{
    private Rigidbody2D enemyRB;
    private GameObject enemyGuide;
    private Animator enemyAnimator;
    private GameObject playerGameObject;
    private TextDirector textDirector;

    private bool collideHit;

    void Start()
    {
        //Acquire the enemys rigidBody
        enemyRB = GetComponent<Rigidbody2D>();
        //Acquire the guide object for the enemy
        enemyGuide = transform.Find("Enemy Guide").gameObject;
        //Acquire the enemy animator
        enemyAnimator = transform.Find("WildAlienGFX").gameObject.GetComponent<Animator>();
        //Acquire the player object
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        //Acquire the Text Director
        textDirector = GameObject.Find("Canvas").transform.Find("TextDirector").GetComponent<TextDirector>();


        enemyAnimator.SetBool("alienRun",true);
    }

    private void FixedUpdate()
    {
        Vector2 guideDirection = (enemyGuide.transform.position - transform.position).normalized;

        if(!collideHit)
            enemyRB.velocity = guideDirection * 6;
        else
            enemyRB.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemyAnimator.SetTrigger("alienBite");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && collideHit == false)
        {
            collideHit = true;
            textDirector.SendDeathText(2);
            enemyAnimator.SetBool("alienIdle",true);
            enemyAnimator.SetBool("alienRun", false);
            
        }
    }
}
