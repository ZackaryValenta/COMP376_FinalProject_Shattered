using UnityEngine;
using System.Collections;

public class DestoryBurger : MonoBehaviour {

	public GameObject explode;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	//private void destoryByCollide()
	//{
	//	Destroy (gameObject);
	//}
	
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag ("Player"))
		{
			Instantiate (explode, transform.position, transform.rotation);
			Destroy (gameObject);
		}
	}
}
