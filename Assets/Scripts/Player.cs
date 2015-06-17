using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	[SerializeField]
	float maxHealth;
	[SerializeField]
	float currentHealth;
	[SerializeField]
	float moveSpeed;
	[SerializeField]
	float ungroundedMoveSpeed;
	[SerializeField]
	float maxFallTime;				// amount of time player can fall without taking damage upon landing
	[SerializeField]
	float fallDamage;
	[SerializeField]
	float jumpUpForce;
	[SerializeField]
	float jumpMomentumForce;
	[SerializeField]
	float jumpDownForce;
	[SerializeField]
	float damagePushForce;
	[SerializeField]
	LayerMask whatIsGround;
	[SerializeField]
	bool isDoubleJumpUnlocked;		// double jump power-up unlocked
	[SerializeField]
	bool isSpinKickUnlocked;		// spin kick power up unlocked
	[SerializeField]
	Checkpoint currentCheckpoint;		// spin kick power up unlocked
	
	// Animator booleans
	bool running;
	bool grounded;
	bool rising;
	[SerializeField]
	bool isDead;
	[SerializeField]
	bool isKicking;
	bool isSpinKicking;

	// Invincibility timer
	float invincibilityDuration = 1.0f;
	float invincibleTimer;
	bool invincible;

	// control variables
	bool allowDoubleJump;				// tracks if player can double jump (is false if they have already jumped twice)
	float groundCheckRadius = 0.5f;
	Vector2 facingDirection;
	float currentFallTime;
	bool hasJumped;						// used for downward force added when player starts falling

	// component references
	Animator animator;
	Rigidbody2D rigidBody2D;
	List<GroundCheck> groundCheckList;
	
	// Reference to audio sources
	AudioSource jumpSound;
	AudioSource landingSound;
	AudioSource kickSound;
	AudioSource spinKickSound;
	AudioSource takeDamageSound;
	
	// Use this for initialization
	void Start ()
	{
		currentHealth   = maxHealth;
		currentFallTime = 0.0f;

		grounded             = true;
		isDoubleJumpUnlocked = false;
		isSpinKickUnlocked   = false;
		allowDoubleJump      = true;
		isKicking            = false;
		isSpinKicking        = false;
		invincible           = false;
		hasJumped            = false;

		animator        = GetComponent<Animator>();
		rigidBody2D     = GetComponent<Rigidbody2D>();
		groundCheckList = new List<GroundCheck>();

		GroundCheck[] groundChecksArray = transform.GetComponentsInChildren<GroundCheck>();
		foreach(GroundCheck g in groundChecksArray)
		{
			groundCheckList.Add (g);
		}

		AudioSource[] audioSources = GetComponents<AudioSource>();
		jumpSound                  = audioSources[0];
		landingSound               = audioSources[1];
		kickSound                  = audioSources[2];
		spinKickSound              = audioSources[3];
		takeDamageSound            = audioSources[4];
		FaceDirection (Vector2.right);
		respawn ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		// **********************************************************************************************
		// check if player is dead
		if (Mathf.Approximately (currentHealth, 0.0f) || currentHealth < 0.0f)
		{
			currentHealth = 0.0f;
			isDead = true;
		}

		if (!isDead)
		{
			// **********************************************************************************************
			// check for landing
			bool justGrounded = CheckGrounded ();
			if(!grounded && justGrounded)		// if player was not grounded last frame (i.e. grounded), but became grounded this frame (i.e. justGrounded)
			{
				allowDoubleJump = true;
				landingSound.Play ();
				if (currentFallTime > maxFallTime)
				{
					currentHealth -= fallDamage;
				}
				currentFallTime = 0.0f;
				hasJumped = false;		// in case player jumped and didn't fall before becoming grounded again
			}
			grounded = justGrounded;
			
			// **********************************************************************************************
			// if player is not attacking, take commands
			running = false;
			if (!isKicking && !isSpinKicking)
			{
				takeUserInput ();
			}
			
			rising = !Mathf.Approximately(rigidBody2D.velocity.y, 0.0f) && rigidBody2D.velocity.y > 0.0f;
			
			// **********************************************************************************************
			// add to current fall time
			if (!grounded && !rising)
			{
				// if just started falling and has jumped since was last grounded
				if (Mathf.Approximately(currentFallTime, 0.0f) && hasJumped)
				{
					rigidBody2D.AddForce(-Vector2.up * jumpDownForce, ForceMode2D.Impulse);
					hasJumped = false;
				}
				currentFallTime += Time.deltaTime;
			}
			
			// **********************************************************************************************
			// if invincible, see if invincibility has run out
			if(invincible)
			{
				invincibleTimer += Time.deltaTime;
				if(invincibleTimer >= invincibilityDuration)
				{
					invincible = false;
					invincibleTimer = 0.0f;
				}
			}
		}
		UpdateAnimator();
	}

	private void takeUserInput()
	{
		// **********************************************************************************************
		// set speed to grounded or not
		float speed = 0.0f;
		if (grounded)
		{
			speed = moveSpeed;
		}
		else
		{
			speed = ungroundedMoveSpeed;
		}

		if(Input.GetButton ("Left") && !Input.GetButton ("Right"))
		{
			transform.Translate (-Vector2.right * speed * Time.deltaTime);
			FaceDirection(-Vector2.right);
			running = true;
		}
		
		if(Input.GetButton ("Right") && !Input.GetButton ("Left"))
		{
			transform.Translate (Vector2.right * speed * Time.deltaTime);
			FaceDirection(Vector2.right);
			running = true;
		}
		
		if(Input.GetButton ("Kick") && !Input.GetButton ("SpinKick"))
		{
			kickSound.Play ();
			isKicking = true;
		}
		
		if(Input.GetButton ("SpinKick") && !Input.GetButton ("Kick") && isSpinKickUnlocked)
		{
			spinKickSound.Play ();
			isSpinKicking = true;
		}
		
		if((Input.GetButtonDown ("Jump")) &&
		   (grounded || (isDoubleJumpUnlocked && allowDoubleJump)))
		{
			if (!grounded)
			{
				allowDoubleJump = false;
			}
			jumpSound.Play ();
			rigidBody2D.AddForce(Vector2.up * jumpUpForce, ForceMode2D.Impulse);
			if (running)
			{
				rigidBody2D.AddForce(facingDirection * jumpMomentumForce, ForceMode2D.Impulse);
			}
			hasJumped = true;
		}
	}

	public void toggleSpinKicking()
	{
		isSpinKicking = !isSpinKicking;
	}

	public bool checkIfSpinKicking()
	{
		return isSpinKicking;
	}

	public void toggleKicking()
	{
		isKicking = !isKicking;
	}
	
	public bool checkIfKicking()
	{
		return isKicking;
	}

	public void setIsDead(bool b)
	{
		isDead = b;
	}

	public float getSpeed()
	{
		return moveSpeed;
	}
	
	public float getUngroundedSpeed()
	{
		return ungroundedMoveSpeed;
	}

	public void setDoubleJumpUnlocked(bool b)
	{
		isDoubleJumpUnlocked = b;
	}

	public void setSpinKickUnlocked(bool b)
	{
		isSpinKickUnlocked = b;
	}

	public void playDeathSound()
	{
		takeDamageSound.Play ();
	}

	public bool isGrounded()
	{
		return grounded;
	}

	public void setCurrentCheckpoint(Checkpoint newCheckpoint)
	{
		currentCheckpoint.close ();
		currentCheckpoint = newCheckpoint;
		currentCheckpoint.open ();
	}
	
	public Checkpoint getCurrentCheckpoint()
	{
		return currentCheckpoint;
	}

	public void respawn()
	{
		if (currentCheckpoint)
		{
			transform.position = currentCheckpoint.getRespawnPosition ();
			isDead = false;
			currentHealth = maxHealth;
		}
	}
	
	public void TakeDamage(float dmg)
	{
		if(!invincible)
		{
			currentHealth         -= dmg;
			invincible             = true;
			rigidBody2D.velocity   = Vector2.zero;
			Vector2 forceDirection = new Vector2(-facingDirection.x, 1.0f) * damagePushForce;
			rigidBody2D.AddForce(forceDirection, ForceMode2D.Impulse);
			takeDamageSound.Play ();
		}
	}
	
	public Vector2 GetFacingDirection()
	{
		return facingDirection;
	}
	
	private void FaceDirection(Vector2 direction)
	{
		facingDirection = direction;
		if(direction == Vector2.right)
		{
			Vector3 newScale = new Vector3(Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);
			transform.localScale = newScale;
		}
		else if(direction == -Vector2.right)
		{
			Vector3 newScale = new Vector3(-Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);
			transform.localScale = newScale;
		}
	}
	
	private bool CheckGrounded()
	{
		foreach(GroundCheck g in groundCheckList)
		{
			if(g.CheckGrounded(groundCheckRadius, whatIsGround, gameObject))
			{
				return true;
			}
		}
		return false;
	}

	private void UpdateAnimator()
	{
		animator.SetBool ("isRunning", running);
		animator.SetBool ("isGrounded", grounded);
		animator.SetBool ("isRising", rising);
		animator.SetBool ("isDead", isDead);
		animator.SetBool ("isKicking", isKicking);
		animator.SetBool ("isSpinKicking", isSpinKicking);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Enemy"))
		{
			TakeDamage(col.gameObject.GetComponent<EnemyDamage> ().getDamage ());
		}
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{
	}
	
	void OnCollisionExit2D(Collision2D col)
	{
	}

}
