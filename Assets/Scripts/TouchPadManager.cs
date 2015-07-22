using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;

public class TouchPadManager : MonoBehaviour {

    public Text sensInfo;
    public TouchPad touchPad;
    public FirstPersonController fpc;

	// Use this for initialization
	void Start () {
        Sensetivity();
    }

    void Sensetivity()
    {
        sensInfo.text = "X=" + fpc.m_MouseLook.XSensitivity + " Y=" + fpc.m_MouseLook.YSensitivity + " smooth: " + fpc.m_MouseLook.smooth;
    }

    public void UpX()
    {
        fpc.m_MouseLook.XSensitivity += 0.1f;
        Sensetivity();
    }

    public void DownX()
    {
        fpc.m_MouseLook.XSensitivity -= 0.1f;
        Sensetivity();
    }

    public void UpY()
    {
        fpc.m_MouseLook.YSensitivity += 0.1f;
        Sensetivity();
    }

    public void DownY()
    {
        fpc.m_MouseLook.YSensitivity -= 0.1f;
        Sensetivity();
    }

    public void Absolute()
    {
        touchPad.controlStyle = TouchPad.ControlStyle.Absolute;
    }

    public void Relative()
    {
        touchPad.controlStyle = TouchPad.ControlStyle.Relative;
    }

    public void Swipe()
    {
        touchPad.controlStyle = TouchPad.ControlStyle.Swipe;
    }

    public void Smooth()
    {
        fpc.m_MouseLook.smooth = !fpc.m_MouseLook.smooth;
        Sensetivity();
    }
}
