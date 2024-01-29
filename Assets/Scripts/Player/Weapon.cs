using FMOD.Studio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public GameObject WeaponMain;
    public GameObject WeaponOrigin;

    public EventInstance ShotInstance;

    internal Action UpdateAction;

    void Update()
    {
        RotateGunToMouseCursor();
        UpdateAction?.Invoke();
    }

    private void RotateGunToMouseCursor()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePosition - WeaponOrigin.transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        WeaponOrigin.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public abstract void Fire();
    public abstract void FireStop();
}
