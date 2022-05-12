using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    [Header("Stamina Parameters")]
    public float playerStamina = 100f;
    [SerializeField] private float maxStamina = 100f;
    [HideInInspector] public bool hasRegenerated = true;
    [HideInInspector] public bool weAreSprinting = false;

    [Header("Stamina Regen Parameters")]
    [SerializeField] private float staminaDrain = 0.5f;
    [SerializeField] private float staminaRegen = 0.5f;

    [Header("Stamina Speed Parameters")]
    [SerializeField] private int slowedRunSpeed = 4;
    [SerializeField] private int normalRunSpeed = 8;

    [Header("Stamina UI elements")]
    [SerializeField] private Slider staminaSlider;

    private PlayerMovement pM;

    void Start()
    {
        pM = GetComponent<PlayerMovement>();
    }


    void Update()
    {
        if(!weAreSprinting)
        {
            if(playerStamina <= maxStamina - 0.01)
            {

                playerStamina += staminaRegen * Time.deltaTime;
                updateStamina(1);

                if(playerStamina >= maxStamina)
                {
                    pM.SetRunSpeed(normalRunSpeed);
                    hasRegenerated = true;
                }
            }
        }
    }

    public void Sprinting()
    {

        if(hasRegenerated)
        {
            weAreSprinting = true;
            playerStamina -= staminaDrain * Time.deltaTime;
            updateStamina(1);

            if(playerStamina <= 0)
            {
                hasRegenerated = false;
            }
        }
    }

    void updateStamina(int value)
    {
        
        staminaSlider.value = playerStamina / maxStamina;




    }


}
