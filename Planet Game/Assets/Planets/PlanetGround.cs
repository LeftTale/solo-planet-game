using UnityEngine;

public class PlanetGround : MonoBehaviour
{
    private GameObject player;
    private CharacterController2D controller;

    private void Awake()
    {
        player = GameObject.Find("MainPlayer").gameObject;
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
