using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetHitAudio : MonoBehaviour
{
	public AudioSource AudioSource;
	public AudioClip[] AudioClipArray;
	AudioClip lastClip;
	public float MinVolume = 0.05f;
	public float MaxVolume = 0.5f;

	public void PlayEnemyGetHitSound()
	{
		AudioSource.PlayOneShot(RandomClip(), Random.Range(MinVolume, MaxVolume));
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
