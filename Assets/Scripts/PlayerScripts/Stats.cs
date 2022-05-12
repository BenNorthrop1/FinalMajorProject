using System.Collections;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public float maxHealth;
    public float health;

    public static event Action onPlayedDamaged;

    public static int currentCoin;
    public TextMeshProUGUI coinsUi;




    void Start()
    {
        
    }


    void Update()
    {
        coinsUi.SetText(currentCoin + "");
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        onPlayedDamaged?.Invoke();


        if(health <= 0)
        {
            health = 0;
            Debug.Log("You're Dead");
        }
    }
    

}
