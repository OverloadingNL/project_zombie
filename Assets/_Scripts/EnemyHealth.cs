using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public bool isDead = false;

    [SerializeField] float hitPoints = 100f;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");

        anim.SetTrigger("damage");
        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            Kill();
        }

    }

    private void Kill()
    {
        isDead = true;
        GetComponent<CapsuleCollider>().isTrigger = true;
        anim.SetTrigger("death");
        Destroy(gameObject, 5f);
    }
}
