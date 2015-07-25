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
        Selector.myDelegate = new Selector.MyDelegate(Keep);
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

    public void Keep()
    {
        GameObject keepObject = Selector.selectedObject;
        Item keepObjectItem = keepObject.GetComponent<Item>();
        foreach (Transform slot in contentPanel.transform)
        {
            if (slot.childCount > 0)
            {
                Item slotItem = slot.GetChild(0).GetComponent<Item>();
                if (slotItem.type == Item.TypeOfObject.Item)
                {
                    if (slotItem.oName.Equals(keepObjectItem.oName))
                    {
                        slotItem.count += keepObjectItem.count;
                        slotItem.UpdateCount();
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
                GameObject icon = Instantiate(iconPrefabs[keys.IndexOf(item.oName)]);
                icon.name = icon.name.Replace("(Clone)", "");
                icon.GetComponent<Item>().condition = item.condition;
                icon.GetComponent<Item>().UpdateCondition();
                icon.GetComponent<Item>().count = item.count;
                icon.GetComponent<Item>().UpdateCount();
                icon.transform.SetParent(slot, false);
                return;
            }
        }
    }

    public void Drop(GameObject o)
    {
        Vector3 dropPos = player.transform.position + player.transform.forward *2;
        GameObject dropObject = Instantiate(objectPrefabs[keys.IndexOf(o.name)], dropPos, player.transform.rotation) as GameObject;
        dropObject.GetComponent<Item>().condition = o.GetComponent<Item>().condition;
        dropObject.GetComponent<Item>().count = o.GetComponent<Item>().count;
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
