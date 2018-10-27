using UnityEngine;

public class InputVisualizer : MonoBehaviour
{
	public bool Raw;
	public float Scale;
	public string Horizontal;
	public string Vertical;

	private void Update()
	{
		transform.localPosition = Scale * (Raw
			                     ? new Vector3(Input.GetAxisRaw(Horizontal), Input.GetAxisRaw(Vertical))
			                     : new Vector3(Input.GetAxis(Horizontal), Input.GetAxis(Vertical)));
	}
}