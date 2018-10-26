using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class CameraController : MonoBehaviour 
{
	public List<GameObject> players;
	public float cameraDamping;
	public Vector3 offset;
	public float minimumOrthographicSize;
	public float maximumOrthographicSize;

	private float currentDegreeOffset;
	
	void Start()
	{
		players = new List<GameObject>();
	}

	void LateUpdate ()
	{
		if(players.Count == 0)
		{
			return;
		}
		Vector3 midpoint = Vector3.zero;
		for(int x = 0; x < players.Count; ++x)
		{
			midpoint += players[x].transform.position;
		}
		midpoint /= players.Count;
		float distance = Mathf.Min(Mathf.Max(minimumOrthographicSize, ((midpoint - players[0].transform.position) * 2).magnitude), maximumOrthographicSize);
		this.transform.position = Vector3.Lerp(this.transform.position, midpoint + offset, cameraDamping);
		Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, distance, cameraDamping);
	}

	public void ScreenShake(float totalMagnitude)
	{
		StopAllCoroutines();
		StartCoroutine(DoShake(totalMagnitude));
	}

	private IEnumerator DoShake(float totalMagnitude)
	{
		int numFrames = (int)(totalMagnitude / 2f);
		float amountToShake = totalMagnitude / 50f;
		//Debug.Log("F:" + numFrames);
		//Debug.Log("A:" + amountToShake);
		for(int x = 0; x < numFrames; ++x)
		{
			transform.Translate(Random.Range(-amountToShake, amountToShake), Random.Range(-amountToShake, amountToShake), 0);
			yield return null;
		}	
	}
}
