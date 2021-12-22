using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Diamond : MonoBehaviour
{
	public int gems = 1;

	[SerializeField] private AudioClip _gemCollecionClip;

	public static event Action<int> OnGemCollection;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			OnGemCollection?.Invoke(gems);
			AudioManager.Instance.PlayAudioEffect(_gemCollecionClip);
			Destroy(gameObject);
		}
	}
}
