using System.Runtime.Remoting.Lifetime;
using UnityEngine;
using System.Collections;

public class Expire : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
