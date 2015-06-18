using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
	[SerializeField] private bool usePlayerSpeed;
	[SerializeField] private float cameraSpeed;						// speed at which to follow player, if not matching player speed
	[SerializeField] private float distantSpeedMultiplier;			// when camera is far from player, scale speed to return faster
	[SerializeField] private float multiplyDistance;				// distance from player above which camera should speed up
	[SerializeField] private float aheadPlayerBufferPercentage;
	[SerializeField] private float behindPlayerBufferPercentage;
	[SerializeField] private float topBufferPercentage;
	[SerializeField] private float bottomBufferPercentage;

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

		Vector3 leftLower           = camera.ScreenToWorldPoint (new Vector3 (0, 0, -transform.position.z));
		Vector3 rightUpper          = camera.ScreenToWorldPoint (new Vector3 (camera.pixelWidth, camera.pixelHeight, (target.position - transform.position).z));
		Player targetPlayer         = target.gameObject.GetComponent<Player> ();
		float leftBufferPercentage  = aheadPlayerBufferPercentage;
		float rightBufferPercentage = behindPlayerBufferPercentage;
		if (targetPlayer && targetPlayer.GetFacingDirection ().Equals(Vector2.right))
		{
			leftBufferPercentage  = behindPlayerBufferPercentage;
			rightBufferPercentage = aheadPlayerBufferPercentage;
		}
		float leftBoundary  = transform.position.x - (Mathf.Abs(leftLower.x - rightUpper.x)*(0.5f - leftBufferPercentage));
		float rightBoundary = transform.position.x + (Mathf.Abs(leftLower.x - rightUpper.x)*(0.5f - rightBufferPercentage));
		float lowerBoundary = transform.position.y - (Mathf.Abs(rightUpper.y - leftLower.y)*(0.5f - bottomBufferPercentage));
		float upperBoundary = transform.position.y + (Mathf.Abs(rightUpper.y - leftLower.y)*(0.5f - topBufferPercentage));


		Vector3 newPosition = transform.position;
		if (target.position.x < leftBoundary || target.position.x > rightBoundary)
		{
			Vector3 adjustDirection = Vector3.Normalize(new Vector3 (((target.position.x < leftBoundary) ? -1.0f : 1.0f), 0.0f, 0.0f));
			newPosition += adjustDirection * speed * Time.deltaTime;
		}

		if (target.position.y < lowerBoundary || target.position.y > upperBoundary)
		{
			Vector3 adjustDirection = Vector3.Normalize(new Vector3 (0.0f, ((target.position.y < lowerBoundary) ? -1.0f : 1.0f), 0.0f));
			newPosition += adjustDirection * speed * Time.deltaTime;
		}
		transform.position = newPosition;
    }
}
