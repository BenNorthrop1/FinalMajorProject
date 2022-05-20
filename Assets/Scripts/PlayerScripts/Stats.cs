using System.Collections;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;

    public int regenDelay;

    public UiManager uiManager;

    public static int currentCoin;
    public TextMeshProUGUI coinsUi;

    public GameObject deathScreen;

    public PlayerMovement playerMovement;

    public MouseLook mouseLook;

    public static bool isDead;


    void Start()
    {
        currentHealth = maxHealth;
        uiManager.SetMaxHealth(maxHealth);

        playerMovement.enabled = true;
        mouseLook.enabled = true;
        Time.timeScale = 1;

        deathScreen.SetActive(false);
        isDead = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        coinsUi.SetText(currentCoin + "");

        if(UiManager.isRespawned)
        {    
            deathScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            playerMovement.enabled = true;
            mouseLook.enabled = true;
            Time.timeScale = 1;
        }

       
    }

    void FixedUpdate()
    {

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Dead();
            isDead = true;
        }

        uiManager.SetHealth(currentHealth);
    }

    void Dead()
    {
        Cursor.lockState = CursorLockMode.None;
        playerMovement.enabled = false;
        mouseLook.enabled = false;
        Time.timeScale = 0;

        deathScreen.SetActive(true);


    }

    }
    

