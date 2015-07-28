using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Axe : MonoBehaviour {

    public Button button;

    private bool hit = false;
    private bool isEquip = false;

    GameObject go;
    
    IEnumerator WaitAndHit(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        go = Selector.selectedObject;
        go.GetComponent<Tree>().HealthDown();
    }

    public void Hit()
    {
        StartCoroutine(WaitAndHit(0.5F));
    }

    public void Equip()
    {
        GameObject arms = GameObject.Find("Arms");
        Animator animator = arms.GetComponent<Animator>();

        isEquip = !isEquip;

        if (isEquip)
        {
            animator.SetBool("axe", true);
        }
        else
        {
            animator.SetBool("axe", false);
        }

        button.gameObject.SetActive(true);
        button.onClick.AddListener(delegate() { Hit(); });
    }
}
