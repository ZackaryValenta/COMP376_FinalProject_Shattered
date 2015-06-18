using UnityEngine;
using System.Collections;

public class PowerupStrength : MonoBehaviour {
	
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			col.gameObject.GetComponent<Player>().setSpinKickUnlocked(true);
			col.gameObject.GetComponent<Player>().triggerSpinKicking ();
			Destroy (gameObject);
		}
	}
}
