using UnityEngine;
using System.Collections;

[System.Serializable]
public class ItemDetails {

    public enum TypeOfObject
    {
        Item,
        Weapon,
        Food,
        Note
    }

    public TypeOfObject type;
    public string name;
    public int count = 1;
    public int condition = 100;
}
