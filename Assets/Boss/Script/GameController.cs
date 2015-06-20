using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    private Player playerScript;

	void Start ()
	{
	    playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	void Update () {
	
        if (playerScript.getCurrentHealth() <= 0)
            Application.LoadLevel("GameOver");

	}
}
