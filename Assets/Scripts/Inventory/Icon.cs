using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Icon : MonoBehaviour {

    public ItemDetails details;
    public bool isEquip = false;

    void Start()
    {
        details.name = gameObject.name.Replace("(Clone)", "");
        gameObject.GetComponent<Button>().onClick.AddListener(delegate() { OnClickItemButton(); });
    }

    void OnDestroy()
    {
        if (isEquip)
            OnClickItemButton();
    }

    public void OnClickItemButton()
    {
        switch (details.type)
        {
            case ItemDetails.TypeOfObject.Item:
                break;

            case ItemDetails.TypeOfObject.Weapon:
                isEquip = !isEquip;    
                GameObject.Find("Arms").GetComponent<WeaponManager>().Equip(details.name);
                break;

            case ItemDetails.TypeOfObject.Note:
                break;
        }
    }

    public void UpdateCount()
    {
        gameObject.transform.FindChild("Count Text").GetComponent<Text>().text = "x" + details.count;
    }

    public void UpdateCondition()
    {
        gameObject.transform.FindChild("Slider").GetComponent<Slider>().value = details.condition;
    }
}
