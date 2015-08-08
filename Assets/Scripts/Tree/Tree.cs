using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour {
    
    public int health = 100;
    private float speed = 300;

    public void HealthDown()
    {
        health -= 10;
        if (health <= 0)
            StartCoroutine(DestroyTree(15));
    }


    IEnumerator DestroyTree(float waitTime)
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        //gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        
        yield return new WaitForSeconds(waitTime);

        Destroy(gameObject.transform.parent.gameObject);
    }
}
