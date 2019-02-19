using UnityEngine;

public class QuitButton : MonoBehaviour
{
	public void QuitGame()
	{
		Debug.Log("Game has been quit.");
		Application.Quit();
	}
}