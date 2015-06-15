using UnityEngine;
using System.Collections;

public class CaveBlockingStone : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	private void explode()
	{
		Destroy (gameObject);
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{

		if (col.gameObject.CompareTag ("Player") &&
			col.gameObject.GetComponent<Player> ().checkIfSpinKicking ())
		{
			explode ();
		}
	}
}
