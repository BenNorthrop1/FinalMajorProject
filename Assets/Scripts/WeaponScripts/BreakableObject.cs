using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public GameObject desroyedVersion;

    public PlayerInteraction pI;

    public GameObject Coin;

    public bool spawnCoin;

    public void Start()
    {

    }

    void Update()
    {

        
           
        
        


        

    
    }

    void OnCollisionEnter(Collision collision)
    {
        if(pI.isPickedUp = true)
        {
            if (collision.gameObject.tag == "Player")
            {
                Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            }
        }

    }




    public void Break()
    {
        GameObject Clone = (GameObject)Instantiate(desroyedVersion, transform.position , transform.rotation);
        Destroy(gameObject);
        Destroy(Clone, 5.0f);

        if(spawnCoin == true)
        {
        int[] amount = new int[] {1, 2, 3, 4, 5};

        int randomValue = Random.Range(0, amount .Length -1);
        int coinAmount = amount[randomValue];

        for(int i = 0; i < coinAmount; i++)
        {
            Instantiate(Coin, transform.position , transform.rotation);
        }

        }
        

    
    }






}
