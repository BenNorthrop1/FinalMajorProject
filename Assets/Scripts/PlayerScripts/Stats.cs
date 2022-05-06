using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public static float maxHealth;
    public static float health;
    public static int currentCoin;
    public TextMeshProUGUI coinsUi;

    [SerializeField] private WeaponUi weaponUi;



    void Start()
    {
        
    }


    void Update()
    {
        coinsUi.SetText(currentCoin + "");
    }

    public void UpdateWeaponUI(Weapon newWeapon)
    {
        weaponUi.UpdateInfo(newWeapon.itemIcon, newWeapon.magazineSize, newWeapon.magazineCount);
    }
}
