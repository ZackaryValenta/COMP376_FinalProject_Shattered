using UnityEngine;
using System.Collections;

public class Killzone : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Player"))
		{
			col.gameObject.GetComponent<Player>().setIsDead(true);
		}
	}
}
