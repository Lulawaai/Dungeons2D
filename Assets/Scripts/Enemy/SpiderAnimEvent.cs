using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimEvent : MonoBehaviour
{
	[SerializeField] private GameObject _acidPrefab;
	private GameObject _acid;

	public void Fire()
	{
		_acid = Instantiate(_acidPrefab, transform.position, Quaternion.identity);
	}
}
