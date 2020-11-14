using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    ////////////////////////////////////////////////////////
    [Space(10)]
    [Header("Player info")]
    private Rigidbody2D rbPlayer;
    private Camera playerCam;
    CharacterController2D playerController;
    [SerializeField] GameObject player;
    
    
    [Space(10)]
    [Header("Planet info")]
    private Camera planetCam;
    [SerializeField] GameObject planetBody;
    [SerializeField] float _force;

    [Space(10)]
    [Header("Misc")]
    private float smooth = 10f;
    private  Transform myTransform;
    private Quaternion targetRotation;
    /////////////////////////////////////////////////////////



    /// <summary>
    /// Implement trolley Cam
    /// </summary>


    void Awake()
    {
        //Get the players rigidbody
        //Rigidbody is used to add force for gravity
        rbPlayer = player.GetComponent<Rigidbody2D>();
        playerCam = player.GetComponentInChildren<Camera>();
        planetCam = planetBody.GetComponentInChildren<Camera>();
        playerController = player.GetComponent<CharacterController2D>();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && other.GetType() != typeof(BoxCollider2D))
        {
            playerController.SetAttracted();

            //planetCam.transform.position = playerCam.transform.position; 
            //Switches cameras accordingly
            planetCam.enabled = true;
            playerCam.enabled = false;

            //while ((Vector2)planetCam.transform.position != new Vector2(0f, 0f))
               // planetCam.transform.localPosition = Vector2.MoveTowards((Vector2)planetCam.transform.localPosition, (Vector2)planetBody.transform.localPosition, 5* Time.deltaTime);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player" && other.GetType() != typeof(BoxCollider2D))
        {
            playerController.UnSetAttracted();
            //return the players gravity to normal
            rbPlayer.gravityScale = 3f;

            //Declares an upright rotation
            Quaternion target = Quaternion.Euler(0, 0, 0);

            //Rotates the player to upright
            while (Quaternion.Angle(player.transform.rotation, target) > 0)
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, target, Time.deltaTime * smooth);

            //Resets the cameras
            playerCam.enabled = true;
            planetCam.enabled = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && other.GetType() != typeof(BoxCollider2D))
        {
            //Save the position of the planet & player
            var planetPosition = planetBody.transform.position;
            var playerPosition = player.transform.position;

            //Set the players gravity to 0
            rbPlayer.gravityScale = 0f;

            // Calculate the magnitude of the force by the rigidbody mass
            var forceMagnitude = rbPlayer.mass * _force * Time.fixedDeltaTime;

            //Calls the GetDirection Method and multiplies it by the faux gravity
            var force = GetDirection(playerPosition, planetPosition) * forceMagnitude;

            //Apply the force as gravity
            rbPlayer.AddForce(force,ForceMode2D.Force);

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

}

