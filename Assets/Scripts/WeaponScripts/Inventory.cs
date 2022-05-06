using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //0 = primary, 1 = secondary, 3 = melee
   [SerializeField] private Weapon[] weapons;

    private Stats stats;

    private void Start()
    {
        stats = GetComponent<Stats>();
        InitVariables();
    }

    private void Update()
    {
        
    }

    public void AddItem(Weapon newItem)
    {
        int newItemIndex = (int)newItem.WeaponClass;

        if(weapons[newItemIndex] != null)
        {
            RemoveItem(newItemIndex);
        }
        
        weapons[newItemIndex] = newItem;

        stats.UpdateWeaponUI(newItem);
    }

    public void RemoveItem(int index)
    {
        weapons[index] = null;
    }
    

    public Weapon GetItem(int index)
    {
        return weapons[index];
    }

    private void InitVariables()
    {
        weapons = new Weapon[5];
    }



}
