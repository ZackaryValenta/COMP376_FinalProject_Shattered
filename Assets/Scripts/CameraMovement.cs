using TreeEditor;
using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smooth = 5;
    [SerializeField] private Vector3 velocity;

    private float initialZDistance;

    void Start()
    {
        initialZDistance = transform.position.z;
    }

    void Update()
    {
        Vector3 newPosition = new Vector3(target.position.x, target.position.y, initialZDistance);
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smooth * Time.deltaTime);
    }
}
