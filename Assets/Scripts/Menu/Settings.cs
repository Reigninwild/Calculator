using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using System;

public class Settings : MonoBehaviour {

    public GameObject panel;
    public GameObject videoPanel;
    public GameObject advancedVideoPanel;
    public GameObject controllPanel;
    public GameObject volumePanel;
    public GameObject mainPanel;

    //Volume
    public GameObject smoothSlider;
    public GameObject sensSlider;
    public Toggle smoothToggle;
    public GameObject volumeSlider;

    //Graphics buttons
    public Button[] graphicsButtons;

    //Rendering
    public Slider pixelLightCount;
    public Slider textureQuality;
    public Slider anisotropicTexture;
    public Slider antiAliasing;
    public Toggle softParticles;
    public Toggle realtimeReflection;
    public Toggle billboardsFace;
    //Shadow
    public Slider shadowProjection;
    public Slider shadowDistance;
    public Slider shadowCascades;
    //Other
    public Slider blendWeigth;
    public Slider vSyncCount;
    public Slider lodBias;
    public Slider particalRaycast;
    
	// Use this for initialization
    void Start()
    {
        Load();
        //Rendering
        pixelLightCount.value = QualitySettings.pixelLightCount;
        textureQuality.value = QualitySettings.masterTextureLimit;
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
        anisotropicTexture.value = 0;
        antiAliasing.value = QualitySettings.antiAliasing;
        softParticles.isOn = QualitySettings.softVegetation;
        realtimeReflection.isOn = QualitySettings.realtimeReflectionProbes;
        billboardsFace.isOn = QualitySettings.billboardsFaceCameraPosition;
        //Shadow
        QualitySettings.shadowProjection = ShadowProjection.StableFit;
        shadowProjection.value = 1;
        shadowDistance.value = QualitySettings.shadowDistance;
        shadowCascades.value = QualitySettings.shadowCascades;
        //Other
        QualitySettings.blendWeights = BlendWeights.FourBones;
        blendWeigth.value = 2;
        vSyncCount.value = QualitySettings.vSyncCount;
        lodBias.value = QualitySettings.lodBias;
        particalRaycast.value = QualitySettings.particleRaycastBudget;
	}
    
    public void Load()
    {
        //Load graphics quality
        QualitySettings.SetQualityLevel(PlayerPrefs.HasKey("graphics_level") ? PlayerPrefs.GetInt("graphics_level") : 2);
        //Load controller sens and smooth
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.GetComponent<FirstPersonController>().m_MouseLook.smooth = (PlayerPrefs.GetInt("isSmooth") == 1) ? true : false;
            player.GetComponent<FirstPersonController>().m_MouseLook.smoothTime = PlayerPrefs.HasKey("smooth") ? PlayerPrefs.GetFloat("smooth") : 10;
            player.GetComponent<FirstPersonController>().m_MouseLook.XSensitivity = PlayerPrefs.HasKey("sensetivity") ? PlayerPrefs.GetFloat("sensetivity") : 1;
            player.GetComponent<FirstPersonController>().m_MouseLook.YSensitivity = PlayerPrefs.HasKey("sensetivity") ? PlayerPrefs.GetFloat("sensetivity") : 1;
        }
        //Load volume
        AudioListener.volume = PlayerPrefs.HasKey("volume") ? PlayerPrefs.GetFloat("volume") : 1;

