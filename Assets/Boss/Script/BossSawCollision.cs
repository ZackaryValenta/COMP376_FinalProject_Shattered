using UnityEngine;
using System.Collections;

public class BossSawCollision : MonoBehaviour
{
    [SerializeField] private GameObject bossGameObject;

    private Boss bossScript;

    void Start()
    {
        bossScript = bossGameObject.GetComponent<Boss>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (bossScript.SawCanCut)
        {
            if (other.tag == "Player")
                Debug.Log("French Fries the Player");
            else if (other.tag == "BoltBox")
                Debug.Log("Bolt Cut In Half");
        }
    }
}