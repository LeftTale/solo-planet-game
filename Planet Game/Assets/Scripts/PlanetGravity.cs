using UnityEngine;


public class PlanetGravity : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rbPlayer;
    public bool inRange = false;
    public GameObject planetBody;

    
    // Start is called before the first frame update
    void Start()
    {
       rbPlayer = player.GetComponent<Rigidbody2D>();
      
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        if (inRange)
        {
            rbPlayer.gravityScale = 0f;
            rbPlayer.AddForce(new Vector2((planetBody.transform.position.x - (player.transform.position.x)),(planetBody.transform.position.y - (player.transform.position.y))));
        }
        else if (!inRange)
            rbPlayer.gravityScale = 3f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("attracted");
            inRange = true;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = false;
        }
    }
}
