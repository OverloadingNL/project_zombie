using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 25f;
    [SerializeField] GameObject muzzleFlash;
    [SerializeField] float muzzleFlashTime = .1f;
    [SerializeField] GameObject enemyHitEffect;
    [SerializeField] GameObject defaultHitEffect;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        muzzleFlash.SetActive(true);
        StartCoroutine(DisableMuzzleFlash());
        ProcessRaycast();
    }

    IEnumerator DisableMuzzleFlash()
    {
        yield return new WaitForSeconds(muzzleFlashTime);
        muzzleFlash.SetActive(false);
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);

            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null)
            {
                return;
            }
            target.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        switch (hit.transform.tag)
        {
            case "Enemy":
                GameObject impactEnemy = Instantiate(enemyHitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactEnemy, 1f);
                break;
            default:
                GameObject impactDefault = Instantiate(defaultHitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactDefault, 1f);
                break;
        }
    }
}
