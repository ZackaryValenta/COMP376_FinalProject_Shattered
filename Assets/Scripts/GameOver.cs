using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

    public void RestartGame()
    {
        Application.LoadLevel("Open");
    }
}
