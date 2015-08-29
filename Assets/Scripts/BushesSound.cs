using UnityEngine;
using System.Collections;

public class BushesSound : MonoBehaviour
{
    public AudioClip sound;
    AudioSource audio;


    public void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = sound;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
            audio.Play();
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
            audio.Play();
    }
}
