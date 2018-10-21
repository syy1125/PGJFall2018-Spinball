using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBounceable { }

public class Bumper : MonoBehaviour 
{
	public float startingBounceForce;
	public float bounceIncrement;
	public float timeBeforeDecay;
	public float startingV;
	public float vIncrement;

	private float currentBounceForce;
	private SpriteRenderer _sprite;
	private float lastChange;
	private float vValue;

	void Start()
	{
		currentBounceForce = startingBounceForce;
		lastChange = 0;
		vValue = startingV;
		_sprite = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		if(lastChange == 0 || lastChange + timeBeforeDecay > Time.time)
		{
			return;
		}
		currentBounceForce -= bounceIncrement;
		_sprite.color = Color.HSVToRGB(0, 0, vValue - vIncrement);
		vValue -= vIncrement;
		lastChange = Time.time;
		if(vValue == startingV)
		{
			lastChange = 0;
			currentBounceForce = startingBounceForce;
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
		if(rb != null)
		{
			rb.velocity = Vector2.zero;
			rb.AddForce(currentBounceForce * (col.gameObject.transform.position - this.transform.position) );
			currentBounceForce += bounceIncrement;
			lastChange = Time.time;
			Debug.Log(lastChange);
			_sprite.color = Color.HSVToRGB(0, 0, vValue + vIncrement);
			vValue += vIncrement;
		}
	}
}
