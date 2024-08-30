using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Enemy
{
	public class Enemy : AbstractEntity, IDamageable
	{
		[SerializeField] GameObject MoneyDrop;
		[SerializeField] GameObject SkillDrop;
		[SerializeField] public int coins = 5;

		private Reinforcements reinforcements;

		private void Start()
		{
			{
				reinforcements = FindFirstObjectByType<Reinforcements>();
			}
		}

		private void DropCoins()
		{
			for (int i = 0; i < coins; i++)
			{
				Instantiate(MoneyDrop, transform.position, Quaternion.identity);
			}
			Debug.Log("Coins dropped: " + coins);
			coins = 0;
		}
		protected override void Die()
		{
			DropSkill();
			DropCoins();
			reinforcements.EnemyDefeated();
			Destroy(gameObject);
		}

		private void DropSkill()
		{
			Instantiate(SkillDrop, transform.position, Quaternion.identity);
			Debug.Log("Oops!");
		}

		public override void TakeDamage(float damage)
		{
			base.TakeDamage(damage);
		}
	}
}
