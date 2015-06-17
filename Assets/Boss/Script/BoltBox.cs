using UnityEngine;
using System.Collections;

public class BoltBox : MonoBehaviour
{
    [SerializeField] private GameObject bossGameObject;
    [SerializeField] private GameObject boltExplosion;

    private Boss bossScript;

    void Start()
    {
        bossScript = bossGameObject.GetComponent<Boss>();
        bossScript.Revivers++;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boss" && (bossScript.SlammingDown || bossScript.SawCanCut))
        {
            Instantiate(boltExplosion, transform.position, Quaternion.identity);
            bossScript.Revivers--;
            Destroy(gameObject);
        }
    }
}
