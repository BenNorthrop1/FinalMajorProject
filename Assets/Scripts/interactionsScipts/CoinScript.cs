using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Stats;

public class CoinScript : MonoBehaviour, IInteractable
{




    public void OnTriggerEnter (Collider other)
    {

        if(other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            currentCoin++;
        }
    }

    public void Collect()
    {

        Destroy(gameObject);
        currentCoin++;

    }

    public void Interact()
    {
        Destroy(gameObject);
        currentCoin++;
    }

    public string GetDescription()
    {
       return "Pick up Coin";
    }
}
