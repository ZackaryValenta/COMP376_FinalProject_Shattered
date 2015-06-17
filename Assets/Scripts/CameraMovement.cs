using TreeEditor;
using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
	[SerializeField] private bool usePlayerSpeed;
	[SerializeField] private float cameraSpeed;						// speed at which to follow player, if not matching player speed
	[SerializeField] private float distantSpeedMultiplier;			// when camera is far from player, scale speed to return faster
	[SerializeField] private float multiplyDistance;				// distance from player above which camera should speed up
	[SerializeField] private float hortizontalBufferPercentage;		// 
	[SerializeField] private float verticalBufferPercentage;

	public Camera camera;

    void Start()
    {
		camera = gameObject.GetComponent<Camera> ();
    }

    void Update()
    {
		float speed = cameraSpeed;
		if (usePlayerSpeed)
		{
			speed = (target.gameObject.GetComponent<Player> ().isGrounded ()) ? target.gameObject.GetComponent<Player> ().getSpeed() :
																				target.gameObject.GetComponent<Player> ().getUngroundedSpeed ();
		}

		if (Mathf.Abs (Vector3.Magnitude (target.position - transform.position)) > multiplyDistance)
		{
			speed *= distantSpeedMultiplier;
		}

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
		/*
		if (target.position.x < leftBoundary || target.position.x > rightBoundary ||
		    target.position.y < lowerBoundary || target.position.y > upperBoundary)
		{
			Debug.Log ("target: " + target.position +
			           "old cam: " + transform.position +
			           "new cam: " + newPosition);
		}
		*/
		transform.position = newPosition;
    }
}
