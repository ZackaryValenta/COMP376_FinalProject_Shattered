using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    private Player playerScript;
	private Boss bossScript;

	void Start ()
	{
	    playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		bossScript = GameObject.Find("BossNew").GetComponent<Boss> ();
	}
	
	void Update () {
	
        if (playerScript.getCurrentHealth() <= 0)
            Application.LoadLevel("GameOver");
		if (bossScript.getCurrentLives () <= 0)
			Application.LoadLevel ("GameOver");
	}
}
