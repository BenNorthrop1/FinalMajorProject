using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New weapon", menuName = "Items /Weapon ")]
public class Weapon : Item
{
    public GameObject prefab;
    public int magazineSize;
    public int magazineCount;
    public float range;

    public WeaponType weaponType;
    
    public WeaponClass WeaponClass;




}

public enum WeaponType{melee, pistol, assault_Rifle, shotgun, sniper , submachine_Gun, rpg};

public enum WeaponClass{primary, secondary, melee , sencondaryMelee};