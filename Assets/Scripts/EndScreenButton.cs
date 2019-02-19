using UnityEngine;

public class EndScreenButton : MonoBehaviour
{
	public void GoToQGBScreen()
	{
		GameStateManager.Instance.GoToQGBSelection();
	}
}