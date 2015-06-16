using TreeEditor;
using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
	[SerializeField] private bool usePlayerSpeed;
    [SerializeField] private float cameraSpeed;
	[SerializeField] private float hortizontalBufferPercentage;
	[SerializeField] private float verticalBufferPercentage;

	private Camera camera;

    void Start()
    {
		camera = gameObject.GetComponent<Camera> ();
    }

    void Update()
    {
		float speed = (usePlayerSpeed) ? target.gameObject.GetComponent<Player> ().getSpeed() : cameraSpeed;

		Vector3 leftLower   = camera.ScreenToWorldPoint (new Vector3 (0, 0, -transform.position.z));
		Vector3 rightUpper  = camera.ScreenToWorldPoint (new Vector3 (camera.pixelWidth, camera.pixelHeight, (target.position - transform.position).z));
		float leftBoundary  = transform.position.x - (Mathf.Abs(leftLower.x - rightUpper.x)*(0.5f - hortizontalBufferPercentage));
		float rightBoundary = transform.position.x + (Mathf.Abs(leftLower.x - rightUpper.x)*(0.5f - hortizontalBufferPercentage));
		float lowerBoundary = transform.position.y - (Mathf.Abs(rightUpper.y - leftLower.y)*(0.5f - verticalBufferPercentage));
		float upperBoundary = transform.position.y + (Mathf.Abs(rightUpper.y - leftLower.y)*(0.5f - verticalBufferPercentage));

		Vector3 newPosition = transform.position;
		if (target.position.x < leftBoundary || target.position.x > rightBoundary)
		{
			Vector3 targetPosition = new Vector3(target.position.x, newPosition.y, newPosition.z);
			newPosition = Vector3.MoveTowards(newPosition, targetPosition, speed * Time.deltaTime);
		}

		if (target.position.y < lowerBoundary || target.position.y > upperBoundary)
		{
			Vector3 targetPosition = new Vector3(newPosition.x, target.position.y, newPosition.z);
			newPosition = Vector3.MoveTowards(newPosition, targetPosition, speed * Time.deltaTime);
		}
		transform.position = newPosition;
    }
}
