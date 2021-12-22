using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
	[SerializeField] protected int health;
	[SerializeField] protected float speed;
	[SerializeField] protected int gems;
	[SerializeField] protected int nrGems;
	[SerializeField] protected bool isAlive = true;
	[SerializeField] protected bool isPlayerAlive = true;

	[SerializeField] protected Transform pointA, pointB;
	protected List<GameObject> diamond = new List<GameObject>();
	[SerializeField] protected GameObject diamondPrefab;

	protected Vector2 currentTargetPos;
	protected Animator anim;
	protected SpriteRenderer spriteRend;
	protected Player player;

	protected bool isHit = false;

	public virtual void Init()
	{
		anim = GetComponentInChildren<Animator>();
		spriteRend = GetComponentInChildren<SpriteRenderer>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		anim.SetBool("IsPlayerAlive", true);
	}

	private void Start()
	{
		Init();
	}

	public virtual void Update()
	{
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && anim.GetBool("InCombat") == false)
		{
			return;
		}
	
		Movement();
	}

	public virtual void OnEnable()
	{
		Player.OnDeath += PlayerDeath;
	}
	public virtual void Movement()
	{
		if (transform.position == pointA.position)
		{
			spriteRend.flipX = false;
		}

		else if (transform.position == pointB.position)
		{
			spriteRend.flipX = true;
		}

		if (transform.position == pointA.position)
		{
			currentTargetPos = pointB.position;
			anim.SetTrigger("Idle");
		}

		else if (transform.position == pointB.position)
		{
			currentTargetPos = pointA.position;
			anim.SetTrigger("Idle");
		}

		if (isHit == false)
		{
			transform.position = Vector2.MoveTowards(transform.position, currentTargetPos, speed * Time.deltaTime);
		}

		float distance = Vector2.Distance(transform.localPosition, player.transform.localPosition);

		if (distance > 2.0f)
		{
			isHit = false;
			anim.SetBool("InCombat", false);
		}

		Vector2 side = player.transform.position - transform.position;
		if (side.x > 0 && anim.GetBool("InCombat") == true)
		{
			spriteRend.flipX = false;
		}
		else if (side.x < 0 && anim.GetBool("InCombat") == true)
		{
			spriteRend.flipX = true;
		}
	}

	public virtual void PlayerDeath(bool isAlive)
	{
		isPlayerAlive = isAlive;
		anim.SetBool("InCombat", false);
		anim.SetBool("IsPlayerAlive", false);
	}

	public virtual void OnDisable()
	{
		Player.OnDeath -= PlayerDeath;
	}
}
