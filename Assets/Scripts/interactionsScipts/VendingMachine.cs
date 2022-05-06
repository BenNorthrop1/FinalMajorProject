using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Stats;

public class VendingMachine : MonoBehaviour, IInteractable
{

    public GameObject reward;

    public Transform rewardSpawnPos;

    public string GetDescription()
    {
        return "Use Vending Machine";
    }

    public void Interact()
    {
        UseVendingMachine();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UseVendingMachine()
    {
        if(currentCoin == 0)
        {
            Debug.Log("Cannot purchase");
        }
        else
        {
            currentCoin--;

            Instantiate(reward, rewardSpawnPos.position , transform.rotation);
        }
    }
}
