using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour
{
    public const float RESTORE_TIME = 60;

    [Header("Падающее")]
    public bool breakable = true;

    [Header("Здоровье")]
    public int health = 100;

    [Header("Объекты выпадающие из дерева во время рубки")]
    public GameObject[] dropObjectsAfterDestroy;

    [Header("Объект выпадающий после рубки")]
    public GameObject dropObjectBeforeDestroy;

    [Header("Звук падающего дерева")]
    public AudioClip treeFallDownAudio;

    Animator animator;
    AudioSource audio;
    private int hitCount = 0;
    private bool restore = true;

    void Start()
    {
        if (breakable)
            animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    public void HealthDown()
    {
        health -= 10;
        if (health <= 0)
        {
            if (breakable)
                animator.SetBool("down", true);
            else
                if (restore)
                StartCoroutine(RestoreTreeHealth());
        }
        else
        {
            if (hitCount == 2)
            {
                Drop();
                hitCount = 0;
            }
            else
            {
                hitCount++;
            }
        }
    }

    IEnumerator RestoreTreeHealth()
    {
        restore = false;
        yield return new WaitForSeconds(RESTORE_TIME);
        health = 100;
        restore = true;
    }

    public void Drop()
    {
        if (dropObjectBeforeDestroy == null)
            return;
        Vector3 position = new Vector3(Random.Range(-1.0F, 1.0F), 2, Random.Range(-1.0F, 1.0F));
        Instantiate(dropObjectBeforeDestroy, gameObject.transform.position + position, Quaternion.identity);
    }

    public void DestroyTree()
    {
        if (dropObjectsAfterDestroy.Length > 0)
        {
            float heightObject = transform.position.y + 8;

            for (int i = 0; i < dropObjectsAfterDestroy.Length; i++)
            {
                heightObject += 3.5f;
                Instantiate(dropObjectsAfterDestroy[i], transform.position + transform.up * heightObject, transform.rotation);
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

    public void PlayTreeFallDownAudio()
    {
        audio.clip = treeFallDownAudio;
        audio.Play();
    }
}
