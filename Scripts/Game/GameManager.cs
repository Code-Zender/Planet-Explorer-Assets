using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private string saveFilePath;
    private SavegameCollection savegameCollection;

    // Zusätzliche Einstellungen, die gespeichert werden sollen
    [System.Serializable]
    public class GameSettings
    {
        public int screenWidth;
        public int screenHeight;
        public bool autoSave;
    }

    private GameSettings gameSettings;

    private void Start()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "savegames.json");
        LoadAllGames(); // Lade vorhandene Spielstände bei Spielstart

        // Wenn keine Einstellungen existieren, setze Standardwerte
        if (gameSettings == null)
        {
            gameSettings = new GameSettings
            {
                screenWidth = Screen.currentResolution.width,
                screenHeight = Screen.currentResolution.height,
                autoSave = false // Standardwert für AutoSave
            };
        }

        // Setze die Auflösung beim Start
        SetResolution(gameSettings.screenWidth, gameSettings.screenHeight);
    }

    public void SaveGameSettings()
    {
        // Speichere die Einstellungen in der JSON-Datei
        SaveAllGames();
    }

    private void SaveAllGames()
    {
        // Speichere die Spielstände und die Einstellungen zusammen
        savegameCollection.settings = gameSettings; // Speichere die Einstellungen in der Sammlung

        string json = JsonUtility.ToJson(savegameCollection, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Alle Spielstände und Einstellungen gespeichert: " + saveFilePath);
    }

    private void LoadAllGames()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            savegameCollection = JsonUtility.FromJson<SavegameCollection>(json);
            gameSettings = savegameCollection.settings; // Lade die gespeicherten Einstellungen
            Debug.Log("Alle Spielstände und Einstellungen geladen.");
        }
        else
        {
            savegameCollection = new SavegameCollection(); // Erstelle neue Sammlung, wenn keine Datei existiert
            gameSettings = new GameSettings(); // Erstelle neue Einstellungen
        }
    }

    public void SetResolution(int width, int height)
    {
        Screen.SetResolution(width, height, Screen.fullScreen);
        gameSettings.screenWidth = width;
        gameSettings.screenHeight = height;
        SaveGameSettings(); // Speichere die geänderte Auflösung
    }

    public void SetAutoSave(bool enableAutoSave)
    {
        gameSettings.autoSave = enableAutoSave;
        SaveGameSettings(); // Speichere die geänderte Auto-Save-Einstellung
    }

    // Beispielmethoden für den Zugriff auf die Einstellungen
    public int GetScreenWidth() => gameSettings.screenWidth;
    public int GetScreenHeight() => gameSettings.screenHeight;
    public bool IsAutoSaveEnabled() => gameSettings.autoSave;




    public void SaveGame(string gameName, SaveData newSaveData)
    {
        Savegame savegame = savegameCollection.savegames.Find(g => g.gameName == gameName);

        if (savegame == null)
        {
            savegame = new Savegame { gameName = gameName };
            savegameCollection.savegames.Add(savegame);
        }

        // Füge den neuen Unterspielstand hinzu und speichere
        savegame.saves.Add(newSaveData);
        savegame.lastSaveName = newSaveData.saveName; // Setze den zuletzt gespeicherten Unter-Spielstand
        SaveAllGames();
    }

    public void DisplayAllSavegames()
    {
        foreach (Savegame game in savegameCollection.savegames)
        {
            Debug.Log("Spielstand: " + game.gameName);
            foreach (SaveData save in game.saves)
            {
                Debug.Log("  Unterspielstand: " + save.saveName + " - Punkte: " + save.score);
            }
        }
    }

    public SaveData LoadSpecificSave(string gameName, string saveName)
    {
        Savegame savegame = savegameCollection.savegames.Find(g => g.gameName == gameName);
        if (savegame != null)
        {
            return savegame.saves.Find(s => s.saveName == saveName);
        }
        Debug.LogWarning("Spielstand oder Unterspielstand nicht gefunden.");
        return null;
    }

    // Neue Methode zur Überprüfung, ob Spielstände existieren
    public bool HasSavegames()
    {
        return savegameCollection != null && savegameCollection.savegames.Count > 0;
    }

    // Neue Methode zum Laden des zuletzt gespeicherten Spielstands
    public SaveData LoadLastSave(string gameName)
    {
        Savegame savegame = savegameCollection.savegames.Find(g => g.gameName == gameName);
        if (savegame != null && !string.IsNullOrEmpty(savegame.lastSaveName))
        {
            return savegame.saves.Find(s => s.saveName == savegame.lastSaveName);
        }
        Debug.LogWarning("Kein letzter Spielstand gefunden.");
        return null;
    }
    public List<Savegame> GetAllSavegames()
    {
        return savegameCollection != null ? savegameCollection.savegames : new List<Savegame>();
    }


}
