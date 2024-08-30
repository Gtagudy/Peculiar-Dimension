using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBol_Jump : MonoBehaviour
{
	[SerializeField] public Transform player;           
	[SerializeField] public float jumpForce = 5.0f;     
	[SerializeField] public float moveSpeed = 2.0f;     
	[SerializeField] public float jumpInterval = 2.0f;
	[SerializeField] public float damage = 1.0f;

	[SerializeField] private Rigidbody rb;
	[SerializeField] public SphereCollider collider;

	[SerializeField] private float jumpTimer;           
	[SerializeField] private bool isJumping;
	

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		jumpTimer = jumpInterval; 
		isJumping = false;
		collider = GetComponent<SphereCollider>();
	}

	void Update()
	{
		jumpTimer -= Time.deltaTime; 
		
		if (jumpTimer <= 0)
		{
			Jump();
			jumpTimer = jumpInterval; 
		}
		if(isJumping)
		{
			MoveTowardsPlayer();
		}
	}

	private void MoveTowardsPlayer()
	{
		Vector3 direction = (player.position - transform.position).normalized;
		rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.z * moveSpeed);
	}

	void Jump()
	{
		rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
		isJumping = true;
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.transform == player && isJumping)
		{
			PlayerScript player = other.transform.GetComponent<PlayerScript>();
			if (player != null)
			{
				player.TakeDamage(damage);
				collider.GetComponent<Collider>().enabled = false;
				StartCoroutine(DeactivateHitboxAfterDelay(0.1f));
			}
		}
	}

	private IEnumerator DeactivateHitboxAfterDelay(float v)
	{
		yield return new WaitForSeconds(v);
		collider.GetComponent<Collider>().enabled = true;
	}
}
