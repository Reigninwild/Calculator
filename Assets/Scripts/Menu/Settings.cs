using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    public GameObject panel;
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

    public void Save()
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
}
