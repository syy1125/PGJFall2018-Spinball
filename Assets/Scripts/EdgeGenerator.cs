using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeGenerator : MonoBehaviour
{

	public GameObject bombPrefab;
	public float edgeDistance;
	public float numBombs;

	void Start () 
	{
		for(int x = 0; x < numBombs; ++x)
		{
			float angle = 2 * Mathf.PI * (x / numBombs);
			Debug.Log(angle);
			float xPos = Mathf.Cos(angle) * edgeDistance;
			float yPos = Mathf.Sin(angle) * edgeDistance;
			Instantiate(bombPrefab, new Vector2(xPos, yPos), Quaternion.identity, this.transform);
		}
	}
}
