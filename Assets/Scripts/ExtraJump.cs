using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExtraJump : MonoBehaviour
{
	[SerializeField] private float _highJumpForce;

	public static event Action<float> OnExtraJump;
	public static event Action OnFinishExtraJump;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			OnExtraJump?.Invoke(_highJumpForce);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			OnFinishExtraJump?.Invoke();
		}
	}
}
