using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
	public float damping;
	public Vector3 offset;

	private void LateUpdate()
	{
		transform.position = Vector3.Lerp(transform.position, Camera.main.transform.position + offset, damping);
	}
}