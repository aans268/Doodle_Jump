using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlatform : MonoBehaviour
{
    public float jumpForce = 10f;

    // Variables pour le mouvement latéral
    public float moveSpeed = 2f; // Vitesse de déplacement
    public float minX = -2f;     // Limite gauche
    public float maxX = 2f;      // Limite droite
    private bool movingRight = true; // Détermine la direction

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y < 0f)
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 velocity = rb.velocity;
                velocity.y = jumpForce;
                rb.velocity = velocity;
            }
        }
    }

    void Update()
    {
        // Mouvement horizontal de gauche à droite
        if (movingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            if (transform.position.x >= maxX)
            {
                movingRight = false; // Inverse la direction
            }
        }
        else
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            if (transform.position.x <= minX)
            {
                movingRight = true; // Inverse la direction
            }
        }
    }
}
