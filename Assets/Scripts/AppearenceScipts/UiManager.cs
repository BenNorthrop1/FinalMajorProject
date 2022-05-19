using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{


    public GameObject pauseMenu;

    public static bool isPaused;

    public Slider slider;

    void Start()
    {

        isPaused = false;

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
    
        if(Input.GetKeyDown(KeyCode.Escape))
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


}
