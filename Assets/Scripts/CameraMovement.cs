using TreeEditor;
using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smooth = 20;
	[SerializeField] private Vector3 velocity;
	[SerializeField] private float hortizontalBufferPercentage;
	[SerializeField] private float verticalBufferPercentage;

	private float initialZPosition;
	private Camera camera;

    void Start()
    {
        initialZPosition = transform.position.z;
		camera = gameObject.GetComponent<Camera> ();
    }

    void Update()
    {
		Vector3 leftLower   = camera.ScreenToWorldPoint (new Vector3 (0, 0, -transform.position.z));
		Vector3 rightUpper  = camera.ScreenToWorldPoint (new Vector3 (camera.pixelWidth, camera.pixelHeight, (target.position - transform.position).z));
		float leftBoundary  = transform.position.x - (Mathf.Abs(leftLower.x - rightUpper.x)*(0.5f - hortizontalBufferPercentage));
		float rightBoundary = transform.position.x + (Mathf.Abs(leftLower.x - rightUpper.x)*(0.5f - hortizontalBufferPercentage));
		float lowerBoundary = transform.position.y - (Mathf.Abs(rightUpper.y - leftLower.y)*(0.5f - verticalBufferPercentage));
		float upperBoundary = transform.position.y + (Mathf.Abs(rightUpper.y - leftLower.y)*(0.5f - verticalBufferPercentage));

		if (target.position.x < leftBoundary || target.position.x > rightBoundary ||
		    target.position.y < lowerBoundary || target.position.y > upperBoundary)
		{
			Vector3 newPosition = new Vector3(target.position.x, target.position.y, initialZPosition);
			transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smooth * Time.deltaTime);
		}

		Debug.Log ("target position: " + target.position +
		           "\nleft/right boundary: " + leftBoundary + " , " + rightBoundary +
		           "\nlower/upper boundary: " + lowerBoundary + " , " + upperBoundary );
    }
}
