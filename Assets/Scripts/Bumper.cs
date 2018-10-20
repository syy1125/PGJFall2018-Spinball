using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBounceable { }

public class Bumper : MonoBehaviour 
{
	public float bounceForce;

	void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		Debug.Log("bounce col");
		Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
		if(rb != null)
		{
			rb.AddForce(-bounceForce * col.contacts[0].normal);
			//rb.AddForce( (this.transform.position - rb.gameObject.transform.position) * bounceForce );
		}
	}
}
