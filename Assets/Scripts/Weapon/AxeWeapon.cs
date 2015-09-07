using UnityEngine;
using System.Collections;
using System;

public class AxeWeapon : MonoBehaviour, IAttack {

    public const string IDLE = "axe";
    private const string KICK = "axeHit";
    public const string MISS = "miss";
    private const string HIT = "hit";

    [Header("Оружие")]
    public GameObject weaponObject;

    [Header("Звуки оружия")]
    public AudioClip missAudio;
    public AudioClip hitAudio;
    
    private Animator animator;
    private AudioSource audio;

    private Ray ray;
    private RaycastHit hit;

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
            animator.SetBool(HIT, true);
        else
            animator.SetBool(MISS, true);

        animator.SetBool(KICK, false);
    }

    public void HitAxe()
    {
        audio.clip = hitAudio;
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

    public bool Equip()
    {
        equip = !equip;
        weaponObject.SetActive(equip);
        animator.SetBool(IDLE, equip);
        return equip;
    }

    public void Attack()
    {
        animator.SetBool(KICK, true);
    }
}
