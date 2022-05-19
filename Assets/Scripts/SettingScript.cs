using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingScript : MonoBehaviour
{
    public AudioMixer audioMixer;

    public TMP_Dropdown resolutionDropdown;

    public Toggle toggle;

    int currentResolutionIndex = 0;

    int qualityIndex;

    float volume;

    float volumeOn = 0;
    float volumeOff = -80;
    
    Resolution[] resolutions;

    void Start()
    {

        



    resolutions = Screen.resolutions;

    resolutionDropdown.ClearOptions();

    List<string> options = new List<string>();

      

    for (int i = 0; i < resolutions.Length; i++)
    {
        string option = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRate + "hz";
        options.Add(option);

        if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
        {
            currentResolutionIndex = i;
        }

        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();


        currentResolutionIndex = PlayerPrefs.GetInt ("screen res index");
        qualityIndex = PlayerPrefs.GetInt("Quality");
        bool isFullscreen = (PlayerPrefs.GetInt("fullscreen") == 1)?true:false;
        volume = PlayerPrefs.GetFloat("volumeData");
        volumeOn = PlayerPrefs.GetFloat("volumeDataOn");
        volumeOff = PlayerPrefs.GetFloat("volumeDataOff");

    }



    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("screen res index" , resolutionIndex);
        PlayerPrefs.Save();
    }

    

    public void SetVolume ()
    {   
     
        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("volumeData", volume);
        PlayerPrefs.Save();
    }
    

     

    public void SetQuality()
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        QualitySettings.SetQualityLevel(2);

        PlayerPrefs.SetInt("Quality" , qualityIndex);
        PlayerPrefs.Save();
    }


    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("fullscreen", ((isFullscreen)? 1:0));
        PlayerPrefs.Save();
    }



    public void toggleMute ()
    {
        if(toggle.isOn)
        {
        audioMixer.SetFloat("Volume", volumeOff);
        }
        else
        {

        audioMixer.SetFloat("Volume", volumeOn);
        PlayerPrefs.SetFloat("volumeDataOff", volumeOff);
        PlayerPrefs.SetFloat("volumeDataOn", volumeOn);
        PlayerPrefs.Save();
        }
    }
}
