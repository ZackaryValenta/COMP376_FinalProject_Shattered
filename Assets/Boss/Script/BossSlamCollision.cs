using UnityEngine;
using System.Collections;

public class BossSlamCollision : MonoBehaviour
{
    [SerializeField] private GameObject bossGameObject;
    [SerializeField] private string groundTag;

    private Boss bossScript;
    private Player playerScript;
    public float SlamDamage = 100;

    void Start()
    {
        bossScript = bossGameObject.GetComponent<Boss>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player" && other.tag != "BossBox" && bossScript.SlammingDown )
            bossScript.SlammedGround();
        if (other.tag == "Player" && bossScript.SlammingDown)
        {
            playerScript.TakeDamage(SlamDamage);
            Debug.Log("Slam Damage");
        }
        if (other.tag == "BossBox" && bossScript.SlammingDown)
            bossScript.Dizzy();
    }
}
