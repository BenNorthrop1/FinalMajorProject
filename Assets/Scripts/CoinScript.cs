using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Stats;

public class CoinScript : MonoBehaviour
{

    public Stats stats;


    public void OnTriggerEnter (Collider other)
    {

        if(other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            currentCoin++;
        }




    }




}
