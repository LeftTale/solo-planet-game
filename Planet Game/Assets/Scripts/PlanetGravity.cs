using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    ////////////////////////////////////////////////////////
    [Space(10)]
    [Header("Player info")]
    private Rigidbody2D rbPlayer;
    public GameObject player;
    public Camera playerCam;
    [Space(10)]
    [Header("Planet info")]
    public GameObject planetBody;
    public Camera planetCam;
    [SerializeField] float _force;
    [Space(10)]
    [Header("Misc")]
    public float smooth = 2.0f;
    public Transform myTransform;
    Transform camTransform;
    private Quaternion targetRotation;
    /////////////////////////////////////////////////////////
    
    
    
    /// <summary>
    /// Implement trolley Cam
    /// </summary>



    void Start()
    {
        //Get the players rigidbody
        //Rigidbody is used to add force for gravity
        rbPlayer = player.GetComponent<Rigidbody2D>();
    }

    /*
     
    // Update is called once per frame
    void Update()
    {
       
        var force = GetDirection(rbPlayer, planetBody.transform.position);
        Debug.DrawRay(rbPlayer.transform.position, force, Color.red);
    }
    */

    private void FixedUpdate()
    {
        
        Vector2 gravityUp = (player.transform.position - planetBody.transform.position).normalized;
        Vector2 bodyUp = player.transform.up;

        //Finds the right angle to set the body and camera to
        targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * player.transform.rotation;

        //Sets the cameras rotation
        planetCam.transform.rotation = Quaternion.Slerp(planetCam.transform.rotation, targetRotation, smooth * Time.deltaTime);

       
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //When the player enters the gravity field Set inRange to true
        if (other.gameObject.tag == "Player")
        {
            //Save the position of the planet & player
            var planetPosition = planetBody.transform.position;
            var playerPosition = player.transform.position;

            //Switches cameras accordingly
            planetCam.enabled = true;
            playerCam.enabled = false;

            //Set the players gravity to 0
            rbPlayer.gravityScale = 0f;

            // Calculate the magnitude of the force by the rigidbody mass
            var forceMagnitude = rbPlayer.mass * _force * Time.fixedDeltaTime;

            //Calls the GetDirection Method and multiplies it by the faux gravity
            var force = GetDirection(playerPosition, planetPosition) * forceMagnitude;

            //Apply the force as gravity
            rbPlayer.AddForce(force);

            //calls attract  
            GravRotate(player.transform, targetRotation);
        }
    }


    //Takes in two vectors and returns the direction between them
     Vector2 GetDirection(Vector2 playerPoint, Vector2 planetPoint)
    {
        // Calculate the delta position and normalize it to just return the direction
        var delta = planetPoint - playerPoint;
        return delta.normalized;
    }

     void GravRotate(Transform body,Quaternion targetRot)
    {
        //Sets the bodys rotation
        body.rotation = Quaternion.Slerp(body.rotation, targetRot, smooth * Time.deltaTime);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //When the player exits the gravity field set the inRange to false
        if (other.gameObject.tag == "Player")
        {
            //return the players gravity to normal
            rbPlayer.gravityScale = 3f;
            //Declares an upright rotation
            Quaternion target = Quaternion.Euler(0, 0, 0);
            
            //Rotates the player to upright
            while(Quaternion.Angle(player.transform.rotation,target)>0)
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, target, Time.deltaTime * smooth);
            
            //Resets the cameras
            playerCam.enabled = true;
            planetCam.enabled = false;
        }
    }
}

