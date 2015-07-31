using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Axe : MonoBehaviour {

    public Animator armsAnimator;
    public Button button;

    private bool hit = false;
    private bool isEquip = false;
    GameObject go;

    void OnEnable()
    {

    }

    void OnDisable()
    {

    }

    IEnumerator WaitAndHit(float waitTime)
    {
        armsAnimator.SetBool("axeHit", true);
        yield return new WaitForSeconds(waitTime);
        armsAnimator.SetBool("axeHit", false);
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
        armsAnimator.SetBool("axe", isEquip);
        //button.SetActive(isEquip);
        button.GetComponent<Button>().onClick.AddListener(delegate() { Hit(); });
    }
}
