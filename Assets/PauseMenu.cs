using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{


    public GameObject pauseMenu;
    public GameObject mapUi;

    public bool isPaused , isMap;


    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        isMap = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape) && isPaused == false)
        {
           isPaused = true;
        }

        if(Input.GetKey(KeyCode.Escape) && isPaused == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            isPaused = false;
        }

        if(Input.GetKey(KeyCode.M) && isMap == false)
        {
            isMap = true;
        }

        if(Input.GetKey(KeyCode.M) && isMap == true)
        {
            isMap = false;
        }


        if(isPaused == true)
        {
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }

        if(isPaused == false)
        {
           pauseMenu.SetActive(false);
        }

        if(isMap == true)
        {
            mapUi.SetActive(true);
        }

        if(isMap == false)
        {
            mapUi.SetActive(false);
        }

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }


}
