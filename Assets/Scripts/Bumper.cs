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
	public float startingShadowScale;
	public float shadowScaleIncrement;

	private float currentBounceForce;
	private SpriteRenderer _sprite;
	private float lastChange;
	private float vValue;
	private float shadowScale;
	private GameObject shadow;

	void Start()
	{
		currentBounceForce = startingBounceForce;
		lastChange = 0;
		vValue = startingV;
		shadowScale = startingShadowScale;
		shadow = transform.GetChild(0).gameObject;
		_sprite = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		if(lastChange == 0 || lastChange + timeBeforeDecay > Time.time)
		{
			return;
		}
		currentBounceForce -= bounceIncrement;
		vValue -= vIncrement;
		_sprite.color = Color.HSVToRGB(0, 0, vValue);
		shadowScale -= shadowScaleIncrement;
			shadow.transform.localScale = new Vector3(shadowScale, shadowScale, 0);
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
			AudioManager.instance.PlayBingSound();
			rb.velocity = Vector2.zero;
			rb.AddForce(currentBounceForce * (col.gameObject.transform.position - this.transform.position) );
			currentBounceForce += bounceIncrement;
			lastChange = Time.time;
			//Debug.Log(lastChange);
			vValue += vIncrement;
			_sprite.color = Color.HSVToRGB(0, 0, vValue);
			shadowScale += shadowScaleIncrement;
			shadow.transform.localScale = new Vector3(shadowScale, shadowScale, 0);
		}
	}
}
