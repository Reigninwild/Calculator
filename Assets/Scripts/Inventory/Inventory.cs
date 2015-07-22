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
    }

    public void Close()
    {
        mainPanel.SetActive(false);
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
    /*
    /// <summary>
    /// Главная панель инвентаря. На ней находятся ващи и др.
    /// </summary>
    public GameObject mainPanel;

    /// <summary>
    /// Панель содержимоого инвентаря
    /// </summary>
    public GameObject contantPanel;

    /// <summary>
    /// Панель инвенторя на которой находятся кнопка для открытия главной панели.
    /// </summary>
    public GameObject gamePanel;

    /// <summary>
    /// Панель с ингридиентами для крафта
    /// </summary>
    public GameObject ingredientPanel;
    
    public static List<Object> items;
    
    // Use this for initialization
    void Start()
    {
        items = new List<Object>();
    }
    
    /// <summary>
    /// Открывает инвентарь
    /// </summary>
    public void ShowMainPanel()
    {
        mainPanel.SetActive(true);
        gamePanel.SetActive(false);
    }

    /// <summary>
    /// Закрывает инвентарь
    /// </summary>
    public void CloseMainPanel()
    {
        mainPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    /// <summary>
    /// Обновление содержимого инвентаря
    /// </summary>
    public void OnClick_KeepItem()
    {
        GameObject so = Selector.selectedObject;
        Object o = Selector.selectedObject.GetComponent<Object>();

        if (o.type == Object.TypeOfObject.Item)
        {
            foreach (Object item in items)
            {
                if (item.name == o.name)
                {
                    item.count += o.count;
                    item.itemInInventory.GetComponent<Item>().count.text = "" + item.count;
                    Destroy(so);
                    return;
                }
            }
        }

        if (items.Count < 16)
        {
            items.Add(o);
            AddInEmptySlot(o);
            Destroy(so);
        }
        else
        {
            so.transform.position += new Vector3(0, 0.2f, 0);
        }
    }

    public void AddInEmptySlot(Object o)
    {
        foreach (Transform child in contantPanel.transform)
        {
            if (child.childCount == 0)
            {
                GameObject go = Instantiate(o.itemInInventory);
                go.transform.SetParent(child.transform, false);
                return;
            }
        }
    }


    public void updateCraft(Craft craft) {
        int wood = craft.wood;
        int stone = craft.stone;
        int rope = craft.rope;
        bool hasKnife = craft.hasKnife;

        bool hasWood = false;
        bool hasStone = false;
        bool hasRope = false;

        int woodIndex = 0;
        int stoneIndex = 0;
        int ropeIndex = 0;

        //Если нет места для хранения крафта
        if (items.Count == 16)
            return;

        //Проверка наличия дерева
        if (wood > 0)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].name.Equals("wood"))
                {
                    if (items[i].count >= wood)
                    {
                        woodIndex = i;
                        hasWood = true;
                        break;
                    }
                    else
                    {
                        //Тут будет сообщение "Недостаточно дерева"
                        return;
                    }
                }
            }
        }
        else
        {
            hasWood = true;
        }

        //Проверка наличия камней
        if (stone > 0)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].name.Equals("stone"))
                {
                    if (items[i].count >= stone)
                    {
                        hasStone = true;
                        break;
                    }
                    else
                    {
                        //Тут будет сообщение "Недостаточно дерева"
                        return;
                    }
                }
            }
        }
        else
        {
            hasStone = true;
        }

        //Проверка наличия веревок
        if (rope > 0)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].name.Equals("rope"))
                {
                    if (items[i].count >= rope)
                    {
                        hasRope = true;
                        break;
                    }
                    else
                    {
                        //Тут будет сообщение "Недостаточно дерева"
                        return;
                    }
                }
            }
        }
        else
        {
            hasRope = true;
        }

        if (hasKnife)
        {
            foreach (Object item in items)
            {
                if (item.name.Equals("knife"))
                {
                    if (hasWood && hasStone && hasRope)
                    {
                        items[woodIndex].count -= wood;
                        items[stoneIndex].count -= stone;
                        items[ropeIndex].count -= rope;
                        //тут будет обновление инвентаря
                        //here will be inventory update function
                        
                    }
                }
            }
        }
        else
        {
            if (hasWood && hasStone && hasRope)
            {
                items[woodIndex].count -= wood;
                items[stoneIndex].count -= stone;
                items[ropeIndex].count -= rope;
                //тут будет обновление инвентаря
                //here will be inventory update
            }
        }
    }

	public void equipWeapon() {

	}*/
}

namespace UnityEngine.EventSystems
{
    public interface IHasChanged : IEventSystemHandler
    {
        void HasChanged();
    }
}
