using UnityEngine;

//Script rotates upon the Game's origin coordinate

public class Rotato : MonoBehaviour
{
	public int countdown = 10; //Time in seconds before stage begins to rotate
	public float rampModifier = 1.0f; //speed increase modifier
	public float rotationalSpeed = 1.0f;
	public float maxSpeed = 60.0f;

	private float stopwatch;

	private void Start()
	{
		stopwatch = 0.0f;
	}

	// Update is called once per frame
	private void Update()
	{
		stopwatch += Time.deltaTime;

		if (stopwatch >= countdown)
		{
			transform.RotateAround(Vector3.zero, Vector3.forward, rotationalSpeed * Time.deltaTime);
			if (rotationalSpeed < maxSpeed)
				rotationalSpeed += Time.deltaTime * rampModifier;
		}
	}
}