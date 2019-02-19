using UnityEngine;

public class RandoBombs : MonoBehaviour
{
	public GameObject[] itemPrefabs;
	public float[] itemProbabilites;

	public int countdown; //time before bomb dropping begin
	public float cooldown; //time between bomb drops

	public float radiusLimit;
	public float radiusMinimum;

	private float stopwatch;
	private float bombStopwatch;


	// Use this for initialization
	private void Start()
	{
		stopwatch = 0.0f;
		bombStopwatch = 0.0f;
	}

	// Update is called once per frame
	private void Update()
	{
		stopwatch += Time.deltaTime;
		bombStopwatch += Time.deltaTime;

		if (stopwatch >= countdown)
		{
			if (bombStopwatch > cooldown)
			{
				bombStopwatch = 0;

				Vector2 placement = Random.insideUnitCircle * radiusLimit;

				while (Mathf.Abs(placement.x) < radiusMinimum && Mathf.Abs(placement.y) < radiusMinimum)
					placement = Random.insideUnitCircle * radiusLimit;

				GameObject bomb = Instantiate(itemPrefabs[getItemIndex()], placement, Quaternion.identity);
			}
		}
	}

	private int getItemIndex()
	{
		float sum = 0;
		for (int x = 0; x < itemProbabilites.Length; ++x)
		{
			sum += itemProbabilites[x];
		}

		float rand = Random.Range(0, sum);
		float temp = 0;
		for (int x = 0; x < itemProbabilites.Length; ++x)
		{
			temp += itemProbabilites[x];
			if (rand <= temp)
			{
				return x;
			}
		}

		return 0;
	}
}