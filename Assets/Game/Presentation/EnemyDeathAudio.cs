using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathAudio : MonoBehaviour
{
	public AudioSource AudioSource;
	public AudioClip[] AudioClipArray;
	AudioClip lastClip;
	public float Volume = 0.1f;
	
	public void PlayEnemyDeathScream()
	{
		AudioSource.PlayOneShot(RandomClip(), Volume);
	}
	
	AudioClip RandomClip()
	{
		int attempts = 3;
		AudioClip newClip = AudioClipArray[Random.Range(0, AudioClipArray.Length)];
		while (newClip == lastClip && attempts > 0)
		{
			newClip = AudioClipArray[Random.Range(0, AudioClipArray.Length)];
			attempts--;
		}
		lastClip = newClip;
		return newClip;
	}
}
