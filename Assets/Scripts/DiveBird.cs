using UnityEngine;
using System.Collections;

public class DiveBird : MonoBehaviour
{
	[SerializeField]
	float attackSpeed;
	[SerializeField]
	float attackCooldown;
	[SerializeField]
	bool startFacingRight;

	// Animator booleans
	public bool isAttacking;
	
	private GameObject followedPlayer;
	private Vector3 attackPosition;
	private Vector2 facingDirection;
	private float attackCountdown;
	private Animator animator;
	
	// Use this for initialization
	void Start ()
	{
		facingDirection = ((startFacingRight) ? -1 : 1) * Vector2.right;		// opposite because sprite is drawn facing left
		Debug.Log (facingDirection);
		animator = gameObject.GetComponentInChildren<Animator> ();
	}
			
	// Update is called once per frame
	void Update ()
	{
		FaceDirection (computeFacingDirection());
		if (followedPlayer)
		{
			if (Mathf.Approximately(attackCountdown, 0.0f) || attackCountdown < 0.0f)
			{
				attackCountdown = attackCooldown;
				isAttacking = true;
			}
			else
			{
				attackCountdown -= Time.deltaTime;
			}
			// if attacking, move towards player
			if (isAttacking)
			{
				Vector3 attackDirection = Vector3.Normalize(followedPlayer.transform.position - transform.position);
				transform.position += attackDirection * attackSpeed * Time.deltaTime;
			}
		}
		UpdateAnimator ();
	}
	
	private Vector2 computeFacingDirection()
	{
		Vector2 returnDirection = facingDirection;
		if (followedPlayer)
		{
			float enemyMinusPlayerX = transform.position.x - followedPlayer.transform.position.x;
			if (enemyMinusPlayerX > 0.0f || Mathf.Approximately(enemyMinusPlayerX, 0.0f))
			{
				returnDirection = Vector2.right;
			}
			else
			{
				returnDirection = -Vector2.right;
			}
		}
		return returnDirection;
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
	
	private void UpdateAnimator()
	{
		animator.SetBool ("isAttacking", isAttacking);
	}
	
	public Vector2 GetFacingDirection()
	{
		return facingDirection;
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Player") && !followedPlayer)
		{
			followedPlayer = col.gameObject;
		}
	}
	
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Player") && followedPlayer && col.gameObject.Equals(followedPlayer))
		{
			followedPlayer = null;
			attackCountdown = 0.0f;
		}
	}
}
