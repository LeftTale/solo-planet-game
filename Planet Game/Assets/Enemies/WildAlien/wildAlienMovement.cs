using UnityEngine;

public class WildAlienMovement : MonoBehaviour
{
    private Rigidbody2D enemyRB;
    private GameObject enemyGuide;
    private Animator enemyAnimator;
    private TextDirector textDirector;
    private PlayerMovement playerScript;

    private bool collideHit;

    void Start()
    {
        //Acquire the enemys rigidBody
        enemyRB = GetComponent<Rigidbody2D>();
        //Acquire the guide object for the enemy
        enemyGuide = transform.Find("Enemy Guide").gameObject;
        //Acquire the enemy animator
        enemyAnimator = transform.Find("WildAlienGFX").gameObject.GetComponent<Animator>();
        //Acquire the Text Director
        textDirector = GameObject.Find("AveryUI").transform.Find("TextDirector").GetComponent<TextDirector>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

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
            playerScript.Dead = true;
            textDirector.SendDeathText(2);
            enemyAnimator.SetBool("alienIdle",true);
            enemyAnimator.SetBool("alienRun", false);
            
        }
    }
}
