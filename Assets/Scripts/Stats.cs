using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour
{
    public static float Health;
    public static int currentCoin;
    public TextMeshProUGUI coinsUi;



    void Start()
    {
        
    }


    void Update()
    {
        coinsUi.SetText(currentCoin + "");
    }
}
