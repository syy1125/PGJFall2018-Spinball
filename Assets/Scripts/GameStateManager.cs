using System.Collections;
using UnityEngine;
using UnityEngine.UI;
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

	private IEnumerator LoadOmegaSceneSequence(QuantumGyroBlade p1QGB, QuantumGyroBlade p2QGB)
	{
		AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync("Omega");

		while (!loadSceneOperation.isDone) yield return null;

		QGBUpdate(p1QGB);
		QGBUpdate(p2QGB);
		GameObject p1 = GameObject.Find("PlayerOne");
		p1.GetComponent<MovementController>().QGB = p1QGB;
		p1.GetComponent<PlayerRendererController>().SetRendererPrefab(p1QGB.P1RendererPrefab);
		GameObject p2 = GameObject.Find("PlayerTwo");
		p2.GetComponent<MovementController>().QGB = p2QGB;
		p2.GetComponent<PlayerRendererController>().SetRendererPrefab(p2QGB.P2RendererPrefab);
	}

	private void QGBUpdate(QuantumGyroBlade QGB)
	{
		QGB.Acceleration = CurveTable.Acceleration[(int)QGB.Acceleration];
		QGB.Power = CurveTable.Power[(int)QGB.Power];
		QGB.Resistance = CurveTable.Resistance[(int)QGB.Resistance];	
	}

	public void GoToOmega(QuantumGyroBlade p1QGB, QuantumGyroBlade p2QGB)
	{
		StartCoroutine(LoadOmegaSceneSequence(p1QGB, p2QGB));
	}

	public void EndGame(Sprite winner)
	{
		StopAllCoroutines();
		StartCoroutine((LoadEndScreen(winner)));
	}

	private IEnumerator LoadEndScreen(Sprite winner)
	{
		AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync("EndScreen");

		while (!loadSceneOperation.isDone) yield return null;

		GameObject info = GameObject.Find("Winner");
		info.GetComponent<Image>().sprite = winner;
	}
}