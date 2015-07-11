using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour {

	public static GameObject selectedObject;

    public GameObject actionButton;
    public float rayHeight;
    
    private RaycastHit hit;
    private Ray ray;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, fwd * rayHeight, Color.green);

        var cam = Camera.main.transform;
        ray = new Ray(cam.position, fwd);

        if (Physics.Raycast(ray, out hit, rayHeight))
        {
            if (hit.collider.tag == "Object")
            {
                actionButton.SetActive(true);
				selectedObject = hit.collider.gameObject;
            }
        }
        else
        {
            actionButton.SetActive(false);
			selectedObject = null;
        }
	}
	/*
    public void isThouchObject()
    {
        GameObject g = hit.collider.gameObject;
        Inventory.AddItem(g.GetComponent<Object>());
        Destroy(g);
    }*/
}
