using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float horizontalSpeed = 2f;  // Vitesse de déplacement horizontal
    public float verticalOscillationAmplitude = 0.1f; // Amplitude de l'oscillation verticale
    public float verticalOscillationSpeed = 2f;  // Vitesse de l'oscillation verticale

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        // Mouvement de gauche à droite
        transform.position = new Vector3(
            initialPosition.x + Mathf.PingPong(Time.time * horizontalSpeed, 4f) - 2f,  // 2f permet un mouvement entre -2 et 2
            initialPosition.y + Mathf.Sin(Time.time * verticalOscillationSpeed) * verticalOscillationAmplitude,
            initialPosition.z
        );
    }
}
