using UnityEngine;
using System.Collections;

public class BossHeadCollision : MonoBehaviour
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
        if (other.tag == "Player" && bossScript.Vulnerable)
            bossScript.TakeDamge();
    }
}
