using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float horizontalSpeed = 2f;  // Vitesse de déplacement horizontal
    public float verticalOscillationAmplitude = 0.1f; // Amplitude de l'oscillation verticale
    public float verticalOscillationSpeed = 2f;  // Vitesse de l'oscillation verticale

    public float horizontalMovementAmplitude = 2f; // Amplitude du mouvement horizontal
    private Vector3 initialPosition;

    void Start()
    {
        // Ajouter une variation aléatoire pour la position de spawn
        float randomOffsetX = Random.Range(-1f, 1f);  // Décalage aléatoire en X
        float randomOffsetY = Random.Range(-0.5f, 0.5f);  // Décalage aléatoire en Y

        initialPosition = transform.position + new Vector3(randomOffsetX, randomOffsetY, 0);
    }

    void Update()
    {
        // Mouvement horizontal réduit avec une oscillation verticale
        transform.position = new Vector3(
            initialPosition.x + Mathf.PingPong(Time.time * horizontalSpeed, horizontalMovementAmplitude) - (horizontalMovementAmplitude / 2),
            initialPosition.y + Mathf.Sin(Time.time * verticalOscillationSpeed) * verticalOscillationAmplitude,
            initialPosition.z
        );
    }
}
