using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
	{
        get
		{
            if (_instance == null)
			{
				Debug.LogError("AudioManager is NULL.");
			}
			return _instance;
		}
	}

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _backgroundMusic;

	private void Awake()
	{
		_instance = this;
	}

	private void OnEnable()
	{
		Shop.OnPurchaseMusic += PlayAudio;
	}

	private void PlayAudio()
	{
		_audioSource.clip = _backgroundMusic;
		_audioSource.Play();
	}

	public void PlayAudioEffect(AudioClip clip)
	{
		_audioSource.PlayOneShot(clip);
	}

	private void OnDisable()
	{
		Shop.OnPurchaseMusic -= PlayAudio;
	}
}
