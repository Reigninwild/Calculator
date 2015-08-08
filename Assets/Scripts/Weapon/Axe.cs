using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Axe : MonoBehaviour
{

    public Animator armsAnimator;
    public Button button;
    public AudioClip emptyHitAudio;
    public AudioClip hitAudio;

    private AudioSource audio;
    private bool isEquip = false;
    private GameObject go;

    private Ray ray;
    private RaycastHit hit;
    private float hitHeight;
    public float timeBetweenShots = 1f;
    private float timestamp;

    void OnEnable()
    {
        button.gameObject.SetActive(true);
        button.onClick.AddListener(() => Hit());
        audio = GetComponent<AudioSource>();
    }

    void OnDisable()
    {
        button.onClick.RemoveListener(() => Hit());
        button.gameObject.SetActive(false);
    }

    IEnumerator WaitAndHit(float waitTime)
    {
        armsAnimator.SetBool("axeHit", true);
        yield return new WaitForSeconds(waitTime);
        armsAnimator.SetBool("axeHit", false);
        GameObject hit = HitObject();
        if (hit != null)
        {
            audio.clip = hitAudio;
            audio.Play();
            ImpactOnObject(hit);
        }
        else
        {
            audio.clip = emptyHitAudio;
            audio.Play();
        }
    }

    public void Hit()
    {
        if (Time.time >= timestamp)
        {
            StartCoroutine(WaitAndHit(0.5F));
            timestamp = Time.time + timeBetweenShots;
        }
    }

    public void Equip()
    {
        isEquip = !isEquip;
        armsAnimator.SetBool("axe", isEquip);
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
        Debug.Log(o.tag);
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
