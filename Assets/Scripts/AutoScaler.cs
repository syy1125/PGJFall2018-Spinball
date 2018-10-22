using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScaler : MonoBehaviour
{
	public int[] Breakpoints;
	public float[] Scales;

	private int _cachedWidth;

	private void Start()
	{
		_cachedWidth = Screen.width;

		UpdateScale();
	}

	private void Update()
	{
		if (Screen.width != _cachedWidth)
		{
			_cachedWidth = Screen.width;

			UpdateScale();
		}
	}

	private void UpdateScale()
	{
		int index = 0;
		while (index < Breakpoints.Length && Breakpoints[index] < _cachedWidth) index++;
		transform.localScale = Vector3.one * Scales[Mathf.Clamp(index, 0, Scales.Length - 1)];
	}
}