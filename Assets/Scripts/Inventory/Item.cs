using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Item : MonoBehaviour {
    public enum TypeOfObject
    {
        Item,
        Weapon,
        Note
    }

    public int condition;
    public int count;
    public string oName;
    public TypeOfObject type;
    
    void Start()
    {
        oName = gameObject.name.Replace("(Clone)", "");
    }

    public void UpdateCount()
    {
        gameObject.transform.FindChild("Count Text").GetComponent<Text>().text = "x" + count;
    }

    public void UpdateCondition()
    {
        gameObject.transform.FindChild("Slider").GetComponent<Slider>().value = condition;
    }
}