        //Init controlls
        if (PlayerPrefs.GetInt("isSmooth") == 1)
        {
            smoothSlider.SetActive(true);
            smoothToggle.isOn = true;
        }
        smoothSlider.GetComponent<Slider>().value = PlayerPrefs.HasKey("smooth") ? PlayerPrefs.GetFloat("smooth") : 10;
        sensSlider.GetComponent<Slider>().value = PlayerPrefs.HasKey("sensetivity") ? PlayerPrefs.GetFloat("sensetivity") : 1;
        graphicsButtons[QualitySettings.GetQualityLevel()].interactable = false;
        volumeSlider.GetComponent<Slider>().value = PlayerPrefs.HasKey("volume") ? PlayerPrefs.GetFloat("volume") : 1;
    }

    public void SaveVideo()
    {
        //Rendering
        QualitySettings.pixelLightCount = (int)pixelLightCount.value;
        QualitySettings.masterTextureLimit = (int)textureQuality.value;

        switch ((int)anisotropicTexture.value)
        {
            case 0:
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
                break;
            case 1:
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
                break;
            case 2:
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
                break;
        }
        QualitySettings.antiAliasing = (int)antiAliasing.value;
        QualitySettings.softVegetation = softParticles.isOn;
        QualitySettings.realtimeReflectionProbes = realtimeReflection.isOn;
        QualitySettings.billboardsFaceCameraPosition = billboardsFace.isOn;
        //Shadow
        switch ((int)shadowProjection.value)
        {
            case 0:
                QualitySettings.shadowProjection = ShadowProjection.CloseFit;
                break;
            case 1:
                QualitySettings.shadowProjection = ShadowProjection.StableFit;
                break;
        }
        QualitySettings.shadowDistance = shadowDistance.value;
        QualitySettings.shadowCascades = (int)shadowCascades.value;
        //Other
        switch ((int)blendWeigth.value)
        {
            case 0:
                QualitySettings.blendWeights = BlendWeights.OneBone;
                break;
            case 1:
                QualitySettings.blendWeights = BlendWeights.TwoBones;
                break;
            case 2:
                QualitySettings.blendWeights = BlendWeights.FourBones;
                break;
        }
        QualitySettings.vSyncCount = (int)vSyncCount.value;
        QualitySettings.lodBias = lodBias.value;
        QualitySettings.particleRaycastBudget = (int)particalRaycast.value;
    }

    public void Show()
    {
        panel.SetActive(true);
    }

    public void Close()
    {
        panel.SetActive(false);
    }

    public void ShowMain()
    {
        mainPanel.SetActive(true);
    }

    public void CloseMain()
    {
        mainPanel.SetActive(false);
    }

    public void ShowVideo()
    {
        videoPanel.SetActive(true);
    }

    public void CloseVideo()
    {
        videoPanel.SetActive(false);
    }

    public void ShowControll()
    {
        controllPanel.SetActive(true);
    }

    public void CloseControll()
    {
        controllPanel.SetActive(false);
    }

    public void ShowVolume()
    {
        volumePanel.SetActive(true);
    }

    public void CloseVolume()
    {
        volumePanel.SetActive(false);
    }

    public void SetQuality(int level)
    {
        graphicsButtons[QualitySettings.GetQualityLevel()].interactable = true;
        switch (level)
        {
            case 0:
                QualitySettings.SetQualityLevel(0);
                break;
            case 1:
                QualitySettings.SetQualityLevel(1);
                break;
            case 2:
                QualitySettings.SetQualityLevel(2);
                break;
            case 3:
                QualitySettings.SetQualityLevel(3);
                break;
            case 4:
                QualitySettings.SetQualityLevel(4);
                break;
            case 5:
                QualitySettings.SetQualityLevel(5);
                break;
        }
        graphicsButtons[level].interactable = false;
        PlayerPrefs.SetInt("graphics_level", level);
    }

    public void AdvancedGraphicSettings()
    {
        advancedVideoPanel.SetActive(!advancedVideoPanel.activeSelf);
    }

    public void OnSmooth()
    {
        smoothSlider.SetActive(smoothToggle.isOn);
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.GetComponent<FirstPersonController>().m_MouseLook.smooth = smoothToggle.isOn;
        }
        PlayerPrefs.SetInt("isSmooth", Convert.ToInt32(smoothToggle.isOn));
    }

    public void SmoothChanged(Text text) {
        float smooth = smoothSlider.GetComponent<Slider>().value;
        text.text = "" + smooth;

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.GetComponent<FirstPersonController>().m_MouseLook.smoothTime = smooth;
        }
        PlayerPrefs.SetFloat("smooth", smooth);
    }

    public void SensetivityChanged(Text text)
    {
        float sens = sensSlider.GetComponent<Slider>().value;
        text.text = "" + sens;

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.GetComponent<FirstPersonController>().m_MouseLook.XSensitivity = sens;
            player.GetComponent<FirstPersonController>().m_MouseLook.YSensitivity = sens;
        }           
        PlayerPrefs.SetFloat("sensetivity", sens);
    }

    public void VolumeChanged()
    {
        float volume = volumeSlider.GetComponent<Slider>().value;
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("volume", volume);
    }
}
