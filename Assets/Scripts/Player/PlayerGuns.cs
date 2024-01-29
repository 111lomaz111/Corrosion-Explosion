using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGuns : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Weapon> Guns;
    internal CustomInput input;


    private Weapon _currentWeapon;
    public Weapon CurrentWeapon
    {
        get => _currentWeapon;
        set
        {
            _currentWeapon = value;
            SwitchVisibleGun();
        }
    }


    void Start()
    {
        CurrentWeapon = Guns.First();
    }

    void Awake()
    {
        input = new CustomInput();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Shooting.performed += Fire;
        input.Player.Shooting.canceled += FireStop;
        input.Player.ChangingWeapon.performed += ChangeWeapon;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Shooting.performed -= Fire;
        input.Player.Shooting.canceled -= FireStop;
        input.Player.ChangingWeapon.performed -= ChangeWeapon;
    }

    private void ChangeWeapon(InputAction.CallbackContext obj)
    {
        CurrentWeapon = Guns.FirstOrDefault(x => x != CurrentWeapon);
    }

    public void Fire(InputAction.CallbackContext obj)
    {
        CurrentWeapon.Fire();
    }

    private void FireStop(InputAction.CallbackContext obj)
    {
        CurrentWeapon.FireStop();
    }
    private void SwitchVisibleGun()
    {
        Guns.ForEach(x =>
        {
            bool isCurrentGun = x == CurrentWeapon;
            x.enabled = isCurrentGun; //disabling script
            x.WeaponOrigin.SetActive(isCurrentGun); //disabling master gameobject
        });
    }
}
