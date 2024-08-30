using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] Animation attack;
    [SerializeField] Collider hurtBox;
    [SerializeField] Animator attackAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            AttackAttempt();
        }
    }

	private void AttackAttempt()
	{
		if(DamagePossible())
        {
            attackAnim.SetTrigger("Square_Attack");


        }
	}

	private bool DamagePossible()
	{
        return true;
	}
}
