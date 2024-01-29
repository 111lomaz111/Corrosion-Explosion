using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothPasteTube: Weapon
{
    public GameObject BulletOrigin;
    public GameObject BulletPrefab;
    public float GunCooldown = 0.5F;

    [SerializeField] private EventReference reference;
    [SerializeField]
    private bool canFire = true;
    
    private void OnEnable()
    {
        canFire = true;
    }

    public override void Fire()
    {
        if (canFire)
        {
            Instantiate(BulletPrefab, BulletOrigin.transform.position, BulletOrigin.transform.rotation);
            AudioManager.Instance.PlayOneShot(reference, this.transform.position);
            StartCoroutine(StopFire());
        }
    }

    public override void FireStop()
    {
    }

    private IEnumerator StopFire()
    {
        canFire = false;
        yield return new WaitForSecondsRealtime(GunCooldown);
        canFire = true;
    }
}

