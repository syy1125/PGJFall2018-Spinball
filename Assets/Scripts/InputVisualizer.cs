using UnityEngine;
using UnityEngine.UI;

public class InputVisualizer : MonoBehaviour
{
	public bool Raw;
	public float Scale;
	public string Horizontal;
	public string Vertical;

	public Text CoordinateDisplay;

	private void Update()
	{
		Vector2 input = (Raw
			? new Vector2(Input.GetAxisRaw(Horizontal), Input.GetAxisRaw(Vertical))
			: new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical)));
		
		transform.localPosition = Scale * input;

		CoordinateDisplay.text = input.ToString();
	}
}