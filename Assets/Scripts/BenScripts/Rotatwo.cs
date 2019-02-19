using UnityEngine;

public class Rotatwo : MonoBehaviour
{
	private static StageRotator stage;
	private float rotationalSpeed;

	private void Awake()
	{
		stage = StageRotator.Instance;
		if (stage == null)
			Destroy(gameObject);
	}

	// Update is called once per frame
	private void Update()
	{
		rotationalSpeed = stage.rotationalSpeed;
		transform.RotateAround(Vector3.zero, Vector3.forward, rotationalSpeed * Time.deltaTime);
	}
}