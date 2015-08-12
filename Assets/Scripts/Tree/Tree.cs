using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour {
    
    public int health = 100;
    public GameObject[] dropObjectsAfterDestroy;
    public GameObject dropObjectBeforeDestroy;

    public void HealthDown()
    {
        health -= 10;
        if (health <= 0)
            StartCoroutine(DestroyTree(3));
        else
            Drop();
    }

    public void Drop()
    {
        if (dropObjectBeforeDestroy == null)
            return;
        Vector3 position = new Vector3(Random.Range(-1.0F, 1.0F), 2, Random.Range(-1.0F, 1.0F));
        Instantiate(dropObjectBeforeDestroy, gameObject.transform.position + position, Quaternion.identity);
    }

    IEnumerator DestroyTree(float waitTime)
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        
        yield return new WaitForSeconds(waitTime);

        if (dropObjectsAfterDestroy.Length > 0)
        {
            for (int i = 0; i < dropObjectsAfterDestroy.Length; i++)
            {
                Vector3 position = new Vector3(Random.Range(-1.0F, 1.0F), 0, Random.Range(-1.0F, 1.0F));
                Instantiate(dropObjectsAfterDestroy[i], gameObject.transform.position + position, gameObject.transform.rotation);
            }
        }
        Destroy(gameObject);
    }
}
