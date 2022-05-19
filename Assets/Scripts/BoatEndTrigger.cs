using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoatEndTrigger : MonoBehaviour, IInteractable
{
    public string GetDescription()
    {
        return "Escape THE ISLAND WHILST YOU STILL CAN";
    }

    public void Interact()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
