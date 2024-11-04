using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SaveSystem saveSystem;
    public GameData gameData;

    private void Start()
    {
        // Daten laden, falls eine Datei existiert
        GameData loadedData = saveSystem.LoadGame();
        if (loadedData != null)
        {
            gameData = loadedData;
            // Geladene Daten im Spiel anwenden (z.B. Position, Inventar)
        }
        else
        {
            gameData = new GameData(); // Erstelle neue Daten, wenn keine Datei vorhanden ist
        }
    }

    // Aufruf, um das Spiel zu speichern
    public void SaveGame()
    {
        saveSystem.SaveGame(gameData);
    }

    // Aufruf, um das Spiel zu laden
    public void LoadGame()
    {
        GameData loadedData = saveSystem.LoadGame();
        if (loadedData != null)
        {
            gameData = loadedData;
            // Geladene Daten auf das Spiel anwenden

        }
    }
}
