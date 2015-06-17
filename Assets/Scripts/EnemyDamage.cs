using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour
{
	[SerializeField]
	float damage;
	
	public float getDamage()
	{
		return damage;
	}
}