using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class End : MonoBehaviour
{
	public static event Action OnGameOver;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player") && GameManager.Instance.HasKeyToCastel)
		{
			UIManager.Instance.WinningGame();
			GameManager.Instance.GameOver = true;

			OnGameOver?.Invoke();
		}
	}
}
