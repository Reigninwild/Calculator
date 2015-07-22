using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IDropHandler {
	public GameObject item {
		get {
			if(transform.childCount > 0) {
				return transform.GetChild(0).gameObject;
			}
			return null;
		}
	}

	#region IDropHandler implementation

	public void OnDrop (PointerEventData eventData)
    {
        Inventory.dropDelegate(DragHandler.itemBeingDragged);
        Destroy(DragHandler.itemBeingDragged);
        ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged());
	}

	#endregion


}

