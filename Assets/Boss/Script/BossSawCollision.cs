using UnityEngine;
using System.Collections;

public class BossSawCollision : MonoBehaviour
{
    [SerializeField] private GameObject bossGameObject;

    private Boss bossScript;
    public float SawDamage = 100;
    private Player playerScript;

    void Start()
    {
        bossScript = bossGameObject.GetComponent<Boss>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (bossScript.SawCanCut)
        {
            if (other.tag == "Player")
            {
                playerScript.TakeDamage(SawDamage);
                Debug.Log("Saw Damage");
            }
            else if (other.tag == "BoltBox")
                bossScript.Dizzy();
        }
    }
}