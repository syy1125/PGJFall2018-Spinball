using System;
using UnityEngine;

public class MovementController : MonoBehaviour
{
	public float InputStrength;
	public string HorizontalAxisName;
	public string VerticalAxisName;
	private Rigidbody2D _rigidbody2D;

	private void Start()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		_rigidbody2D.AddForce(new Vector2(
			Input.GetAxis(HorizontalAxisName) * InputStrength,
			Input.GetAxis(VerticalAxisName) * InputStrength
		));
	}
}