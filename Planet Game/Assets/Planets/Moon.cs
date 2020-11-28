using System;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public GameObject neighbourPlanet;

    public bool halfRotate;
    public bool fullOrbit;
    public bool puzzle1;
    private readonly Vector3 zAxis = new Vector3(0,0,1);
    private float cometSpeed = 10f;
    private float moonTimer = 4f;


    private void Update()
    {
        if (halfRotate)
        {
            moonTimer -= Time.deltaTime;
            if (moonTimer < 0)
            {
                moonTimer = 4f;
                cometSpeed = -cometSpeed;
            }

            transform.RotateAround(neighbourPlanet.transform.position, zAxis, cometSpeed * Time.deltaTime);
        }

        if (puzzle1)
        {
            moonTimer -= Time.deltaTime;
            if (moonTimer < 0)
            {
                moonTimer = 4f;
                cometSpeed = -cometSpeed;
            }

            transform.RotateAround(neighbourPlanet.transform.position, zAxis, cometSpeed * Time.deltaTime);
        }
    }
}
