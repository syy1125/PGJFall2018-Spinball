using System.Collections;
using UnityEngine;
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

	public void GoToMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void GoToQGBSelection()
	{
		SceneManager.LoadScene("QGBSelection");
	}

	private IEnumerator LoadCombatSceneSequence(QuantumGyroBlade p1QGB, QuantumGyroBlade p2QGB)
	{
		AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync("Combat");

		while (!loadSceneOperation.isDone) yield return null;

		GameObject p1 = GameObject.Find("PlayerOne");
		p1.GetComponent<MovementController>().QGB = p1QGB;
		p1.GetComponent<PlayerRendererController>().SetRendererPrefab(p1QGB.P1RendererPrefab);
		GameObject p2 = GameObject.Find("PlayerTwo");
		p2.GetComponent<MovementController>().QGB = p2QGB;
		p2.GetComponent<PlayerRendererController>().SetRendererPrefab(p2QGB.P2RendererPrefab);
	}

	public void GoToCombat(QuantumGyroBlade p1QGB, QuantumGyroBlade p2QGB)
	{
		StartCoroutine(LoadCombatSceneSequence(p1QGB, p2QGB));
	}
}