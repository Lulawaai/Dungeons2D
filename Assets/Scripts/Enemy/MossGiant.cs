using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamageable
{
	private WaitForSeconds _waitForOneSevenSec = new WaitForSeconds(1.7f);
	private WaitForSeconds _waitForZeroTwoSec = new WaitForSeconds(0.2f);

	public int Health { get; set; }

	public override void Init()
	{
		base.Init();
		Health = health;
	}

	public override void Movement()
	{
		if (isAlive == true)
		{
			base.Movement();
		}
	}

	public void Damage()
	{
		if (isAlive == true)
		{
			Health--;
			anim.SetTrigger("Hit");
			isHit = true;
			anim.SetBool("InCombat", true);

			if (Health < 1)
			{
				anim.SetTrigger("Death");
				isAlive = false;

				StartCoroutine(WaitForGems());
			}
		}	
	}
	private IEnumerator WaitForGems()
	{
		yield return _waitForOneSevenSec;

		int j = 1;
		Vector2 pos = transform.position;

		for (int i = 0; i < nrGems; i++)
		{
			yield return _waitForZeroTwoSec;

			Vector2 newPos = new Vector2(j, 0);

			//as GameObject cast it into a gameObject and we can access its properties as the gem value
			GameObject insta = Instantiate(diamondPrefab, pos + newPos, Quaternion.identity) as GameObject;
			insta.GetComponent<Diamond>().gems = base.gems;
			diamond.Add(insta);
			j++;
		}
	}
}
