using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class RocketFlying : MonoBehaviour
{
    private Rigidbody2D rocketRB;
    private float speedIncrement = 1f;
    private RocketControls controls;
    private float spinRot;
    private GameObject guide;
    public GameObject planetDec;
    public Text debugText;
    public List<GameObject> cometsList;
    private float planetDecTimer;
    

    private void Awake()
    {
        controls = new RocketControls();

        controls.RocketFlight.TurnRight.performed += ctx => spinRot = -1;
        controls.RocketFlight.TurnLeft.performed += ctx => spinRot = 1;
        controls.RocketFlight.TurnRight.canceled += ctx => spinRot = 0;
        controls.RocketFlight.TurnLeft.canceled += ctx => spinRot = 0;

        controls.RocketFlight.Forward.started += ctx => MoveForward();
        controls.RocketFlight.Backward.started += ctx => MoveBackward();
    }

    private void Start()
    {
       rocketRB = GetComponent<Rigidbody2D>();
       guide = GameObject.Find("Guide");
       
    }

    private void Update()
    {
        

       // planetDecTimer += Time.deltaTime;

        //if (planetDecTimer > 2)
        //{
            //Gets the direction the rockets flying
            //Vector2 planDir = GetDirection((Vector2) transform.position, (Vector2) guide.transform.position);
            //Vector2 rocketVec = planDir * new Vector2(Mathf.Abs(transform.position.x), Mathf.Abs(transform.position.y));
            
            //Instantiate(planetDec, rocketVec * 1.01f, this.transform.rotation);
           // planetDecTimer = 0f;
        //}

    }

    private void FixedUpdate()
    {
        rocketRB.velocity = GetDirection((Vector2)transform.position,(Vector2)guide.transform.position) * (speedIncrement * 40);

        rocketRB.angularVelocity = spinRot * 50;
    }

    void MoveForward()
    {
        if (speedIncrement < 3f)
        {
            speedIncrement++;
        }
    }

    void MoveBackward()
    {
        if (speedIncrement > 1f)
        {
            speedIncrement--;
        }
    }

    private void OnEnable()
    {
        controls.RocketFlight.Enable();
    }

    private void OnDisable()
    {
        controls.RocketFlight.Disable();
    }
    public Vector2 GetDirection(Vector2 source, Vector2 outD)
    {
        // Calculate the delta position and normalize it to just return the direction
        var delta = outD - source;
        return delta.normalized;
    }

   
}
