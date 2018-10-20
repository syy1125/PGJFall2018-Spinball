using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraController : MonoBehaviour 
{
	public GameObject playerOne;
	public GameObject playerTwo;
	public float cameraDamping;
	public Vector3 offset;
	public float minimumOrthographicSize;
	public float maximumOrthographicSize;

	//private

	void Start () 
	{
		this.transform.position = ((playerOne.transform.position + playerTwo.transform.position) / 2) + offset;
	}
	
	void LateUpdate () 
	{
		Vector3 midpoint = (playerOne.transform.position + playerTwo.transform.position) / 2;
		float distance = Mathf.Min(Mathf.Max(minimumOrthographicSize, (playerOne.transform.position - playerTwo.transform.position).magnitude), maximumOrthographicSize);
		this.transform.position = Vector3.Lerp(this.transform.position, midpoint + offset, cameraDamping);
		Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, distance, cameraDamping);
		//this.transform.position = toFollow.transform.position + offset;
		//this.transform.position = Vector3.Lerp(this.transform.position, toFollow.transform.position + offset, cameraDamping);
	}
}
