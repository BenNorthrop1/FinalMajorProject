using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public static int selectedWeapon = 0;

    private WeaponPickUp weaponPickUp;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousthing = selectedWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }
        if (previousthing != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    public void SelectWeapon()
    {
        int i = 0;
        if (transform.childCount > 0)
        {
            foreach (Transform weapon in transform)
            {
                //Wasn't sure why this was looking for component in children. This foreach loop should be looping through the weapons themselves.
                //the transform of this should be the game object which the guns get parented too. So this script should be attached to the gameobject.
                //Added a disable/enable function to the weapon script to ensure they don't fire/do anything strange
                if (i == selectedWeapon)
                {
                    var weaponObject = weapon.gameObject;

                    weaponObject.GetComponent<WeaponPickUp>().primaryWeapon = true;
                    weaponObject.GetComponent<WeaponScript>().enabled = true;
                    weaponObject.SetActive(true);
                }
                else
                {
                    var weaponObject = weapon.gameObject;
                    weaponObject.GetComponent<WeaponPickUp>().primaryWeapon = false;
                    weaponObject.GetComponent<WeaponScript>().enabled = false;
                    weaponObject.SetActive(false);
                }
                i++;
            }
        }
    }

    public void SetWeaponPickedUpAsPrimary(int newWeaponIndex)
    {
        selectedWeapon = newWeaponIndex;
        SelectWeapon();
    }
}