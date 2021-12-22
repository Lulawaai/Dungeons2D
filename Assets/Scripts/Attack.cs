using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
	[SerializeField] private bool _canDamage = true;

	private WaitForSeconds _waitHalfSecond = new WaitForSeconds(0.5f);

	private void OnTriggerEnter2D(Collider2D other)
	{
		IDamageable hit = other.GetComponent<IDamageable>();

		//This one calls the IDamageble
		if (hit != null && _canDamage)
		{
			_canDamage = false;
			hit.Damage();
			StartCoroutine(CanAttackRoutine());
		}
	}

	private IEnumerator CanAttackRoutine()
	{
		yield return _waitHalfSecond;
		_canDamage = true;
	}
}