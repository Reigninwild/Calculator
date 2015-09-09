using UnityEngine;
using System.Collections;

public class Torch : MonoBehaviour {

    public const string IDLE = "torch";

    [Header("Оружие")]
    public GameObject weaponObject;

    [Header("Звуки оружия")]
    public AudioClip fireAudio;

    private Animator animator;
    private AudioSource audio;

    private event Icon.Condition conditionEvent;

    private bool equip = false;

    void Start () {
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        weaponObject.SetActive(false);

        InvokeRepeating("Fire", 100, 1);
    }

    public bool Equip(Icon.Condition c)
    {
        equip = !equip;
        weaponObject.SetActive(equip);
        animator.SetBool(IDLE, equip);

        if (equip == true)
            conditionEvent = c;
        else
            conditionEvent -= c;

        audio.clip = fireAudio;
        audio.Play();

        return equip;
    }

    private void Fire()
    {
        conditionEvent(-1);
    }
}
