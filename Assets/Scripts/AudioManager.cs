using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour 
{
	public static AudioManager instance;

	public AudioClip[] bombs;
	public AudioClip[] clangs;
	public AudioClip[] bumperBings;
	public AudioClip deathSound;

	private AudioSource source;

	void Start () 
	{
		if(instance != null)
		{
			Destroy(gameObject);
		}
		instance = this;
		DontDestroyOnLoad(gameObject);

		source = GetComponent<AudioSource>();
	}

	public void PlayBombSound()
	{
		source.PlayOneShot(bombs[Random.Range(0, bombs.Length)], 1f);
	}

	public void PlayClangSound()
	{
		StartCoroutine(PlayOneShotClang());
	}

	private IEnumerator PlayOneShotClang()
	{
		int numClangs = Random.Range(2, 4);
		for(int x = 0; x < numClangs; ++x)
		{
			source.PlayOneShot(clangs[Random.Range(0, clangs.Length)]);
			for(int y = 0; y < 5; ++y)
			{
				yield return null;
			}
		}
	}

	public void PlayBingSound(int index)
	{
		//Debug.Log("1:" + index);
		index = Mathf.Min(bumperBings.Length - 1, index);
		//Debug.Log("2:" + index);
		source.PlayOneShot(bumperBings[bumperBings.Length - 1 - index]);
	}

	public void PlayDeathSound()
	{
		source.PlayOneShot(deathSound);
	}
}
