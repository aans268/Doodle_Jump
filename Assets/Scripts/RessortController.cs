using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessortController : MonoBehaviour
{
    public float bounceForce = 20f; // Force du rebond

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Vérifier si l'objet qui entre en collision est le joueur
        if (collision.CompareTag("Player"))
        {
            // Récupérer le point de contact de la collision
            Vector2 contactPoint = collision.ClosestPoint(transform.position);

            // Vérifier si le joueur entre en collision par le dessus du ressort
            if (contactPoint.y > transform.position.y)
            {
                // Appliquer une force verticale au joueur pour le faire sauter
                Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    // Appliquer la force de rebond sur l'axe Y
                    rb.velocity = new Vector2(rb.velocity.x, bounceForce);
                }
            }
        }
        
    }
}
