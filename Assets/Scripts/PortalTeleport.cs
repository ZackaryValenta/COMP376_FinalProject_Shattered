using UnityEngine;
using System.Collections;

public class PortalTeleport : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col)	
	{
		if (col.gameObject.CompareTag ("Player")) 
		{
			Application.LoadLevel("BossScene");
		}
	}
}
