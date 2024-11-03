using UnityEngine;

public class CameraFollowWithBounds : MonoBehaviour
{
    public Transform player;         // Referenz auf den Spieler
    public float followSpeed = 2f;   // Geschwindigkeit, mit der die Kamera dem Spieler folgt
    public Vector2 followBounds = new Vector2(2f, 2f); // Bereich, in dem sich der Spieler frei bewegen kann

    private Vector3 initialOffset;   // Anfangsoffset zwischen Kamera und Spieler

    void Start()
    {
        // Anfangsoffset zwischen Kamera und Spieler speichern
        initialOffset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Zielposition der Kamera (mit anfänglichem Offset)
        Vector3 targetPosition = player.position + initialOffset;

        // Berechne die Differenz zwischen der aktuellen Kamera-Position und der Zielposition
        Vector3 offset = targetPosition - transform.position;

        // Check, ob Spieler den Folgebereich überschreitet (in X- und Y-Richtung)
        bool isOutOfBoundsX = Mathf.Abs(offset.x) > followBounds.x;
        bool isOutOfBoundsY = Mathf.Abs(offset.y) > followBounds.y;

        // Nur bewegen, wenn der Spieler außerhalb des Bereichs ist
        if (isOutOfBoundsX || isOutOfBoundsY)
        {
            // Zielposition anpeilen, damit der Spieler in den Folgebereich zurückkehrt
            Vector3 newPosition = transform.position;

            if (isOutOfBoundsX)
            {
                newPosition.x = Mathf.Lerp(transform.position.x, targetPosition.x, followSpeed * Time.deltaTime);
            }
            if (isOutOfBoundsY)
            {
                newPosition.y = Mathf.Lerp(transform.position.y, targetPosition.y, followSpeed * Time.deltaTime);
            }

            transform.position = newPosition;
        }
    }
}
