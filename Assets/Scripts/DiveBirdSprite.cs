using UnityEngine;
using System.Collections;

public class DiveBirdSprite : MonoBehaviour
{
	// Animator booleans
	bool hitPlayer;

	private Animator animator;
	private bool isDead;

	// Use this for initialization
	void Start ()
	{
		hitPlayer = false;
		animator = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateAnimator ();
	}
	
	public void setAttackingFalse()
	{
		transform.parent.GetComponent<DiveBird> ().isAttacking = false;
	}
	
	public void setHitPlayerFalse()
	{
		if (isDead)
		{
			die();
		}
		hitPlayer = false;
	}
	
	private void UpdateAnimator()
	{
		animator.SetBool ("hitPlayer", hitPlayer);
	}
	
	void die()
	{
		Destroy (transform.parent.gameObject);
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			if (col.gameObject.GetComponent<Player> ().checkIfKicking () || col.gameObject.GetComponent<Player> ().checkIfSpinKicking ())
			{
				isDead = true;
			}
			hitPlayer = true;
			setAttackingFalse();
		}
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			if (col.gameObject.GetComponent<Player> ().checkIfKicking () || col.gameObject.GetComponent<Player> ().checkIfSpinKicking ())
			{
				isDead = true;
			}
			hitPlayer = true;
			setAttackingFalse();
		}
	}
}
