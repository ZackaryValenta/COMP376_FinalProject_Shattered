using UnityEngine;
using System.Collections;

public class EnemySparrow : MonoBehaviour
{
	[SerializeField]
	float speed;
	[SerializeField]
	float maxLeftDistance;
	[SerializeField]
	float maxRightDistance;

	private Vector2 facingDirection;
	private float leftXPosition;
	private float rightXPosition;
	private bool hasBeenDetectedOutside;

	// Use this for initialization
	void Start ()
	{
		facingDirection = Vector2.right;
		leftXPosition   = transform.position.x - maxLeftDistance;
		rightXPosition  = transform.position.x + maxRightDistance;
		hasBeenDetectedOutside = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if ((transform.position.x < leftXPosition || Mathf.Approximately(transform.position.x, leftXPosition) ||
		     transform.position.x > rightXPosition || Mathf.Approximately(transform.position.x, rightXPosition)) &&
		    !hasBeenDetectedOutside)
		{
			FaceDirection (-facingDirection);
			hasBeenDetectedOutside = true;
		}
		if (hasBeenDetectedOutside && (transform.position.x > leftXPosition && transform.position.x < rightXPosition))
		{
			hasBeenDetectedOutside = false;
		}
		transform.position += new Vector3(facingDirection.x, facingDirection.y, 0.0f) * speed * Time.deltaTime;
	}

	private void FaceDirection(Vector2 direction)
	{
		facingDirection = direction;
		if(direction == Vector2.right)
		{
			Vector3 newScale = new Vector3(Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);
			transform.localScale = newScale;
		}
		else if(direction == -Vector2.right)
		{
			Vector3 newScale = new Vector3(-Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);
			transform.localScale = newScale;
		}
	}
	
	public Vector2 GetFacingDirection()
	{
		return facingDirection;
	}
}
