using UnityEngine;
using UnityEngine.Serialization;

public class MovementController : MonoBehaviour
{
	[FormerlySerializedAs("InputStrength")]
	public float BaseInputStrength;

	public string HorizontalAxisName;
	public string VerticalAxisName;
	public QuantumGyroBlade QGB;

	private Rigidbody2D _rigidbody2D;

	private void Start()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		float horiz = Input.GetAxisRaw(HorizontalAxisName);
		float vert = Input.GetAxisRaw(VerticalAxisName);

		// We multiply by Resistance here to counteract the effect of mass multiplier
		Vector2 movementForce = new Vector2(horiz, vert).normalized
		                        * BaseInputStrength
		                        * QGB.Acceleration;
		float degrees = Vector2.Angle(_rigidbody2D.velocity, movementForce);
		if (150 > degrees)
		{
			_rigidbody2D.AddForce(movementForce);
		}
		else
		{
			_rigidbody2D.AddForce(movementForce * 0.6f);
		}
	}
}