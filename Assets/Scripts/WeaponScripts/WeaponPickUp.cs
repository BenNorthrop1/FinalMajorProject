using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour, IInteractable
{
    private WeaponScript weaponScript; // this references the weapon script to disable it in the future

    private WeaponSwitch weaponSwitch; // this refernces the brackeys weapon switch

    private Rigidbody rb;

    private BoxCollider coll;

    public Transform gunpos; // this is the position where the weapons are held

    public Transform DropSpot; // this is the spot where the weapons is dropped

    //This was still a static bool which is why you couldn't pick up two. A static bool means it is shared across all the same scripts. So if it is == true in one it is ==true in all of them.
    public bool equipped; //this check if it is held by the player

    public bool primaryWeapon; // this checks if its active or not

    void Start()
    {
        coll = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        weaponSwitch = gunpos.GetComponent<WeaponSwitch>();
        weaponScript = GetComponent<WeaponScript>();

        if (!equipped)
        {
            weaponScript.enabled = false; //then enables the weapon script
            coll.isTrigger = false; // this disables the trigger
            rb.isKinematic = false; // this disables the kinematic(doesnt use physics)
        }
        if (equipped)
        {
            weaponScript.enabled = true;
            coll.isTrigger = true;
            rb.isKinematic = true;
        }
    }

    public string GetDescription()
    {
        return $"Pickup {weaponScript.WeaponName}"; // this uses an interaction system and takes the weapons name and displays it in game
    }

    public void Interact()
    {
        Debug.Log("pickedUP"); // this is empty as i dont use the pick up in here I put the code for that below
    }

    void Update()
    {
        if (primaryWeapon && Input.GetKeyDown(KeyCode.Q)) Drop(); // this makes it so when you press q it intiates the drop function
    }

    public void Pickup()
    {
        equipped = true;
        primaryWeapon = true;
        transform.SetParent (gunpos); //this sets the parent of the object
        transform.localPosition = Vector3.zero; //this sets the position to zero
        transform.localRotation = Quaternion.Euler(Vector3.zero); // this sets the rotation to zero

        rb.isKinematic = true;
        coll.isTrigger = true;

        weaponScript.enabled = true;
        int pickedUpWeaponIndex = transform.GetSiblingIndex();
        weaponSwitch.SetWeaponPickedUpAsPrimary (pickedUpWeaponIndex);
    }

    public void Drop()
    {
        equipped = false;
        primaryWeapon = false;
        transform.SetParent(null); //this gets rid of the parent

        rb.isKinematic = false;
        coll.isTrigger = false;

        transform.position = DropSpot.position; // this sets the weapon to the drop position

        weaponScript.enabled = false; // this disables the script

        weaponSwitch.SelectWeapon();
    }
}