using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Player : MonoBehaviour, IDamageable
{
	private PlayerInput _playerInput;
	private InputAction _moveAction;

	[SerializeField] private Rigidbody2D _rb2D;

	[SerializeField] private float _jumpForce;
	[SerializeField] private float _lowJumpForce;
	[SerializeField] private float _highJumpForce;
	[SerializeField] private float _speed;
	[SerializeField] private bool _isGrounded = false;
	[SerializeField] private float _onMove;
	[SerializeField] private int _health;
	[SerializeField] private bool _isAlive = true;
	[SerializeField] private Transform _reSpawnFallenPos;

	private WaitForSeconds _waitForOneSec = new WaitForSeconds(1f);

	private bool _canJump = false;
	private bool _canAttack = false;
	public int gemsCollected;

	public static event Action<float> OnMove;
	public static event Action<bool> OnJumping;
	public static event Action OnAttacking;
	public static event Action OnHit;
	public static event Action<bool> OnDeath;

	public int Health { get; set; }

	private void Awake()
	{
		_playerInput = GetComponent<PlayerInput>();
		_moveAction = _playerInput.actions["Move"];
	}

	private void OnEnable()
	{
		Diamond.OnGemCollection += CollectGems;
		Shop.OnPurchaseItem += CollectGems;
		Shop.OnPurchaseLifes += AddLifes;
		ExtraJump.OnExtraJump += HigherJump;
		ExtraJump.OnFinishExtraJump += NormalJump;
		FallZone.OnFallingZone += Fallen;
	}

	private void Start()
	{
		Health = 4;
		_jumpForce = _lowJumpForce;
	}

	void Update()
	{
		if (GameManager.Instance.GameOver == false)
		{
			Movement();
		}
		
		if (_canAttack && IsGrounded() == true)
		{
			OnAttacking?.Invoke();
			_canAttack = false;
		}
	}

	private void OnJump()
	{
		_canJump = true;
	}

	private void OnAttack()
	{
		if (_isAlive)
		{
			_canAttack = true;
		}
	}

	private void Movement()
	{
		if (_isAlive)
		{
			//float moveX = Input.GetAxisRaw("Horizontal");
			Vector2 move = _moveAction.ReadValue<Vector2>();

			_isGrounded = IsGrounded();

			//if (Input.GetKeyDown(KeyCode.Space) && _isGrounded == true)
			if (_canJump && _isGrounded == true)
			{
				_rb2D.velocity = new Vector2(_rb2D.velocity.x, _jumpForce);

				OnJumping?.Invoke(true);
				_canJump = false;
				StartCoroutine(ResetJumpRoutine());
			}

			_rb2D.velocity = new Vector2(move.x * _speed, _rb2D.velocity.y);
			_onMove = move.x;

			OnMove?.Invoke(_onMove);
		}
	}

	private bool IsGrounded()
	{
		//1 << 8 - to indicate that the raycast is checking layer 8 
		RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1, 1 << 8);
		Debug.DrawRay(transform.position, Vector2.down, Color.green);

		if (hitInfo.collider != null)
		{
			return true;
		}
		else
			return false;
	}

	private IEnumerator ResetJumpRoutine()
	{
		yield return _waitForOneSec;

		OnJumping?.Invoke(false);
	}

	public void Damage()
	{
		if (_isAlive)
		{
			Health--;

			UIManager.Instance.RemoveLivesUI(Health);

			if (Health > 1)
			{
				OnHit?.Invoke();
			}

			else if (Health < 1)
			{
				_isAlive = false;
				OnDeath?.Invoke(_isAlive);
				UIManager.Instance.GameOver();
			}
		}
	}

	private void AddLifes()
	{
		if (Health < 4)
		{
			Health++;

			UIManager.Instance.AddLifesUI(Health - 1);
		}
	}

	private void CollectGems(int gems)
	{
		if (_isAlive)
		{
			gemsCollected += gems;
			UIManager.Instance.OpenShop(gemsCollected);
			UIManager.Instance.GemCount(gemsCollected);
		}
	}

	private void HigherJump(float highJumpForce)
	{
		_jumpForce = highJumpForce;
	}

	private void NormalJump()
	{
		_jumpForce = _lowJumpForce;
	}

	private void Fallen()
	{
		Damage();

		transform.position = _reSpawnFallenPos.position;
	}

	private void OnDisable()
	{
		Diamond.OnGemCollection -= CollectGems;
		Shop.OnPurchaseItem -= CollectGems;
		Shop.OnPurchaseLifes -= AddLifes;
		ExtraJump.OnExtraJump -= HigherJump;
		ExtraJump.OnFinishExtraJump -= NormalJump;
		FallZone.OnFallingZone -= Fallen;
	}
}
