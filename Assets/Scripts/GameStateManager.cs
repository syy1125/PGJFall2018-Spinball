﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
	public static GameStateManager Instance;

	public bool ChonnoleUnlocked = false;

	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	public void GoToCharacterSelection()
	{
		SceneManager.LoadScene("QGBSelection");
	}
}