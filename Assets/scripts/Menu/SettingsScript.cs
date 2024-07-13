using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    [Header("Important elements")]

    public AudioSource effects;
    public AudioSource music;

    [Header("Values")]

    
    Resolution[] resolutions;
    public int fullscreenvalue;
  
     public float fovslidervalue;
    public float effectsvolumeslidervalue;
    public float musicvolumeslidervalue;
    [Header("UI Elements")]

    public GameObject settingspanel;
    public Dropdown resolutionDropdown; 
    public Dropdown quality;
    public Slider fovslider;
    public Slider effectsvolumeslider;
    public Slider musicvolumeslider;
    public Toggle fullscreentooggle;

    [Header("UI Tabs")]

    public GameObject generaltab;
    public GameObject audiotab;
    public GameObject videotab;

    
  
    
   
 
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " " + resolutions[i].refreshRate + "Hz";
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {

                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        generaltab.SetActive(true);
        audiotab.SetActive(false);
        videotab.SetActive(false);

        if (PlayerPrefs.GetInt("full") == 1)
        {
            fullscreentooggle.isOn = true;

        }
        else
        {
            fullscreentooggle.isOn = false;
        }
        fovslider.value = PlayerPrefs.GetFloat("FOV", fovslidervalue);
        if (PlayerPrefs.HasKey("qualityindex"))
        {
            quality.value = PlayerPrefs.GetInt("qualityindex", 0);
        }
        if (SceneManager.GetActiveScene().name == "Game")
        {
            Camera.main.orthographicSize = fovslider.value;
        }
        effectsvolumeslider.value = PlayerPrefs.GetFloat("VolumeEffects", effectsvolumeslidervalue);
        musicvolumeslider.value = PlayerPrefs.GetFloat("VolumeMusic", musicvolumeslidervalue);
        effectsvolumeslider.minValue = 0;
        effectsvolumeslider.maxValue = 1;
        musicvolumeslider.minValue = 0;
        musicvolumeslider.maxValue = 1;
        settingspanel.SetActive(false);
    }
    public void SetResolution(int resolutionindex)
    {
        Resolution resolution = resolutions[resolutionindex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetQuality(int index)
    {

        PlayerPrefs.SetInt("qualityindex", quality.value);
        PlayerPrefs.Save();
        QualitySettings.SetQualityLevel(index);


    }
    public void SetFullScreen(bool fullscreen)
    {
        if (fullscreen == true)
        {
            PlayerPrefs.SetInt("full", 1);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt("full", 0);
            PlayerPrefs.Save();
        }

        Screen.fullScreen = fullscreen;
    }
    public void ChangeFOVSlider(float value)
    {
        fovslidervalue = value;
        PlayerPrefs.SetFloat("FOV", fovslidervalue);
        PlayerPrefs.Save();
        Camera.main.orthographicSize = fovslidervalue;
    }
    public void ChangeEffectsVolumeSlider()
    {
        effectsvolumeslidervalue = effectsvolumeslider.value;
        PlayerPrefs.SetFloat("VolumeEffects", effectsvolumeslidervalue);
        PlayerPrefs.Save();
        effects.volume = effectsvolumeslidervalue;
    }
    public void ChangeMusicVolumeSlider()
    {
        musicvolumeslidervalue = musicvolumeslider.value;
        PlayerPrefs.SetFloat("VolumeMusic", musicvolumeslidervalue);
        PlayerPrefs.Save();
        music.volume = musicvolumeslidervalue;
    }
    public void SettingsBtn()
    {

        settingspanel.SetActive(true);
    }
    public void SettingsCloseBtn()
    {
        settingspanel.SetActive(false);

    }
    public void GeneralTabBtn()
    {
        generaltab.SetActive(true);
        audiotab.SetActive(false);
        videotab.SetActive(false);
    }
    public void AudioTabBtn()
    {

        audiotab.SetActive(true);
        videotab.SetActive(false);
        generaltab.SetActive(false);
    }
    public void VideoTabBtn()
    {
        videotab.SetActive(true);
        audiotab.SetActive(false);
        generaltab.SetActive(false);
    }
   
}
