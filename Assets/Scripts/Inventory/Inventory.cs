using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IHasChanged {

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
    /// Элемент инвентаря
    /// </summary>
    public GameObject itemButton;

    private static List<Object> items;
    
    void Awake() {

    }

	// Use this for initialization
	void Start () {
        items = new List<Object>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    /// <summary>
    /// Открывает инвентарь
    /// </summary>
    public void ShowMainPanel() {
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
    /// Добавление в список вещей нового элемента
    /// </summary>
    /// <param name="o">Объект добавляемого элемента</param>
    public static void AddItem(Object o)
    {
        items.Add(o);
    }

    /// <summary>
    /// Обновление содержимого инвентаря
    /// </summary>
    public void UpdateContant()
    {
        foreach (Transform child in contantPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (var item in items)
        {
            GameObject go = Instantiate(itemButton);
            go.transform.SetParent(contantPanel.transform, false);
        }
    }

	#region IHasChanged implementation
	public void HasChanged ()
	{
		Debug.Log ("Drop");
	}
	#endregion
}

namespace UnityEngine.EventSystems {
	public interface IHasChanged : IEventSystemHandler {
		void HasChanged();
	}
}
