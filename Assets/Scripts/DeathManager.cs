using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeathManager : MonoBehaviour
{
	public float RespawnDelay;
	public Text DeathDisplay;
	public Vector3 respawnCenter;
	public float respawnDistance;
	public float killDistance;

	private int _deaths;

	private void Start()
	{
		_deaths = 0;
		UpdateDeathDisplay();
	}

	private void Update()
	{
		if((gameObject.transform.position - respawnCenter).magnitude >= killDistance)
		{
			Kill();
		}
//		if (Input.GetKeyDown(KeyCode.Space))
//		{
//			Kill();
//		}
	}

	private void UpdateDeathDisplay()
	{
		DeathDisplay.text = _deaths.ToString();
	}

	private IEnumerator RespawnSequence()
	{
		AudioManager.instance.PlayDeathSound();
		gameObject.SetActive(false);
		_deaths++;
		UpdateDeathDisplay();

		yield return new WaitForSeconds(RespawnDelay);

		int angle = Random.Range(0, 360);
		transform.position = new Vector3(Mathf.Cos(angle) * respawnDistance, Mathf.Sin(angle) * respawnDistance, 0);
		gameObject.SetActive(true);
	}

	public void Kill()
	{
		// Start coroutine on an active object - since RespawnSequence deactivates this object the rest of the coroutine would've halted.
		GameStateManager.Instance.StartCoroutine(RespawnSequence());
	}
}