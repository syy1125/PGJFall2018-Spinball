using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour 
{
	public static AudioManager instance;

	public AudioClip bomb;
	public AudioClip[] clangs;

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
		source.PlayOneShot(bomb, 1f);
	}

	public void PlayClangSound()
	{
		source.PlayOneShot(clangs[Random.Range(0, clangs.Length)]);
	}
}
