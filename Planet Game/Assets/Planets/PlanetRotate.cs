using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetRotate : MonoBehaviour
{
    private Vector3 zAxis = new Vector3(0,0,1);
    private float menuTimer = 20f;
    private TextMeshProUGUI textWall;

    private void Start()
    {
        textWall = GameObject.Find("TextWall").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        gameObject.transform.Rotate(zAxis, 10 * Time.deltaTime);

        menuTimer -= Time.deltaTime;
        if (menuTimer < 11)
            textWall.text =
                "Game Created by Caolan Barron \nRocket Sprite Created by Liam Gill \nAll other Assets were found on \nItch.io\nopenGameArt.com\nZapsplat.com";

        if (menuTimer < 0)
            SceneManager.LoadScene(0);
    }
}
