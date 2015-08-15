using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessageSystem : MonoBehaviour {

    public Text tooltipText;
    public Text messageBoxText;

    private Animator animator;

	void Start () {
        animator = GetComponent<Animator>();
	}

    public void Tooltip(string text, float time_delay)
    {
        tooltipText.text = text;
        StartCoroutine(TooltipDelay(time_delay));
    }

    IEnumerator TooltipDelay(float second)
    {
        animator.SetBool("tooltipFadeIn", true);
        yield return new WaitForSeconds(second);
        animator.SetBool("tooltipFadeIn", false);
    }

    public void MessageBox(string text)
    {
        messageBoxText.text = text;
        animator.SetBool("messageBoxFadeIn", true);
    }

    public void CloseMessageBox()
    {
        animator.SetBool("messageBoxFadeIn", false);
    }
}
