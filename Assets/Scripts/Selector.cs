using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour {

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
        
        ray = new Ray(transform.position, fwd);

        if (Physics.Raycast(ray, out hit, rayHeight))
        {
            if (hit.collider.tag == "Object")
            {
                actionButton.SetActive(true);
            }
        }
        else
        {
            actionButton.SetActive(false);
        }
	}

    public void isThouchObject()
    {
        GameObject g = hit.collider.gameObject;
        Inventory.AddItem(g.GetComponent<Object>());
        Destroy(g);
    }
}
