using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour {

    public Animator playerAnimator;

    private bool isEquip = false;
    private string currentWeapon;
    
    public void Equip(string weapon)
    {
        if (isEquip)
        {
            playerAnimator.SetBool(currentWeapon, false);

            if (weapon.Equals(currentWeapon))
            {
                isEquip = false;
            }
            else
            {
                playerAnimator.SetBool(weapon, true);
                currentWeapon = weapon;
            }
        }
        else
        {
            playerAnimator.SetBool(weapon, true);
            isEquip = true;
            currentWeapon = weapon;
        }        
    }
}
