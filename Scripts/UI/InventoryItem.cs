using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public Image itemIcon; // Referenz auf das Image-Objekt im Slot

    public void SetItem(Sprite icon)
    {
        itemIcon.sprite = icon;
        itemIcon.enabled = true;
    }

    public void ClearItem()
    {
        itemIcon.sprite = null;
        itemIcon.enabled = false;
    }
}
