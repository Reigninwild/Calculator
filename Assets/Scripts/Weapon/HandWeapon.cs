using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HandWeapon : MonoBehaviour
{
    public enum TypeOfWeapon
    {
        axe,
        pickaxe
    }

    public TypeOfWeapon typeOfWeapon;
    public Animator armsAnimator;
    public Button hitButton;
    public AudioClip missAudio;
    public AudioClip hitAudio;
    
    private AudioSource audio;
    private bool isEquip = false;
    private GameObject go;

    private Ray ray;
    private RaycastHit hit;
    private float hitHeight;
    public float timeBetweenShots = 1f;
    public float hitTime = 1f;
    private float timestamp;

    private string IDLE;
    private string HIT;

    void Start()
    {
        IDLE = typeOfWeapon.ToString();
        HIT = typeOfWeapon.ToString() + "Hit";
    }

    void OnEnable()
    {
        audio = GetComponent<AudioSource>();
        hitButton.gameObject.SetActive(true);
        hitButton.onClick.AddListener(() => Hit());
    }

    void OnDisable()
    {
        hitButton.onClick.RemoveListener(() => Hit());
        hitButton.gameObject.SetActive(false);
    }

    IEnumerator WaitAndHit(float waitTime)
    {
        armsAnimator.SetBool(HIT, true);
        yield return new WaitForSeconds(waitTime);
        armsAnimator.SetBool(HIT, false);
        GameObject hit = HitObject();
        if (hit != null)
        {
            audio.clip = hitAudio;
            audio.Play();
            ImpactOnObject(hit);
        }
        else
        {
            audio.clip = missAudio;
            audio.Play();
        }
    }

    public void Hit()
    {
        if (Time.time >= timestamp)
        {
            StartCoroutine(WaitAndHit(hitTime));
            timestamp = Time.time + timeBetweenShots;
        }
    }

    public void Equip()
    {
        isEquip = !isEquip;
        armsAnimator.SetBool(IDLE, isEquip);
    }

    public GameObject HitObject()
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

    public void ImpactOnObject(GameObject o)
    {
        switch (o.tag)
        {
            case "Tree":
                o.GetComponent<Tree>().HealthDown();
                break;
        }
    }

    void Update()
    {
#if !MOBILE_INPUT
        if (Input.GetKey(KeyCode.Q))
        {
            Hit();
        }
#endif
    }
}
