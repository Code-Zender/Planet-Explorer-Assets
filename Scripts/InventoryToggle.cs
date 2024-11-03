using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    public GameObject inventoryPanel; // Referenz auf das Panel-Objekt

    // Diese Methode wird vom Button aufgerufen
    public void ToggleInventory()
    {
        // Sichtbarkeit umschalten
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
}
