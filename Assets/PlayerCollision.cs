using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour 
{
	public float cooldownTime = 0.2f;

	private Dictionary<GameObject, float> cooldowns;
	private Rigidbody2D rigidbody;

	void Start () 
	{
		rigidbody = GetComponent<Rigidbody2D>();
		cooldowns = new Dictionary<GameObject, float>();
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{
		Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
		Debug.Log("OTHER VELOCITY: " + rb.velocity);
		Debug.Log("MY VELOCITY: " + rigidbody.velocity);
		if(rb != null && col.gameObject.CompareTag("Player"))
		{
			float val;
			if(!cooldowns.TryGetValue(col.gameObject, out val) || val + cooldownTime <= Time.time)
			{
				Debug.Log("val: " + val);
				Debug.Log("time: " + Time.time);
				cooldowns.Add(col.gameObject, Time.time);
				float temp;
				cooldowns.TryGetValue(col.gameObject, out temp);
				Debug.Log("temp: " + temp);
				Vector3 velocity = rb.velocity;
				rb.velocity = col.contacts[0].normal * rigidbody.velocity;
				rigidbody.velocity = velocity * -col.contacts[0].normal;
			}
		}
	}

}
