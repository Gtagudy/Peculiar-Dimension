using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEntity : MonoBehaviour
{
	protected float health;
	/// <summary>
	/// This function deals with the Entities health and how it can
	/// be harmed. If it reaches less than or equal to 0, it calls Die()
	/// </summary>
	/// <param name="damage">Damage is provided by the weapon that dealt it, which hurts the health</param>
	public virtual void TakeDamage(float damage)
	{
		health -= damage;
		Debug.Log("Ouch " + this.name +"!: " + damage);
		if(health <= 0)
		{
			Die();
		}
	}

	/// <summary>
	/// This abstract class is to be defined in whatever class abstracts from it.
	/// </summary>
	protected abstract void Die();
}
