using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escape : MonoBehaviour, IInteractable
{

    public UiManager uiManager;

  



    public string GetDescription()
    {
        if(uiManager.boatPartsCounter == 7)
        {
            return"Leave";

        }
        else
        {
            return"You still need to get parts";
        }
    }

    public void Interact()
    {
        if(uiManager.boatPartsCounter == 7)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            Debug.Log("cannot leave yet");
        }
    }
}
