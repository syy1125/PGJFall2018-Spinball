using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour 
{
	public float cutoffSpeed;

	private ParticleSystem _particleSystem;
	private Rigidbody2D _rigidbody;

	void Start () 
	{
		_particleSystem = GetComponent<ParticleSystem>();
		_rigidbody = transform.parent.GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () 
	{
		if(_rigidbody.velocity.magnitude <= cutoffSpeed)
		{
			_particleSystem.Stop();
		}
		else
		{
			_particleSystem.Play();
		}
	}
}
