using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // Dies ermöglicht die Serialisierung in JSON
public class GameData
{
    public int playerLevel;
    public Vector3 playerPosition;
    public List<string> inventoryItems;

    // Konstruktor, falls du Initialwerte brauchst
    public GameData()
    {

        playerLevel = 1;
        playerPosition = Vector3.zero;
        inventoryItems = new List<string>();
    }
}
