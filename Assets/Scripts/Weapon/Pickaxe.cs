using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pickaxe : MonoBehaviour {

    public Animator armsAnimator;
    public Button button;

    private bool isEquip = false;
    private GameObject go;

    private Ray ray;
    private RaycastHit hit;
    private float hitHeight;

    void OnEnable()
    {
        button.gameObject.SetActive(true);
        button.onClick.AddListener(() => Hit());
    }

    void OnDisable()
    {
        button.onClick.RemoveListener(() => Hit());
        button.gameObject.SetActive(false);
    }

    IEnumerator WaitAndHit(float waitTime)
    {
        armsAnimator.SetBool("pickaxeHit", true);
        yield return new WaitForSeconds(waitTime);
        armsAnimator.SetBool("pickaxeHit", false);
        //go = Selector.selectedObject;
        //go.GetComponent<Tree>().HealthDown();
    }

    public void Hit()
    {
        StartCoroutine(WaitAndHit(0.5F));
    }

    public void Equip()
    {
        isEquip = !isEquip;
        armsAnimator.SetBool("pickaxe", isEquip);
        //button.SetActive(isEquip);
        //button.GetComponent<Button>().onClick.AddListener(delegate() { Hit(); });
    }

    public GameObject HitObject()
    {
        Vector3 fwd = Camera.main.transform.TransformDirection(Vector3.forward);
        ray = new Ray(transform.position, fwd);

        return hit.collider.gameObject;
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
