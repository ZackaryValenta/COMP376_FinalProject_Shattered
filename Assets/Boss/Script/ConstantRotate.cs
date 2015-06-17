using UnityEngine;
using System.Collections;

public class ConstantRotate : MonoBehaviour
{

    [SerializeField] private float speed = 5;
	
	void Update () {
        transform.Rotate(0.0f,0.0f,speed*Time.deltaTime);
	}
}
