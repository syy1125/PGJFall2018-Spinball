using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPulse : MonoBehaviour
{
	public float Period;
	public int Count;

	private Color _baseColor;

	private IEnumerator Start()
	{
		Text t = GetComponent<Text>();
		_baseColor = t.color;
		float startTime = Time.time;

		while (Time.time - startTime < Period * Count)
		{
			float stage = (Time.time - startTime) * 2 / Period % 2;
			t.color = new Color(_baseColor.r, _baseColor.g, _baseColor.b, stage > 1 ? 2 - stage : stage);
			yield return null;
		}
		
		Destroy(gameObject);
	}
}