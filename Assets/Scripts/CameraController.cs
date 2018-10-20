using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
	public float cameraDamping;
	public GameObject toFollow;

	private Vector3 offset;

	void Start () 
	{
		offset = this.transform.position - toFollow.transform.position;
	}
	
	void LateUpdate () 
	{
		//this.transform.position = toFollow.transform.position + offset;
		this.transform.position = Vector3.Lerp(this.transform.position, toFollow.transform.position + offset, cameraDamping);
	}
}
