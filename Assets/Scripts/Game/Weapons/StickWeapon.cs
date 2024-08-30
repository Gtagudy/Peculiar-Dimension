using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickWeapon : Weaponbase, IWeapon
{
	[SerializeField] BoxCollider hurtbox;
	/// <summary>
	/// This Awake function sets the damageamount, attackrange,
	/// hurtbox by grabbing the box collider, and disables
	/// the hurtbox collider incase it in on
	/// </summary>
	private void Awake()
	{
		damageAmount = 10f;
		attackRange = 1f;
		hurtbox = GetComponent<BoxCollider>();
		hurtbox.enabled = false;
	}
	/// <summary>
	/// Attack Action is a function to be called elsewhere, and it will enable a hitbox 
	/// to combine with a trigger.
	/// </summary>
	public override void AttackAction()
	{
		EnableHurtbox();
	}
	/// <summary>
	/// Enable Hitbox goes to the base class, to Enable Hitbox and debugs whatever your hitting
	/// </summary>
	public override void EnableHurtbox()
	{
		base.EnableHurtbox();
		Debug.Log("Hitting " + gameObject.name);
	}
	/// <summary>
	/// Disable Hurtbox goes to the base class to disable the hitbox and debug whatever your hitting
	/// </summary>
	public override void DisableHurtbox()
	{
		base.DisableHurtbox();
		Debug.Log("hitting " + gameObject.name);
	}
	/// <summary>
	/// Get Damage bridges the gap between this weapon hitting your opponent and dealing damage
	/// </summary>
	/// <returns>Returns the damage the weapon deals</returns>
	public float GetDamage()
	{
		return damageAmount;
	}
	/// <summary>
	/// Get Range bridges the gap between you swinging your weapon and if it hits the opponent
	/// </summary>
	/// <returns>Returns the attack range provided by the weapon</returns>
	public float GetRange()
	{
		return attackRange;
	}
	private void OnDrawGizmos()
	{
		if (hurtbox != null)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireCube(hurtbox.bounds.center, hurtbox.bounds.size); // Visualize the hurtbox
		}
	}
}
