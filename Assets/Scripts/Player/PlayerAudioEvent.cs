using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioEvent : MonoBehaviour
{
	[SerializeField] private AudioSource _audioSource;
	[SerializeField] private AudioClip _playerJumpClip;
	[SerializeField] private AudioClip _playerSwordClip;

	public void PlayerJumpAudio()
	{
		_audioSource.clip = _playerJumpClip;
		_audioSource.Play();
	}

	public void PlayerSwordAudio()
	{
		_audioSource.clip = _playerSwordClip;
		_audioSource.Play();
	}
}
