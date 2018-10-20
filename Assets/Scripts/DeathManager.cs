using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeathManager : MonoBehaviour
{
	public float RespawnDelay;
	public Text DeathDisplay;
	private int _deaths;

	private void Start()
	{
		_deaths = 0;
		UpdateDeathDisplay();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Kill();
		}
	}

	private void UpdateDeathDisplay()
	{
		DeathDisplay.text = _deaths.ToString();
	}

	private IEnumerator RespawnSequence()
	{
		gameObject.SetActive(false);
		_deaths++;
		UpdateDeathDisplay();

		yield return new WaitForSeconds(RespawnDelay);

		transform.position = new Vector3(1, 1, 0);
		gameObject.SetActive(true);
	}

	public void Kill()
	{
		// Start coroutine on an active object - since RespawnSequence deactivates this object the rest of the coroutine would've halted.
		GameStateManager.Instance.StartCoroutine(RespawnSequence());
	}
}