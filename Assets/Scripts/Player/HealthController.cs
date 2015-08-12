using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {
    
    public float health = 100;
    public float thirst = 100;
    public float hunger = 100;
    public float fatigue = 100;

    public Slider healthSlider;
    public Slider thirstSlider;
    public Slider hungerSlider;
    public Slider fatigueSlider;

    public float decreaseHealth = 0;
    public float decreaseThirst = 0.1f;
    public float decreaseHunger = 0.1f;
    public float decreaseFatigue = 0.1f;


    public float updateHealthTimeSecond = 1;
    public float nextUpdateHealth = 0;

	void Start () {
	
	}
	
	void Update () {
        if (nextUpdateHealth < Time.time)
        {
            ThirstDown(decreaseThirst);
            HungerDown(decreaseHunger);
            FatigueDown(decreaseFatigue);

            UpdateUI();

            nextUpdateHealth = Time.time + updateHealthTimeSecond;
        }
    }

    void UpdateUI()
    {
        healthSlider.value = health;
        thirstSlider.value = thirst;
        hungerSlider.value = hunger;
        fatigueSlider.value = fatigue;
    }

    void HealthUp(float value)
    {
        health = ((health + value) > 100) ? 100 : (health + value);
    }

    void HealthDown(float value)
    {
        health = ((health - value) <= 0) ? 0 : (health - value);
    }

    void ThirstUp(float value)
    {
        thirst = ((thirst + value) > 100) ? 100 : (thirst + value);
    }

    void ThirstDown(float value)
    {
        thirst = ((thirst - value) <= 0) ? 0 : (thirst - value);
        if (thirst == 0)
            decreaseHealth += 0.2f;
    }

    void HungerUp(float value)
    {
        hunger = ((hunger + value) > 100) ? 100 : (hunger + value);
    }

    void HungerDown(float value)
    {
        hunger = ((hunger - value) <= 0) ? 0 : (hunger - value);
        if (hunger == 0)
            decreaseHealth += 0.3f;
    }

    void FatigueUp(float value)
    {
        fatigue = ((fatigue + value) > 100) ? 100 : (fatigue + value);
    }

    void FatigueDown(float value)
    {
        fatigue = ((fatigue - value) <= 0) ? 0 : (fatigue - value);
        if (fatigue == 0)
            decreaseHealth += 0.5f;
    }


}
