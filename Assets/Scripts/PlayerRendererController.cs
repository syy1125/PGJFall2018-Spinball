﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRendererController : MonoBehaviour
{
	public float MaxSpinsPerSecond = 4;

	public GameObject RendererPrefab;
	private Rigidbody2D _rigidbody2D;
	private GameObject _renderer;

	private void Start()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();

		foreach (Transform child in transform)
		{
			if (!child.CompareTag("PlayerRenderer")) continue;

			_renderer = child.gameObject;
			return;
		}

		_renderer = Instantiate(
			RendererPrefab,
			transform
		);
	}

	public void SetRendererPrefab(GameObject prefab)
	{
		RendererPrefab = prefab;
		Destroy(_renderer);
		_renderer = Instantiate(
			prefab,
			transform
		);
		if(this.gameObject.name.Equals("PlayerOne"))
		{
			DeathManager.p1Sprite = _renderer.GetComponent<SpriteRenderer>().sprite;
		}
		else
		{
			DeathManager.p2Sprite = _renderer.GetComponent<SpriteRenderer>().sprite;
		}
	}

	private void Update()
	{
		_renderer.transform.Rotate(
			0,
			0,
			Mathf.Min(_rigidbody2D.velocity.magnitude * 0.5f, MaxSpinsPerSecond) * Time.deltaTime * 360
		);
	}
}