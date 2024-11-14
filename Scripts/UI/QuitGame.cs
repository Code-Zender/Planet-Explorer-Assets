using UnityEngine;
#if UNITY_EDITOR
using UnityEditor; // Nur im Editor verfügbar
#endif

public class QuitGameInEditor : MonoBehaviour
{
    public void Quit()
    {
#if UNITY_EDITOR
        // Stoppt den Play Mode im Editor
        EditorApplication.isPlaying = false;
#else
        // Beendet das Spiel im Build
        Application.Quit();
#endif

        Debug.Log("Spiel beendet"); // Meldung zur Überprüfung
    }
}
