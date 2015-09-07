﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    private Animator playerAnimator;

    private string currentWeapon;

    public AxeWeapon axeWeapon;
    public StoneWeapon stoneWeapon;

    public GameObject hitButton;

    IAttack attack;

    public void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    public void Equip(string weapon)
    {
        if (currentWeapon != weapon)
            Equip(currentWeapon);

        switch(weapon)
        {
            case "axe":
                currentWeapon = axeWeapon.Equip() == true ? weapon : "";
                attack = axeWeapon;
                break;

            case "stone":
                currentWeapon = stoneWeapon.Equip() == true ? weapon : "";
                attack = stoneWeapon;
                break;
        }

        if (currentWeapon == "")
            hitButton.SetActive(false);
        else
            hitButton.SetActive(true);
    }

    void FixedUpdate()
    {
#if !MOBILE_INPUT
        if (Input.GetKey(KeyCode.Q))
        {
            attack.Attack();
        }
#endif

#if MOBILE_INPUT
        if (HitButton.isDown)
        {
            attack.Attack();
        }
#endif
    }
}
