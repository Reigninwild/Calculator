﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
    public delegate void PickUpDelegate();
    public static PickUpDelegate pickUpDelegate;

    public static GameObject selectedObject;

    public GameObject actionButton;
    public float rayHeight;

    private RaycastHit hit;
    private Ray ray;

    // Use this for initialization
    void Start()
    {

    }

    public Vector3 GetRayHitPoint()
    {
        return hit.point;
    }

    public Vector3 GetRayDirection()
    {
        return ray.direction;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

#if UNITY_EDITOR
        Debug.DrawRay(transform.position, fwd * rayHeight, Color.green);
#endif
        ray = new Ray(transform.position, fwd);

        if (Physics.Raycast(ray, out hit, rayHeight))
        {
            if (hit.collider.tag.Equals("Object"))
            {
                actionButton.SetActive(true);
                selectedObject = hit.collider.gameObject;
            }
            else
            {
                if (hit.collider.tag.Equals("Tree"))
                {
                    selectedObject = hit.collider.gameObject;
                }
                else
                {
                    actionButton.SetActive(false);
                    selectedObject = null;
                }
            }


#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.E))
            {
                pickUpDelegate();
            }
#endif
        }
        else
        {
            actionButton.SetActive(false);
            selectedObject = null;
        }
    }
}
