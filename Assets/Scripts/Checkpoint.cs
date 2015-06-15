using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{
	[SerializeField]
	bool isOpen;
	[SerializeField]
	GameObject respawnPosition;

	private Animator animator;

	// Use this for initialization
	void Start ()
	{
		animator = gameObject.GetComponent<Animator> ();
		if (!animator && gameObject.GetComponentsInChildren<Animator> ().Length == 1)
		{
			animator = gameObject.GetComponentsInChildren<Animator> ()[0];
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		updateAnimator ();
	}

	private void updateAnimator()
	{
		if (animator)
		{
			animator.SetBool ("isOpen", isOpen);
		}
	}

	public void open()
	{
		isOpen = true;
	}
	
	public void close()
	{
		isOpen = false;
	}

	public Vector3 getRespawnPosition()
	{
		return respawnPosition.transform.position;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag ("Player"))
		{
			col.gameObject.GetComponent<Player> ().getCurrentCheckpoint().close();
			col.gameObject.GetComponent<Player> ().setCurrentCheckpoint(this);
			open ();
		}
	}
}
