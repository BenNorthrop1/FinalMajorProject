using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject settingsPanel;


    void Start()
    {
        settingsPanel.SetActive(false);
    }

    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Settings()
    {
        settingsPanel.SetActive(true);

    }

    public void Back()
    {
        settingsPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
