using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

[XmlType("Object")]
public class Object : MonoBehaviour {

    [XmlElement("Name")]
    public string Name { get; set; }

    [XmlElement("Position")]
    public Vector3 position { get; set; }

    public enum TypeOfObject
    {
        Item,
        Weapon,
        Note
    }

    public string title;
    public string description;
    public string icon;
    public int condition;
    public int count;
    public TypeOfObject type;

    public Object() { }

    public Object(string name, Vector3 position)
    {
        this.Name = name;
        this.position = position;
    }
}
