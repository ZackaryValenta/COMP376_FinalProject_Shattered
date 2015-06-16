using UnityEngine;
using System.Collections;

public class GeneralCommands : MonoBehaviour
{
	[SerializeField]
	Player player;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			player.setIsDead (true);
		}
	}
}
