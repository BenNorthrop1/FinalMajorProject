using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour , IInteractable
{   
    public UiManager uiManager;
    public string partName;
    public int partAmount;


    public string GetDescription()
    {
        return $"Pickup {partName}";
    }

    public void Interact()
    {
        uiManager.boatPartsCounter += partAmount;
        LeanTween.scale(gameObject, new Vector3(0,0,0), 0.5f).setOnComplete(OnDestroy);
    }
    
    void OnDestroy() 
    {
        Destroy(gameObject);
    }

}
