using UnityEngine;
using System.Collections;

public class BoltBox : MonoBehaviour
{
    [SerializeField] private GameObject bossGameObject;
    [SerializeField] private GameObject boltExplosion;
    [SerializeField] private GameObject boltBullet;

    private Boss bossScript;

    void Start()
    {
        bossScript = bossGameObject.GetComponent<Boss>();
        bossScript.Revivers++;
        StartCoroutine(ReviveBoss());
    }

    private IEnumerator ReviveBoss()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (bossScript.currentLives == 0)
            {
                GameObject bulletObject = Instantiate(boltBullet, transform.position, Quaternion.identity) as GameObject;
                BoltBullet bulletScript = bulletObject.GetComponent<BoltBullet>();
                bulletScript.SetTargetLocation(bossGameObject.transform.position);
            }
        }
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
