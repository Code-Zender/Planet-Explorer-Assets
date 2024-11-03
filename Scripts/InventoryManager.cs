using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventorySlotPrefab; // Prefab des Inventory-Slots
    public int slotCount = 20; // Anzahl der Slots

    private void Start()
    {
        // Erstelle die Slots und füge sie dem InventoryPanel hinzu
        for (int i = 0; i < slotCount; i++)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, transform);
            slot.name = "Slot_" + i; // Benenne jeden Slot zur Übersichtlichkeit
        }
    }
}
