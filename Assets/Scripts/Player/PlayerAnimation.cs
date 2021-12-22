using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
	[SerializeField] private Animator _playerAnim;
	[SerializeField] private SpriteRenderer _spriteRend;

	[SerializeField] private Animator _swordAnim;
	[SerializeField] private SpriteRenderer _swordArcRend;
	[SerializeField] private Transform _swordArcTrans;

	private void OnEnable()
	{
		Player.OnMove += Move;
		Player.OnJumping += Jump;
		Player.OnAttacking += Attack;
		Player.OnHit += Hit;
		Player.OnDeath += Death;
		End.OnGameOver += GameOver;
	}

	private void Move(float move)
	{
		FlipPlayer(move);

		_playerAnim.SetFloat("Move", Mathf.Abs(move));
	}

	private void FlipPlayer(float move)
	{
		if (move < 0)
		{
			_spriteRend.flipX = true;
			_swordArcRend.flipX = true;
			_swordArcRend.flipY = true;
			_swordArcTrans.localPosition = new Vector2(-_swordArcTrans.localPosition.x, _swordArcTrans.localPosition.y);
		}	

		else if (move > 0)
		{
			_spriteRend.flipX = false;
			_swordArcRend.flipX = false;
			_swordArcRend.flipY = false;
			_swordArcTrans.localPosition = new Vector2(_swordArcTrans.localPosition.x, _swordArcTrans.localPosition.y);
		}
	}

	private void Jump(bool isJumping)
	{
		if (isJumping == true)
			_playerAnim.SetBool("IsJumping", true);
		else if (isJumping == false)
			_playerAnim.SetBool("IsJumping", false);
	}

	private void Attack()
	{
		_playerAnim.SetTrigger("Attack");
		_swordAnim.SetTrigger("SwordAnim");
	}

	private void Hit()
	{
		_playerAnim.SetTrigger("Hit");
	}

	private void Death(bool isAlive)
	{
		_playerAnim.SetTrigger("Death");
	}

	private void GameOver()
	{
		_playerAnim.SetBool("GameOver", true);
	}

	private void OnDisable()
	{
		Player.OnMove -= Move;
		Player.OnJumping -= Jump;
		Player.OnAttacking -= Attack;
		Player.OnHit -= Hit;
		Player.OnDeath -= Death;
	}
}
