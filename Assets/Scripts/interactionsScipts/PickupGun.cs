using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGun : MonoBehaviour, IInteractable
{
    void start()
    {
    string WeaponName = GetComponentInChildren<WeaponScript>().WeaponName;
    }

    public string GetDescription()
    {
        return "Pickup Weapon";
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }
}