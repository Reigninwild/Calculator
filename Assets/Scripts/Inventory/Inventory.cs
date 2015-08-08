using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IHasChanged
{
    public delegate void DropDelegate(GameObject o);
    public static DropDelegate dropDelegate;

    public Transform player;
    public GameObject contentPanel;
    public GameObject mainPanel;
    public GameObject gamePanel;

    public List<GameObject> objectPrefabs;
    public List<GameObject> iconPrefabs;
    public List<string> keys;

    public int engagedSlots;

    void Start()
    {
        Selector.pickUpDelegate = new Selector.PickUpDelegate(PickUp);
        dropDelegate = new DropDelegate(Drop);
    }

    public void Show()
    {
        mainPanel.SetActive(true);
        gamePanel.SetActive(false);
    }

    public void Close()
    {
        mainPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void PickUp()
    {
        GameObject keepObject = Selector.selectedObject;
        Item keepObjectItem = keepObject.GetComponent<Item>();
        foreach (Transform slot in contentPanel.transform)
        {
            if (slot.childCount > 0)
            {
                Icon slotItem = slot.GetChild(0).GetComponent<Icon>();
                if (slotItem.details.type == ItemDetails.TypeOfObject.Item)
                {
                    if (slotItem.details.name.Equals(keepObjectItem.details.name))
                    {
                        slotItem.details.count += keepObjectItem.details.count;
                        Destroy(keepObject);
                        return;
                    }
                }
            }
        }

        if (engagedSlots < 16)
        {
            Add(keepObjectItem);
            engagedSlots++;
            Destroy(keepObject);
        }
    }

    public void Add(Item item)
    {
        foreach (Transform slot in contentPanel.transform)
        {
            if (slot.childCount < 1)
            {
                GameObject icon = Instantiate(iconPrefabs[keys.IndexOf(item.details.name)]);
                icon.name = icon.name.Replace("(Clone)", "");
                icon.GetComponent<Icon>().details = item.details;
                icon.GetComponent<Icon>().UpdateCondition();
                icon.GetComponent<Icon>().UpdateCount();
                icon.transform.SetParent(slot, false);
                return;
            }
        }
    }

    public void Drop(GameObject o)
    {
        Vector3 dropPos = player.transform.position + player.transform.forward *2;
        GameObject dropObject = Instantiate(objectPrefabs[keys.IndexOf(o.name)], dropPos, player.transform.rotation) as GameObject;
        dropObject.GetComponent<Item>().details = o.GetComponent<Icon>().details;
    }

    #region IHasChanged implementation
    public void HasChanged()
    {
        Debug.Log("Drop");
    }
    #endregion
}

namespace UnityEngine.EventSystems
{
    public interface IHasChanged : IEventSystemHandler
    {
        void HasChanged();
    }
}
