using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {

    [Header("Здоровье")]
    public int health = 100;

    [Header("Объекты выпадающие после разламывания")]
    public GameObject[] dropObjectsAfterDestroy;
        
    AudioSource audio;
    AudioClip crashRockAudio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void HealthDown()
    {
        health -= 10;
        if (health <= 0)
        {
            PlayCrashRockAudio();
            DestroyRock();
        }
    }
    
    public void DestroyRock()
    {
        if (dropObjectsAfterDestroy.Length > 0)
        {
            for (int i = 0; i < dropObjectsAfterDestroy.Length; i++)
            {
                Instantiate(dropObjectsAfterDestroy[i], transform.position + transform.up, transform.rotation);
            }
        }

        Destroy(gameObject.transform.parent.gameObject);
    }
    
    public void PlayCrashRockAudio()
    {
        audio.clip = crashRockAudio;
        audio.Play();
    }
}
