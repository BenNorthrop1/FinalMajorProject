using System.Collections;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;

    public UiManager uiManager;

    public static int currentCoin;
    public TextMeshProUGUI coinsUi;




    void Start()
    {
        currentHealth = maxHealth;
        uiManager.SetMaxHealth(maxHealth);
    }


    void Update()
    {
        coinsUi.SetText(currentCoin + "");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        uiManager.SetHealth(currentHealth);
    }


    }
    

