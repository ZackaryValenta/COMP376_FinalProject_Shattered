using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    Transform mTarget;
    [SerializeField]
    float mFollowSpeed;
    [SerializeField]
    float mFollowRange;

    float mArriveThreshold = 0.05f;

    void Update ()
    {
        if(mTarget != null)
        {
            // TODO: Make the enemy follow the target "mTarget"
			//       only if the target is close enough (distance smaller than "mFollowRange")
			Vector3 MegaManPosition     = mTarget.GetComponent<MegaMan>().transform.position;
			Vector3 jammingerPosition = gameObject.GetComponent<Jamminger>().transform.position;
			Vector3 jammingerToMegaMan  = MegaManPosition - jammingerPosition;
			float distance              = Vector3.Magnitude(jammingerToMegaMan);
			if (distance < mFollowRange)
			{
				float followStep = mFollowSpeed * Time.deltaTime;
				gameObject.GetComponent<Jamminger>().transform.position = Vector3.MoveTowards(transform.position, mTarget.position, followStep);
			}
        }
    }

    public void SetTarget(Transform target)
    {
        mTarget = target;
    }
}
