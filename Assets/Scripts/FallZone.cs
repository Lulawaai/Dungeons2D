using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FallZone : MonoBehaviour
{
	public static event Action OnFallingZone;
	private WaitForSeconds _waitForOneSec = new WaitForSeconds(1f);

	[SerializeField] private bool _canPass;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			StartCoroutine(RespawnRoutine());
		}
	}

	private IEnumerator RespawnRoutine()
	{
		yield return _waitForOneSec;
		OnFallingZone?.Invoke();
	}
}
