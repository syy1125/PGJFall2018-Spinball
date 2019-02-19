using UnityEngine;

public class PlayerSelectionConfig : MonoBehaviour
{
	public static PlayerSelectionConfig Instance { get; private set; }

	public GameObject FallbackPrefab;
	public float RenderScale = 20;

	public float[] IntervalCurve;

	public float MaxSpeed = 1;
	public float BuildupTime = 1;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}
}