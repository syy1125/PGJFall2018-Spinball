﻿using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
	public GameObject sparkPrefab;

	private float globalCollisionMultiplier = 1.5f;

	private Rigidbody2D _rigidbody2D;
	private ParticleSystem _particleSystem;
	private Coroutine _slowMotionCoroutine;

	private void Start()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
		//_particleSystem = GetComponent<ParticleSystem>();
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		GameObject other = col.gameObject;
		Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

		if (rb != null && other.CompareTag("Player"))
		{
			QuantumGyroBlade selfQGB = GetComponent<MovementController>().QGB;
			QuantumGyroBlade opponentQGB = other.GetComponent<MovementController>().QGB;
			Feedback(other, _rigidbody2D.velocity.magnitude + rb.velocity.magnitude);
			float a1 = Vector2.Angle(rb.velocity, transform.position - other.transform.position);
			float a2 = Vector2.Angle(_rigidbody2D.velocity, transform.position - other.transform.position);
			if (a1 > a2)
			{
				DoCollision(
					_rigidbody2D, rb, selfQGB.Power, selfQGB.Resistance, opponentQGB.Power, opponentQGB.Resistance
				);
			}
			else
			{
				DoCollision(
					rb, _rigidbody2D, opponentQGB.Power, opponentQGB.Resistance, selfQGB.Power, selfQGB.Resistance
				);
			}
		}
	}

	private void OnTriggerStay2D(Collider2D col)
	{
		GameObject other = col.gameObject;
		Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

		if (rb != null && other.CompareTag("Player"))
		{
			Instantiate(sparkPrefab, (other.transform.position + transform.position) / 2, Quaternion.identity);
			Vector2 dir = gameObject.transform.position - other.transform.position;
			rb.AddForce(-dir * 20);
			_rigidbody2D.AddForce(dir * 20);
		}
	}

	private void DoCollision(Rigidbody2D instigator, Rigidbody2D receiver, float instPow, float instRes, float recPow,
		float recRes)
	{
		Vector2 dir = instigator.transform.position - receiver.transform.position;
		dir.Normalize();
		float recMag = Vector3.Project(instigator.velocity, dir).magnitude * (instPow / recRes);
		float instMag = Vector3.Project(receiver.velocity, dir).magnitude * (recPow / instRes);
		instigator.velocity = dir * globalCollisionMultiplier * instMag;
		receiver.velocity = -dir * globalCollisionMultiplier * recMag;
	}

	private void Feedback(GameObject other, float totalMagnitude)
	{
		AudioManager.instance.PlayClangSound();
		Camera.main.GetComponent<CameraController>().ScreenShake(totalMagnitude);
		Instantiate(
			sparkPrefab, (other.transform.position + transform.position) / 2, Quaternion.identity
		);

		if (_slowMotionCoroutine != null)
		{
			StopCoroutine(_slowMotionCoroutine);
		}

		_slowMotionCoroutine = StartCoroutine(SlowMotion(totalMagnitude));
	}

	private IEnumerator SlowMotion(float magnitude)
	{
		Time.timeScale = Mathf.Clamp01(3 / magnitude);
		yield return new WaitForSecondsRealtime(Mathf.Log10(magnitude + 1) / 5);
		Time.timeScale = 1f;
		_slowMotionCoroutine = null;
	}
}