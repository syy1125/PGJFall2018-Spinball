using UnityEngine;

public class ImageSpinner : MonoBehaviour
{
	public float rotationalSpeed = 1.0f;

	// Update is called once per frame
	private void Update()
	{
		transform.Rotate(0, 0, Time.deltaTime * rotationalSpeed, Space.World);
	}
}