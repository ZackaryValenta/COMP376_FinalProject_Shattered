using UnityEngine;
using System.Collections;

public class EnemySparrowSprite : MonoBehaviour
{
	private bool isDead;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isDead)
		{
			die();
		}
	}

	void die()
	{
		Destroy (transform.parent.gameObject);
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Player") &&
		    (col.gameObject.GetComponent<Player> ().checkIfKicking () || col.gameObject.GetComponent<Player> ().checkIfSpinKicking ()))
		{
			isDead = true;
		}
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Player") &&
		    (col.gameObject.GetComponent<Player> ().checkIfKicking () || col.gameObject.GetComponent<Player> ().checkIfSpinKicking ()))
		{
			isDead = true;
		}
	}
}
