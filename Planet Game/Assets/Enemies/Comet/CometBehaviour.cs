using System;
using UnityEngine;

public class CometBehaviour : MonoBehaviour
{
    public GameObject planetGameObject;
    public GameObject cometGameObject;
    private readonly Vector3 zAxis = new Vector3(0,0,1);
    private TextDirector textDirector;
    private GameObject playerObject;

    private void Start()
    {
       
        textDirector = GameObject.Find("AveryUI").transform.Find("TextDirector").GetComponent<TextDirector>();
        playerObject = GameObject.Find("MainPlayer");
    }

    private void FixedUpdate()
    {
        //rotates the comet around the planet
        cometGameObject.transform.RotateAround(planetGameObject.transform.position, zAxis, 50 * Time.deltaTime);

        transform.Rotate(zAxis,10 * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            textDirector.SendDeathText(3);
            playerObject.GetComponent<Animator>().SetTrigger("Death");
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(),other.gameObject.GetComponent<Collider2D>());
            playerObject.GetComponent<PlayerMovement>().Dead = true;
        }
    }
}
