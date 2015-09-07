using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class HitButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static bool isDown = false;
    
    void Awake()
    {
        gameObject.SetActive(false);
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        isDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDown = false;
    }
}
