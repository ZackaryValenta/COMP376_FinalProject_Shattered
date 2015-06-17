using UnityEngine;
using System.Collections;

public class PortalBossEnter : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			col.gameObject.GetComponent<Player>().setDoubleJumpUnlocked(true);
			col.gameObject.GetComponent<Player>().setSpinKickUnlocked(true);
		}
	}
}
