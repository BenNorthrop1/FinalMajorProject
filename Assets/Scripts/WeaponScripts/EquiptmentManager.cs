using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquiptmentManager : MonoBehaviour
{

    private Animator anim;
    private Inventory inventory;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(inventory.GetItem(0).weaponType == WeaponType.assault_Rifle)
            {
                anim.SetInteger("WeaponType" , (int)WeaponType.assault_Rifle);
            }
            if(inventory.GetItem(0).weaponType == WeaponType.shotgun)
            {
                anim.SetInteger("WeaponType" , (int)WeaponType.shotgun);
            }
            if(inventory.GetItem(0).weaponType == WeaponType.rpg)
            {
                anim.SetInteger("WeaponType" , (int)WeaponType.rpg);
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(inventory.GetItem(1).weaponType == WeaponType.pistol)
            {
                anim.SetInteger("WeaponType" , (int)WeaponType.pistol);
            }
            if(inventory.GetItem(1).weaponType == WeaponType.submachine_Gun)
            {
                anim.SetInteger("WeaponType" , (int)WeaponType.submachine_Gun);
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            
        }






    }
}
