using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
	public static GameStateManager Instance;

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

	public void GoToCombat(QuantumGyroBlade p1QGB, QuantumGyroBlade p2QGB)
	{
		StartCoroutine(LoadSceneSequence(p1QGB, p2QGB, "Combat"));
	}

	public void GoToOmega(QuantumGyroBlade p1QGB, QuantumGyroBlade p2QGB)
	{
		StartCoroutine(LoadSceneSequence(p1QGB, p2QGB, "Omega"));
	}

	private IEnumerator LoadSceneSequence(QuantumGyroBlade p1QGB, QuantumGyroBlade p2QGB, string scene)
	{
		AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(scene);

		while (!loadSceneOperation.isDone) yield return null;

		GameObject p1 = GameObject.Find("PlayerOne");
		QGBUpdate(p1.GetComponent<MovementController>(), p1QGB);
		p1.GetComponent<PlayerRendererController>().SetRendererPrefab(p1QGB.LoadRenderer("P1"));
		
		GameObject p2 = GameObject.Find("PlayerTwo");
		QGBUpdate(p2.GetComponent<MovementController>(), p2QGB);
		p2.GetComponent<PlayerRendererController>().SetRendererPrefab(p2QGB.LoadRenderer("P2"));
	}	

	private void QGBUpdate(MovementController movementController, QuantumGyroBlade QGB)
	{
		Debug.Log("ind " + QGB.Acceleration + " " + QGB.Power + " " + QGB.Resistance);
		movementController.QGB.Acceleration = CurveTable.Acceleration[(int)(QGB.Acceleration - 1)];
		movementController.QGB.Power = CurveTable.Power[(int)(QGB.Power - 1)];
		movementController.QGB.Resistance = CurveTable.Resistance[(int)(QGB.Resistance - 1)];
	}

	public void EndGame(Sprite winner)
	{
		StopAllCoroutines();
		StartCoroutine((LoadEndScreen(winner)));
	}

	private IEnumerator LoadEndScreen(Sprite winner)
	{
		Time.timeScale = 0.5f;
		yield return new WaitForSeconds(1);
	
		AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync("EndScreen");

		while (!loadSceneOperation.isDone) yield return null;
		Time.timeScale = 1f;
		
		GameObject info = GameObject.Find("Winner");
		info.GetComponent<Image>().sprite = winner;
	}
}