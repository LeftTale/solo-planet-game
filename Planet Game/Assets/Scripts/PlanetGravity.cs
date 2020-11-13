using UnityEngine;


public class PlanetGravity : MonoBehaviour
{
    [SerializeField] float _force;

    public float smooth = 2.0f;

    public GameObject player;
    public Rigidbody2D rbPlayer;
    public bool inRange = false;
    public GameObject planetBody;
    public Transform myTransform;
    

    // Start is called before the first frame update
    void Start()
    {
        //Get the players rigidbody
       rbPlayer = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Debug dawg
         */
        var force = GetDirection(rbPlayer, planetBody.transform.position);
        Debug.DrawRay(rbPlayer.transform.position, force, Color.red);
    }

    private void FixedUpdate()
    {
        //Save the position of the planet
        var currentPosition = (Vector2)planetBody.transform.position;

        if (inRange)
        {
            //Set the players gravity to 0
            rbPlayer.gravityScale = 0f;

            // Calculate the magnitude of the force by the rigidbody mass
            var forceMagnitude = rbPlayer.mass * _force * Time.fixedDeltaTime;

            //Calls the GetDirection Method and multiplies it by the faux gravity
            var force = GetDirection(rbPlayer, currentPosition) * forceMagnitude;

            //Apply the force
            rbPlayer.AddForce(force);

            myTransform = player.transform;

            Attract(myTransform);
        }
        else if (!inRange)
        {
            //return the players gravity to normal
            rbPlayer.gravityScale = 3f;
            Quaternion target = Quaternion.Euler(0, 0, 0);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, target, Time.deltaTime * smooth);
        }   
    }

    static Vector2 GetDirection(Rigidbody2D body, Vector2 point)
    {
        // Calculate the delta position and normalize it to just return the direction
        var delta = point - body.position;
        return delta.normalized;
    }

    public void Attract(Transform body)
    {
        //This is done Twice ***CLEAN****(Finding the upwards direction)
        Vector2 gravityUp = (body.position - planetBody.transform.position).normalized;
        Vector2 bodyUp = body.up;

        Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;
        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, smooth * Time.deltaTime);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        //When the player enters the gravity field Set inRange to true
        if(other.gameObject.tag == "Player")
        {
            inRange = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //When the player exits the gravity field set the inRange to false
        if (other.gameObject.tag == "Player")
        {
            inRange = false;
        }
    }
}
