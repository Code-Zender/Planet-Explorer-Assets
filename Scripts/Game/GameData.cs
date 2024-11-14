using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.LightTransport;

[System.Serializable]
public class SavegameCollection
{
    public List<Savegame> savegames = new List<Savegame>();
    public GameManager.GameSettings settings; // Einstellungen als Teil der Sammlung speichern
}

[System.Serializable]
public class SaveData
{
    public string saveName;
    public int score;
    public Vector3 playerPosition;
    public string world;
}


[System.Serializable]
public class Savegame
{
    public string gameName;
    public List<SaveData> saves = new List<SaveData>();
    public string lastSaveName; // Referenz zum zuletzt gespeicherten Unter-Spielstand
}

