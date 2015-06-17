using UnityEngine;
using System.Collections;

public class BossSlamCollision : MonoBehaviour
{
    [SerializeField] private GameObject bossGameObject;
    [SerializeField] private string groundTag;

    private Boss bossScript;

    void Start()
    {
        bossScript = bossGameObject.GetComponent<Boss>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == groundTag && bossScript.SlammingDown )
            bossScript.SlammedGround();
        else if (other.tag == "Player")
            Debug.Log("Cut Player"); //TODO call player script to damage him
        else if (other.tag == "BossBox")
            Debug.Log("BossBox");   //TODO electrocute/shock boss for a moment, remote BossBox
    }
}
