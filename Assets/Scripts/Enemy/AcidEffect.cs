using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEffect : MonoBehaviour
{
	[SerializeField] private float _speed;
	[SerializeField] private float _acidTimeLife;

	private void Start()
	{
		Destroy(gameObject, _acidTimeLife);
	}

	private void Update()
	{
		Movement();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		IDamageable hit = other.GetComponent<IDamageable>();

		if (other.CompareTag("Player"))
		{
			//This one calls the IDamageble
			if (hit != null)
			{
				hit.Damage();
				Destroy(gameObject);
			}
		}
	}

	private void Movement()
	{
		transform.Translate(Vector2.left * _speed * Time.deltaTime);
	}
}
