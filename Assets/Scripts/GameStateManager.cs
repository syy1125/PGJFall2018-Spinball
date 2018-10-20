using UnityEngine;

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
		}
	}
}