using UnityEngine;

public class CreditsButton : MonoBehaviour
{
	public GameObject credits;

	public void ShowHideCredits()
	{
		if (credits.activeSelf == false)
		{
			Debug.Log("Showing credits.");
			credits.SetActive(true);
		}
		else
		{
			Debug.Log("Hiding credits.");
			credits.SetActive(false);
		}
	}
}