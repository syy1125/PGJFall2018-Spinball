using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
	public void PlayNextScene()
	{
		SceneManager.LoadScene(
			SceneManager.GetActiveScene().buildIndex + 1
		); //loads next scene in the build index on button press
		Debug.Log("Button working.");
	}
}