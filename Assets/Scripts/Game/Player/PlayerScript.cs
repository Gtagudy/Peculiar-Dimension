using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : AbstractEntity, IDamageable
{
	[SerializeField] protected Animator animator;

	[SerializeField] SpriteRenderer spriteRenderer;

	[SerializeField] float speed;
	[SerializeField] float groundDist;
	[SerializeField] LayerMask terrainLayer;

	[SerializeField] StickWeapon weapon;

	[SerializeField] Rigidbody rb;

	[SerializeField] float sr;

	[SerializeField] AudioSource hurtemSound;
	[SerializeField] AudioSource pickupCoins;
	[SerializeField] AudioSource depositCoins;
	//[SerializeField] Collider hurtbox;
	private bool isAttacking;

	bool faceRight = true;

	public PlayerState currentState = PlayerState.IDLE;

	public int pocketSize;
	public int coins;
	public GameObject coinObj;

	int volume = 50;
	[SerializeField] int coinVolume = 30;
	
	/// <summary>
	/// This Void Start() method assigns the rigid body to this current
	/// rigid body
	/// </summary>
	void Start()
	{
		rb = gameObject.GetComponent<Rigidbody>();
		//hurtbox = weapon.GetComponent<Collider>();

		coins = 5;
		pocketSize = 15;
	}
	/// <summary>
	/// This Update() function is to set up both the movement and the state
	/// system for the Player
	/// </summary>
	void Update()
	{
		RaycastHit hit;
		Vector3 castPos = transform.position;
		castPos.y += 1;
		if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
		{
			if (hit.collider != null)
			{
				Vector3 movePos = transform.position;
				movePos.y = hit.point.y + groundDist;
				transform.position = movePos;
			}
		}
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		Vector3 moveDir = new Vector3(x, 0, y);
		rb.velocity = moveDir * speed;
		if (moveDir.x > 0 && !faceRight) Flip();
		if (moveDir.x < 0 && faceRight) Flip();

		animator.SetFloat("Speed", (Mathf.Abs(moveDir.x) + Mathf.Abs(moveDir.y)));

		switch (currentState)
		{
			case PlayerState.IDLE:
				IdleState();
				break;
			case PlayerState.WALKING:
				WalkingState();
				break;
			case PlayerState.ATTACKING:
				AttackingState();
				break;
		}

		StateTransitionHandler();
	}
	/// <summary>
	/// This function is called within Update, and it processes the transition between
	/// the trio of states
	/// </summary>
	private void StateTransitionHandler()
	{
		if(Input.GetMouseButtonDown(0))
		{
			currentState = PlayerState.ATTACKING;
		} else if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) // ||
		{
			currentState = PlayerState.WALKING;
		}// else { currentState = PlayerState.IDLE; }

	}
	/// <summary>
	/// This is the attacking state
	/// </summary>
	private void AttackingState()
	{
		if(!isAttacking)
		{
			isAttacking = true;

			weapon.AttackAction();
			animator.SetTrigger("Attack");
		}
		StartCoroutine(DeactivateHitboxAfterDelay(0.1f));
	}

	private void OnTriggerEnter(Collider other)
	{
		IDamageable enemy = other.GetComponent<IDamageable>();
		if (other.CompareTag("Enemy") && enemy != null && currentState == PlayerState.ATTACKING)
		{
			//HandleAttack(other);
			Debug.Log($"Hit: {other.name}");

			enemy.TakeDamage(10f);

			hurtemSound.PlayOneShot(hurtemSound.clip, volume);
		}
	
	}

	void OnDrawGizmos()
	{
		
		//Gizmos.color = Color.green;
		//Gizmos.DrawCube(transform.position, hurtbox.transform.position);
	}
	/// <summary>
	/// This function is to deactivate the weapons hitbox after a short delay provided in the 
	/// Attack State. Also makes isAttacking false
	/// </summary>
	/// <param name="v">V is the time in seconds for the hitbox to turn off</param>
	/// <returns>Returns a yield of WaitForSeconds()</returns>
	private IEnumerator DeactivateHitboxAfterDelay(float v)
	{
		yield return new WaitForSeconds(v);
		weapon.DisableHurtbox(); 
		isAttacking = false;
		currentState = PlayerState.IDLE;
	}
	private IEnumerator DeactivateHitboxAfterDamaged(float v)
	{

		Debug.Log("Immune!");
		yield return new WaitForSeconds(v);
		BoxCollider boxCollider = GetComponent<BoxCollider>();

		boxCollider.enabled = true;
	}

	private void WalkingState()
	{


	}

	private void IdleState()
	{

	}

	private void Flip()
	{
		faceRight = !faceRight;
		spriteRenderer.flipX = !faceRight;
	}

	public override void TakeDamage(float damage)
	{

		base.TakeDamage(damage);

		if (coins >= 0)
		{
			BoxCollider boxCollider = GetComponent<BoxCollider>();
			hurtemSound.PlayOneShot(hurtemSound.clip, volume);

			boxCollider.enabled = false;
			StartCoroutine(DeactivateHitboxAfterDamaged(2.0f));
			DropCoins();
		}
		else
		{
			Die();
		}
	}

	private void DropCoins()
	{
		for (int i = 0; i < coins; i++) 
		{
			Instantiate(coinObj, transform.position, Quaternion.identity);
		}
		coins = 0;
		Debug.Log("Coins dropped: " + coins);
	}

	protected override void Die()
	{
		//GameManager.Instance.PlayerLoses();
		Debug.Log("L + ratio + rip bozo");
	}

	public void CoinUp(int coinValue)
	{
		if(coins + coinValue <= pocketSize)
		{
			coins += coinValue;
			pickupCoins.PlayOneShot(pickupCoins.clip, coinVolume);


			Debug.Log("We got: " + coins);
		}
		else
		{
			Debug.Log("Alright get them coins to the deposite!");
		}
	}

	public void DepositCoins(CoinRegister Register)
	{
		if (Register.IsPlayerDepositing())
		{
			depositCoins.PlayOneShot(depositCoins.clip, volume);

			GameManager.Instance.AddCoins(coins);
			coins = 0;
			Debug.Log("Kerching!");
		}
	}
}
