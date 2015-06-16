using UnityEngine;
using System.Collections;

public class PowerupJump : MonoBehaviour {
	
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			col.gameObject.GetComponent<Player>().setDoubleJumpUnlocked(true);
			Destroy (gameObject);
		}
	}
}
