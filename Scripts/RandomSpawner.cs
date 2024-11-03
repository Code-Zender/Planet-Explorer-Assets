using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;  // Das Prefab, das gespawnt werden soll (z. B. rohgold)
    public int numberOfObjects = 20;  // Anzahl der Objekte, die sofort gespawnt werden sollen
    public Vector2 spawnAreaMin;      // Minimale Position für zufälliges Spawnen (unten links)
    public Vector2 spawnAreaMax;      // Maximale Position für zufälliges Spawnen (oben rechts)

    private void Start()
    {
        // Spawne sofort eine bestimmte Anzahl von Objekten
        for (int i = 0; i < numberOfObjects; i++)
        {
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        // Generiere eine zufällige Position im definierten Bereich
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 5); // Setze Z auf 1, damit es hinter dem Spieler ist

        // Spawne das Objekt an der zufälligen Position
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }

}
