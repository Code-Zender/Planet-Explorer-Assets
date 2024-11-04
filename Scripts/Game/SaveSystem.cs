using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private string saveFilePath;

    private void Awake()
    {
        // Dateipfad festlegen (an einem persistenten Ort, der auch in der Build-Version funktioniert)
        saveFilePath = Path.Combine(Application.persistentDataPath, "savegame.json");
    }

    // Speichert die Daten in einer JSON-Datei
    public void SaveGame(GameData data)
    {
        string json = JsonUtility.ToJson(data, true);        // Konvertiert GameData zu JSON
        File.WriteAllText(saveFilePath, json);               // Schreibt JSON in die Datei
        Debug.Log("Spiel gespeichert unter " + saveFilePath);
    }

    // Lädt die Daten aus der JSON-Datei
    public GameData LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);           // Liest JSON aus der Datei
            GameData data = JsonUtility.FromJson<GameData>(json);   // Konvertiert JSON zu GameData
            Debug.Log("Spiel geladen von " + saveFilePath);
            return data;
        }
        else
        {
            Debug.LogWarning("Speicherdatei nicht gefunden!");
            return null;

        }
    }
}
