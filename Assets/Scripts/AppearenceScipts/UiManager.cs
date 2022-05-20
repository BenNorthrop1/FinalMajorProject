
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{

    public GameObject pauseMenu;

    public static bool isPaused;
    
    public static bool isRespawned;
    public Transform spawnPoint;
    public Transform player;
    public Slider slider;

    void Start()
    {
        isRespawned = false;
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
        isRespawned = true;
        player.position = spawnPoint.position;
    }


}
