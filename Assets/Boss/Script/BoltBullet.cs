using UnityEngine;
using System.Collections;

public class BoltBullet : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    private Rigidbody2D _rigidBody2D;


    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    public void SetTargetLocation(Vector3 targetLocation)
    {
        Vector3 direction = targetLocation - transform.position;
        _rigidBody2D.velocity = direction*speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boss")
        {
            Destroy(gameObject);
        }
    }
}
