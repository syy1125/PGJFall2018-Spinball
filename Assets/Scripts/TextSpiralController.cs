﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextSpiralController : MonoBehaviour
{
	private const float RAD = 180 / Mathf.PI;

	public TextAsset ContentFile;
	public GameObject CharacterPrefab;

	public string ViewedOpeningPrefKey = "ViewedOpening";
	public bool SkipInEditor;

	public float InitialRadius = 100;
	public float TextScale = 0.2f;
	public float ScaleMultiplier = 1;
	public float AngleDelta;

	public float AngularSpeed = 0.1f;
	private float _scaleSpeed;
	private Camera _mainCamera;

	private void Start()
	{
		CheckForSkip();
		
		string content = ContentFile.text;

		float scale = 1;
		float angle = Mathf.PI / 2;

		for (int charIndex = 0; charIndex < content.Length; charIndex++)
		{
			GameObject characterObject = Instantiate(
				CharacterPrefab,
				Vector3.zero,
				Quaternion.AngleAxis(RAD * (angle - Mathf.PI / 2), Vector3.forward),
				transform
			);

			characterObject.transform.localPosition = new Vector3(
				InitialRadius * scale * Mathf.Cos(angle),
				InitialRadius * scale * Mathf.Sin(angle),
				0
			);
			characterObject.transform.localScale = new Vector3(scale * TextScale, scale * TextScale, scale * TextScale);
			characterObject.GetComponent<Text>().text = content[charIndex].ToString();

			scale *= ScaleMultiplier;
			angle -= AngleDelta;
		}

		_mainCamera = Camera.main;

		// MATH
		_scaleSpeed = Mathf.Pow(1 / ScaleMultiplier, AngularSpeed / AngleDelta);
	}

	private void CheckForSkip()
	{
		if (PlayerPrefs.HasKey(ViewedOpeningPrefKey))
		{
			if (Application.isEditor && !SkipInEditor)
			{
				return;
			}

			GameStateManager.Instance.GoToMainMenu();
		}
		else
		{
			PlayerPrefs.SetInt(ViewedOpeningPrefKey, 1);
		}
	}

	private void Update()
	{
		_mainCamera.orthographicSize /= Mathf.Pow(_scaleSpeed, Time.deltaTime);
		_mainCamera.transform.Rotate(Vector3.back * AngularSpeed * Time.deltaTime * RAD);

		if (Input.anyKeyDown)
		{
			if (!Input.GetKeyDown(KeyCode.Mouse0) && !Input.GetKeyDown(KeyCode.Mouse1))
			{
				GameStateManager.Instance.GoToMainMenu();
			}
		}
	}
}