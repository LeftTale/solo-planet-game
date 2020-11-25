
using System.Collections;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
    private bool attracted;
    private bool planGrounded;
    private Camera playerCam;
    private GameObject guide;
    private TextMeshProUGUI debugText;
    private bool boostCoolDown;
    private GameObject cursorGameObject;
    public Animator sliderAnimator;
    public AudioSource audioSource;
    private ParticleSystem boosterParticleSystem;
    private ParticleSystem speedParticleSystem;
    private ParticleSystem.VelocityOverLifetimeModule velocityModule;

    [Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

    [System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }


    private void Start()
    {
        cursorGameObject = GameObject.Find("Controller Cursor").transform.Find("Cursor").gameObject;
        velocityModule = boosterParticleSystem.velocityOverLifetime;
    }

    private void Awake()
	{
		//On awake gets the players rigidbody  Creates and event for landing on the ground
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
        playerCam = GameObject.Find("PlayerCam").GetComponent<Camera>();
        guide = transform.Find("Guide").gameObject;
        debugText = GameObject.Find("Debug Text").GetComponent<TextMeshProUGUI>();
        boosterParticleSystem = transform.Find("Boost Particles").GetComponent<ParticleSystem>();
        

        if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !boostCoolDown)
        {
            Vector2 boostDirVector2 =
                GetDirection(playerCam.WorldToScreenPoint(transform.position), Input.mousePosition);
            m_Rigidbody2D.AddForce(boostDirVector2 * (m_JumpForce * 2));
            boosterParticleSystem.Emit(100);
            audioSource.Play();
            StartCoroutine(BoostCD());
            
        }
        else if (Input.GetAxisRaw("Fire1") > 0.9f && !boostCoolDown)
        {
            Vector2 cursorPos = cursorGameObject.transform.position;
            Vector2 boostCurVector2 = GetDirection(transform.position, cursorPos);
            m_Rigidbody2D.AddForce(boostCurVector2 * (m_JumpForce * 2));
            boosterParticleSystem.Emit(100);
            audioSource.Play();
            StartCoroutine(BoostCD());
            
        }
    }

    IEnumerator BoostCD()
    {
        boostCoolDown = true;
        sliderAnimator.SetBool("coolDownTimerStart",true);

        yield return new WaitForSeconds(6f);

        sliderAnimator.SetBool("coolDownTimerStart",false);
        boostCoolDown = false;
    }

    void controlParticles(Vector2 Cursor)
    {
        if (Cursor.x < 0)
            velocityModule.xMultiplier = -70;
        else
            velocityModule.xMultiplier = 70;
    }

    private void FixedUpdate()
	{
		//Check if the object was grounded and then resets loop
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}

   

	public void Move(float move, bool jump)
	{
        //If the player is in regular gravity
        if (m_Rigidbody2D.gravityScale >= 3f)
        {
            if (m_Grounded || m_AirControl)
            {
                // Move the character by finding the target velocity
                Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
                // And then smoothing it out and applying it to the character

                m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity,
                    m_MovementSmoothing);

                debugText.text = "State 1";
            }
        }
        else if (m_Rigidbody2D.gravityScale == 0.5f)
        {
            Vector2 directionVector2 = new Vector2(move,Input.GetAxisRaw("Vertical")*-1) * 5;
            m_Rigidbody2D.AddForce(directionVector2);
        }
        else
        {
            Vector2 guideDir = GetDirection(transform.position, guide.transform.position);
            //only control the player if grounded or airControl is turned on
            if (planGrounded && Attracted)
            {
                debugText.text = "State 3";
                Vector2 targetVelocity = guideDir * (2f * Mathf.Abs(move));

                m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity,
                    m_MovementSmoothing);
            }
            else if (attracted)
            {
                debugText.text = "State 2";
                Vector2 targetForce = guideDir * (2f * Mathf.Abs(move));
                m_Rigidbody2D.AddForce(targetForce);
            }
            
        }
        


        // If the input is moving the player right and the player is facing left...
        if (move > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (move < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // If the player should jump...
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.AddRelativeForce(new Vector2(0f, m_JumpForce));
        }
    }


    private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    public bool Attracted
    {
        get => attracted;
        set => attracted = value;
    }

    public bool PlanGrounded
    {
        get => planGrounded;
        set => planGrounded = value;
    }
    public Vector2 GetDirection(Vector2 source, Vector2 outD)
    {
        // Calculate the delta position and normalize it to just return the direction
        var delta = outD - source;
        return delta.normalized;
    }
}