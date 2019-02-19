using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeathManager : MonoBehaviour
{
	public static Sprite p1Sprite;
	public static Sprite p2Sprite;

	public float RespawnDelay = 2;
	public Vector3 respawnCenter;
	public float respawnDistance;
	public float killDistance;

	public GameObject[] pips;
	public Sprite emptyPips;
	public Sprite fullPips;
	public GameObject playerDeathPrefab;

	private int _deaths;

	private void Start()
	{
		_deaths = 0;
		UpdateDeathDisplay();
	}

	private void Update()
	{
		if ((gameObject.transform.position - respawnCenter).magnitude >= killDistance)
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
		//DeathDisplay.text = _deaths.ToString();
		for (int x = 0; x < _deaths && x < pips.Length; ++x)
		{
			pips[x].GetComponent<Image>().sprite = fullPips;
		}
	}

	private IEnumerator RespawnSequence()
	{
		AudioManager.instance.PlayDeathSound();
		Instantiate(playerDeathPrefab, gameObject.transform.position, Quaternion.identity);
		gameObject.SetActive(false);
		_deaths++;
		UpdateDeathDisplay();
		if (_deaths == pips.Length)
		{
			if (gameObject.name.Equals("PlayerOne"))
			{
				GameStateManager.Instance.EndGame(p2Sprite);
			}
			else
			{
				GameStateManager.Instance.EndGame(p1Sprite);
			}
		}
		else
		{
			yield return new WaitForSeconds(RespawnDelay);

			int angle = Random.Range(0, 360);
			transform.position = new Vector3(Mathf.Cos(angle) * respawnDistance, Mathf.Sin(angle) * respawnDistance, 0);
			gameObject.SetActive(true);
		}
	}

	public void Kill()
	{
		// Start coroutine on an active object - since RespawnSequence deactivates this object the rest of the coroutine would've halted.
		GameStateManager.Instance.StartCoroutine(RespawnSequence());
	}

	public int getDeaths()
	{
		return _deaths;
	}
}