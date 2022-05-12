using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterScript : MonoBehaviour
{
    public Transform player;

    public Transform spawnPoint;

    public Slider drowningSlider;

    public GameObject slider;

    public float maxOxygen;

    private float currentOxygen;

    void Start()
    {
        currentOxygen = maxOxygen;
    }

    void Update()
    {
        drowningSlider.value = currentOxygen;

        if(currentOxygen <= 0)
        {
            player.position = spawnPoint.position;
        }

    }


  
    void OnTriggerEnter(Collider col)
    {

        slider.SetActive(true);
    }

    void OnTriggerStay(Collider col)
    {
        StartCoroutine( TakeAwayOxygen() );
    }
    
    void OnTriggerExit(Collider col)
    {
        slider.SetActive(false);
        StartCoroutine( AddOxygen());
    }

    IEnumerator TakeAwayOxygen()
    {
        yield return new WaitForSeconds(5);

        for(int i = 0; i < maxOxygen; i++)
        {
        currentOxygen--;
        yield return new WaitForSeconds(2);

        }
    }

        IEnumerator AddOxygen()
    {
        yield return new WaitForSeconds(5);

        for(int i = 0; i < maxOxygen; i++)
        {
        currentOxygen++;
        yield return new WaitForSeconds(1);

        }
    }
}

