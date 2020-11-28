using Cinemachine;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class PlanetGravity : MonoBehaviour
{
    ////////////////////////////////////////////////////////
    [Space(10)]
    [Header("Player info")]
    private Rigidbody2D rbPlayer;
    private CharacterController2D playerController;
    private GameObject player;

    [Space(10)] 
    [Header("EnemyInfo")] 
    private Quaternion enemyTargetRotation;
    public Transform enemyTransform;
    private Rigidbody2D enemyRB;

    [Space(10)]
    [Header("Planet info")]
    [SerializeField] GameObject planetBody;
    [SerializeField] float _force;

    [Space(10)]
    [Header("Misc")]
    private float smooth = 10f;
    private Quaternion targetRotation;

    [Space(10)]
    [Header("Cameras")]
    public CinemachineVirtualCamera virtualCamera;
    /////////////////////////////////////////////////////////


    private void Awake()
    {
        //Get the players rigidbody
        //Rigidbody is used to add force for gravity
        player = GameObject.Find("MainPlayer").gameObject;
        rbPlayer = player.GetComponent<Rigidbody2D>();
        playerController = player.GetComponent<CharacterController2D>();

        if (enemyTransform != null)
            enemyRB = enemyTransform.GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {   
        Vector2 playerGravityUp = (player.transform.position - planetBody.transform.position).normalized;
        Vector2 playerBodyUp = player.transform.up;

        //Finds the right angle to set the body to
        targetRotation = Quaternion.FromToRotation(playerBodyUp, playerGravityUp) * player.transform.rotation;

        if (enemyTransform != null)
        {
            Vector2 enemyGravityUp = (enemyTransform.position - planetBody.transform.position).normalized;
            Vector2 enemyBodyUp = enemyTransform.up;

            enemyTargetRotation = Quaternion.FromToRotation(enemyBodyUp, enemyGravityUp) * enemyTransform.rotation;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType() != typeof(BoxCollider2D))
        {
            //Set the players gravity to 0
            rbPlayer.gravityScale = 0f;
            playerController.Attracted = true;

            virtualCamera.Priority = 12;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player") && other.GetType() != typeof(BoxCollider2D))
        {
            //return the players gravity to normal
            rbPlayer.gravityScale = 0.1f;

            //Declares an upright rotation
            Quaternion target = Quaternion.Euler(0, 0, 0);

            //Rotates the player to upright
            while (Quaternion.Angle(player.transform.rotation, target) > 0)
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, target, Time.deltaTime * smooth);


            //Sets that the player has left the planet radius
            playerController.Attracted = false;

            //Gives the player a boost as they leave the planet radius
            var outDir = GetDirection(planetBody.transform.position, player.transform.position) * 5;
            rbPlayer.AddForce(outDir, ForceMode2D.Force);

            virtualCamera.Priority = 10;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType() != typeof(BoxCollider2D))
        {
            //Save the position of the planet & player
            var planetPosition = planetBody.transform.position;
            var playerPosition = player.transform.position;

            // Calculate the magnitude of the force by the rigidbody mass
            var forceMagnitude = rbPlayer.mass * _force * Time.fixedDeltaTime;

            //Calls the GetDirection Method and multiplies it by the faux gravity
            var force = GetDirection(playerPosition, planetPosition) * forceMagnitude;

            //Apply the force as gravity
            rbPlayer.AddForce(force,ForceMode2D.Force);

            //calls the rotation method  
            GravRotate(player.transform, targetRotation);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            // Calculate the magnitude of the force by the rigidbody mass
            var forceMagnitude = enemyRB.mass * _force * Time.fixedDeltaTime;

            //Calls the GetDirection Method and multiplies it by the faux gravity
            var force = GetDirection(enemyTransform.position, planetBody.transform.position) * forceMagnitude;

            //Apply the force as gravity
            enemyRB.AddForce(force, ForceMode2D.Force);

            GravRotate(enemyTransform, enemyTargetRotation);
        }
    }

    //Takes in two vectors and returns the direction between them
    private Vector2 GetDirection(Vector2 playerPoint, Vector2 planetPoint)
    {
        // Calculate the delta position and normalize it to just return the direction
        var delta = planetPoint - playerPoint;
        return delta.normalized;
    }

    private void GravRotate(Transform body,Quaternion targetRot)
    {
        //Sets the body rotation
        body.rotation = Quaternion.Slerp(body.rotation, targetRot, Time.deltaTime * smooth);
    }

}

