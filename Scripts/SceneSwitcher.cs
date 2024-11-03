using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectSceneSwitcher : MonoBehaviour
{
    public string sceneName; // Name der Szene, zu der gewechselt werden soll

    private void OnMouseDown()
    {
        // Szene wechseln, wenn das Objekt angeklickt wird
        SceneManager.LoadScene(sceneName);
    }
}
