using UnityEngine;

public class ExplosionAnimation : MonoBehaviour
{
	private void Start()
	{
		Destroy(gameObject, 0.5f);
	}
}