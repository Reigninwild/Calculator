using UnityEngine;
using System.Collections;

public class Craft : MonoBehaviour {

    [Header("Ingedients")]
	public int wood;
	public int stone;
    public int rope;

    [Header("Instrument")]
	public bool hasKnife;

    [Space(10)]
	public string craftName;

    public void OnCraft()
    {
        CheckIngredient();
    }

    void CheckIngredient()
    {
        Inventory inventory = GameObject.Find("InventoryUI").GetComponent<Inventory>();

        if (wood > 0)
            if (!inventory.CheckAvail("wood", wood))
                return;

        if (stone > 0)
            if (!inventory.CheckAvail("stone", stone))
                return;

        if (rope > 0)
            if (!inventory.CheckAvail("rope", rope))
                return;

        if (hasKnife)
            if (!inventory.CheckAvail("knife", 1))
                return;

        inventory.CraftItem(craftName);
    }
}
