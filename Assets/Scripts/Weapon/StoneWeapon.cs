using UnityEngine;
using System.Collections;

public class StoneWeapon : MonoBehaviour, IAttack {

    public const string IDLE = "stone";
    private const string KICK = "stoneHit";

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
        audio.clip = hitAudio;
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
