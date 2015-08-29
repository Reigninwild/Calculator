using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour
{
    public float destroyTime = 5f;
    public int health = 100;
    public GameObject[] dropObjectsAfterDestroy;
    public GameObject dropObjectBeforeDestroy;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void HealthDown()
    {
        health -= 10;
        if (health <= 0)
            StartCoroutine(DestroyTree(destroyTime));
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
        rb.isKinematic = false;
        Cocos();
        //rb.AddForceAtPosition(selector.GetRayDirection() * 10, selector.GetRayHitPoint(), ForceMode.Force);

        yield return new WaitForSeconds(waitTime);

        if (dropObjectsAfterDestroy.Length > 0)
        {
            for (int i = 0; i < dropObjectsAfterDestroy.Length; i++)
            {
                Vector3 position = new Vector3(Random.Range(-1.0F, 1.0F), 0, Random.Range(-1.0F, 1.0F));
                Instantiate(dropObjectsAfterDestroy[i], gameObject.transform.position + position, gameObject.transform.rotation);
            }
        }
        Destroy(gameObject.transform.parent.gameObject);
    }

    public void Cocos()
    {
        Transform parent = gameObject.transform.parent;
        foreach (Transform c in parent.GetComponentsInChildren<Transform>())
        {
            if (c.gameObject.name.Equals("cocos") || c.gameObject.name.Equals("bananas"))
            {
                c.transform.parent = parent.parent;
                c.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
}
