
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{





public int boatPartsCounter;

public TMP_Text objectivesList;

public float duration;
public LeanTweenType easeType;

public GameObject objectiveScreen;

public GameObject pauseMenu;

public static bool isPaused;

private bool isObjectiveOpen;

public Transform spawnPoint;
public Transform player;
public Slider slider;

void Start()
{
    isPaused = false;
    isObjectiveOpen = true;

}
public void SetMaxHealth(int health)
{
    slider.maxValue = health;
    slider.value = health;
}

public void SetHealth(int health)
{
    slider.value = health;
}




void Update()
{

    objectivesList.text = boatPartsCounter.ToString();




    if(Input.GetKeyDown(KeyCode.Tab))
    {
        if(isObjectiveOpen == true)
        {

            ObjectiveClose();
        }
        else
        {
            ObjectiveOpen();
        }
    }





    if(Input.GetKeyDown(KeyCode.Escape)&& !Stats.isDead == true)
    {
        if(isPaused)
        {
            UnPause();
        }
        else
        {
            Pause();
        }


    }
    

    

}

void ObjectiveOpen()
{
    LeanTween.moveX(objectiveScreen.GetComponent<RectTransform>(), -316, duration).setEase(easeType).setOnComplete(ObjectiveActive);
    isObjectiveOpen = true;
}

void ObjectiveClose()
{
    LeanTween.moveX(objectiveScreen.GetComponent<RectTransform>(), 316, duration).setEase(easeType).setOnComplete(ObjectiveUnactive);
    isObjectiveOpen = false;
}

void ObjectiveActive()
{
    objectiveScreen.SetActive(true);

}

void ObjectiveUnactive()
{
    objectiveScreen.SetActive(false);

}
void UnPause() //not paused
{
    pauseMenu.SetActive(false);
    Cursor.lockState = CursorLockMode.Locked;
    Time.timeScale = 1f;
    isPaused = false;
}

void Pause() //paused
{
    pauseMenu.SetActive(true);
    Cursor.lockState = CursorLockMode.None;
    Time.timeScale = 0f;
    isPaused = true;
}


public void Quit()
{
    Application.Quit();
}

public void Resume()
{
    pauseMenu.SetActive(false);
    Cursor.lockState = CursorLockMode.Locked;
    Time.timeScale = 1f;
    isPaused = false;
}

public void returnToMenu()
{
    SceneManager.LoadScene(0);
}

public void Respawn()
{
    Scene scene = SceneManager.GetActiveScene();
    SceneManager.LoadScene(scene.name);
}


}
