using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Adds score to a static variable if the player picks up a crystal
        if (other.gameObject.CompareTag("Score"))
        {
            other.gameObject.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject);
            int tempscore = GameManager.Score;
            GameManager.Score = tempscore + 1;

        }
    }
}
