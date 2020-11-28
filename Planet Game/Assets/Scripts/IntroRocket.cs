using UnityEngine;
using UnityEngine.Playables;

public class IntroRocket : MonoBehaviour
{
    private ParticleSystem particle;
    private GameObject player;
    private SpriteRenderer playerSprite;
    private PlayableDirector director;
    private void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        player = GameObject.Find("MainPlayer");
       
        playerSprite = player.GetComponent<SpriteRenderer>();
        director = GetComponent<PlayableDirector>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetButton("Crouch"))
        {
            particle.Play();
            
            playerSprite.enabled = false;
            GameManager.isInputEnabled = false;
            director.Play();
        }
    }
}
