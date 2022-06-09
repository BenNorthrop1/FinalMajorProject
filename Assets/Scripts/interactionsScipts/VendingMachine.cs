using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Stats;

public class VendingMachine : MonoBehaviour, IInteractable
{

    public GameObject[] rewards;

    public int Price;
    
    private GameObject reward;
    private int index;

    public Transform rewardSpawnPos;

    public string GetDescription()
    {
        return "Use Vending Machine for : " + Price.ToString();
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
            currentCoin -= Price;

            index = Random.Range(0, rewards.Length);
            reward = rewards[index];

            Instantiate(reward, rewardSpawnPos.position , transform.rotation);
        }
    }
}
