using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoneWeapon : MonoBehaviour, IAttack {

    public const string IDLE = "stone";
    private const string KICK = "stoneHit";

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
    
    private event Icon.Condition conditionEvent;

    private bool equip = false;

    void Start ()
    {
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();

        weaponObject.SetActive(false);
    }

    public void checkHitStone()
    {
        GameObject hit = HitObject();
        
        if (hit != null)
            HitStone();
        else
            MissStone();

        animator.SetBool(KICK, false);
    }

    public void HitStone()
    {
        string colliderTag = hit.collider.gameObject.tag;
        PlayHitSound(colliderTag);
        HealthDown(colliderTag);
        audio.Play();
        ImpactOnObject(hit.collider.gameObject);
    }

    public void MissStone()
    {
        audio.clip = missAudio;
        audio.Play();
    }

    private void ImpactOnObject(GameObject o)
    {
        switch (o.tag)
        {
            case "Stone":
                o.GetComponent<Rock>().HealthDown();
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
        switch (target)
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
