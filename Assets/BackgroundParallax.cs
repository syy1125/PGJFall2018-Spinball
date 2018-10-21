using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour 
{
	public float damping;
	public Vector3 offset;

	void LateUpdate () 
	{
		this.transform.position = Vector3.Lerp(this.transform.position, Camera.main.transform.position + offset, damping);
	}
}
