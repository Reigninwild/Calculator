using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IHasChanged
{
    public delegate void DropDelegate(GameObject o);
    public static DropDelegate dropDelegate;
    public static DropDelegate destroyDelegate;

    public Transform player;
    public GameObject contentPanel;
    public GameObject mainPanel;
    public GameObject gamePanel;

    public GameObject iconPrefab;

    [Header("Список предметов в инвентаре")]
    public List<ItemDetails> itemList;

    [Header("Список всех объектов игры")]
    public List<GameObject> itemObjectList;

    void Start()
    {
        Init();
        Selector.pickUpDelegate = new Selector.PickUpDelegate(PickUp);
        dropDelegate = new DropDelegate(Drop);
        destroyDelegate = new DropDelegate(DestroyItem);
    }

    private void Init()
    {
        itemList = new List<ItemDetails>();
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

        //Has "Item" component only
        if (keepObject == null)
            return;

        Item keepObjectItem = keepObject.GetComponent<Item>();

        if (Add(keepObjectItem))
        {
            Destroy(keepObject);
        }
    }

    public bool Add(Item item)
    {
        if (item.details.type == ItemDetails.TypeOfObject.Weapon)
        {
            if (itemList.Count < 16)
            {
                itemList.Add(item.details);
                CreateIcon(item.details);
                return true;
            }
        }
        else
        {
            foreach (ItemDetails i in itemList)
            {
                if (item.details.name.Equals(i.name))
                {
                    i.count += item.details.count;
                    UpdateIcon(i);
                    return true;
                }
            }

            if (itemList.Count < 16)
            {
                itemList.Add(item.details);
                CreateIcon(item.details);
                return true;
            }
        }

        return false;
    }

    public void CreateIcon(ItemDetails item)
    {
        foreach (Transform slot in contentPanel.transform)
        {
            if (slot.childCount < 1)
            {
                GameObject icon = Instantiate(iconPrefab);
                icon.name = icon.name.Replace("(Clone)", "");
                icon.GetComponent<Icon>().details = item;
                icon.GetComponent<Icon>().UpdateCondition();
                icon.GetComponent<Icon>().UpdateCount();
                icon.transform.SetParent(slot, false);
                return;
            }
        }
    }

    public void UpdateIcon(ItemDetails item)
    {
        foreach (Transform slot in contentPanel.transform)
        {
            if (slot.childCount > 1)
            {
                Icon icon = slot.GetChild(0).GetComponent<Icon>();
                if (ReferenceEquals(icon.details, item))
                {
                    icon.UpdateCondition();
                    icon.UpdateCount();
                }
            }
        }
    }

    public GameObject FindObject(string name)
    {
        foreach (GameObject i in itemObjectList)
        {
            if (i.GetComponent<Item>().details.name.Equals(name))
            {
                return i;
            }
        }

        return null;
    }

    public void Drop(GameObject iconObject)
    {
        Vector3 dropPos = player.transform.position + player.transform.forward * 2;
        Icon icon = iconObject.GetComponent<Icon>();

        GameObject dropObject = Instantiate(
            FindObject(icon.details.name),
            dropPos,
            player.transform.rotation) as GameObject;

        dropObject.GetComponent<Item>().details = icon.details;

        for (int i = 0; i < itemList.Count; i++)
        {
            if (ReferenceEquals(icon.details, itemList[i]))
            {
                itemList.Remove(itemList[i]);
            }
        }

        if (icon.isEquip)
            icon.OnClickItemButton();
    }
    
    public void DestroyItem(GameObject iconObject)
    {
        Icon icon = iconObject.GetComponent<Icon>();
        
        for (int i = 0; i < itemList.Count; i++)
        {
            if (ReferenceEquals(icon.details, itemList[i]))
            {
                itemList.Remove(itemList[i]);
            }
        }

        if (icon.isEquip)
            icon.OnClickItemButton();

        Destroy(iconObject);
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
