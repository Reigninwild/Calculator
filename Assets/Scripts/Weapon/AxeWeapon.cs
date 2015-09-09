using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class AxeWeapon : MonoBehaviour, IAttack {

    public const string IDLE = "axe";
    private const string KICK = "axeHit";
    public const string MISS = "miss";
    private const string HIT = "hit";

    [Header("Оружие")]
    public GameObject weaponObject;

    [Header("Звуки оружия")]
    public AudioClip missAudio;
    public AudioClip hitTreeAudio;
    public AudioClip hitStoneAudio;
    public AudioClip hitTerreinAudio;

    private Animator animator;
    private AudioSource audio;

    private Ray ray;
    private RaycastHit hit;

    public GameObject smoke;

    private event Icon.Condition conditionEvent;

    private bool equip = false;

    void Start ()
    {
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();

        weaponObject.SetActive(false);
    }

    public void checkHitAxe()
    {
        GameObject hit = HitObject();
        
        if (hit != null)
        {
            HitAxe();
            animator.SetBool(HIT, true);
        }
        else
        {
            MissAxe();
            animator.SetBool(MISS, true);
        }

        animator.SetBool(KICK, false);
    }

    public void HitAxe()
    {
        string colliderTag = hit.collider.gameObject.tag;
        Instantiate(smoke, hit.point, Quaternion.identity);
        PlayHitSound(colliderTag);
        HealthDown(colliderTag);
        audio.Play();
        ImpactOnObject(hit.collider.gameObject);
        animator.SetBool(HIT, false);
    }

    public void MissAxe()
    {
        audio.clip = missAudio;
        audio.Play();
        animator.SetBool(MISS, false);
    }

    private void ImpactOnObject(GameObject o)
    {
        switch (o.tag)
        {
            case "Tree":
                o.GetComponent<Tree>().HealthDown();
                break;
        }
    }

    private GameObject HitObject()
    {
        Vector3 fwd = Camera.main.transform.TransformDirection(Vector3.forward);

#if UNITY_EDITOR
        Debug.DrawRay(Camera.main.transform.position, fwd * 3, Color.red);
#endif

        ray = new Ray(Camera.main.transform.position, fwd);

        if (Physics.Raycast(ray, out hit, 3))
            return hit.collider.gameObject;
        else
            return null;
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

        return equip;
    }

    public void Attack()
    {
        animator.SetBool(KICK, true);
    }

    private void PlayHitSound(string target)
    {
        switch(target)
        {
            case "Tree":
                audio.clip = hitTreeAudio;
                break;

            case "Stone":
                audio.clip = hitStoneAudio;
                break;

            case "Terrain":
                audio.clip = hitTerreinAudio;
                break;

            default:
                audio.clip = hitTerreinAudio;
                break;
        }
    }

    private void HealthDown(string target)
    {
        switch (target)
        {
            case "Tree":
                conditionEvent(-1);
                break;

            case "Stone":
                conditionEvent(-10);
                break;

            case "Terrain":
                conditionEvent(-1);
                break;

            default:
                conditionEvent(-1);
                break;
        }
    }
}
