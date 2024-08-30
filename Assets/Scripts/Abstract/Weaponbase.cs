using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weaponbase : MonoBehaviour
{
    public float damageAmount;
    public float attackRange;
    public abstract void AttackAction();

    public virtual void EnableHurtbox()
    {
        this.GetComponent<Collider>().enabled = true;
    }
    public virtual void DisableHurtbox()
    {
        this.GetComponent <Collider>().enabled = false;
    }
}
