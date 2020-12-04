using UnityEngine;
using UnityEngine.Playables;

public class IntroRocket : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer playerSprite;
    private PlayableDirector director;
    private void Start()
    {
        player = GameObject.Find("MainPlayer");
       
        playerSprite = player.GetComponent<SpriteRenderer>();
        director = GetComponent<PlayableDirector>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetButton("Crouch"))
        {
            playerSprite.enabled = false;
            GameManager.isInputEnabled = false;
            director.Play();
        }
    }
}
